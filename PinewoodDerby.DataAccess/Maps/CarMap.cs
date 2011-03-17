using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PinewoodDerby.DataAccess.Models;

namespace PinewoodDerby.DataAccess.Maps
{
   public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            this.Table("cars");

            this.Id(m => m.Id).Column("id");
            this.Map(m => m.LossCount).Column("loss_count");
            this.Map(m => m.Number).Column("number");
            this.Map(m => m.Weight).Column("weight");
        }
    }
}
