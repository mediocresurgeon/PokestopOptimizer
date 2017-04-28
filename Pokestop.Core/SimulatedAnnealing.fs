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

    let Shuffle<'T when 'T : equality> (list:List<'T>) (random:Random) =
        // Using Durstenfeld's version of Fisher–Yates shuffle
        // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
        let elements = List.toArray list
        let highestRandomIndex = elements.Length - 1
        for i in 0..highestRandomIndex do
            let highIndexToSwap = highestRandomIndex - i
            let lowIndexToSwap = random.Next highestRandomIndex
            let lowIndexItem = Array.get elements lowIndexToSwap
            Array.set elements lowIndexToSwap (Array.get elements highIndexToSwap)
            Array.set elements highIndexToSwap lowIndexItem
        elements |> Array.toList

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
     
    let GetAcceptanceProbability (currentEnergy:float) (newEnergy:float) (temperature:float) =
        if (currentEnergy > newEnergy) then
            1.0
        else
            Math.Exp((currentEnergy - newEnergy) / temperature)
     
    let rec GetTwoDifferentNonNegativeIntegers (random:Random) (maxExclusive:int) =
        // This will loop endlessly if maxExclusive is 1
        // What will happen if maxExclusive is zero? Negative?
        let num1 = random.Next(maxExclusive)
        let num2 = (num1 + 1) % maxExclusive
        (num1, num2)

    let OptimizeOrderByDistance (places:List<Coordinate>) (startingTemperature:float) (finalTemperature:float) (coolingRate:float) (random:Random) =
        if places.Length < 4 then // lists of 3 or smaller always yield the same distance
            places
        else
            let getEarthDistance = GetRouteDistance 6371.0<kilometer>
            let listLength = places.Length

            let randomizedPlaces = Shuffle places

            let mutable bestSolution = places
            let mutable computationBudget = startingTemperature
            let budgetMultiplier = 1.0 - coolingRate
            while computationBudget > finalTemperature do
                let index1, index2 = GetTwoDifferentNonNegativeIntegers random listLength

                let newSolution = Reorder bestSolution index1 index2

                let bestSolutionDistance = getEarthDistance bestSolution
                let bestSolutionEnergy = (bestSolutionDistance.Value / 1.0<kilometer>) / float listLength
                let newSolutionDistance = getEarthDistance newSolution
                let newSolutionEnergy = (newSolutionDistance.Value / 1.0<kilometer>) / float listLength

                let acceptanceProbability = GetAcceptanceProbability bestSolutionEnergy newSolutionEnergy computationBudget
                if acceptanceProbability > random.NextDouble() then
                    bestSolution <- newSolution
                computationBudget <- computationBudget * budgetMultiplier
            bestSolution