using Microsoft.AspNetCore.Mvc;
using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using SudokuAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Controllers
{
    [Route("api/sudoku")]
    public class SudokuController : Controller
    {
        private ISudokuInfoRepository _sudokuInfoRepository;
        private SudokuGeneratorService _sudokuGeneratorService;
        public SudokuController(ISudokuInfoRepository sudokuInfoRepository, 
            SudokuGeneratorService sudokuGeneratorService)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
            _sudokuGeneratorService = sudokuGeneratorService;
        }

        [HttpGet()]
        public IActionResult GetDailySudokuList()
        {
            var sudokuList = _sudokuInfoRepository.GetDailySudokuList();

            return Ok(sudokuList);
        }

        [HttpGet("{dailySudokuId}", Name = "GetDailySudoku")]
        public IActionResult GetDailySudoku(int dailySudokuId)
        {
            var sudoku = _sudokuInfoRepository.GetDailySudoku(dailySudokuId);

            return Ok(sudoku);
        }

        [HttpPost()]
        public IActionResult CreateDailySudoku([FromBody]DailySudokuCreate sudoku)
        {
            DailySudoku dailySudokuEntity = new DailySudoku()
            {
                Date = sudoku.Date,
                Difficulty = sudoku.Difficulty,
                SudokuGrid = _sudokuGeneratorService.GenerateSudoku(sudoku.Difficulty)
            };
            var result = _sudokuInfoRepository.CreateDailySudoku(dailySudokuEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetDailySudoku", new { dailySudokuId = dailySudokuEntity.Id }, dailySudokuEntity);
        }

        [HttpPut("{dailySudokuId}")]
        public IActionResult UpdateDailySudoku(int dailySudokuId, [FromBody]DailySudokuCreate dailySudokuUpdate)
        {
            var dailySudoku = _sudokuInfoRepository.GetDailySudoku(dailySudokuId);

            if (dailySudoku == null)
            {
                return NotFound();
            }

            var result = _sudokuInfoRepository.UpdateDailySudoku(dailySudoku, dailySudokuUpdate);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{dailySudokuId}")]
        public IActionResult DeleteDailySudoku(int dailySudokuId)
        {
            var dailySudoku = _sudokuInfoRepository.GetDailySudoku(dailySudokuId);

            if (dailySudoku == null)
            {
                return NotFound();
            }

            var result = _sudokuInfoRepository.DeleteDailySudoku(dailySudoku);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
