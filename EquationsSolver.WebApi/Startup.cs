using EquationsSolver.WebApi.Config;
using EquationsSolver.WebApi.Infrastructure;
using Nancy.Owin;
using Ninject;
using Owin;

namespace EquationsSolver.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IKernel container = new StandardKernel(new NinjectModuleConfig());


            var nancyConfig = new NancyOptions {Bootstrapper = new NinjectNancyAppBootstrapper(container)};


            //app.UseNancy();
            app.UseNancy(nancyConfig);
        }
    }
}