using laundry.project.Business;
using laundry.project.Entities;


namespace laundry.project.Presentation
{
class DisplayManager
    {
        public void DicplayConfiguration(List<Machine> machines, Dictionary<int, string> map)
        {

            $"** Laundry config **".WriteLineLeft(ConsoleColor.Yellow);
            foreach (var machine in machines)
            {

                $"Input : {machine.Input} Machine : {machine.IdMachine}".WriteLineLeft(ConsoleColor.Yellow);
                foreach (var cycle in machine.Cycles)
                {

                    $"cycle : {cycle.NameCycle} Machine : {cycle.DureeCycle}".WriteLineLeft(ConsoleColor.Yellow);
                }
            }
            "".WriteLineLeft(ConsoleColor.Yellow);
            "".WriteLineLeft(ConsoleColor.Yellow);

            $"** MapInputMAchine key Value **".WriteLineLeft(ConsoleColor.Yellow);

            foreach (var (key, value) in Structure.MapInputMachine)
            {

                ($"{key}<->{value} ").WriteLineLeft(ConsoleColor.Yellow);
            }
            "".WriteLineLeft(ConsoleColor.Yellow);
        }
        public void DicplayMenuPrincipale(List<Machine> machines, Dictionary<int, string> map)
        {
            string? decision;
            do
            {
                $"** MENU **".WriteLineLeft(ConsoleColor.White);
                $"1-Arrêter Machine".WriteLineLeft(ConsoleColor.White);
                $"2-Démarrer Machine".WriteLineLeft(ConsoleColor.White);
                $"3-Lancer cycle Machine".WriteLineLeft(ConsoleColor.White);
                "Choix : ".WriteLeft(ConsoleColor.Green);
                string? state = Uitility.ReadLineLeft(ConsoleColor.Green);
                switch (state)
                {
                    case "1": DicplayMenuMachineON(machines, map); break;
                    case "2": DicplayMenuMachineOFF(machines, map); break;
                    case "3": DicplayMenuMachineCycle(machines, map); break;
                }
                "Continuer : ".WriteLeft(ConsoleColor.Green);
                decision = Uitility.ReadLineLeft(ConsoleColor.Green);
            } while (decision != "q");
        }

        private void DicplayMenuMachineCycle(List<Machine> machines, Dictionary<int, string> map)
        {
            IEnumerable<Machine> machineCyle = machines.Where(m => m.CurrentState == MachineState.D);
            if (machineCyle.Count() == 0)
            {
                $"NO MACHINE ON".WriteLeft(ConsoleColor.Red);
                DicplayMenuPrincipale(machines, map);
                return;
            }
            foreach (var m in machineCyle)
                $"machine {m.IdMachine}".WriteLeft(ConsoleColor.White);

            "Choix :".WriteLeft(ConsoleColor.Green);
            string? choixMachine = Uitility.ReadLineLeft(ConsoleColor.Green);
            Machine? machine = machines.FirstOrDefault(m => m.IdMachine == choixMachine);
            if (machine != null)
            {
                foreach (var c in machine.Cycles)
                    $"cycle {c.NameCycle}  {c.DureeCycle}".WriteLeft(ConsoleColor.White);
                "Choix :".WriteLeft(ConsoleColor.Green);
                string? choixCycle = Uitility.ReadLineLeft(ConsoleColor.Green);
                Cycle? cycle = machine.Cycles.FirstOrDefault(c => c.NameCycle == choixCycle);
                if (cycle != null)
                {
                    Structure.MapInputTension[machine.Input] = 10;
                    StateMachineManager.StartCycle(machine, cycle);
                }
            }
        }

        public void DicplayMenuMachineOFF(List<Machine> machines, Dictionary<int, string> map)
        {
            IEnumerable<Machine> machineOFF = machines.Where(m => m.CurrentState == MachineState.A);
            if (machineOFF.Count()==0)
            {
                $"NO MACHINE OFF".WriteLeft(ConsoleColor.Red);
                DicplayMenuPrincipale(machines, map);
                return;
            }
            foreach (var m in machineOFF)
                $"machine {m.IdMachine}".WriteLeft(ConsoleColor.White);
            "Choix :".WriteLeft(ConsoleColor.Green);
            string? choix = Uitility.ReadLineLeft(ConsoleColor.Green);
            Machine? machine = machines.FirstOrDefault(m => m.IdMachine == choix);
            if (machine != null)
            {
                
                Structure.MapInputTension[machine.Input] = 5;
            }
            Console.WriteLine();
            DicplayMenuPrincipale(machines, map);
        }
        public void DicplayMenuMachineON(List<Machine> machines, Dictionary<int, string> map)
        {
            IEnumerable<Machine> machineON = machines.Where(m => m.CurrentState == MachineState.D);
            if (machineON.Count() == 0)
            {
                $"NO MACHINE ON".WriteLeft(ConsoleColor.Red);
                DicplayMenuPrincipale(machines, map);
                return;
            }
            foreach (var m in machineON)
                $"machine {m.IdMachine}".WriteLeft(ConsoleColor.White);
            "Choix :".WriteLeft(ConsoleColor.Green);
            string? choix = Uitility.ReadLineLeft(ConsoleColor.Green);
            Machine? machine = machines.FirstOrDefault(m => m.IdMachine == choix);
            if (machine != null)
            {
                
                Structure.MapInputTension[machine.Input] = 0;
            }
            Console.WriteLine();
            DicplayMenuPrincipale(machines, map);
        }



    }
}
