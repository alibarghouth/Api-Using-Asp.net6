using Add_Database_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Add_Database_Model.Service
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDBContext _context;

        public GenreService(ApplicationDBContext context)
        {
            _context = context;
        }

        public  Genre DeleteGenreAsync(Genre genre)
        {
            _context.Remove(genre);

            _context.SaveChanges();
            return genre;

        }

        public async Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
             return  await _context.Genres.OrderBy(p => p.Name).ToListAsync();

        }

        public async Task<Genre> GetGenrebyidAsync(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(m => m.Id == id);

           
        }

        public  Task<bool> IsValid(byte id)
        {
            return  _context.Genres.AnyAsync(c => c.Id == id);
        }

        public async Task<Genre> PostGenreAsync(Genre genre)
        {
             await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public  Genre UpdateGenreAsync(Genre genre)
        {
            _context.Update(genre);

            _context.SaveChanges();

            return genre;


        }
    }
}
