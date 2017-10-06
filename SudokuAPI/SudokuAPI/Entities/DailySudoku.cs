using SudokuAPI.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SudokuAPI.Entities
{
    public class DailySudoku
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(81)]
        public string SudokuGrid { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }
    }
}
