using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission07.Models
{
    public class BookContext : DbContext
    {

        public BookContext()
        {

        }

        public BookContext(DbContextOptions<BookContext>options):base (options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
