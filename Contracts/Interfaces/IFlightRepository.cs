using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IFlightRepository : IRepository<Flight>
    {
        IEnumerable<Flight> GetFlights(string fromLocation, string toLocation);
        bool Any(Guid id);
    }
}
