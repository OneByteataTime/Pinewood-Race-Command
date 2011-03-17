using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PinewoodDerby.DataAccess.Models;

namespace PinewoodDerby.DataAccess.Maps
{
    public class RacerMap : ClassMap<Racer>
    {
        public RacerMap()
        {
            this.Table("racers");

            this.Id(m => m.Id).Column("id");

            this.Map(m => m.Lastname).Column("last_name");
            this.Map(m => m.Firstname).Column("first_name");
            this.Map(m => m.Den).Column("den");

            this.References<Race>(m => m.Race)
                .Column("race_id")
                .Cascade.All();
        }
    }
}
