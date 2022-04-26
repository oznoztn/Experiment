using Experiment.Core.Entities;

namespace Experiment.Core.Repository;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(Guid id);
}