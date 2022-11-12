namespace Add_Database_Model.Dtos
{
    public class MoviesDto:Base
    { 
        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }

    }
}
