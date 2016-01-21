using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository.RepositoryFactoryBase;
using Entities.Models;

namespace Repository.RepositoryFactoryCore
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private UmartContext _dataContext;

        public UmartContext Get()
        {
            return _dataContext ?? (_dataContext = new UmartContext());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
            }
        }
    }
}
