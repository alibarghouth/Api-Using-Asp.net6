namespace Add_Database_Model.Dtos
{
    public class GenresDto
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
