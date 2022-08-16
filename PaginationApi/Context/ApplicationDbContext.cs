using Microsoft.EntityFrameworkCore;
using PaginationApi.Models;

namespace PaginationApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       
        public DbSet<User> Users { get; set; }
    }
}
