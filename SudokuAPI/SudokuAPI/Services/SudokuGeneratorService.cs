using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Services
{
    public class SudokuGeneratorService
    {
        //public static List<String> ROWS = new List<String>(new String[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
        //public static List<String> COLS = new List<String>(new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" });
        //public static List<String> SQUARES = cross(COLS, ROWS);
        //public static String DIGITS = "123456789";

        //public static List<List<String>> UNITLIST = new List<List<String>>();
        //public static Dictionary<string, List<List<string>>> UNITS = new Dictionary<string, List<List<string>>>();
        //public static Dictionary<string, List<string>> PEERS = new Dictionary<string, List<string>>();

        //public static int squaresFound = 0;
        //public static int cycles = 0;

        //public static List<String> cross(List<String> a, List<String> b)
        //{
        //    var result = new List<String>();
        //    for (var i = 0; i < a.Count; i++)
        //    {
        //        for (var j = 0; j < b.Count; j++)
        //        {
        //            result.Add(a[i] + b[j]);
        //        }
        //    }
        //    return result;
        //}

        //public static Char[] shuffleChars(this Random rng, Char[] array)
        //{
        //    int n = array.Length;
        //    while (n > 1)
        //    {
        //        int k = rng.Next(n--);
        //        Char temp = array[n];
        //        array[n] = array[k];
        //        array[k] = temp;
        //    }
        //    return array;
        //}

        //public static Dictionary<string, string> assign(Dictionary<string, string> grid, String s, String d)
        //{
        //    String otherValuesString = grid[s].Replace(d, string.Empty);
        //    Char[] otherValues = otherValuesString.ToCharArray();

        //    Dictionary<string, string> elGrid = new Dictionary<string, string>(grid);

        //    foreach (Char value in otherValues)
        //    {
        //        elGrid = eliminate(elGrid, s, Char.ToString(value));
        //        if (elGrid == null)
        //        {
        //            return null;
        //        }
        //    }

        //    return elGrid;
        //}

        //public static Dictionary<string, string> eliminate(Dictionary<string, string> grid, String s, String d)
        //{
        //    cycles++;
        //    if (grid == null)
        //    {
        //        return null;
        //    }

        //    Dictionary<string, string> tempGrid = new Dictionary<string, string>(grid);

        //    if (!tempGrid[s].Contains(d))
        //    {
        //        return tempGrid;
        //    }

        //    String tempString = tempGrid[s].Replace(d, string.Empty);
        //    tempGrid.Remove(s);
        //    tempGrid.Add(s, tempString);
        //    if (tempGrid[s].Length < 1)
        //    {
        //        return null;
        //    }
        //    else if (tempGrid[s].Length == 1)
        //    {
        //        String d2 = tempGrid[s];
        //        bool all = true;
        //        foreach (String peer in PEERS[s])
        //        {
        //            tempGrid = eliminate(tempGrid, peer, d2);
        //            if (tempGrid == null)
        //            {
        //                all = false;
        //            }
        //        }
        //        if (!all)
        //        {
        //            return null;
        //        }
        //    }

        //    foreach (List<String> unit in UNITS[s])
        //    {
        //        List<String> dPlaces = unit.FindAll(x => tempGrid[x].Contains(d));

        //        if (dPlaces.Count == 0)
        //        {
        //            return null;
        //        }
        //        else if (dPlaces.Count == 1)
        //        {
        //            tempGrid = assign(tempGrid, dPlaces[0], d);
        //            if (tempGrid == null)
        //            {
        //                return null;
        //            }
        //        }
        //    }

        //    return tempGrid;
        //}

        //public static int squareCount(String difficulty)
        //{
        //    if (difficulty == "easy")
        //    {
        //        return 35;
        //    }
        //    else if (difficulty == "medium")
        //    {
        //        return 28;
        //    }
        //    return 20;
        //}

        //public static Dictionary<string, string> solve(Dictionary<string, string> grid)
        //{
        //    if (grid == null)
        //    {
        //        return null;
        //    }

        //    Dictionary<string, string> tempGrid = new Dictionary<string, string>(grid);
        //    int tempSquaresFound = SQUARES.FindAll(x => tempGrid[x].Length == 1).Count;
        //    if (tempSquaresFound > squaresFound)
        //    {
        //        squaresFound = tempSquaresFound;
        //        Console.WriteLine("Found {0} squares in {1} eliminations.", squaresFound, cycles);
        //        cycles = 0;
        //    }


        //    if (squaresFound == SQUARES.Count)
        //    {
        //        return tempGrid;
        //    }

        //    List<String> candidates = SQUARES.FindAll(x => tempGrid[x].Length > 1);

        //    candidates.Sort(delegate (String x, String y)
        //    {
        //        if (tempGrid[x].Length != tempGrid[y].Length)
        //            return tempGrid[y].Length - tempGrid[x].Length;
        //        else
        //            return String.Compare(x, y, false);
        //    });

        //    String s = candidates[0];

        //    Char[] digitsLeft = tempGrid[s].ToCharArray();
        //    Random rng = new Random();
        //    shuffleChars(rng, digitsLeft);
        //    String d = Char.ToString(digitsLeft[0]);
        //    Dictionary<string, string> returnValue = new Dictionary<string, string>();
        //    foreach (Char digit in digitsLeft)
        //    {
        //        returnValue = solve(assign(tempGrid, s, Char.ToString(digit)));
        //        if (returnValue != null)
        //        {
        //            return returnValue;
        //        }
        //    }

        //    return null;
        //}

        public string GenerateSudoku(Difficulty difficulty)
        {
            //foreach (String row in ROWS)
            //{
            //    UNITLIST.Add(cross(COLS, new List<String>(new String[] { row })));
            //}

            //foreach (String col in COLS)
            //{
            //    UNITLIST.Add(cross(new List<String>(new String[] { col }), ROWS));
            //}

            //List<List<String>> groupCols = new List<List<String>>();
            //groupCols.Add(new List<string>(new String[] { "A", "B", "C" }));
            //groupCols.Add(new List<string>(new String[] { "D", "E", "F" }));
            //groupCols.Add(new List<string>(new String[] { "G", "H", "I" }));

            //List<List<String>> groupRows = new List<List<String>>();
            //groupRows.Add(new List<string>(new String[] { "1", "2", "3" }));
            //groupRows.Add(new List<string>(new String[] { "4", "5", "6" }));
            //groupRows.Add(new List<string>(new String[] { "7", "8", "9" }));

            //foreach (List<String> col in groupCols)
            //{
            //    foreach (List<String> row in groupRows)
            //    {
            //        UNITLIST.Add(cross(col, row));
            //    }
            //}

            //foreach (String square in SQUARES)
            //{
            //    List<String> peers = new List<String>();
            //    List<List<String>> units = new List<List<String>>();

            //    foreach (List<String> unit in UNITLIST)
            //    {
            //        if (unit.Contains(square))
            //        {
            //            units.Add(unit);

            //            foreach (String peer in unit)
            //            {
            //                if (String.Compare(peer, square) != 0 && !peers.Contains(peer))
            //                {
            //                    peers.Add(peer);
            //                }
            //            }
            //        }
            //    }

            //    UNITS.Add(square, units);
            //    PEERS.Add(square, peers);
            //}

            //var start = DateTime.UtcNow.Millisecond;

            //var minSquares = squareCount("hard");

            //Dictionary<string, string> grid = new Dictionary<string, string>();

            //foreach (String key in SQUARES)
            //{
            //    grid.Add(key, DIGITS);
            //}

            //var fullGrid = solve(grid);

            //var end = DateTime.UtcNow.Millisecond;
            return "016002400320009000040103000005000069009050300630000800000306010000400072004900680";
        }
    }
}
