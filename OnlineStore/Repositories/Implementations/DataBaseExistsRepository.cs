namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DataBaseExistsRepository : IDataBaseExistsRepository
{
    private readonly AppDbContext _context;

    public DataBaseExistsRepository(AppDbContext context)
    {
        _context = context;
    }

    // category exists
    public async Task<bool> CategoryExists(int id)
    {
        var result = await _context.Categories.FindAsync(id);
        if (result == null)
            return false;

        return true;
    }
    // Tag exists
    public async Task<bool> TagExists(int id)
    {
        var result = await _context.Tags.FindAsync(id);
        if (result == null)
            return false;

        return true;
    }
    // Attribute Value exists
    public async Task<bool> AttributeValueExists(int id)
    {
        var result = await _context.AttributeValues.FindAsync(id);
        if (result == null)
            return false;

        return true;
    }
     // user exists
    public async Task<bool> UserExists(int id)
    {
        var result = await _context.Users.FindAsync(id);
        if (result == null)
            return false;

        return true;
    }
}
