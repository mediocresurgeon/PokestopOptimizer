namespace Pokestop.Core

open Microsoft.FSharp.Collections
open Pokestop.Core.GreatCircle
open System

module SimulatedAnnealing =

    let Reorder<'T when 'T : equality> (list:List<'T>) (index1:int) (index2:int) =
        let item1 = list.Item index1
        let item2 = list.Item index2

        let transform arg =
            if arg = item1 then item2
            elif arg = item2 then item1
            else arg
        
        list |> List.map transform

    let GetRouteDistance<[<Measure>] 'u> (planetRadius:float<'u>) (places:List<Coordinate>) = 
        if places.IsEmpty then None
        else 
            let getDistance = GreatCircleDistance planetRadius

            let rec calcDistance (remainingPlaces:List<Coordinate>) (runningTotal:float<'u>) = 
                let tail = remainingPlaces.Tail
                // case 1: there is only one coordinate left. find the distance between this coordinate and the first coordinate.
                if tail.IsEmpty then
                    let distance = getDistance places.Head remainingPlaces.Head
                    distance + runningTotal
                // case 2: there are at least two coordinates (head + tail.head). Calculate their distance and recurse.
                else
                    let distance = getDistance remainingPlaces.Head tail.Head
                    calcDistance tail (runningTotal + distance)

            let distance = calcDistance places 0.0<_>
            Some (distance)
     
    let GetAcceptanceProbability<[<Measure>] 'u> (previousDistance:float<'u>) (newDistance:float<'u>) (temperature:float) =
        if (newDistance < previousDistance) then
            1.0
        else
            let diff = float (previousDistance - newDistance)
            Math.Exp(diff/ temperature)
     
    let rec GetTwoDifferentNonNegativeIntegers (random:Random) (maxExclusive:int) =
        // This will loop endlessly if maxExclusive is 1
        // What will happen if maxExclusive is zero? Negative?
        let num1 = random.Next(maxExclusive)
        let num2 = random.Next(maxExclusive)
        if num1 = num2 then
            GetTwoDifferentNonNegativeIntegers random maxExclusive
        else
            (num1, num2)

    let OptimizeOrderByDistance (places:List<Coordinate>) (random:Random) =
        if places.Length < 4 then // lists of 3 or smaller always yield the same distance
            places
        else
            let getEarthDistance = GetRouteDistance 6371.0<kilometer>
            let indexCount = places.Length

            let mutable bestSolution = places
            let mutable computationBudget = 10000.0
            while computationBudget > 1.0 do
                let index1, index2 = GetTwoDifferentNonNegativeIntegers random indexCount

                let newSolution = Reorder bestSolution index1 index2

                let bestSolutionDistance = getEarthDistance bestSolution
                let newSolutionDistance = getEarthDistance newSolution
                let acceptanceProbability = GetAcceptanceProbability bestSolutionDistance.Value newSolutionDistance.Value computationBudget
                if acceptanceProbability > random.NextDouble() then
                    bestSolution <- newSolution
                computationBudget <- computationBudget * (1.0 - 0.0003)
            bestSolution