using System.Data.Entity;
using System.Diagnostics;

namespace LibraryManagementSystem.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryDB")
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<IssuedBook> IssuedBooks { get; set; } = null!;
    }
}
