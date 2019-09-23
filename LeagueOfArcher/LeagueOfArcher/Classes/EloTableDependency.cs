using System;
using System.Collections.Generic;
using System.Text;
using LeagueOfArcher.Classes;

namespace LeagueOfArcher.Classes
{
    public class EloTableDependency : EloTable
    {

        public EloTableDependency() { }

        public EloTableDependency(int id, string name, float elo, float ratiobest) : base(id, name, elo, ratiobest)
        {
            this.ID = id;
            this.name = name;
            this.elo = elo;
            this.ratioBest = ratiobest;
        }

        public string AveString
        {
            get { return "Best ratio belong to " + name + " = " + ratioBest.ToString("0.00"); }
        }
    }
}
