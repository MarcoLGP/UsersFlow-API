using System.ComponentModel.DataAnnotations;

namespace UsersFlow_API.DTOs
{
    public class NoteDTO
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
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
    }

    public class NewNoteDTO
    {
        [Required(ErrorMessage = "Title é obrigatório")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content é obrigatório")]
        [MaxLength(400)]
        public string Content { get; set; }
        [Required(ErrorMessage = "Created é obrigatório")]
        public DateTime Created { get; set; }

    }
}
