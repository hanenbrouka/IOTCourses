using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Entities
{
    public class Message
    {
        string _idMachine;
        DateTime _date;
        MachineState _state;

        public Message(string idMachine, DateTime date, MachineState state)
        {
            _idMachine = idMachine;
            _date = date;
            _state = state;
        }

        public string IdMachine { get => _idMachine; set => _idMachine = value; }
        public DateTime Date { get => _date; set => _date = value; }
        internal MachineState State { get => _state; set => _state = value; }
    }
}
