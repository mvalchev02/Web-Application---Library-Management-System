namespace WebApplication1.Models
{
    public class ReaderBook
    {
        public int Id { get; set; }
        public int LivreId { get; set; }
        public int LecteurId { get; set; }
        public DateTime BookGet { get; set; }
        public DateTime BookReturn { get; set; }
        public virtual Books? Livre { get; set; }
        public virtual Readers? Lecteur { get; set; }

    }
}
