﻿using AutoMapper;
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
    [Route("api/challenge")]
    public class CommentsController : Controller
    {
        private ISudokuInfoRepository _sudokuInfoRepository;

        private readonly int _currentUser;
        public CommentsController(ISudokuInfoRepository sudokuInfoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
            _currentUser = httpContextAccessor.CurrentUser();
        }

        [HttpGet("{challengeId}/comments")]
        public IActionResult GetCommentslist(int challengeId)
        {
            if(!_sudokuInfoRepository.ChallengeExists(challengeId))
            {
                return NotFound();
            }

            var commentsList = _sudokuInfoRepository.GetCommentsList(challengeId);
            var commentsListResult = Mapper.Map<IEnumerable<CommentDto>>(commentsList);

            return Ok(commentsListResult);
        }

        [HttpGet("{challengeId}/comments/{commentId}", Name = "GetComment")]
        public IActionResult GetComment(int challengeId, int commentId)
        {
            if (!_sudokuInfoRepository.ChallengeExists(challengeId))
            {
                return NotFound();
            }

            var comment = _sudokuInfoRepository.GetComment(challengeId, commentId);
            var commentResult = Mapper.Map<CommentDto>(comment);

            return Ok(commentResult);
        }

        [Authorize]
        [HttpPost("{challengeId}/comments")]
        public IActionResult CreateComment(int challengeId, [FromBody]CommentCreate comment)
        {
            if (!_sudokuInfoRepository.ChallengeExists(challengeId))
            {
                return NotFound();
            }

            var challenge = _sudokuInfoRepository.GetChallenge(challengeId);

            Comment commentEntity = new Comment()
            {
                Challenge = challenge,
                ChallengeId = challengeId,
                Date = DateTime.Now,
                Message = comment.Message
            };

            var result = _sudokuInfoRepository.CreateComment(commentEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var commentResult = Mapper.Map<CommentDto>(commentEntity);

            return CreatedAtRoute("GetComment", new { challengeId = challengeId, commentId = commentResult.Id }, commentResult);
        }

        [Authorize]
        [HttpPut("{challengeId}/comments/{commentId}")]
        public IActionResult UpdateComment(int challengeId, int commentId, [FromBody]CommentCreate comment)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser))
            {
                if (!_sudokuInfoRepository.ChallengeExists(challengeId))
                {
                    return NotFound();
                }

                var result = _sudokuInfoRepository.UpdateComment(challengeId, commentId, comment);

                if (!result)
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }

            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpDelete("{challengeId}/comments/{commentId}")]
        public IActionResult DeleteComment(int challengeId, int commentId)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser))
            {
                if (!_sudokuInfoRepository.ChallengeExists(challengeId))
                {
                    return NotFound();
                }

                var comment = _sudokuInfoRepository.GetComment(challengeId, commentId);

                if (comment == null)
                {
                    return NotFound();
                }

                var result = _sudokuInfoRepository.DeleteComment(comment);

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
