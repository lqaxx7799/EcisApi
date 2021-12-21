using EcisApi.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    //public interface IDatabaseTransaction : IDisposable
    //{
    //    void Commit();
    //    void Rollback();
    //}

    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction();
    }

    //public class EntityDatabaseTransaction : IDatabaseTransaction
    //{
    //    private readonly IDbContextTransaction _transaction;

    //    public EntityDatabaseTransaction(DataContext context)
    //    {
    //        _transaction = context.Database.BeginTransaction();
    //    }

    //    public void Commit()
    //    {
    //        _transaction.Commit();
    //    }

    //    public void Rollback()
    //    {
    //        _transaction.Rollback();
    //    }

    //    public void Dispose()
    //    {
    //        _transaction.Dispose();
    //    }
    //}

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return dataContext.Database.BeginTransaction();
        }
    }
}
