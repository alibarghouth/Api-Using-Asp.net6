namespace Add_Database_Model.Dtos
{
    public class MoviesDetailsDto : Base
    {
        public int Id { get; set; }


        public byte[] Poster { get; set; }


        public byte GenreId { get; set; }


        public string GenreName { get; set; }


        public string GenreDescription { get; set; }

    }
}
