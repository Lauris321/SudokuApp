using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using SudokuAPI.Models;
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
        private readonly int _currentUser;
        public SudokuController(ISudokuInfoRepository sudokuInfoRepository, 
            SudokuGeneratorService sudokuGeneratorService,
            IHttpContextAccessor httpContextAccessor)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
            _sudokuGeneratorService = sudokuGeneratorService;
            _currentUser = httpContextAccessor.CurrentUser();
        }

        [HttpGet()]
        public IActionResult GetDailySudokuList()
        {
            var sudokuList = _sudokuInfoRepository.GetDailySudokuList();
            ICollection<DailySudokuListDto> sudokuListDto = new List<DailySudokuListDto>();
            
            Mapper.Map(sudokuList, sudokuListDto);

            return Ok(sudokuListDto);
        }
        
        [HttpGet("{dailySudokuId}", Name = "GetDailySudoku")]
        public IActionResult GetDailySudoku(int dailySudokuId)
        {
            if (!_sudokuInfoRepository.SudokuExists(dailySudokuId))
            {
                return NotFound();
            }

            var sudokuEntity = _sudokuInfoRepository.GetDailySudoku(dailySudokuId);

            ICollection<DailySudokuScoreDto> scoresList = new List<DailySudokuScoreDto>();

            foreach(DailySudokuUser score in sudokuEntity.ScoresList)
            {
                scoresList.Add(new DailySudokuScoreDto
                {
                    CompletionTime = score.CompletionTime,
                    UserId = score.UserId,
                    Username = _sudokuInfoRepository.GetUser(score.UserId).Username,
                    DailySudokuId = score.DailySudokuId
                });
            }

            DailySudokuDto dailySudoku = new DailySudokuDto()
            {
                Date = sudokuEntity.Date,
                Difficulty = sudokuEntity.Difficulty,
                Id = sudokuEntity.Id,
                ScoresList = scoresList,
                SudokuGrid = sudokuEntity.SudokuGrid
            };

            return Ok(dailySudoku);
        }

        [Authorize]
        [HttpPost()]
        public IActionResult CreateDailySudoku([FromBody]DailySudokuCreate sudoku)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser))
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

            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpPut("{dailySudokuId}")]
        public IActionResult UpdateDailySudoku(int dailySudokuId, [FromBody]DailySudokuCreate dailySudokuUpdate)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser))
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

            return StatusCode(403, "Forbidden!");
        }

        [HttpDelete("{dailySudokuId}")]
        public IActionResult DeleteDailySudoku(int dailySudokuId)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser))
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
            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpPost("{dailySudokuId}/addScore")]
        public IActionResult AddSudokuScore(int dailySudokuId, [FromBody]ScoreCreate score)
        {
            if (!_sudokuInfoRepository.SudokuExists(dailySudokuId))
            {
                return NotFound();
            }
            if (_sudokuInfoRepository.SudokuScoreExists(dailySudokuId, _currentUser))
            {
                return StatusCode(400, "This users score already exists.");
            }

            DailySudokuUser dailySudokuUserEntity = new DailySudokuUser()
            {
                CompletionTime = score.CompletionTime,
                DailySudokuId = dailySudokuId,
                UserId = _currentUser
            };
            var result = _sudokuInfoRepository.createSudokuScore(dailySudokuUserEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetDailySudoku", new { dailySudokuId = dailySudokuUserEntity.DailySudokuId }, dailySudokuUserEntity.DailySudoku);
        }
    }
}
