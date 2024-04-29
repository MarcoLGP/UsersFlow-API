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

            await _noteRepository.updateNote(note);

            return true;
        }

        public async Task<IEnumerable<NoteDTO>?> getAllNotesByUser(int userId, int skip, int take)
        {
            var notesFound = await _noteRepository.getAllNotesByUser(userId, skip, take);

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
                    Public = obj.Public
                };
            });
        }

        public async Task<IEnumerable<NoteDTO>?> getAllPublicNotes(int skip, int take)
        {
            var result =  await _noteRepository.getAllPublicNotes(skip, take);

            if (!result.Any())
                return null;


            return result.Select(obj =>
            {
                return new NoteDTO
                {
                    Author = obj.User.Name,
                    Title = obj.Title,
                    NoteId = obj.NoteId,
                    Created = obj.Created,
                    Content = obj.Content,
                    Public = obj.Public
                };
            });
        }
    }
}
