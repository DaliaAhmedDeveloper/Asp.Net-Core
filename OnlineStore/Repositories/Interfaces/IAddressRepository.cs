namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IAddressRepository : IGenericRepository<Address>
{
    Task<IEnumerable<Address>> GetAllByUserAsync(int id);
    Task<Address> ChangeDefaultStatus(Address address);
    Task MakeDefaultNone(Address address);
}