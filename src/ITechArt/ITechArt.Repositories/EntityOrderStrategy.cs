using System;
using System.Linq.Expressions;

namespace ITechArt.Repositories
{
    public class EntityOrderStrategy<TEntity>
    {
        public Expression<Func<TEntity, object>> OrderBy { get; }

        public bool Ascending { get; }


        private EntityOrderStrategy(Expression<Func<TEntity, object>> orderBy, bool ascending)
        {
            OrderBy = orderBy;
            Ascending = ascending;
        }


        public static EntityOrderStrategy<TEntity> CreateAscending(Expression<Func<TEntity, object>> orderBy)
        {
            var strategy = new EntityOrderStrategy<TEntity>(orderBy, true);

            return strategy;
        }

        public static EntityOrderStrategy<TEntity> CreateDescending(Expression<Func<TEntity, object>> orderBy)
        {
            var strategy = new EntityOrderStrategy<TEntity>(orderBy, false);

            return strategy;
        }
    }
}
