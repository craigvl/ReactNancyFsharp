namespace EquationsCore
open System.Numerics
open MathNet.Symbolics
open MathNet.Numerics
open Operators
open MathNet.Symbolics.Rational
open FParsec;
open Microsoft.FSharp.Collections

    module Equations =
                    
        // possible outcomes of an equation
        type EquationOutcome = 
           | NoSolution 
           | UniqueSolution 
           | ManySolutions 

        exception NotLinearEquationError

        // evaluate a numerical expression
        let evaluateExpr(numExpr : Expression) : float = 
            let symbols = new System.Collections.Generic.Dictionary<string, FloatingPoint>() 
            let output = MathNet.Symbolics.Evaluate.evaluate symbols numExpr
            output.RealValue
       
        // parse an expression
        let parseExpr(raw : string) : Expression = 
            match MathNet.Symbolics.Infix.parseOrUndefined raw with
                | Undefined -> invalidArg "expression" (sprintf "Expression passed cannot be parsed '%s'." raw)
                | exp -> exp
            
        let parse(equation : string) : Expression * Expression = 
             let tokens =  equation.Split('=')                                                  
             match tokens with 
                | [|left;right|] -> parseExpr left,parseExpr right
                | _ -> invalidArg "equation" (sprintf "Equation passed can only have 2 components '%s'." equation)


        // resolve a linear  equation on a polynomial form of (ax + b) = 0
        // -> 1 solution found          : (OneSolution, valueForX)
        // -> No solution found         : (NoSolution, null)
        // -> Infinite solution found   : (ManySolution, null)
        let resolvePolynomial(a : float,b : float) : EquationOutcome * Option<float> = 
           if a.AlmostEqual(0.0) then 
                let outcome = (if b.AlmostEqual(0.0) then EquationOutcome.ManySolutions else EquationOutcome.NoSolution)
                (outcome, Option.None)
            else 
                 (EquationOutcome.UniqueSolution, Option.Some((b/a)*(-1.0))) 
        

        let reduce (equation : Expression * Expression, var : string)  : EquationOutcome * Option<float> = 
            let x = symbol var // unknown (aka X)
            
            // reduce the two side of the equations
            let reducedEquation = match equation with | (left,right) -> (Rational.simplify x (left), Rational.simplify x (right))        

            // reduce polynomial form ax + c = 0
            let coefficients = Polynomial.coefficients x (match reducedEquation with | (a,b) -> a - b)
            
            // ax + b = 0
            // get coefficient for x*0 (c) and x*1 (a) -> (ax + b) = 0
        
            match coefficients with 
                | [|b|] -> resolvePolynomial(0.0, evaluateExpr(b)) // 0 + b = 0
                | [|b;a|] -> resolvePolynomial(evaluateExpr(a), evaluateExpr(b)) // ax + b = 0
                | _ -> raise(NotLinearEquationError) // x2 + ax + b = 0 (not linear)

//
//            coefficients match 
//            if(coefficients.Length == 1){
//            
//            }
//
//            if(coefficients.Length >= 2) then raise(NotLinearEquationError)
//
//           
//
//            let b = Array.get coefficients (0)   // c
//            let a = Array.get coefficients (1)  // b
//
//            let aValue = evaluateExpr(a)
//            let bValue = evaluateExpr(b)
//
//         //   if(aValue == 0.0) then failwith "Divisor cannot be zero."
//
//
//         //   let outcome = EquationOutcome.UniqueSolution;
//        //    let result = 0;
//
//
//
//
//            if aValue.AlmostEqual(0.0) then 
//                let outcome = (if bValue.AlmostEqual(0.0) then EquationOutcome.ManySolutions else EquationOutcome.NoSolution)
//              //  let result =  Single.NaN;
//                (outcome, Option.None)
//              //   (outcome, 0.0)
//            else 
//                 (EquationOutcome.UniqueSolution, Option.Some((bValue/aValue)*(-1.0)))

//
//            If a = 0, then, when b = 0 every number is a solution of the equation, but if b ≠ 0 there are no solutions (and the equation is said to be inconsistent.)
//
//
//
//            let result = bValue/aValue
            
//
//            let symbols = new System.Collections.Generic.Dictionary<string, FloatingPoint>() //Map.ofSeq [ "a",  2.0; "b",  3.0 ]
//            let output = MathNet.Symbolics.Evaluate.evaluate symbols (c / a)
//            output.RealValue

        let resolve(rawEquation: string) = reduce(parse(rawEquation), "x")


//
//
//        val x = Symbolics.Expression.Symbol("x");
//            //  var y = Symbol.NewSymbol("x");
//
//            var left = Rational.Simplify(x, Infix.ParseOrUndefined("3*(1+x)")); //3x + 3
//            var right = Rational.Simplify(x, Infix.ParseOrUndefined("2*x-1"));  //2x - 1
//
//            var res = Rational.Simplify(x, (left - right)); // = 0
//            var coef = Polynomial.Coefficients(x, (left - right));
//            //var eq = Evaluate.Evaluate(new Dictionary<string, FloatingPoint>() {{"x", 0}}, res);
//
//            // ax + c = 0 -> x = c/a
//
//            var c = coef.First(); // c
//            var a = coef.Last();  // b
//
//            var xValue = c / a;
//
//}

//
//type Equation(left, right) = 
//   
//    resolve 
//
//    val x = MathNet.Symbolics.Expression.Symbol("x");
//            //  var y = Symbol.NewSymbol("x");
//
//            var left = Rational.Simplify(x, Infix.ParseOrUndefined("3*(1+x)")); //3x + 3
//            var right = Rational.Simplify(x, Infix.ParseOrUndefined("2*x-1"));  //2x - 1
//
//            var res = Rational.Simplify(x, (left - right)); // = 0
//            var coef = Polynomial.Coefficients(x, (left - right));
//            //var eq = Evaluate.Evaluate(new Dictionary<string, FloatingPoint>() {{"x", 0}}, res);
//
//            // ax + c = 0 -> x = c/a
//
//            var c = coef.First(); // c
//            var a = coef.Last();  // b
//
//            var xValue = c / a;


//
//type Equation(left, right) = 
//   
//    resolve 
//
//    val x = MathNet.Symbolics.Expression.Symbol("x");
//            //  var y = Symbol.NewSymbol("x");
//
//            var left = Rational.Simplify(x, Infix.ParseOrUndefined("3*(1+x)")); //3x + 3
//            var right = Rational.Simplify(x, Infix.ParseOrUndefined("2*x-1"));  //2x - 1
//
//            var res = Rational.Simplify(x, (left - right)); // = 0
//            var coef = Polynomial.Coefficients(x, (left - right));
//            //var eq = Evaluate.Evaluate(new Dictionary<string, FloatingPoint>() {{"x", 0}}, res);
//
//            // ax + c = 0 -> x = c/a
//
//            var c = coef.First(); // c
//            var a = coef.Last();  // b
//
//            var xValue = c / a;

