using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Domain.Entity
{
    public class Screen
    {
        public int Id { get; private set; }
        public int TheaterId { get; private set; }
        public Theater Theater { get; private set; } = null!;
        public string Name { get; private set; } = string.Empty; 
        public int TotalSeats { get; private set; }

        private Screen() {
            // For EF Core Materialization
        }

        public Screen(Theater theater, string name, int totalSeats)
        {
            if (theater is null) throw new ArgumentNullException(nameof(theater));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Screen name is required.", nameof(name));
            if (totalSeats <= 0) throw new ArgumentException("Total seats must be greater than zero.", nameof(totalSeats));

            Theater = theater;
            TheaterId = theater.Id;
            Name = name;
            TotalSeats = totalSeats;
        }

        public void UpdateScreenDetails(string name, int totalSeats)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name;
            if (totalSeats > 0) TotalSeats = totalSeats;
        }
    }

}
