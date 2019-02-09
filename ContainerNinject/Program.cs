using System;
using Ninject;
using Ninject.Modules;

namespace ContainerNinject
{
    #region without DI

    //public class Engine
    //{
    //    public double GetSize()
    //    {
    //        return 2.5; // in liters
    //    }
    //}

    //public class Car
    //{
    //    private readonly Engine _engine;

    //    public Car()
    //    {
    //        _engine = new Engine();
    //    }

    //    public void GetDescription()
    //    {
    //        Console.WriteLine("Engine size: {0}", _engine.GetSize());
    //    }
    //}

    #endregion without DI

    #region refactoring according to DI

    public class Car
    {
        private readonly IEngine _engine;

        public Car(IEngine engine)
        {
            _engine = engine;
        }

        public void GetDescription()
        {
            Console.WriteLine("Engine size: {0}L", _engine.GetSize());
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

    internal class Engine2 : IEngine
    {
        public double GetSize()
        {
            return 1000; // in liters
        }
    }

    #endregion refactoring according to DI

    #region how use Ninject (!! download:  PM> Install-Package Ninject)

    public class MyConfigModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IEngine>().To<Engine>();
            //Bind<IEngine>().To<Engine1>();
            Bind<IEngine>().To<Engine2>();

            Bind<Car>().ToSelf().InSingletonScope();
        }
    }

    #endregion

    internal class Program
    {
        private static void Main()
        {
            IEngine engine = new Engine(); //refactoring according to DI
            var car_0 = new Car(engine);
            car_0.GetDescription();


            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel(new MyConfigModule());

            // Using Car
            var car = ninjectKernel.Get<Car>();
            car.GetDescription();

            Console.WriteLine();
        }
    }
}