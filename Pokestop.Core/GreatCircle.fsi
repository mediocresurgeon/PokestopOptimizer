namespace Pokestop.Core

    module GreatCircle =
          
        [<Measure>] type kilometer
        [<Measure>] type degree
        [<Measure>] type radian
        [<Measure>] type hour
        [<Measure>] type speed = kilometer/hour

        val degreesToRadians : float<degree> -> float<radian>

        [<Sealed>]
        type Coordinate =
            new : latitude:float<degree> * longitude:float<degree> -> Coordinate
            member Latitude : float<degree>
            member Longitude : float<degree>
    
        val GreatCircleDistance : radius:float<'u> -> location1:Coordinate -> location2: Coordinate -> float<'u>