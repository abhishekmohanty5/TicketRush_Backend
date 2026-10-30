using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketRush.Domain.Enums;

namespace TicketRush.Domain.Entity
{ 

    public class Seat
    {
        public int Id { get; private set; }
        public int ScreenId { get; private set; }
        public Screen Screen { get; private set; } = null!;
        public string Row { get; private set; } = string.Empty;
        public int Number { get; private set; }
        public SeatType Type { get; private set; }

        public string Label => $"{Row}{Number}";

        private Seat() { }

        public Seat(Screen screen, string row, int number, SeatType type)
        {
            if (screen is null) throw new ArgumentNullException(nameof(screen));
            if (string.IsNullOrWhiteSpace(row)) throw new ArgumentException("Seat row is required.", nameof(row));
            if (number <= 0) throw new ArgumentException("Seat number must be greater than zero.", nameof(number));
            if (!Enum.IsDefined(typeof(SeatType), type)) throw new ArgumentOutOfRangeException(nameof(type), "Invalid seat type.");

            Screen = screen;
            ScreenId = screen.Id;
            Row = row.Trim().ToUpperInvariant();
            Number = number;
            Type = type;
        }

        public void ChangeType(SeatType type)
        {
            if (!Enum.IsDefined(typeof(SeatType), type))
                throw new ArgumentOutOfRangeException(nameof(type), "Invalid seat type.");

            Type = type;
        }
    }

}
