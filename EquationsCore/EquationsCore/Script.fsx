// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.
#load @"..\..\packages\MathNet.Numerics.FSharp.3.8.0\MathNet.Numerics.fsx"
#load @"..\..\packages\MathNet.Symbolics.0.9.0\MathNet.Symbolics.fsx"

#load "Equations.fs"
open EquationsCore

resolve("3*(1+x) = 2*x-1")

// Define your library scripting code here

