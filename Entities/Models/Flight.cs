using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Flight
    {
        [Key]
        public Guid FlightId { get; set; }
        [Required]
        public string AircraftType { get; set; }
        [Required]
        public string FromLocation { get; set; }
        [Required]
        public string ToLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
