using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Entities
{
    public class ChallengeUser
    {
        public TimeSpan? CompletionTime { get; set; }
        public bool? Passed { get; set; }

        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }
        public int ChallengeId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
