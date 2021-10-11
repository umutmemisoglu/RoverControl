using Microsoft.Extensions.DependencyInjection;
using RoverControl.Model;
using RoverControl.Model.Enums;
using RoverControl.Service.Interfaces;
using RoverControl.Service.Methods;
using RoverControl.Validation.Interfaces;
using RoverControl.Validation.Methods;
using RoverControl.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoverControl.Worker
{
    class Program
    {
        #region MAIN
        static void Main(string[] args)
        {
            try
            {
                #region Initialize Services
                RegisterServices();
                #endregion

                #region Input Parameters Validations
                var validationService = serviceProvider.GetService<Func<ValidationTypes, IValidation>>();

                IValidation plateauValidation = validationService(ValidationTypes.Plateau);
                string plateau = Console.ReadLine();
                plateauValidation.Validate(plateau);

                IValidation locationValidation = validationService(ValidationTypes.Location);
                string location = Console.ReadLine().ToUpper();
                locationValidation.Validate(location);

                IValidation instructionValidation = validationService(ValidationTypes.Instruction);
                string instruction = Console.ReadLine().ToUpper();
                instructionValidation.Validate(instruction);
                #endregion

                #region Parameters
                string[] roverCurrenlocation = location.Split(" ");
                string[] platueLocation = plateau.Split(" ");

                Coordinate plateauCoordinate = new Coordinate(Convert.ToInt32(platueLocation[0]), Convert.ToInt32(platueLocation[1]));
                Coordinate roverCurrentCoordinate = new Coordinate(Convert.ToInt32(roverCurrenlocation[0]), Convert.ToInt32(roverCurrenlocation[1]));
                Directions roverCurrentDirection = Enum.Parse<Directions>(roverCurrenlocation[2]);
                List<Instruction> roverInstruction = instruction.ToCharArray().Select(i => (Instruction)Enum.Parse(typeof(Instruction), i.ToString())).ToList();
                #endregion

                #region Rover Movement 
                IRoverMoveService roverMoveService = serviceProvider.GetService<IRoverMoveService>();
                Rover rover = roverMoveService.Move(new Rover(roverCurrentCoordinate, roverCurrentDirection), roverInstruction, plateauCoordinate);

                Console.WriteLine(rover.GetLocation());

                Console.ReadKey();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }
        #endregion

        #region SERVICE REGISTERITION
        static ServiceProvider serviceProvider { get; set; }
        static ServiceCollection services { get; set; }
        static void RegisterServices()
        {
            services = new ServiceCollection();

            services.AddSingleton<IRoverMoveService, RoverMoveService>();

            services.AddSingleton<Func<ValidationTypes, IValidation>>(
             provider => validationType =>
             {
                 switch (validationType)
                 {
                     case ValidationTypes.Instruction:
                         return new InstructionValidation();
                     case ValidationTypes.Location:
                         return new LocationValidation();
                     case ValidationTypes.Plateau:
                         return new PlateauValidation();
                     default:
                         throw new ArgumentException("Validation type is not found");
                 }
             });

            serviceProvider = services.BuildServiceProvider();
        }
        #endregion
    }
}
