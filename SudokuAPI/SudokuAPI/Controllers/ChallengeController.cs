using Microsoft.AspNetCore.Mvc;
using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using SudokuAPI.Services;
using SudokuAPI.UpdateContracts;
using System;

namespace SudokuAPI.Controllers
{
    [Route("api/challenge")]
    public class ChallengeController : Controller
    {
        private ISudokuInfoRepository _sudokuInfoRepository;
        private SudokuGeneratorService _sudokuGeneratorService;
        public ChallengeController(ISudokuInfoRepository sudokuInfoRepository,
            SudokuGeneratorService sudokuGeneratorService)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
            _sudokuGeneratorService = sudokuGeneratorService;
        }

        [HttpGet()]
        public IActionResult GetChallengesList()
        {
            var challengesList = _sudokuInfoRepository.GetDailyChallengesList();

            return Ok(challengesList);
        }

        [HttpGet("{challengeId}", Name = "GetChallenge")]
        public IActionResult GetChallenge(int challengeId)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            return Ok(challenge);
        }

        [HttpPost()]
        public IActionResult CreateChallenge([FromBody]ChallengeCreate challenge)
        {
            Challenge challengeEntity = new Challenge()
            {
                Date = DateTime.Now,
                Difficulty = challenge.Difficulty,
                SudokuGrid = _sudokuGeneratorService.GenerateSudoku(challenge.Difficulty),
                CompletionTime = TimeSpan.Zero
                
            };
            var result = _sudokuInfoRepository.CreateChallenge(challengeEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetChallenge", new { challengeId = challengeEntity.Id }, challengeEntity);
        }

        [HttpPut("{challengeId}")]
        public IActionResult UpdateChallenge(int challengeId, [FromBody]ChallengeUpdate challengeUpdate)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            if (challenge == null)
            {
                return NotFound();
            }

            var result = _sudokuInfoRepository.UpdateChallenge(challenge, challengeUpdate);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{challengeId}")]
        public IActionResult DeleteChallenge(int challengeId)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            if (challenge == null)
            {
                return NotFound();
            }

            var result = _sudokuInfoRepository.DeleteChallenge(challenge);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
