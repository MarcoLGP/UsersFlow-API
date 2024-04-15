using UsersFlow_API.DTOs;
using UsersFlow_API.Models;

namespace UsersFlow_API.Services
{
    public interface INoteService
    {
        public Task<Note> addNote(Note note);
        public Task<bool?> removeNote(int noteId);
        public Task<bool?> updateNote(NoteResumeDTO note, int noteId);
        public Task<IEnumerable<NoteDTO>?> getAllNotesByUser(int userId);
        public Task<string?> getNoteContent(int noteId);
    }
}
