﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataModels
{
    public class title_basics
    {
        public string  tconst { get; set; }
        public string  titletype { get; set; }
        public string  primarytitle { get; set; }
        public string  originaltitle { get; set; }
        public bool  isadult { get; set; }
        public string startyear { get; set; }
        public int?  runtimeminutes { get; set; }
        public string genres { get; set; }
    }
}