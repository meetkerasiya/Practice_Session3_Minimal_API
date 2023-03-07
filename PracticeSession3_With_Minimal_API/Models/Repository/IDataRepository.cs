namespace PracticeSession3_With_Minimal_API.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        void Add(TEntity entity);
        void Delete(int id);
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Update(TEntity entity);
        void Save();
    }
}