using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class FriendshipDto
    {
        public int FriendId { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
