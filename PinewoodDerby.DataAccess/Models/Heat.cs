using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinewoodDerby.DataAccess.Models
{
    public class Heat
    {
        public virtual int Id { get; set; }
        public virtual int Number { get; set; }
        public virtual int Lane { get; set; }
        public virtual float Time { get; set; }
    }
}
