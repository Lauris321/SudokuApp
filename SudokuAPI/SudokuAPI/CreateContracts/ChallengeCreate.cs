using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.CreateContracts
{
    public class ChallengeCreate
    {
        public Difficulty Difficulty { get; set; }
    }
}
