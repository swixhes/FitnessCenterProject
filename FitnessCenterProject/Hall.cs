﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class Hall
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Hall(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }
    }
}
