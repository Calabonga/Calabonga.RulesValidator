using Calabonga.RulesValidator.Demo.Data.Base;
using Calabonga.RulesValidator.Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.RulesValidator.Demo.Data
{
    /// <summary>
    /// Database for application
    /// </summary>
    public class ApplicationDbContext : DbContextBase, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #region System

        public DbSet<Log> Logs { get; set; }

        #endregion
    }
}