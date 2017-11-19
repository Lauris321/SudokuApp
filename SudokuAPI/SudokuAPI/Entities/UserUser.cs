using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Entities
{
    public class UserUser
    {
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey("User1Id")]
        public User User1 { get; set; }
        public int User1Id { get; set; }

        public FriendshipStatus Status { get; set; }
    }
}
