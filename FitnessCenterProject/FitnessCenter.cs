using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class FitnessCenter : IPrintable
    {
        public string Name { get; set; }
        public List<Hall> Halls { get; set; }
        public List<Training> Trainings { get; set; }

        public FitnessCenter()
        {
            throw new NotImplementedException();
        }

        public void PrintToDisplay()
        {
            throw new NotImplementedException();
        }
    }
}
