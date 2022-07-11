using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Domain.Entities;

namespace Inventory.Data.Repositories;

public class UserOperationClaimRepository : GenericRepository<UserOperationClaim>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(IMongoDbContext context) : base(context)
    {
    }
}