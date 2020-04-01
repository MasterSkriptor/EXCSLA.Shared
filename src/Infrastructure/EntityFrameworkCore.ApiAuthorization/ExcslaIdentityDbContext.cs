using EXCSLA.Shared.Core;
using EXCSLA.Shared.Core.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Data
{
    /// <summary>
    /// The base DbContext for EXCSLA.Shared. It implements the event dispatcher. It also automatically registers the configuration
    /// options in the config folder.
    /// </summary>
    /// <typeparam name="T">The base dbcontext you wish to use. Example: DbContext or IdentityDbContext</typeparam>
    public abstract class ExcslaIdentityDbContext<TUser> : ApiAuthorizationDbContext<TUser> where TUser : IdentityUser
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public ExcslaIdentityDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, IDomainEventDispatcher dispatcher) : base(options, operationalStoreOptions)
        {
            _dispatcher = dispatcher;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.ClearEvents();
                foreach (var domainEvent in events)
                {
                    await _dispatcher.DispatchAsync(domainEvent).ConfigureAwait(false);
                }
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Scans assembly for config files, they are located in the config folder under data.
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            // Let the base do its thing
            base.OnModelCreating(builder);
        }
    }
}
