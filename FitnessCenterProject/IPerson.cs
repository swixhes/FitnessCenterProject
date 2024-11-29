using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int Age { get; set; }
        string Nationality { get; set; }
        void PrintToDisplay();
    }
}
