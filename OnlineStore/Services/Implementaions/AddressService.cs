using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Repositories;

namespace OnlineStore.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepo;
    private readonly IStringLocalizer<AddressService> _localizer;
    public AddressService(IAddressRepository addressRepo, IStringLocalizer<AddressService> localizer)
    {
        _addressRepo = addressRepo;
        _localizer = localizer;
    }
    // add new address 
    public async Task<Address> Add(int userId , CreateAddressDto CreateAddressDto)
    {
        var address = new Address
        {
            UserId     =  userId,
            FullName   = CreateAddressDto.FullName,
            Street     = CreateAddressDto.Street,
            City       = CreateAddressDto.City,
            Country    = CreateAddressDto.Country,
            ZipCode    = CreateAddressDto.ZipCode,
            IsDefault  = CreateAddressDto.IsDefault
        };
        var addresses = await _addressRepo.GetAllByUserAsync(userId);
        if (CreateAddressDto.IsDefault == true)
        {
            foreach (var _address in addresses)
            {
                await _addressRepo.MakeDefaultNone(_address);
            }
        }
        return await _addressRepo.AddAsync(address);
    }
    // add new address 
    public async Task<Address?> Update(int userId , UpdateAddressDto updateAddressDto)
    {
        if (!updateAddressDto.AddressId.HasValue)
            throw new NotFoundException(_localizer["AddressIdNotFound"]);

        var existingAddress = await _addressRepo.GetByIdAsync(updateAddressDto.AddressId.Value);
        if (existingAddress == null)
            return null;

        var addresses = await _addressRepo.GetAllByUserAsync(userId);
        if (updateAddressDto.IsDefault == true)
        {
            foreach (var _address in addresses)
            {
                await _addressRepo.MakeDefaultNone(_address);
            }
        }

        existingAddress.FullName    = updateAddressDto.FullName;
        existingAddress.Street      = updateAddressDto.Street;
        existingAddress.City        = updateAddressDto.City;
        existingAddress.Country     = updateAddressDto.Country;
        existingAddress.ZipCode     = updateAddressDto.ZipCode;
        existingAddress.IsDefault   = updateAddressDto.IsDefault;
        var success = await _addressRepo.UpdateAsync(existingAddress);
        if (!success) return null;
        return existingAddress;
    }
    // remove Address
    public async Task<bool> Delete(int id)
    {
        bool status = await _addressRepo.DeleteAsync(id);
        if (!status)
            throw new NotFoundException(string.Format(_localizer["AddressNotFound", id]));
        return status;
    }
    // list all addresses
    public async Task<IEnumerable<Address>> ListByUser(int userId)
    {
        return await _addressRepo.GetAllByUserAsync(userId);
    }

    // make it default
    public async Task<Address> MakeItDefault(int id)
    {
        var address = await _addressRepo.GetByIdAsync(id);
        if (address == null)
            throw new NotFoundException(_localizer["AddressNotFound", id]);

        var updatedAddress = await _addressRepo.ChangeDefaultStatus(address);
        return updatedAddress;
    }
}