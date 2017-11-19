using SudokuAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.CreateContracts
{
    public class ChallengeScoreCreate
    {
        public ICollection<UserGameCreate> AssigneesList { get; set; } = new List<UserGameCreate>();
    }
}
