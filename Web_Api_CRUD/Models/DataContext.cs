using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //protected override void OnCofiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //}
        public DbSet<Product> Products { get; set; }

        internal object Entry()
        {
            throw new NotImplementedException();
        }
    }
}
