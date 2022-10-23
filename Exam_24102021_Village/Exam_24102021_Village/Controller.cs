
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Village
{
    public class Controller
    {
        private List<Village> villages;
        private List<Rebel> rebels;
        private int totalAttacksPerformed;
        public Controller()
        {
            this.villages = new List<Village>();
            this.rebels = new List<Rebel>();
        }

        public string ProceseVillageCommand(List<string> args)
        {
            Village village = new Village(args[0], args[1]);
            this.villages.Add(village);

            return $"Created Village {village.Name}!";
        }



        public string ProcessSettleCommand(List<string> args)
        {
            Peasant peasant = new Peasant(args[0], int.Parse(args[1]), int.Parse(args[2]));
            Village village = this.GetVillageByName(args[3]);
            if (village != null)
            {
                village.AddPeasant(peasant);
            }

            return $"Settled Peasant {peasant.Name} in Village {village.Name}!";
        }



        public string ProcessRebelCommand(List<string> args)
        {
            Rebel rebel = new Rebel(args[0], int.Parse(args[1]), int.Parse(args[2]));
            this.rebels.Add(rebel);

            return $"Rebel {rebel.Name} joined the gang!";
        }



        public string ProcessDayCommand(List<string> args)
        {
            Village village = this.GetVillageByName(args[0]);
            int dailyResources = village.PassDay();

            return $"Village {village.Name} resource increased with {dailyResources}!";
        }



        public string ProcessAttackCommand(List<string> args)
        {
            if (this.rebels.Count > 0)
            {
                List<Rebel> rebs = this.rebels.Take(int.Parse(args[0])).ToList();
                int rebelsHarm = rebs.Sum(r => r.Harm);

                Village village = this.GetVillageByName(args[1]);
                village.Resource -= rebelsHarm;
                List<Peasant> killedPeasents = village.KillPeasants(rebs.Count / 2);
                totalAttacksPerformed++;

                return $"Village {village.Name} lost {rebelsHarm} resources and {killedPeasents.Count} peasants!";
            }

            return "No rebels to perform raid...";
        }

        public string ProcessInfoCommand(List<string> args)
        {
            StringBuilder sb = new StringBuilder();

            switch (args[0])
            {
                case "Rebel":
                    if (this.rebels.Count <= 0) return "No Rebels";
                    sb.Append("Rebels:");
                    sb.AppendLine(" ");
                    foreach (var rebel in this.rebels)
                    {
                        sb.AppendLine(rebel.ToString());
                    }
                    break;
                case "Village":
                    if (this.villages.Count <= 0) return "No Villages";
                    sb.Append("Villages: ");
                    foreach (var village in this.villages)
                    {
                        sb.AppendLine();
                        sb.Append(village.ToString());
                    }
                    break;
            }

            return sb.ToString().Trim();
        }



        public string ProcessEndCommand()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Villages: {this.villages.Count}");
            sb.AppendLine($"Rebels: {this.rebels.Count}");
            sb.Append($"Attacks on Villages: {totalAttacksPerformed}");

            return sb.ToString();
        }

        private Village GetVillageByName(string name)
        {
            return this.villages.Where(v => v.Name.Equals(name)).FirstOrDefault();
        }
    }

}