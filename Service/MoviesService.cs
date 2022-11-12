using Add_Database_Model.Dtos;
using Add_Database_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Add_Database_Model.Service
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDBContext _context;

        public MoviesService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMovies(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie DeleteMoviesAsync(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(byte GenreId = 0)
        {
            return await _context.Movies
                .Where(x => x.GenreId == GenreId || GenreId == 0)
                .Include(m => m.Genre)
                .OrderBy(m => m.Rate)
                .ToListAsync();

        }

      
        

        public async Task<Movie> GetMoviesByIdAsync(int id)
        {
            return await _context.Movies
                .Include(c => c.Genre)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Movie UpdateMoviesAsync(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return(movie);
        }
    }
}
