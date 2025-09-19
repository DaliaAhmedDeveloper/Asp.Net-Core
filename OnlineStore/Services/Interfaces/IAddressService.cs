namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;

public interface IAddressService
{
    Task<Address> Add(int userId , CreateAddressDto address);
    Task<Address?>  Update(int userId , UpdateAddressDto updateAddressDto);
    Task<bool> Delete(int id);
    Task<IEnumerable<Address>> ListByUser(int userId);
    Task<Address> MakeItDefault(int id);

}