using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace OrderSystem.Common.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        //TODO: Change to the real DbContext
        private DbContext _context = null;

        private IDbContextTransaction _transaction;

        public UnitOfWork(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }
        //private readonly FinancialGoalManagerDbContext _context;
        //public UnitOfWork(FinancialGoalManagerDbContext context)
        //{
        //    _context = context;
        //}

        public async Task BeginTransactionAsync()
            => _transaction = await _context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
    }
}
