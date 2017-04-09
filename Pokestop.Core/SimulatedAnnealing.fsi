﻿namespace Pokestop.Core

open System
open Pokestop.Core.GreatCircle

    module SimulatedAnnealing =

        val Reorder<'T when 'T : equality> : list : List<'T> -> index1 : int -> index2 : int -> List<'T>

        val GetRouteDistance<[<Measure>] 'u> : planetRadius:float<'u> -> places:List<Coordinate> -> float<'u> option

        val GetAcceptanceProbability<[<Measure>] 'u> : previousDistance : float<'u> -> newDistance : float<'u> -> temperature : float -> float

        val GetTwoDifferentNonNegativeIntegers : random : Random -> maxExclusive : int -> int * int

        val OptimizeOrderByDistance : places : List<Coordinate> -> random : Random -> List<Coordinate>