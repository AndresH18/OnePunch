namespace OnePunchApi.Data.Repository;

public interface IRepository<T>
{
    T Create(T t);
    IEnumerable<T> GetAll();
    T? Get(int  id);
    void Update(T t);
    void Delete(T t);

    void SaveChanges();
}