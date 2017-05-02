module Pokestop.Core.UnitTests.IO

open System
open System.IO
open System.Reflection
open NUnit.Framework
open Pokestop.Core.IO

// https://stackoverflow.com/questions/1175056/value-of-the-last-element-of-a-list#answer-1175123
let rec last = function
    | hd :: [] -> hd
    | hd :: tl -> last tl
    | _ -> failwith "Empty list."

[<Test>]
let ``Read coordinates from file``() = 
    let assembly = Assembly.GetExecutingAssembly()
    use stream = assembly.GetManifestResourceStream("TestCoordinates.gpx")

    let coordinates = GetCoordinatesFromGpxFile stream

    Assert.IsNotEmpty(coordinates)

    let firstCoordinate = coordinates.Head
    Assert.AreEqual("Hagatna Pillbox", firstCoordinate.Name)
    Assert.AreEqual(13.47852, firstCoordinate.LatitudeDeg)
    Assert.AreEqual(144.7516, firstCoordinate.LongitudeDeg)

    let finalCoordinate = last coordinates
    Assert.AreEqual("Chief Quipuha Statue", finalCoordinate.Name)
    Assert.AreEqual(13.47725, finalCoordinate.LatitudeDeg)
    Assert.AreEqual(144.7538, finalCoordinate.LongitudeDeg)