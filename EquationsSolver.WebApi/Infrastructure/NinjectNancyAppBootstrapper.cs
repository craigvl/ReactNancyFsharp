using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Ninject;

namespace EquationsSolver.WebApi.Infrastructure
{
    public class NinjectNancyAppBootstrapper : NinjectNancyBootstrapper
    {
        private readonly IKernel _container;

        public NinjectNancyAppBootstrapper(IKernel existingContainer)
        {
            _container = existingContainer;
        }

        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            pipelines.EnableCors();
        }

        protected override IKernel GetApplicationContainer()
        {
            _container.Load<FactoryModule>();

            return _container;
        }
    }
}