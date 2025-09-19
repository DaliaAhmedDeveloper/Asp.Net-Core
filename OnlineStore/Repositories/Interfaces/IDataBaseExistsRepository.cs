namespace OnlineStore.Repositories;

public interface IDataBaseExistsRepository
{
    Task<bool> CategoryExists(int id);
    Task<bool> TagExists(int id);
    Task<bool> AttributeValueExists(int id);
}