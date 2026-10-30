using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketRush.Domain.Enums;

namespace TicketRush.Domain.Entity
{
    public class Booking
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public int ShowId { get; private set; }
        public Show Show { get; private set; } = null!;
        public DateTime BookedAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public BookingStatus Status { get; private set; } 

        private Booking() { } 

        public Booking(int userId, int showId, decimal totalAmount)
        {
            if (userId <= 0) throw new ArgumentException("UserId must be greater than zero.", nameof(userId));
            if (showId <= 0) throw new ArgumentException("ShowId must be greater than zero.", nameof(showId));
            if (totalAmount <= 0) throw new ArgumentException("Total amount must be greater than zero.", nameof(totalAmount));

            UserId = userId;
            ShowId = showId;
            TotalAmount = totalAmount;
            BookedAt = DateTime.UtcNow;
            Status = BookingStatus.Pending;
        }

        public void ConfirmBooking() => Status = BookingStatus.Confirmed;

        public void CancelBooking()
        {
            if (Status == BookingStatus.Cancelled) throw new InvalidOperationException("Booking is already cancelled.");
            Status = BookingStatus.Cancelled;
        }
    }
}
