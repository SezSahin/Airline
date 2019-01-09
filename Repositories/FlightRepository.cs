using Contracts.Interfaces;
using Entities.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(DataContext db) : base(db)
        {
            
        }

        public IEnumerable<Flight> GetFlights(string fromLocation, string toLocation)
        {
            return _db.Flights.Where(x => x.FromLocation == fromLocation && x.ToLocation == toLocation);
        }
        public bool Any(Guid id)
        {
            return _db.Set<Flight>().Any();
        }
    }
}
