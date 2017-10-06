using SudokuAPI.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuAPI.Entities
{
    public class Challenge
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
        
        public TimeSpan CompletionTime { get; set; }

        public ICollection<Comment> CommentsList { get; set; } = new List<Comment>();

        //[ForeignKey("CreatorId")]
        //public User Creator { get; set; }

        //public int CreatorId { get; set; }

        //public ICollection<User> AssigneesList { get; set } = new List<User>();
    }
}
