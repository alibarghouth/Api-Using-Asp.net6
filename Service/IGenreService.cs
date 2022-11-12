using Add_Database_Model.Dtos;

namespace Add_Database_Model.Service
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenreAsync();

        Task<Genre> GetGenrebyidAsync(byte id);

        Task<Genre> PostGenreAsync(Genre genre);     
        
        Genre UpdateGenreAsync(Genre genre);

        Genre DeleteGenreAsync(Genre genre);    
        
        Task<bool> IsValid(byte id);
    }
}
