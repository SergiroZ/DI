using System;
using System.Collections.Generic;

/// <summary>
/// https://medium.com/webbdev/asp-509ac1eb793
/// </summary>

namespace CoreSimple
{
    public interface ILayer
    {
        void Write(string text);
    }

    public class ConsoleLayer : ILayer
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class DebugLayer : ILayer
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class Logging_undo
    {
        private readonly ILayer _instance;

        public Logging_undo(int i) =>
            _instance = i == 1 ? (ILayer) new ConsoleLayer() : new DebugLayer();

        public void Write(string text)
        {
            _instance.Write(text);
        }
    }

    public class Logging
    {
        private readonly ILayer _instance;

        public Logging(ILayer instance) => _instance = instance;

        public void Write(string text)
        {
            _instance.Write(text);
        }
    }

    public static class IoCContainer
    {
        private static readonly Dictionary<Type, Type> _registeredObjects =
            new Dictionary<Type, Type>();

        public static dynamic Resolve<TKey>()
        {
            return Activator.CreateInstance(_registeredObjects[typeof(TKey)]);
        }

        public static void Register<TKey, TConcrete>() where TConcrete : TKey
        {
            _registeredObjects[typeof(TKey)] = typeof(TConcrete);
        }
    }


    internal class Program
    {
        private static void Main()
        {
            var log_0 = new Logging_undo(2);
            log_0.Write("Hello !!");

            var log = new Logging(new DebugLayer()); //Ioc and DIP
            log.Write("Hello DI !!!");

            //*************** IoC container **********
            IoCContainer.Register<ILayer, ConsoleLayer>();

            ILayer layer = IoCContainer.Resolve<ILayer>();
            layer.Write("Hello from IoC!!!");
        }
    }
}