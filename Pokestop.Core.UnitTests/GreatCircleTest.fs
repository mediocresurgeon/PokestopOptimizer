module Pokestop.Core.UnitTests.GreatCircleTest

open NUnit.Framework
open Pokestop.Core.GreatCircle

// http://www.movable-type.co.uk/scripts/latlong.html

let GreatCircleDistanceOnEarth = GreatCircleDistance 6371.0<kilometer>

[<Test>]
let ``Zero distance``() =
    // Arrange
    let point1 = new Coordinate(latitude = 1.0<degree>, longitude = 1.0<degree>)
    let point2 = new Coordinate(latitude = 1.0<degree>, longitude = 1.0<degree>)
    // Act
    let distance = GreatCircleDistanceOnEarth point1 point2
    // Assert
    Assert.AreEqual(0, distance)

[<Test>]
let ``Smallish distance``() = 
    // Arrange
    let point1 = new Coordinate(latitude = 1.0<degree>, longitude = 1.0<degree>)
    let point2 = new Coordinate(latitude = 2.0<degree>, longitude = 2.0<degree>)
    // Act
    let distance = GreatCircleDistanceOnEarth point1 point2
    // Assert
    Assert.GreaterOrEqual(distance, 157)
    Assert.LessOrEqual(distance, 158)

[<Test>]
let ``Medium distance``() = 
    // Arrange
    let point1 = new Coordinate(latitude = 1.0<degree>, longitude = 1.0<degree>)
    let point2 = new Coordinate(latitude = 50.0<degree>, longitude = 50.0<degree>)
    // Act
    let distance = GreatCircleDistanceOnEarth point1 point2
    // Assert
    Assert.GreaterOrEqual(distance, 7140)
    Assert.LessOrEqual(distance, 7141)

[<Test>]
let ``Big distance``() = 
    // Arrange
    let point1 = new Coordinate(latitude = -90.0<degree>, longitude = 0.0<degree>)
    let point2 = new Coordinate(latitude = 90.0<degree>, longitude = 0.0<degree>)
    // Act
    let distance = GreatCircleDistanceOnEarth point1 point2
    // Assert
    Assert.GreaterOrEqual(distance, 20015)
    Assert.LessOrEqual(distance, 20016)