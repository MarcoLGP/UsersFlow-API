using UsersFlow_API.DTOs;
using UsersFlow_API.Models;
using UsersFlow_API.Repositories;

namespace UsersFlow_API.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Note> addNote(Note note)
        {
            await _noteRepository.addNote(note);
            return note;
        }

        public async Task<bool?> removeNote(int noteId)
        {
            var note = await _noteRepository.getNoteById(noteId);
            
            if (note is null)
                return null;
            
            await _noteRepository.removeNote(note);

            return true;
        }

        public async Task<bool?> updateNote(NoteResumeDTO noteResumeDTO, int noteId)
        {
            var note = await _noteRepository.getNoteById(noteId);

            if (note is null)
                return null;

            note.Title = noteResumeDTO.Title;
            note.Content = noteResumeDTO.Content;
            note.Public = noteResumeDTO.Public;
            note.Locked = noteResumeDTO.Locked;

            await _noteRepository.updateNote(note);

            return true;
        }

        public async Task<IEnumerable<NoteDTO>?> getAllNotesByUser(int userId)
        {
            var notesFound = await _noteRepository.getAllNotesByUser(userId);

            if (!notesFound.Any())
                return null;

            return notesFound.Select(obj =>
            {
                return new NoteDTO
                {
                    Title = obj.Title,
                    NoteId = obj.NoteId,
                    Created = obj.Created,
                    Content = obj.Content,
                    Locked = obj.Locked,
                    Public = obj.Public
                };
            });
        }
    }
}
