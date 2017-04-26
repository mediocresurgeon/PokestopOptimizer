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
    [<StructuredFormatDisplay("<wpt lat=\"{LatitudeDeg}\" lon=\"{LongitudeDeg}\"><time>2014-01-01T00:00:00Z</time></wpt>")>]
    type Coordinate(latitude:float<degree>, longitude:float<degree>) =
      member this.LatitudeDeg = latitude
      member this.LongitudeDeg = longitude
      member this.LatitudeRad = degreesToRadians this.LatitudeDeg
      member this.LongitudeRad = degreesToRadians this.LongitudeDeg


    let GreatCircleDistance<[<Measure>] 'u> (radius:float<'u>) (location1:Coordinate) (location2:Coordinate) =
        let haversine (θ: float<radian>) = 0.5 * (1.0 - Math.Cos(θ/1.0<radian>))
        2.0 * radius * Math.Asin(Math.Sqrt(haversine(location2.LatitudeRad - location1.LatitudeRad)+Math.Cos(location1.LatitudeRad/1.0<radian>)*Math.Cos(location2.LatitudeRad/1.0<radian>)*haversine(location2.LongitudeRad - location1.LongitudeRad)))
        
        
        
        (*


        let dlon = (degreesToRadians location2.Longitude) - (degreesToRadians location1.Longitude)
        let dlat = (degreesToRadians location2.Latitude) - (degreesToRadians location1.Latitude)

        let cos (degrees:float<degree>) = Math.Cos (degreesToRadians degrees / 1.0<radian>)

        // Intermediate result a.
        let a = (sin (dlat / 2.0<_>)) ** 2.0 + ((cos location1.Latitude) * (cos location2.Latitude) * (sin (dlon / 2.0<_>)) ** 2.0);

        // Intermediate result c (great circle distance in Radians).
        let c = 2.0 * (asin (sqrt a));

        // Distance.
        let earthRadiusKms = 6371.0;
        let distance = earthRadiusKms * c;

        distance


        let square x = x * x
        // take the sin of the half and square the result
        let sinSquareHalf (radians:float<radian>) = (Math.Sin >> square) (radians / 2.0<radian>)
        let cos (degrees:float<degree>) = Math.Cos (degreesToRadians degrees / 1.0<radian>)

        let latitudeDiff = location1.Latitude - location2.Latitude |> degreesToRadians
        let longitudeDiff = location1.Latitude - location2.Latitude |> degreesToRadians

        let a = sinSquareHalf latitudeDiff + cos location1.Latitude * cos location2.Latitude * sinSquareHalf longitudeDiff
        let circumference = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0-a))

        radius * circumference
        *)