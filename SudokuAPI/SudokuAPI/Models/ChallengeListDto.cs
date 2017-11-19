using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class ChallengeListDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
        public TimeSpan CompletionTime { get; set; }
        public int CreatorId { get; set; }
    }
}
