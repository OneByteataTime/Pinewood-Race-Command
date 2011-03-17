using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinewoodDerby.DataAccess.Models
{
    public class Racer 
    {
        public virtual int Id { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Den { get; set; }
        public virtual Race Race { get; set; }
    }
}
