using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class NoteDTO
    {
        public string Author { get; set; }
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Public { get; set; }
        public DateTime Created { get; set; }
    }

    public class NoteResumeDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(400)]
        public string Content { get; set; }
        [Required]
        public bool Public { get; set; }
    }

    public class NewNoteDTO
    {
        [Required(ErrorMessage = "Title é obrigatório")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content é obrigatório")]
        [MaxLength(400)]
        public string Content { get; set; }
        [Required(ErrorMessage = "Public é obrigatório")]
        public bool Public { get; set; }

    }
}
