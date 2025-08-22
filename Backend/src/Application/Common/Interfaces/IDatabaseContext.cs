using Domain.Entities;

namespace Application.Common.Interfaces;
public interface IDatabaseContext
{
    DbSet<Gadget> Gadgets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
