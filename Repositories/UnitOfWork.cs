using Contracts.Interfaces;
using Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;
        public UnitOfWork(DataContext db)
        {
            _db = db;
            FlightRepository = new FlightRepository(_db);
        }
        public IFlightRepository FlightRepository { get; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
