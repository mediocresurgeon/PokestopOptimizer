module Pokestop.Core.UnitTests.SimulatedAnnealing

open System
open NUnit.Framework
open Pokestop.Core.GreatCircle
open Pokestop.Core.SimulatedAnnealing

let earthRadius = 6371.0<kilometer>

[<Test>]
let ``Swap two adjacent elements``() = 
    let originalList = [ 1; 2; 3; ]

    let newList = Reorder originalList 0 1

    Assert.AreEqual(originalList.Item 0, newList.Item 1)
    Assert.AreEqual(originalList.Item 1, newList.Item 0)

[<Test>]
let ``Swap first and last elements``() = 
    let originalList = [ 1; 2; 3; 4; ]

    let newList = Reorder originalList 3 0

    Assert.AreEqual(originalList.Item 0, newList.Item 3)
    Assert.AreEqual(originalList.Item 3, newList.Item 0)

[<Test>]
let ``Total distance of empty list is zero``() =
    let locations = [ ]

    let totalDistance = GetRouteDistance earthRadius locations

    Assert.IsTrue(totalDistance.IsNone)

[<Test>]
let ``Total distance of single element is zero``() =
    let loc1 = new Coordinate(1.0<degree>, 1.0<degree>)
    let locations = [ loc1 ]

    let totalDistance = GetRouteDistance earthRadius locations

    Assert.IsTrue(totalDistance.IsSome)
    Assert.AreEqual(0, totalDistance.Value)

[<Test>]
let ``Total distance of two close elements``() =
    let loc1 = new Coordinate(latitude = 1.0<degree>, longitude = 1.0<degree>)
    let loc2 = new Coordinate(latitude = 2.0<degree>, longitude = 2.0<degree>)
    let locations = [ loc1; loc2; ]

    let totalDistance = GetRouteDistance earthRadius locations

    Assert.IsTrue(totalDistance.IsSome)
    Assert.GreaterOrEqual(totalDistance.Value, 314)
    Assert.LessOrEqual(totalDistance.Value, 315)

[<Test>]
let ``Total distance of three elements``() =
    let loc1 = new Coordinate(latitude = 0.0<degree>, longitude = 0.0<degree>)
    let loc2 = new Coordinate(latitude = 90.0<degree>, longitude = 0.0<degree>)
    let loc3 = new Coordinate(latitude = 0.0<degree>, longitude = 90.0<degree>)
    let locations = [ loc1; loc2; loc3; ]

    let totalDistance = GetRouteDistance earthRadius locations

    Assert.IsTrue(totalDistance.IsSome)
    Assert.GreaterOrEqual(totalDistance.Value, 30022)
    Assert.LessOrEqual(totalDistance.Value, 30023)

[<Test>]
let ``Shortest path between 4 points``() =
    let loc1 = new Coordinate(latitude = 1.0<degree>, longitude = 1.0<degree>)
    let loc2 = new Coordinate(latitude = 2.0<degree>, longitude = 2.0<degree>)
    let loc3 = new Coordinate(latitude = 1.0<degree>, longitude = 2.0<degree>)
    let loc4 = new Coordinate(latitude = 2.0<degree>, longitude = 1.0<degree>)
    let locations = [ loc1; loc2; loc3; loc4; ]
    let rand = new Random()
    let startingTemp = 1000.0
    let coolingRate = 0.0003

    let optimalOrder = OptimizeOrderByDistance locations startingTemp coolingRate rand
    let optimalDistance = GetRouteDistance earthRadius optimalOrder

    Assert.GreaterOrEqual(optimalDistance.Value, 444)
    Assert.LessOrEqual(optimalDistance.Value, 445)