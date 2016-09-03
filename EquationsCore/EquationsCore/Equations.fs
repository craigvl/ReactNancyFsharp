namespace EquationsCore
open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics
open Operators
open MathNet.Numerics
open MathNet.Symbolics
open MathNet.Symbolics.Rational
open Microsoft.FSharp.Collections


    module Equations =


        let resolve (equation : string) : FloatingPoint = 
            let x = symbol "x"
            
           // let tokens = equation.Split('=')
            let l =  Array.ofSeq(equation.Split('='))
            let simplified = match l with | [|left;right|] -> (Rational.simplify x (Infix.parseOrUndefined(left)),Rational.simplify x (Infix.parseOrUndefined(right)))
           // | _ -> null
           
           //  Infix.parseOrUndefined("3*(1+x)")

        //    let left = Rational.simplify(x, Infix.parseOrUndefined("3*(1+x)")) //3x + 3
           // let right = Rational.simplify(x, Infix.parseOrUndefined("2*x-1"))  //2x - 1

            // polynomial form
            let coefficients = Polynomial.coefficients x (match simplified with | (a,b) -> a - b)
            //Rational.reduce
            let c = Array.get coefficients 0   // c
            let a = Array.get coefficients 1  // b
//            Evaluate.evaluate c
//            MathNet.Symbolics.Evaluate.evaluate(c / a)
            
            let symbols = new System.Collections.Generic.Dictionary<string, FloatingPoint>() //Map.ofSeq [ "a",  2.0; "b",  3.0 ]
            MathNet.Symbolics.Evaluate.evaluate symbols (c / a)

        resolve("3*(1+x) = 2*x-1")


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

