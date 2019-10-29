using System;
using System.Collections.Generic;

namespace Homework2 {
    public class Car {
        public string Name { get; set; }
        public List<ICarPart> Parts { get; set; }
    }

    public abstract class CarFactory {
        public abstract IWheel CreateWheel();
        public abstract ICarcass CreateCarcass();
        public abstract IEngine CreateEngine();

        public abstract string CarName { get; }

        public Car CreateCar() {
            return new Car() {
                Name = CarName,
                Parts = new List<ICarPart>() {
                    CreateCarcass(),
                    CreateEngine(),
                    CreateWheel(),
                    CreateWheel(),
                    CreateWheel(),
                    CreateWheel()
                }
            };
        }
    }

    public interface ICarPart {
        string Name { get; }
    }

    public interface IWheel : ICarPart {}

    public interface ICarcass : ICarPart {}

    public interface IEngine : ICarPart {}


    public class BMWWheel : IWheel {
        public string Name => "BMW Wheel";
    }

    public class BMWCarcass : ICarcass {
        public string Name => "BMW Carcass";
    }

    public class BMWEngine : IEngine {
        public string Name => "BMW Engine";
    }

    public class BMWFactory : CarFactory {
        public override string CarName => "BMW";

        public override ICarcass CreateCarcass() {
            return new BMWCarcass();
        }

        public override IEngine CreateEngine() {
            return new BMWEngine();
        }

        public override IWheel CreateWheel() {
            return new BMWWheel();
        }
    }


    public class AudiWheel : IWheel {
        public string Name => "Audi Wheel";
    }

    public class AudiCarcass : ICarcass {
        public string Name => "Audi Carcass";
    }

    public class AudiEngine : IEngine {
        public string Name => "Audi Engine";
    }

    public class AudiFactory : CarFactory {
        public override string CarName => "Audi";

        public override ICarcass CreateCarcass() {
            return new AudiCarcass();
        }

        public override IEngine CreateEngine() {
            return new AudiEngine();
        }

        public override IWheel CreateWheel() {
            return new AudiWheel();
        }
    }


    class Program {
        private static void PrintCar(Car car) {
            Console.WriteLine($"{car.Name} consists of:");

            foreach (var part in car.Parts) {
                Console.WriteLine(part.Name);
            }

            Console.WriteLine("---------------");
        }

        private static void Main(string[] args) {
            var bmwFactory = new BMWFactory();
            var audiFactory = new AudiFactory();

            var bmw = bmwFactory.CreateCar();
            var audi = audiFactory.CreateCar();

            PrintCar(bmw);
            PrintCar(audi);

            Console.ReadKey();
        }
    }
}
