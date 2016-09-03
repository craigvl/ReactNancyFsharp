using EquationsSolver.WebApi.Services;
using Ninject.Modules;

namespace EquationsSolver.WebApi.Config
{
    public class NinjectModuleConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IEquationService>().ToConstant<EquationService>(new EquationService()); // singleton
        }
    }
}

