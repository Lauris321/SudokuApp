using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Services
{
    public interface IHashService
    {
        string CalculateHash(string input);
    }
}
