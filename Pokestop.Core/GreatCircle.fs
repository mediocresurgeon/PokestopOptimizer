namespace Pokestop.Core

open System

// http://www.fssnip.net/4a/title/calculating-the-distance-on-earth-with-units-of-measure

module GreatCircle =
         
    [<Measure>] type kilometer
    [<Measure>] type degree
    [<Measure>] type radian
    [<Measure>] type hour
    [<Measure>] type speed = kilometer/hour

    let degreesToRadians (degrees:float<degree>) = System.Math.PI * degrees / 180.0<degree/radian>

    [<Sealed>]
    type Coordinate(latitude:float<degree>, longitude:float<degree>) =
      member this.Latitude = latitude
      member this.Longitude = longitude

    let GreatCircleDistance<[<Measure>] 'u> (radius:float<'u>) (location1:Coordinate) (location2:Coordinate) =
        let square x = x * x
        // take the sin of the half and square the result
        let sinSquareHalf (radians:float<radian>) = (Math.Sin >> square) (radians / 2.0<radian>)
        let cos (degrees:float<degree>) = Math.Cos (degreesToRadians degrees / 1.0<radian>)

        let latitudeDiff = location1.Latitude - location2.Latitude |> degreesToRadians
        let longitudeDiff = location1.Latitude - location2.Latitude |> degreesToRadians

        let a = sinSquareHalf latitudeDiff + cos location1.Latitude * cos location2.Latitude * sinSquareHalf longitudeDiff
        let circumference = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0-a))

        radius * circumference