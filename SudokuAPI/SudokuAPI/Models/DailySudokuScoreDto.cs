using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class DailySudokuScoreDto
    {
        public TimeSpan CompletionTime { get; set; }
        
        public int UserId { get; set; }
        public String Username { get; set; }
        public int DailySudokuId { get; set; }
    }
}
