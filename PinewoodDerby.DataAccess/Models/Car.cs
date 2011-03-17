using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinewoodDerby.DataAccess.Models
{
    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Number { get; set; }
        public virtual float Weight { get; set; }
        public virtual int LossCount { get; set; }
    }
}
