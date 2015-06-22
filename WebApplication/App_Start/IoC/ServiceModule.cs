namespace WebApplication
{
    using Autofac;

    class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // TODO register any services and context here.
        }
    }
}