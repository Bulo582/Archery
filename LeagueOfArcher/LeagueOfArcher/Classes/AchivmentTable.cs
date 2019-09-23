using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LeagueOfArcher.Classes
{
    [Table("AchivmentTable")]
    public class AchivmentTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50), NotNull]
        public string name { get; set; }
        public string[] achivmentList { get; set; }

        public AchivmentTable() { }

        public AchivmentTable(string name)
        {
            this.name = name;
        }

        public AchivmentTable(string name, string[] achivmentlist)
        {
            this.name = name;
            this.achivmentList = achivmentlist;
        }

    }
}
