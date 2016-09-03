namespace EquationsSolver.WebApi.Services
{
    public interface IEquationService
    {
        EquationResultModel GetEquationResult(string equation);
    }
}