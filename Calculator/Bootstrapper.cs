using Calculator.ViewModels;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Calculator
{
    public class Bootstrapper : BootstrapperBase
    {
        private IWindsorContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {         
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override void Configure()
        {

            Serilog.ILogger log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(path: @"D:\Prog\Calculator\Calculator\Calculator\Logs\", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .CreateLogger();
            _container = new WindsorContainer();
            _container.Register(Component.For<Serilog.ILogger>().Instance(log));
            _container.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton());
            _container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton());
            _container.Register(Component.For<ShellViewModel>().ImplementedBy<ShellViewModel>());
        
        }
        protected override object GetInstance(Type service, string key)
        {

            var instance = _container.Resolve(service);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var array = _container.ResolveAll(service);
            object[] trgArray = new object[array.Length];
            Array.Copy(array, trgArray, array.Length);
            return trgArray;
        }
    }
}
