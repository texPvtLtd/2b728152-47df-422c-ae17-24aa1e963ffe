﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity;
using Repository.RepositoryFactoryBase;
using System.Linq.Expressions;
using System.Threading;
using LinqKit;
using System.Data;
using Entities.Models;


namespace Repository.RepositoryFactoryCore
{
    public abstract class RepositoryFactory<TEntity> where TEntity : class
    {
        private UmartContext _dataContext;
        private readonly DbSet<TEntity> _dbSet;      

        protected IDatabaseFactory _databaseFactory { get; private set; }
        protected RepositoryFactory(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            _dbSet = DataContext.Set<TEntity>();            
        }

        protected UmartContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public virtual void Insert(TEntity entity)
        {            
            _dbSet.Add(entity);           
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }       

        public virtual void Update(TEntity entity)
        {            
            _dbSet.Attach(entity);            
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual int ExecuteCommand(string query)
        {
            return _dataContext.Database.ExecuteSqlCommand(query);
        }

        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public Task<List<TEntity>> GetListWithNoTrackingAsync(Expression<Func<TEntity, bool>> exp = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (exp != null)
            {
                query = query.Where(exp);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            
            return query.AsNoTracking().ToListAsync<TEntity>();
        }

        public List<TEntity> GetListWithNoTracking(Expression<Func<TEntity, bool>> exp = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (exp != null)
            {
                query = query.Where(exp);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            return query.AsNoTracking().ToList<TEntity>();
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }

        public TEntity GetWithNoTracking(Expression<Func<TEntity, bool>> exp)
        {
            return _dbSet.AsNoTracking().Where(exp).FirstOrDefault();
        }
    }
}
