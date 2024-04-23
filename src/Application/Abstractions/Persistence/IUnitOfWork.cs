namespace CloudHotel.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<Result<bool, Error>> Commit();
    Task<Result<Guid, Error>> Commit(Guid id);
}