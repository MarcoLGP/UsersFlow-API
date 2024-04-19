﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersFlow_API.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Content { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public bool Locked { get; set; }
        [Required]
        public bool Public { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}