namespace WebApplication1.Models
{
    public class Readers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string FavoriteGenres { get; set; }

        public virtual ICollection<ReaderBook>? ReaderBooks { get;}

    }
}
