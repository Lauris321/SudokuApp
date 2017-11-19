using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Authorization Authorization { get; set; }

        public ICollection<ChallengeListDto> CreatedChallengeList { get; set; } = new List<ChallengeListDto>();

        public ICollection<ChallengeScoreDto> AssignedChallengesList { get; set; } = new List<ChallengeScoreDto>();

        public ICollection<DailySudokuScoreDto> DailySudokuScoresList { get; set; } = new List<DailySudokuScoreDto>();

        public ICollection<FriendshipDto> RequestedFriendshipsList { get; set; } = new List<FriendshipDto>();
        public ICollection<FriendshipDto> AcceptedFriendshipsList { get; set; } = new List<FriendshipDto>();
    }
}
