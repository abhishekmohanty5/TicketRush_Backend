using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Domain.Entity
{
    public class BookingSeat
    {
        public int Id { get; private set; }
        public int BookingId { get; private set; }
        public string SeatNumber { get; private set; } = string.Empty; 

        private BookingSeat() { } // For EF Core

        public BookingSeat(int bookingId, string seatNumber)
        {
            if (bookingId <= 0) throw new ArgumentException("BookingId must be greater than zero.", nameof(bookingId));
            if (string.IsNullOrWhiteSpace(seatNumber)) throw new ArgumentException("Seat number is required.", nameof(seatNumber));

            BookingId = bookingId;
            SeatNumber = seatNumber;
        }
    }

}
