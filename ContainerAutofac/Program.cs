using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ContainerAutofac
{
    public class Car
    {
        private readonly IEngine _engine;
        public string Name { get; set; }

        public Car(IEngine engine)
        {
            _engine = engine;
        }

        public void GetDescription()
        {
            Console.WriteLine("Engine size: {0}L", _engine.GetSize());
        }

        public void GetName()
        {
            if (Name == null) return;
            Console.WriteLine("The car name: {0}", Name);
        }
    }

    public interface IEngine
    {
        double GetSize();
    }

    public class Engine : IEngine
    {
        public double GetSize()
        {
            return 2.5; // in liters
        }
    }

    public class Engine1 : IEngine
    {
        public double GetSize()
        {
            return 10; // in liters
        }
    }


    internal class Program
    {
        private static void Main()
        {
            var builder = new ContainerBuilder();

            // variant 1: 
            //builder.RegisterType<Engine>();
            //builder.Register<IEngine>(x => x.Resolve<Engine>());

            // or variant 2:
            builder.RegisterType<Engine1>().As<IEngine>();

            builder.RegisterType<Car>();

            var container = builder.Build();

            var car = container.Resolve<Car>();
            car.GetDescription();
        }
    }
}