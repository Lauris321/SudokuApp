using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.CreateContracts
{
    public class DailySudokuCreate
    {
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
