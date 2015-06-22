namespace WebApplication
{
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    class IoC
    {
        public static IContainer Container { get; }

        static IoC()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(thisAssembly);
            builder.RegisterControllers(thisAssembly);
            builder.RegisterAssemblyModules(thisAssembly);
            Container = builder.Build();
        }
    }
}