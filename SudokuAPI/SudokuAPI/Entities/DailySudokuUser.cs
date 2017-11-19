using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Entities
{
    public class DailySudokuUser
    {
        public TimeSpan CompletionTime { get; set; }

        [ForeignKey("DailySudokuId")]
        public DailySudoku DailySudoku { get; set; }
        public int DailySudokuId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
