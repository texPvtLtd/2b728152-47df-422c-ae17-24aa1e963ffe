﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository.RepositoryFactoryBase;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using Entities.Models;

namespace Repository.RepositoryFactoryCore
{
    public class UnitOfWork : IUnitOfWorkFactory
    {
        private readonly IDatabaseFactory _databaseFactory;
        private UmartContext _dataContext;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        protected UmartContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public int SaveChanges()
        {
           return DataContext.SaveChanges();
        }

        public void Dispose()
        {
            DataContext.Dispose();
            //this.databaseFactory.Dispose();
        }

        #region Unit of Work Transactions
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter)DataContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();           
        }

        #endregion
    }
}
