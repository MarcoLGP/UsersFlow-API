using UsersFlow_API.Models;

namespace UsersFlow_API.Repositories
{
    public interface INoteRepository
    {
        public Task<Note?> getNoteById(int noteId);
        public Task<Note> addNote(Note note);
        public Task removeNote(Note note);
        public Task updateNote(Note note);
        public Task<IEnumerable<Note>> getAllNotesByUser(int userId, int skip, int take);
        public Task<IEnumerable<Note>> getAllPublicNotes(int skip, int take);
    }
}
