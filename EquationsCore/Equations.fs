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
            
            // get coefficient for x*0 (c) and x*1 (a) -> (ax + b) = 0        
            match coefficients with 
                | [|b|] -> resolvePolynomial(0.0, evaluateExpr(b)) // 0 + b = 0
                | [|b;a|] -> resolvePolynomial(evaluateExpr(a), evaluateExpr(b)) // ax + b = 0
                | _ -> raise(NotLinearEquationError) // x2 + ax + b = 0 (not linear)



        let resolve(rawEquation: string) = reduce(parse(rawEquation), "x")
