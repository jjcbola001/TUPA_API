using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.Common
{
    public class TitleList
    {
        public List<Title> Titles { get;set;}
    }

    public class Title
    {
        public string RoD_ID { get; set; }
        public string RoD_Name { get; set; }
        public string Title_Number { get; set; }
        public string Title_Type { get; set; }
        public int NumberOfCoOwner { get; set; }
        public string Owner_Name { get; set; }
        public string Location { get; set; }
    }
}
