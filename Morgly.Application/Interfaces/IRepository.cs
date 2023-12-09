namespace Morgly.Application.Interfaces;

public interface IRepository<T>
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The entity represeted by its id</returns>
    /// throws <see cref="EntityNotFoundException"/>
    Task<T> Get(Guid id);
    
    /// <summary>
    /// Retrieves all entities of type T.
    /// </summary>
    /// <returns>A Task that represents the asynchronous operation. The task result contains an IEnumerable of T representing all entities.</returns>

    Task<IEnumerable<T>> GetAll();
}

// Create exception class EntityNotFoundException


public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}
