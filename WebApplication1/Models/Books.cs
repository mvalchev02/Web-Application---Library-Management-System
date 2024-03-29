﻿namespace WebApplication1.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime Released { get; set; } 

        public virtual ICollection<ReaderBook>? ReaderBooks { get;}
    }
}
