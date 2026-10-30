using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Domain.Entity
{
    public class Show
    {
        public int Id { get; private set; }
        public int MovieId { get; private set; }
        public Movie Movie { get; private set; } = null!;
        public int ScreenId { get; private set; }
        public Screen Screen { get; private set; } = null!;
        public DateTime StartTime { get; private set; }
        public decimal BasePrice { get; private set; }

        private Show() { } // For EF Core

        public Show(Movie movie, Screen screen, DateTime startTime, decimal basePrice)
        {
            if (movie is null) throw new ArgumentNullException(nameof(movie));
            if (screen is null) throw new ArgumentNullException(nameof(screen));
            if (basePrice <= 0) throw new ArgumentException("Base price must be greater than zero.", nameof(basePrice));

            Movie = movie;
            MovieId = movie.Id;
            Screen = screen;
            ScreenId = screen.Id;
            StartTime = startTime;
            BasePrice = basePrice;
        }

        public void Reschedule(DateTime newTime)
        {
            if (newTime < DateTime.UtcNow) throw new ArgumentException("Cannot schedule shows in the past.");
            StartTime = newTime;
        }

        public void AdjustPricing(decimal newPrice)
        {
            if (newPrice <= 0) throw new ArgumentException("Price must be greater than zero.");
            BasePrice = newPrice;
        }
    }

}
