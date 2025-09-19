namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepo;

    public WarehouseService(IWarehouseRepository warehouseRepo)
    {
        _warehouseRepo = warehouseRepo;
    }

    /*=========== API ========================*/

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Warehouse>> GetAllForWeb()
    {
        return await _warehouseRepo.GetAllAsync();
    }
    // get all with pagination
    public async Task<PagedResult<Warehouse>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _warehouseRepo.CountAllAsync();
        var warehouses = await _warehouseRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Warehouse>
        {
            Items = warehouses,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Warehouse?> GetForWeb(int id)
    {
        return await _warehouseRepo.GetByIdAsync(id);
    }

    // add new warehouse
    public async Task<Warehouse> CreateForWeb(WarehouseViewModel model)
    {
        var warehouse = new Warehouse
        {
            Code = model.Code,
            Name = model.Name,
            Address = model.Address,
            City = model.City,
            State = model.State,
            Country = model.Country,
            ZipCode = model.ZipCode,
            Phone = model.Phone,
            Email = model.Email,
            IsActive = model.IsActive,
            IsDefault = model.IsDefault,
        };
        await _warehouseRepo.AddAsync(warehouse);
        return warehouse;
    }
    // update warehouse
    public async Task<Warehouse> UpdateForWeb(WarehouseViewModel model, Warehouse warehouse)
    {
        warehouse.Id = model.Id;
        warehouse.Code = model.Code;
        warehouse.Name = model.Name;
        warehouse.Address = model.Address;
        warehouse.City = model.City;
        warehouse.State = model.State;
        warehouse.Country = model.Country;
        warehouse.ZipCode = model.ZipCode;
        warehouse.Phone = model.Phone;
        warehouse.Email = model.Email;
        warehouse.IsActive = model.IsActive;
        warehouse.IsDefault = model.IsDefault;
        
        await _warehouseRepo.UpdateAsync(warehouse);
        return warehouse;
    }
    // delete warehouse
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _warehouseRepo.DeleteAsync(id);
    }

}