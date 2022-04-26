using System.Diagnostics.CodeAnalysis;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Repository;

namespace Experiment.API.Fakes;

// No need to unit test fake implementations
[ExcludeFromCodeCoverage]
public class FakeUserRepository : IUserRepository
{
    private static readonly List<User> Users = new()
    {
        new User()
        {
            Id = Guid.Parse("0246a66d-0627-4f11-aadd-2f91a725cf76"),
            RegisterationDate = DateTime.UtcNow,
            UserType = UserType.Affiliate,
        },
        new User()
        {
            Id = Guid.Parse("6b0b2241-cca8-478e-9f3c-c70c4444d46a"),
            RegisterationDate = DateTime.UtcNow,
            UserType = UserType.Affiliate,
        },
        new User()
        {
            Id = Guid.Parse("0a331800-25e1-49f4-914a-11869a383ead"),
            RegisterationDate = DateTime.UtcNow.AddYears(-3),
            UserType = UserType.Customer
        },
        new User()
        {
            Id = Guid.Parse("14a78e3c-db40-49dc-b3ce-777982c5145f"),
            RegisterationDate = DateTime.UtcNow,
            UserType = UserType.Customer
        },
        new User()
        {
            Id = Guid.Parse("473cb273-5142-4ccd-9e40-8e6bb1eaf75b"),
            RegisterationDate = DateTime.UtcNow.AddYears(-3),
            UserType = UserType.Employee
        }
    };

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await Task.FromResult(Users.FirstOrDefault(t => t.Id == id));
    }
}