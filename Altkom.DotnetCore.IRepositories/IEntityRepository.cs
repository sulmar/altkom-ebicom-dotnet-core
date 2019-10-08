using System.Collections.Generic;

namespace Altkom.DotnetCore.IRepositories
{
    public interface IEntityRepository<TEntity, TKey>
    {
        ICollection<TEntity> Get();
        TEntity Get(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TKey id);
        bool IsExists(TKey key);
    }

}
