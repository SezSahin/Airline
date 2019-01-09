using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Airline.Controllers
{
    [AutoValidateAntiforgeryToken] //prevents Cross site scripting
    public class HomeController : Controller
    {
        private readonly DataContext _db;
        private readonly HttpClient _client;

        private Uri BaseEndPoint { get; set; }
        public HomeController(DataContext db)
        {
            BaseEndPoint = new Uri("http://localhost:15782/api/Flight");
            _client = new HttpClient();
            _db = db;
        }

        //                  READ THIS
        // ************
        // ********I was supposed to consume my API, but I got some weird exception on my JsonConvert, so I failed this part of the assignment.********
        // ************


        // Consuming the API below.

        // GET: Home
        public async Task<ActionResult> Index()
        {
            // JEG SKREV DEN FORKERTE LOCALHOST OG TÆNKTE IKKE PÅ DET!!! FORSTOD IKKE HVORFOR DET IKKE VIRKEDE I 3 TIMER.. Jeg græder snart.
            var response = await _client.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Flight>>(data));
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _db.Flights.ToListAsync());
        //}

        // GET: Home/Details/5
        public async Task<IActionResult> Details(Guid Id)
        {
            var response = await _client.GetAsync(BaseEndPoint + "/" + Id.ToString(), HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Flight>(data));
        }

        // Consuming the API below.

        // GET: Home/Create
        public async Task<ActionResult> Create(Flight flight)
        {
            var response = await _client.PostAsJsonAsync(BaseEndPoint, flight);

            //response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        // Consuming the API below. UNFINISHED

        // GET: Home/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // use HTTP client to read data from API. Move on once the headers have been read. Errors are caught slightly quicker this way.
            var response = await _client.GetAsync(BaseEndPoint + $"/{id}", HttpCompletionOption.ResponseHeadersRead);
            // Turn the response body into a string
            var data = await response.Content.ReadAsStringAsync();
            var flight = JsonConvert.DeserializeObject<Flight>(data);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FlightId,AircraftType,FromLocation,ToLocation,DepartureTime,ArrivalTime")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Post the created gift as JSON to API. HttpClient handles serialization for us
                    var response = await _client.PutAsJsonAsync<Flight>(BaseEndPoint + $"/{id}", flight);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    if(!await FlightExists(flight.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Home/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var response = await _client.DeleteAsync(BaseEndPoint + "/" + Id.ToString());
            //response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(JsonConvert.DeserializeObject<Flight>(data));
        }

        [HttpGet]
        public async Task<IActionResult> Index(string FromLocation, string ToLocation)
        {
            var response = await _client.GetAsync(BaseEndPoint + "/" + FromLocation + "/" + ToLocation, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Flight>>(data));
        }

        private async Task<bool> FlightExists(Guid id)
        {
            var response = await _client.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var context = JsonConvert.DeserializeObject<List<Flight>>(data);
            return context.Any(e => e.FlightId == id);
        }
    }
}