namespace Add_Database_Model.Service
{
    public interface IMoviesService
    {
        //حطينا ديفلت فاليو
        Task<IEnumerable<Movie>> GetMoviesAsync(byte GenreId = 0 );

        Task <Movie> GetMoviesByIdAsync(int id);

        Task<Movie> AddMovies(Movie movie);

        Movie UpdateMoviesAsync(Movie movie);

        Movie DeleteMoviesAsync(Movie movie);
    }
}
