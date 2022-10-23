using System;

using System.Collections.Generic;

using System.Linq;



namespace Village

{

    public class Engine
    {

        private Controller controller;

        private bool isRunning;

        public Engine(Controller controller)
        {

            this.controller = controller;
            this.isRunning = true;
        }



        public void Run()
        {

            while (isRunning)
            {

                string[] splittedInput = Console.ReadLine().Split();

                string command = splittedInput[0];

                List<string> arguments = splittedInput
                    .Skip(1)
                    .ToList();

                string result = "";

                switch (command)
                {

                    case "Village":

                        result = controller.ProceseVillageCommand(arguments);

                        break;

                    case "Settle":

                        result = controller.ProcessSettleCommand(arguments);

                        break;

                    case "Rebel":

                        result = controller.ProcessRebelCommand(arguments);

                        break;

                    case "Day":

                        result = controller.ProcessDayCommand(arguments);

                        break;

                    case "Attack":

                        result = controller.ProcessAttackCommand(arguments);

                        break;

                    case "Info":

                        result = controller.ProcessInfoCommand(arguments);

                        break;

                    case "End":

                        result = controller.ProcessEndCommand();

                        this.isRunning = false;

                        break;

                    default:

                        result = "Invalid command";

                        break;

                }

                Console.WriteLine(result);

            }

        }

    }

}