using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class ChallengeDto
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string SudokuGrid { get; set; }
        
        public Difficulty Difficulty { get; set; }

        public TimeSpan CompletionTime { get; set; }

        public ICollection<CommentDto> CommentsList { get; set; } = new List<CommentDto>();

        public int CreatorId { get; set; }

        public ICollection<ChallengeScoreDto> AssigneesList { get; set; } = new List<ChallengeScoreDto>();
    }
}
