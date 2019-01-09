using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFlightRepository FlightRepository { get; }
        int Complete();
    }
}
