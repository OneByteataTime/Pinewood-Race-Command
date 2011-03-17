using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinewoodDerby.DataAccess.Models;
using FluentNHibernate.Mapping;

namespace PinewoodDerby.DataAccess.Maps
{
    public class RaceMap : ClassMap<Race>
    {
        public RaceMap()
        {
            this.Table("races");

            this.Id(m => m.Id).Column("id");
            this.Map(m => m.Name).Column("name");
            this.Map(m => m.Description).Column("description");
            this.Map(m => m.RaceDate).Column("race_date");
            this.Map(m => m.CreateDate).Column("create_date");

            this.HasMany<Racer>(m => m.Racers)
                .AsSet()
                .Cascade.All()
                .KeyColumn("race_id")
                .Inverse();
        }
    }
}
