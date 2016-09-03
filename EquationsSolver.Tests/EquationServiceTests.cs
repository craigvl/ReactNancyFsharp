using System;
using EquationsCore;
using EquationsSolver.WebApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationsSolver.Tests
{
    [TestClass]
    public class EquationServiceTests
    {
        // Most of the equation parsing, reducing and solving has been tested on the core already
        // this just test that the service forward correectly the result from Equation.Core
        private IEquationService _equationService;

        [TestInitialize]
        public void Init()
        {
            _equationService = new EquationService();
        }

        [TestMethod]
        public void Resolve_SimpleEquation_WithXOnOneSide()
        {
            var equation = "1*x+1 = 3";
            var expected = 2D;
            var actual = _equationService.GetEquationResult(equation);

            Assert.AreEqual(expected, actual.Result.Value);
            Assert.IsTrue(actual.Outcome == EquationResultModel.OutcomeTypes.OneSolution);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Resolve_SimpleEquation_WithMalformattedInput_ShouldThrowArgumentException()
        {
            var equation = "xf 2= 4x";
            Equations.resolve(equation);
        }
    }
}