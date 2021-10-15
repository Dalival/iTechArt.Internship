using System;
using System.Linq.Expressions;

namespace ITechArt.Repositories
{
    public class EntityOrderStrategy<TEntity>
    {
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public bool Ascending { get; private set; }


        public static EntityOrderStrategy<TEntity> CreateAscending(Expression<Func<TEntity, object>> orderBy)
        {
            var strategy = new EntityOrderStrategy<TEntity>
            {
                OrderBy = orderBy,
                Ascending = true
            };

            return strategy;
        }

        public static EntityOrderStrategy<TEntity> CreateDescending(Expression<Func<TEntity, object>> orderBy)
        {
            var strategy = new EntityOrderStrategy<TEntity>
            {
                OrderBy = orderBy,
                Ascending = false
            };

            return strategy;
        }
    }
}
