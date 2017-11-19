using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using SudokuAPI.Models;
using SudokuAPI.Services;
using SudokuAPI.UpdateContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SudokuAPI.Controllers
{
    [Route("api/challenge")]
    public class ChallengeController : Controller
    {
        private ISudokuInfoRepository _sudokuInfoRepository;
        private SudokuGeneratorService _sudokuGeneratorService;
        private readonly int _currentUser;
        public ChallengeController(ISudokuInfoRepository sudokuInfoRepository,
            SudokuGeneratorService sudokuGeneratorService,
            IHttpContextAccessor httpContextAccessor)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
            _sudokuGeneratorService = sudokuGeneratorService;
            _currentUser = httpContextAccessor.CurrentUser();
        }
        
        [HttpGet()]
        public IActionResult GetChallengesList()
        {
            var challengesList = _sudokuInfoRepository.GetChallengesList();

            ICollection<ChallengeListDto> challengesDtoList = new List<ChallengeListDto>();

            foreach (Challenge challenge in challengesList)
            {
                challengesDtoList.Add(new ChallengeListDto()
                {
                    Id = challenge.Id,
                    CreatorId = challenge.CreatorId,
                    CompletionTime = challenge.CompletionTime,
                    Date = challenge.Date,
                    Difficulty = challenge.Difficulty
                });
            }
            
            return Ok(challengesDtoList);
        }

        [Authorize]
        [HttpGet("{challengeId}", Name = "GetChallenge")]
        public IActionResult GetChallenge(int challengeId)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            ICollection<CommentDto> commentsList = new List<CommentDto>();

            foreach (Comment comment in challenge.CommentsList)
            {
                commentsList.Add(new CommentDto()
                {
                    Id = comment.Id,
                    Date = comment.Date,
                    Message = comment.Message
                });
            }

            ICollection<ChallengeScoreDto> scoresList = new List<ChallengeScoreDto>();

            foreach (ChallengeUser score in challenge.AssigneesList)
            {
                scoresList.Add(new ChallengeScoreDto()
                {
                    ChallengeId = score.ChallengeId,
                    UserId = score.UserId,
                    CompletionTime = score.CompletionTime,
                    Passed = score.Passed
                });
            }

            ChallengeDto challengeDto = new ChallengeDto()
            {
                Id = challenge.Id,
                CompletionTime = challenge.CompletionTime,
                CreatorId = challenge.CreatorId,
                Date = challenge.Date,
                Difficulty = challenge.Difficulty,
                SudokuGrid = challenge.SudokuGrid,
                CommentsList = commentsList,
                AssigneesList = scoresList
            };

            return Ok(challengeDto);
        }

        [Authorize]
        [HttpPost()]
        public IActionResult CreateChallenge([FromBody]ChallengeCreate challenge)
        {
            Challenge challengeEntity = new Challenge()
            {
                Date = DateTime.Now,
                Difficulty = challenge.Difficulty,
                SudokuGrid = _sudokuGeneratorService.GenerateSudoku(challenge.Difficulty),
                CompletionTime = TimeSpan.Zero,
                CreatorId = _currentUser
            };
            var result = _sudokuInfoRepository.CreateChallenge(challengeEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetChallenge", new { challengeId = challengeEntity.Id }, challengeEntity);
        }

        [Authorize]
        [HttpPut("{challengeId}")]
        public IActionResult UpdateChallenge(int challengeId, [FromBody]ChallengeUpdate challengeUpdate)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == challenge.CreatorId)
            {
                if (challenge == null)
                {
                    return NotFound();
                }

                if (challenge.CompletionTime != null)
                {
                    return StatusCode(403, "The completion time is already entered!");
                }

                challenge.CompletionTime = challengeUpdate.CompletionTime;

                var result = _sudokuInfoRepository.UpdateChallenge(challenge);

                if (!result)
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }
            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpDelete("{challengeId}")]
        public IActionResult DeleteChallenge(int challengeId)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            if (challenge == null)
            {
                return NotFound();
            }

            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == challenge.CreatorId)
            {
                var result = _sudokuInfoRepository.DeleteChallenge(challenge);

                if (!result)
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }
            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpPost("{challengeId}/assignees")]
        public IActionResult CreateChallengeAssigneeScores(int challengeId, [FromBody]ChallengeScoreCreate challengeScoreCreate)
        {
            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == challenge.CreatorId)
            {
                if (challenge == null)
                {
                    return NotFound();
                }

                ICollection<ChallengeUser> assignedChallengesList = new List<ChallengeUser>();

                foreach (UserGameCreate challengeScore in challengeScoreCreate.AssigneesList)
                {
                    challenge.AssigneesList.Add(new ChallengeUser
                    {
                        UserId = challengeScore.UserId,
                        CompletionTime = null,
                        ChallengeId = challengeId,
                        Passed = false
                    });
                }

                var result = _sudokuInfoRepository.UpdateChallenge(challenge);

                if (!result)
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }

            return StatusCode(403, "Forbidden!");
        }
    }
}
