using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects;

namespace WOPR.Domain.Data
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : BaseDO
    {
        public TEntity Get(long id)
        {
            throw new NotImplementedException();
        }

        public TEntity GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public interface IRepository<TEntity>
        where TEntity : BaseDO
    {
        TEntity Get(long id);

        TEntity GetAll();
    }
}
