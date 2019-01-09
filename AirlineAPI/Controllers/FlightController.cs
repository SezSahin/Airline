using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FlightController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Flight
        [HttpGet]
        public IEnumerable<Flight> Get()
        {
            return _unitOfWork.FlightRepository.GetAll();
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public Flight Get(Guid id)
        {
            return _unitOfWork.FlightRepository.Get(id);
        }

        [HttpGet("{fromLocation}/{toLocation}")]
        public IActionResult GetLocationBased(string fromLocation, string toLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var flight = _unitOfWork.FlightRepository.GetFlights(fromLocation, toLocation).Where(x => x.FromLocation == fromLocation && x.ToLocation == toLocation);

            return Ok(flight);
        }

        // POST: api/Flight
        [HttpPost]
        public IActionResult Create([FromBody] Flight flight)
        {
            _unitOfWork.FlightRepository.Add(flight);
            _unitOfWork.Complete();
            return Ok();
        }

        //PUT: api/Flight/5
        [HttpPut("")]
        public IActionResult Edit(Guid? id, Flight flight)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.FlightRepository.Get(flight.FlightId) is null)
                {
                    return NotFound();
                }

                var oldFlight = _mapper.Map<Flight>(_unitOfWork.FlightRepository.Get(flight.FlightId));
                try
                {
                    //oldFlight.AircraftType = flight.AircraftType;
                    //oldFlight.FromLocation = flight.FromLocation;
                    //oldFlight.ToLocation = flight.ToLocation;
                    oldFlight.DepartureTime = flight.DepartureTime;
                    oldFlight.ArrivalTime = flight.ArrivalTime;

                    _unitOfWork.Complete();
                    return Ok();
                }
                catch (Exception)
                {
                    return Conflict();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute]Guid id, [FromRoute]Flight flight)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.FlightRepository.Get(flight.FlightId) is null)
                {
                    return NotFound();
                }

                var oldFlight = _mapper.Map<Flight>(_unitOfWork.FlightRepository.Get(flight.FlightId));
                try
                {
                    oldFlight.DepartureTime = flight.DepartureTime;
                    oldFlight.ArrivalTime = flight.ArrivalTime;

                    _unitOfWork.Complete();
                    return Ok();
                }
                catch (Exception)
                {
                    return Conflict();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var flightID = _unitOfWork.FlightRepository.Get(id);
            if (flightID  == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.FlightRepository.Remove(flightID);
                _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception)
            {

                return Conflict();
            }

        }

        //private bool FlightExists(Guid id)
        //{
        //    return _unitOfWork.FlightRepository.Any(id);
        //}
    }
}
