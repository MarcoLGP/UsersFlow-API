using Microsoft.EntityFrameworkCore;
using UsersFlow_API.Context;
using UsersFlow_API.Models;

namespace UsersFlow_API.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApiDbContext _context;

        public async Task<Note?> getNoteById(int noteId)
        {
            return await _context.Notes.FirstOrDefaultAsync(c => c.NoteId == noteId);
        }

        public NoteRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Note> addNote(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task removeNote(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task updateNote(Note note)
        { 
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> getAllNotesByUser(int userId, int skip, int take)
        {
            var result = await _context.Notes
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Created)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToListAsync();
            
            return result;
        }

        public async Task<IEnumerable<Note>> getAllPublicNotes(int skip, int take)
        {
            var result = await _context.Notes
                .Where(c => c.Public)
                .OrderByDescending(c => c.Created)
                .Include(c => c.User)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToListAsync();
            
            return result;
        }
    }
}
