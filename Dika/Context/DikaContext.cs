using Dika.Models;
using Microsoft.EntityFrameworkCore;

namespace Dika.Context
{
    public partial class DikaContext : DbContext
    {
        public DikaContext(DbContextOptions<DikaContext> options) : base(options)
        {

        }
        public DbSet<Invertory> Invertories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelbuilder.HasSequence("ContactIDSequence")
                .StartsAt(0)
            .IncrementsBy(10);

            modelbuilder.HasSequence("D").StartsAt(0);

            modelbuilder.HasSequence("val");

            OnModelCreatingPartial(modelbuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
