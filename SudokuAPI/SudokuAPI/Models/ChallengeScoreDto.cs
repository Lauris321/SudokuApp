using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class ChallengeScoreDto
    {
        public TimeSpan? CompletionTime { get; set; }
        public bool? Passed { get; set; }

        public int ChallengeId { get; set; }
        public int UserId { get; set; }
    }
}
