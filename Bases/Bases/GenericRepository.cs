using Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Bases.Bases
{
    public class GenericRepository<TEntity, TKeyType> where TEntity : BaseEntity<TKeyType>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GenericRepository<TEntity, TKeyType>> _logger;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext, ILogger<GenericRepository<TEntity, TKeyType>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Untrack(TEntity obj)
        {
            _dbContext.Entry(obj).State = EntityState.Detached;
        }

        public async Task<TEntity> Update(TEntity obj, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<TEntity> Select(TKeyType id, CancellationToken cancellationToken = default, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var query = Queryable(null, includes);
            return await query.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TModel>> GetWhereModel<TModel>(IQueryable<TModel> query, CancellationToken cancellationToken = default)
        {
            return await query
               .ToListAsync(cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var query = Queryable(predicate, includes);
            return await GetWhereModel(query, cancellationToken);
        }

        public async Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var query = Queryable(predicate);
            return await query
               .CountAsync(cancellationToken)
               .ConfigureAwait(false);
        }

        public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return QueryableModel<TEntity, TKeyType>(where, includes);
        }

        public IQueryable<TModel> QueryableModel<TModel, TKeyModel>(Expression<Func<TModel, bool>> where, Func<IQueryable<TModel>, IQueryable<TModel>> includes = null) where TModel : BaseEntity<TKeyModel>
        {
            var query = _dbContext.Set<TModel>().AsQueryable();
            if (where != null)
                query = query.Where(where);
            if (includes != null)
                query = includes(query);
            return query;
        }

        public async Task<TEntity> Insert(TEntity obj, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(obj);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return obj;
        }
    }
}
