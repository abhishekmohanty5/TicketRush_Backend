using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Domain.Entity
{
    
    public class Theater
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Location { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public List<Screen> Screens { get; private set; } = new List<Screen>();

        private Theater() { }

        public Theater(string name, string location)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Theater name is required.", nameof(name));
            if (string.IsNullOrWhiteSpace(location)) throw new ArgumentException("Theater location is required.", nameof(location));

            Name = name;
            Location = location;
        }

        public void Deactivate()
        {
            // Put safety checks here if needed (e.g., check for active shows)
            IsActive = false;
        }

        public void UpdateDetails(string name, string location)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name;
            if (!string.IsNullOrWhiteSpace(location)) Location = location;
        }

        public void AddScreen(Screen screen)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            Screens.Add(screen);
        }
    }

}
