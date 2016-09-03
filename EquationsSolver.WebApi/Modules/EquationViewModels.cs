using System;
using EquationsSolver.WebApi.Services;

namespace EquationsSolver.WebApi.Modules
{
    /// <summary>
    /// Define an equation, ready to be resolved
    /// </summary>
    public class EquationViewModel
    {
        public EquationViewModel()
        {
            
        }
        public EquationViewModel(string equation, string variableName)
        {
            VariableName = variableName;
            Equation = equation;
        }
        public string Equation { get; set; }
        public string VariableName { get; set; }
    }

    /// <summary>
    /// Represent a resolved equation
    /// </summary>
    public class EquationWithResultViewModel : EquationViewModel
    {
        protected EquationWithResultViewModel(EquationViewModel equation)
            : base(equation.VariableName, equation.Equation)
        {

        }

        public double? Result { get; set; }
        public string Outcome { get; set; }
        public string Error { get; set; }

        public static EquationWithResultViewModel CreateFromError(EquationViewModel equation, Exception ex)
        {
            return new EquationWithResultViewModel(equation)
            {
                Error = ex.Message
            };
        }

        public static EquationWithResultViewModel CreateFromResult(EquationViewModel equation, EquationResultModel result)
        {
            return new EquationWithResultViewModel(equation)
            {
                Outcome = result.Outcome.ToString().ToLowerInvariant(),
                Result = result.Result
            };
        }
    }
}
