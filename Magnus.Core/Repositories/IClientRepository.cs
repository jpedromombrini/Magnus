using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IClientRepository : IRepository<Client>
{
    void DeletePhonesRange(IEnumerable<ClientPhone> items);
    void DeleteSocialMediasRange(IEnumerable<ClientSocialMedia> items);
}