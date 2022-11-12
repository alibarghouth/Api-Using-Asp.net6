namespace Add_Database_Model.Dtos
{
    public class Base
    {
        [MaxLength(100)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoreLine { get; set; }
    }
}
