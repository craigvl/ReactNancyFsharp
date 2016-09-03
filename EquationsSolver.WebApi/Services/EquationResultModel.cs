

namespace EquationsSolver.WebApi.Services
{
    public class EquationResultModel
    {
        public enum OutcomeTypes
        {
            OneSolution,
            InfiniteSolutions,
            NoSolution
        }

        public static EquationResultModel CreateFromUniqueSolution(double result)
        {
            return new EquationResultModel()
            {
                Outcome = OutcomeTypes.OneSolution,
                Result = result
            };
        }

        public static EquationResultModel CreateFromNoSolution()
        {
            return new EquationResultModel()
            {
                Outcome = OutcomeTypes.NoSolution,
                Result = null
            };
        }

        public static EquationResultModel CreateFromManySolution()
        {
            return new EquationResultModel()
            {
                Outcome = OutcomeTypes.InfiniteSolutions,
                Result = null
            };
        }
        public double? Result { get; private set; }
        public OutcomeTypes Outcome { get; private set; }
    }
}
