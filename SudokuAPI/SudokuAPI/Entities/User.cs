using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Authorization Authorization { get; set; }

        public ICollection<Challenge> CreatedChallengeList { get; set; } = new List<Challenge>();

        public ICollection<ChallengeUser> AssignedChallengesList { get; } = new List<ChallengeUser>();

        public ICollection<UserUser> RequestedFriendshipsList { get; } = new List<UserUser>();

        public ICollection<UserUser> AcceptedFriendshipsList { get; } = new List<UserUser>();

        public ICollection<DailySudokuUser> DailySudokuScoresList { get; } = new List<DailySudokuUser>();
    }
}
