using System;
using EquationsSolver.WebApi.Services;
using Nancy;
using Nancy.ModelBinding;

namespace EquationsSolver.WebApi.Modules
{
    public class EquationModule : NancyModule
    {
        public EquationModule(IEquationService service)
        {
            Post["/resolve"] = _ =>
            {
                // deserialize the request body data 
                var inputVm = this.Bind<EquationViewModel>();

                try
                {
                    // get results from the service
                    return
                        Response.AsJson(EquationWithResultViewModel.CreateFromResult(inputVm,
                            service.GetEquationResult(inputVm.Equation)));
                }
                catch (ArgumentException ex)
                {
                    // parsing error or invalid equation -> return a proper HTTP Code and error details (400)
                    return Response.AsJson(EquationWithResultViewModel.CreateFromError(EquationWithResultViewModel.CreateFromError(inputVm, ex), ex),
                        HttpStatusCode.BadRequest);
                }

                // other issues will return a 500 (server crash), this is unandled as it refers to a bug or unexpected behavior 
            };
        }
    }
}