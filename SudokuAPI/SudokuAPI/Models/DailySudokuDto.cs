using SudokuAPI.Entities;
using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class DailySudokuDto
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string SudokuGrid { get; set; }
        
        public Difficulty Difficulty { get; set; }

        public ICollection<DailySudokuScoreDto> ScoresList { get; set; } = new List<DailySudokuScoreDto>();
    }
}
