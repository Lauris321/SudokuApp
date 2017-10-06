using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SudokuAPI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using SudokuAPI.Entities;
using SudokuAPI.Models;
using AutoMapper;
using SudokuAPI.CreateContracts;
using SudokuAPI.Enumerations;

namespace SudokuAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private ISudokuInfoRepository _sudokuInfoRepository;
        public UsersController(ISudokuInfoRepository sudokuInfoRepository)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetUsersList()
        {
            var usersList = _sudokuInfoRepository.GetUsersList();

            return Ok(usersList);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            var user = _sudokuInfoRepository.GetUser(userId);

            return Ok(user);
        }

        [HttpPost()]
        public IActionResult CreateUser([FromBody]UserCreate user)
        {
            User userEntity = new User()
            {
                Authorization = Authorization.User,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username
            };
            var result = _sudokuInfoRepository.CreateUser(userEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetUser", new { userId = userEntity.Id }, userEntity);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody]UserCreate userUpdate)
        {
            var user = _sudokuInfoRepository.GetUser(userId);

            if(!_sudokuInfoRepository.UserExists(userId))
            {
                return NotFound();
            }

            var result = _sudokuInfoRepository.UpdateUser(user, userUpdate);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _sudokuInfoRepository.GetUser(userId);

            if (!_sudokuInfoRepository.UserExists(userId))
            {
                return NotFound();
            }

            var result = _sudokuInfoRepository.DeleteUser(user);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
