using Microsoft.EntityFrameworkCore;

namespace Schaden.Infrastructure
{
    public abstract class SchadenDbContext : DbContext
    {
        public virtual DbSet<Domain.Entities.Schaden> Schaeden { get; set; }
        public virtual DbSet<Domain.Entities.SchadenPosition> SchadenPositionen { get; set; }
        public virtual DbSet<Domain.Entities.Versicherungsnehmer> Versicherungsnehmer { get; set; }
        public virtual DbSet<Domain.Entities.Vertrag> Vertraege { get; set; }
    }
}
