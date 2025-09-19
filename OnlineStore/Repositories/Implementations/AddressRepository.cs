using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(AppDbContext context) : base(context)
    {
    }

    // get all by user
    public async Task<IEnumerable<Address>> GetAllByUserAsync(int id)
    {
        return await _context.Addresses.Where(a => a.UserId == id).ToListAsync();
    }
    public async Task<Address> ChangeDefaultStatus(Address address)
    {
        // Get all addresses for the same user
        var userId = address.UserId;
        var userAddresses = await _context.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();

        // Set all addresses to IsDefault = false
        foreach (var addr in userAddresses)
            addr.IsDefault = false;

        address.IsDefault = true;
        await _context.SaveChangesAsync();

        return address;
    }

    // make isdefault false
    public async Task MakeDefaultNone(Address address)
    {
        address.IsDefault = false;
       await _context.SaveChangesAsync();
    }
}