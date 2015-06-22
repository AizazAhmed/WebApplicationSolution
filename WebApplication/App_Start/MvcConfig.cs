namespace WebApplication
{
    using System.Web.Mvc;
    using Autofac.Integration.Mvc;

    public static class MvcConfig
    {
        public static void RegisterDependencyResolver()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
        }
    }
}