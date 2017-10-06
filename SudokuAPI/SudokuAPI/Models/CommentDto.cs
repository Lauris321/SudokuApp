using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        
        public string Message { get; set; }
        
        public DateTime Date { get; set; }
    }
}
