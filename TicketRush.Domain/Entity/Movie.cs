using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Domain.Entity
{
    public class Movie
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Genre { get; private set; } = string.Empty;
        public int DurationInMinutes { get; private set; }
        public bool IsActive { get; private set; } = true;

        private Movie() { } // For EF Core

        public Movie(string title, string genre, int durationInMinutes)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.", nameof(title));
            if (string.IsNullOrWhiteSpace(genre)) throw new ArgumentException("Genre is required.", nameof(genre));
            if (durationInMinutes <= 0) throw new ArgumentException("Duration must be greater than zero.", nameof(durationInMinutes));

            Title = title;
            Genre = genre;
            DurationInMinutes = durationInMinutes;
            IsActive = true;
        }

        public void UpdateDetails(string title, string genre, int durationInMinutes)
        {
            if (!string.IsNullOrWhiteSpace(title)) Title = title;
            if (!string.IsNullOrWhiteSpace(genre)) Genre = genre;
            if (durationInMinutes > 0) DurationInMinutes = durationInMinutes;
        }

        public void Archive() => IsActive = false;
    }

}
