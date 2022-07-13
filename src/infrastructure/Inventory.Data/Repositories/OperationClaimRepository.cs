using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Domain.Entities;

namespace Inventory.Data.Repositories;

public class OperationClaimRepository : GenericRepository<OperationClaim>, IOperationClaimRepository
{
    public OperationClaimRepository(IMongoDbContext context) : base(context)
    {
    }
}