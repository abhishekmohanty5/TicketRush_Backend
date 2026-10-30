using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Domain.Entity
{
    public class Payment
    {
        public int Id { get; private set; }
        public int BookingId { get; private set; }
        public Booking Booking { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public string Method { get; private set; } = string.Empty;
        public string TransactionId { get; private set; } = string.Empty;
        public DateTime PaidAt { get; private set; }

        private Payment() { }

        public Payment(int bookingId, decimal amount, string method, string transactionId)
        {
            if (bookingId <= 0) throw new ArgumentException("BookingId must be greater than zero.", nameof(bookingId));
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            if (string.IsNullOrWhiteSpace(method)) throw new ArgumentException("Payment method is required.", nameof(method));
            if (string.IsNullOrWhiteSpace(transactionId)) throw new ArgumentException("Transaction id is required.", nameof(transactionId));

            BookingId = bookingId;
            Amount = amount;
            Method = method;
            TransactionId = transactionId;
            PaidAt = DateTime.UtcNow;
        }
    }
}
