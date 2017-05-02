namespace Pokestop.Core

open System.IO
open System.Runtime.Serialization
open System.Xml.Serialization
open Pokestop.Core.GreatCircle

    module IO =

        [<CLIMutable>]
        [<DataContract>]
        type Waypoint = {
            [<XmlAttribute("lat")>]
            Latitude : float<degree>
            [<XmlAttribute("lon")>]
            Longitude : float<degree>
            [<XmlElement("name")>]
            Name : string
        }

        [<CLIMutable>]
        [<DataContract>]
        [<XmlRoot(ElementName = "gpx")>]
        type Gpx = {
            [<XmlElement("wpt")>] 
            Waypoints : Waypoint array
        }

        let ConvertWaypointToCoordinate (waypoint:Waypoint) =
            new Coordinate(waypoint.Name, waypoint.Latitude, waypoint.Longitude)

        let GetCoordinatesFromGpxFile (gpxFile:Stream) =
            let xmlSerializer = new XmlSerializer(typedefof<Gpx>)
            let dataContractResult = xmlSerializer.Deserialize(gpxFile) :?> Gpx // http://stackoverflow.com/questions/31616761/f-casting-operators
            dataContractResult.Waypoints
                |> Array.toList
                |> List.map (fun wpt -> ConvertWaypointToCoordinate wpt)