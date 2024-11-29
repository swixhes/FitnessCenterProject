using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public class Training : IPrintable
    {
        public TrainingType Type { get; set; }
        public Trainer Trainer { get; set; }
        public Hall Hall { get; set; }
        public DateTime Date { get; set; }
        public List<Client> EnrolledClients { get; set; }

        public Training(TrainingType type, Trainer trainer, Hall hall, DateTime date)
        {
            throw new NotImplementedException();
        }
        public void AddClient(Client client)
        {
             throw new NotImplementedException();
        }
        public void PrintToDisplay()
        {
            throw new NotImplementedException();
        }
    }
}
