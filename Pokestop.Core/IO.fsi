namespace Pokestop.Core

open System.IO
open Pokestop.Core.GreatCircle

    module IO =
    
        type Waypoint = {
             Latitude : float<degree>
             Longitude : float<degree>
             Name : string
        }

        type Gpx = {
            Waypoints : Waypoint array
        }

        val GetCoordinatesFromGpxFile : gpxFile:Stream -> List<Coordinate>