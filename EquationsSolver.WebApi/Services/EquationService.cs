



using System;
using EquationsCore;

namespace EquationsSolver.WebApi.Services
{
    public class EquationService : IEquationService
    {
        public static EquationResultModel CreateEquationResultModel(
            EquationsCore.Equations.EquationOutcome solutionType, Microsoft.FSharp.Core.FSharpOption<double> result)
        {
            if (solutionType.IsNoSolution)
            {
                return EquationResultModel.CreateFromNoSolution();
            }
            else if (solutionType.IsManySolutions)
            {
                return EquationResultModel.CreateFromManySolution();
            }
            else
            {
                return EquationResultModel.CreateFromUniqueSolution(result.Value);
            }
        }

        public EquationResultModel GetEquationResult(string equation)
        {
            try
            {
                // call to our F# library
                var res = EquationsCore.Equations.resolve(equation);
                return CreateEquationResultModel(res.Item1, res.Item2);
            }
            catch (Equations.NotLinearEquationError)
            {
                throw new ArgumentException($"Equation '{equation}' is not linear, resolving non-linear equations is not supported.", equation);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Equation '{equation}' cannot be parsed.", equation);
            }

        }
    }
}