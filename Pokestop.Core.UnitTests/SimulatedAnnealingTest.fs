module Pokestop.Core.UnitTests.SimulatedAnnealing

open NUnit.Framework
open Pokestop.Core.SimulatedAnnealing

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