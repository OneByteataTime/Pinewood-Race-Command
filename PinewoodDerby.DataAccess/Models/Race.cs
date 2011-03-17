using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinewoodDerby.DataAccess.Models
{
    public class Race 
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime RaceDate { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public virtual ICollection<Racer> Racers { get; set; }

        public Race()
        {
            this.Racers = new List<Racer>();
            this.CreateDate = DateTime.Now;
        }
    }
}
