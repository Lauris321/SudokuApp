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
using Microsoft.AspNetCore.Authorization;
using AutoMapper.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace SudokuAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private ISudokuInfoRepository _sudokuInfoRepository;
        private IHashService _hashService;

        private readonly int _currentUser;
        

        public UsersController(ISudokuInfoRepository sudokuInfoRepository,
            IHttpContextAccessor httpContextAccessor,
            IHashService hashService)
        {
            _sudokuInfoRepository = sudokuInfoRepository;
            _currentUser = httpContextAccessor.CurrentUser();
            _hashService = hashService;
        }

        [HttpGet()]
        [Authorize]
        public IActionResult GetUsersList()
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser))
            {
                ICollection<User> usersList = (ICollection<User>)_sudokuInfoRepository.GetUsersList();
                ICollection<UserListDto> usersListDto = new List<UserListDto>();

                foreach (User userEntity in usersList)
                {
                    usersListDto.Add(new UserListDto()
                    {
                        Id = userEntity.Id,
                        Username = userEntity.Username,
                        Email = userEntity.Email,
                        Authorization = userEntity.Authorization,
                    });
                }

                return Ok(usersListDto);
            }

            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == userId)
            {
                if (!_sudokuInfoRepository.UserExists(userId))
                {
                    return NotFound();
                }

                var user = _sudokuInfoRepository.GetUser(userId);

                ICollection<DailySudokuScoreDto> scoresList = new List<DailySudokuScoreDto>();

                foreach (DailySudokuUser score in user.DailySudokuScoresList)
                {
                    scoresList.Add(new DailySudokuScoreDto
                    {
                        CompletionTime = score.CompletionTime,
                        UserId = score.UserId,
                        DailySudokuId = score.DailySudokuId
                    });
                }

                ICollection<FriendshipDto> requestedFriendsList = new List<FriendshipDto>();

                foreach (UserUser friendship in user.RequestedFriendshipsList)
                {
                    requestedFriendsList.Add(new FriendshipDto
                    {
                        FriendId = friendship.User1Id,
                        Status = friendship.Status
                    });
                }

                ICollection<FriendshipDto> acceptedFriendsList = new List<FriendshipDto>();

                foreach (UserUser friendship in user.AcceptedFriendshipsList)
                {
                    acceptedFriendsList.Add(new FriendshipDto
                    {
                        FriendId = friendship.UserId,
                        Status = friendship.Status
                    });
                }

                ICollection<ChallengeListDto> createdChallengesList = new List<ChallengeListDto>();

                foreach (Challenge challenge in user.CreatedChallengeList)
                {
                    createdChallengesList.Add(new ChallengeListDto
                    {
                        CompletionTime = challenge.CompletionTime,
                        Id = challenge.Id,
                        Date = challenge.Date,
                        Difficulty = challenge.Difficulty,
                        CreatorId = challenge.CreatorId
                    });
                }

                ICollection<ChallengeScoreDto> assignedChallengesList = new List<ChallengeScoreDto>();

                foreach (ChallengeUser challengeScore in user.AssignedChallengesList)
                {
                    assignedChallengesList.Add(new ChallengeScoreDto
                    {
                        UserId = challengeScore.UserId,
                        CompletionTime = challengeScore.CompletionTime,
                        ChallengeId = challengeScore.ChallengeId,
                        Passed = challengeScore.Passed
                    });
                }

                UserDto userDto = new UserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Authorization = user.Authorization,
                    DailySudokuScoresList = scoresList,
                    RequestedFriendshipsList = requestedFriendsList,
                    AcceptedFriendshipsList = acceptedFriendsList,
                    CreatedChallengeList = createdChallengesList,
                    AssignedChallengesList = assignedChallengesList
                };

                return Ok(userDto);
            }

            return StatusCode(403, "Forbidden!");
        }
        
        [HttpPost()]
        public IActionResult CreateUser([FromBody]UserCreate user)
        {
            User userEntity = new User()
            {
                Authorization = Authorization.User,
                Email = user.Email,
                Password = _hashService.CalculateHash(user.Password),
                Username = user.Username
            };
            var result = _sudokuInfoRepository.CreateUser(userEntity);

            if (!result)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetUser", new { userId = userEntity.Id }, userEntity);
        }

        [Authorize]
        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody]UserCreate userUpdate)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == userId)
            {
                var user = _sudokuInfoRepository.GetUser(userId);
                userUpdate.Password = _hashService.CalculateHash(userUpdate.Password);

                if (!_sudokuInfoRepository.UserExists(userId))
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

            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == userId)
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

            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpPost("{userId}/friendship")]
        public IActionResult AddFriendshipRequest(int userId, [FromBody]FriendshipCreate friend)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == userId)
            {
                if (!_sudokuInfoRepository.UserExists(userId) || !_sudokuInfoRepository.UserExists(friend.FriendId))
                {
                    return NotFound();
                }

                if (_sudokuInfoRepository.FriendshipExists(userId, friend.FriendId))
                {
                    return StatusCode(400, "Friendship already exists.");
                }

                UserUser friendshipEntity = new UserUser()
                {
                    Status = FriendshipStatus.Pending,
                    UserId = userId,
                    User1Id = friend.FriendId
                };

                var result = _sudokuInfoRepository.CreateFriendship(friendshipEntity);

                if (!result)
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return CreatedAtRoute("GetUser", new { userId = friendshipEntity.UserId }, friendshipEntity.User);
            }

            return StatusCode(403, "Forbidden!");
        }

        [Authorize]
        [HttpPut("{userId}/friendship")]
        public IActionResult UpdateFriendshipRequest(int userId, [FromBody]FriendshipDto newFriendshipStatus)
        {
            if (_sudokuInfoRepository.IsAdmin(_currentUser) || _currentUser == userId)
            {
                if (!_sudokuInfoRepository.UserExists(userId) ||
                    !_sudokuInfoRepository.UserExists(newFriendshipStatus.FriendId))
                {
                    return NotFound();
                }

                if (_sudokuInfoRepository.FriendshipExists(userId, newFriendshipStatus.FriendId))
                {
                    return StatusCode(400, "Friendship already exists.");
                }

                var result = _sudokuInfoRepository.UpdateFriendship(
                    _currentUser, 
                    newFriendshipStatus.FriendId, 
                    newFriendshipStatus.Status);

                if (!result)
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }

            return StatusCode(403, "Forbidden!");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AuthRequest authUserRequest)
        {
            var user = _sudokuInfoRepository.GetUser(authUserRequest.UserName);

            if (user != null)
            {
                var checkPwd = _hashService.CalculateHash(authUserRequest.Password) == user.Password;
                if (checkPwd)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Rather_very_long_key"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken("http://localhost:51240/",
                    "http://localhost:51240/",
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                    user.Password = "";
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), user });
                }
            }

            return BadRequest("Could not create token");
        }
    }
}
