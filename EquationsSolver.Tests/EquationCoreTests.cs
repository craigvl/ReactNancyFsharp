using System;
using EquationsCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EquationsSolver.Tests
{
    [TestClass]
    public class EquationCoreTests
    {

        [TestMethod]
        public void ParseExpression_ValidExpression_WithConstants_ShouldPass()
        {
            var exp = "1+1*2";
            Equations.parseExpr(exp);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ParseExpression_SimpleValidExpression_WithConstants_ShouldPass()
        {
            var exp = "2 * x - 6";
            Equations.parseExpr(exp);
            Assert.IsTrue(true);
        }


        
        


        [TestMethod]
        // Even if not supported by the resolver (yet), parsing should still work
        public void ParseExpression_ComplexValidExpression_WithVariables_ShouldPass()
        {
            var exp = "1+1*2+x-z";
            Equations.parseExpr(exp);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseExpression_InvalidExpression_ShouldThrowArgumentException()
        {
            var equation = "1d3*4";
            Equations.parseExpr(equation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseExpression_InvalidEquation_WithMoreThanOneEqualSign_ShouldThrowException()
        {
            var equation = "1+3*(x/(2 + 1))=1 * ((x+1+3) * 4) / 2 = 3";
            Equations.parse(equation);
        }


        [TestMethod]
        public void ParseEquation_SimpleEquation_ShouldReturnTwoComponents()
        {
            var equation = "1*x+1=3";
            var actual = Equations.parse(equation);           
            Assert.IsTrue(!actual.Item1.IsUndefined && !actual.Item2.IsUndefined);
        }


        [TestMethod]
        public void ParseEquation_SimpleEquation_WithTwoXVariables_ShouldReturnTwoComponents()
        {
            var equation = "1*x+1=3*x";
            var actual = Equations.parse(equation);
            Assert.IsTrue(!actual.Item1.IsUndefined && !actual.Item2.IsUndefined);
        }

        [TestMethod]
        public void ParseEquation_SimpleEquation_WithComplexContent_ShouldReturnTwoComponents()
        {
            var equation = "1+3*(x/(2 + 1))=1 * ((x+1+3) * 4) / 2";
            var actual = Equations.parse(equation);
            Assert.IsTrue(!actual.Item1.IsUndefined && !actual.Item2.IsUndefined);
        }

        [TestMethod]
        // Even if not supported by the resolver (yet), parsing should still work
        public void ParseEquation_SimpleEquation_WithXAndYVariable_ShouldReturnTwoComponents()
        {
            var equation = "y+1*x=1-3*x";
            var actual = Equations.parse(equation);
            Assert.IsTrue(!actual.Item1.IsUndefined && !actual.Item2.IsUndefined);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseEquation_InvalidEquation_WithoutEqualSign_ShouldThrowException()
        {
            var equation = "1*x+1-3*x";
            Equations.parse(equation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseEquation_InvalidEquation_WithMoreThanOneEqualSign_ShouldThrowException()
        {
            var equation = "1+3*(x/(2 + 1))=1 * ((x+1+3) * 4) / 2 = 3";
            Equations.parse(equation);
        }

        [TestMethod]
        public void NumericEvaluate_8Div8_ShouldReturn1()
        {
            var exp = Equations.parseExpr("(2*3+2)/2");
            var expected = 4.0d;
            var actual = Equations.evaluateExpr(exp);

            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void ResolveEquation_1Plus1Equals2_ShouldResolveAsManySolutions()
        {
            var exp = "1+1=2";
            var actual = Equations.resolve(exp);
            Assert.IsTrue(actual.Item1.IsManySolutions);
        }

        [TestMethod]
        public void ResolveEquation_1Equals2_ShouldResolveAsNoSolutions()
        {
            var exp = "1=2";
            var actual = Equations.resolve(exp);
            Assert.IsTrue(actual.Item1.IsNoSolution);
        }

        [TestMethod]
        [ExpectedException(typeof(Equations.NotLinearEquationError))]
        public void ResolveEquation_NonLinear_ShouldThrowException()
        {
            Equations.resolve("x*x+x+1 = 3");
        }

        [TestMethod]
        public void ResolveEquation_2EqualsX_ShouldResolveAs2()
        {
            var expected = 2.0d;
            var actual = Equations.resolve("2=x");
            Assert.AreEqual(expected, actual.Item2.Value);
            Assert.IsTrue(actual.Item1.IsUniqueSolution);
        }

        [TestMethod]
        public void ResolveEquation_3XMinus6Equals9_ShouldResolveAs5()
        {
            var expected = 5.0d;
            var actual = Equations.resolve("3*x-6=9");
            Assert.AreEqual(expected, actual.Item2.Value);
            Assert.IsTrue(actual.Item1.IsUniqueSolution);
        }

        [TestMethod]
        public void ResolveEquation_ImpossibleEquation_ShouldResultAsNoSolution()
        {
            var actual = Equations.resolve("11+3*x - 7 = 6*x + 5- 3*x");
            Assert.IsTrue(actual.Item1.IsNoSolution);
        }


        [TestMethod]
        public void ResolveEquation_InfiniteEquation_ShouldResultAsManySolution()
        {
            var actual = Equations.resolve("6*x + 5 - 2*x = 4 + 4*x + 1");
            Assert.IsTrue(actual.Item1.IsManySolutions);
        }
    }
}
