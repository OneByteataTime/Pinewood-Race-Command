using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PinewoodDerby.DataAccess.Models;

namespace PinewoodDerby.DataAccess.Maps
{
    class HeatMap : ClassMap<Heat>
    {
        public HeatMap()
        {
            this.Table("heats");

            this.Id(m => m.Id).Column("id");

            this.Map(m => m.Lane).Column("lane");
            this.Map(m => m.Number).Column("number");
            this.Map(m => m.Time).Column("time");
        }
    }
}
