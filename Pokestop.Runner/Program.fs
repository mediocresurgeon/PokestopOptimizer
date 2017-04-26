open System
open Pokestop.Core.GreatCircle
open Pokestop.Core.SimulatedAnnealing

[<Measure>] type second
[<Measure>] type hour

let secondsPerHour = 3600.0<second/hour>



[<EntryPoint>]
let main argv = 
    //let rand = new Random()
    let waypoints = [ new Coordinate(47.7601<degree>, -122.2029<degree>);
        (*new Coordinate(47.7595<degree>, -122.2037<degree>);
        new Coordinate(47.7589<degree>, -122.20379<degree>);
        new Coordinate(47.7592<degree>, -122.2048<degree>);
        new Coordinate(47.75865<degree>, -122.2045<degree>);
        new Coordinate(47.7581<degree>, -122.2039<degree>);
        new Coordinate(47.75835<degree>, -122.2049<degree>);
        new Coordinate(47.7579<degree>, -122.2062<degree>);
        new Coordinate(47.7576<degree>, -122.2067<degree>);
        new Coordinate(47.7574<degree>, -122.2069<degree>);
        new Coordinate(47.75785<degree>, -122.2082<degree>);
        new Coordinate(47.7581<degree>, -122.2078<degree>);
        new Coordinate(47.75835<degree>, -122.207<degree>);
        new Coordinate(47.75925<degree>, -122.2074<degree>);
        new Coordinate(47.75945<degree>, -122.2066<degree>);
        new Coordinate(47.7597<degree>, -122.2071<degree>);
        new Coordinate(47.76049<degree>, -122.207376<degree>);
        new Coordinate(47.7602<degree>, -122.2066<degree>);
        new Coordinate(47.7602<degree>, -122.2065<degree>);
        new Coordinate(47.7598<degree>, -122.2056<degree>);
        new Coordinate(47.7603<degree>, -122.2056<degree>);
        new Coordinate(47.7603<degree>, -122.2055<degree>);
        new Coordinate(47.7608<degree>, -122.2055<degree>);
        new Coordinate(47.76085<degree>, -122.2043<degree>);
        new Coordinate(47.76063<degree>, -122.2044<degree>);
        new Coordinate(47.7602<degree>, -122.2048<degree>);
        new Coordinate(47.7601<degree>, -122.2043<degree>);
        new Coordinate(47.7602<degree>, -122.2035<degree>);
        new Coordinate(47.7606<degree>, -122.2023<degree>);*)
        new Coordinate(47.7603<degree>, -122.2018<degree>); ]
    
    let mutable prev = waypoints.Head
    for wp in waypoints do
        let distance = GreatCircleDistance 6371.0<kilometer> wp prev
        let time = (distance / 10.5<kilometer/hour>) * secondsPerHour
        printfn "%A: %O" wp time
        prev <- wp

    (*
    let getEarthDistance = GetRouteDistance 6371.0<kilometer>
    let startingRouteDistance = getEarthDistance waypoints
    printfn "Initial distance: %A" startingRouteDistance

    let bestRoute = OptimizeOrderByDistance waypoints rand
    let bestRouteDistance = getEarthDistance bestRoute
    printfn "Final distance:   %A" bestRouteDistance

    if startingRouteDistance > bestRouteDistance then
        printfn "%A" bestRoute
    *)

    0 // return an integer exit code

