open System
open System.Configuration
open Pokestop.Core.GreatCircle
open Pokestop.Core.SimulatedAnnealing

(*
[<Measure>] type second
[<Measure>] type hour

let secondsPerHour = 3600.0<second/hour>
*)



[<EntryPoint>]
let main argv = 
    let startingTemp = float(ConfigurationManager.AppSettings.Item("startingTemperature"))
    let finalTemp = float(ConfigurationManager.AppSettings.Item("finalTemperature"))
    let coolingRate = float(ConfigurationManager.AppSettings.Item("coolingRate"))
    let rand = new Random()
    let waypoints = [
        new Coordinate("point1", 47.7601<degree>, -122.2029<degree>);
        new Coordinate("point2", 47.7595<degree>, -122.2037<degree>);
        new Coordinate("point3", 47.7589<degree>, -122.20379<degree>);
        new Coordinate("point4", 47.7592<degree>, -122.2048<degree>);
        new Coordinate("point5", 47.75865<degree>, -122.2045<degree>);
        new Coordinate("point6", 47.7581<degree>, -122.2039<degree>);
        new Coordinate("point7", 47.75835<degree>, -122.2049<degree>);
        new Coordinate("point8", 47.7579<degree>, -122.2062<degree>);
        new Coordinate("point9", 47.7576<degree>, -122.2067<degree>);
        new Coordinate("point10", 47.7574<degree>, -122.2069<degree>);
        new Coordinate("point11", 47.75785<degree>, -122.2082<degree>);
        new Coordinate("point12", 47.7581<degree>, -122.2078<degree>);
        new Coordinate("point13", 47.75835<degree>, -122.207<degree>);
        new Coordinate("point14", 47.75925<degree>, -122.2074<degree>);
        new Coordinate("point15", 47.75945<degree>, -122.2066<degree>);
        new Coordinate("point16", 47.7597<degree>, -122.2071<degree>);
        new Coordinate("point17", 47.76049<degree>, -122.207376<degree>);
        new Coordinate("point18", 47.7602<degree>, -122.2066<degree>);
        new Coordinate("point19", 47.7602<degree>, -122.2065<degree>);
        new Coordinate("point20", 47.7598<degree>, -122.2056<degree>);
        new Coordinate("point21", 47.7603<degree>, -122.2056<degree>);
        new Coordinate("point22", 47.7603<degree>, -122.2055<degree>);
        new Coordinate("point23", 47.7608<degree>, -122.2055<degree>);
        new Coordinate("point24", 47.76085<degree>, -122.2043<degree>);
        new Coordinate("point25", 47.76063<degree>, -122.2044<degree>);
        new Coordinate("point26", 47.7602<degree>, -122.2048<degree>);
        new Coordinate("point27", 47.7601<degree>, -122.2043<degree>);
        new Coordinate("point28", 47.7602<degree>, -122.2035<degree>);
        new Coordinate("point29", 47.7606<degree>, -122.2023<degree>);
        new Coordinate("point30", 47.7603<degree>, -122.2018<degree>);
    ]
    
    let getEarthDistance = GetRouteDistance 6371.0<kilometer>
    let startingRouteDistance = getEarthDistance waypoints
    printfn "Initial distance: %A" startingRouteDistance

    let mutable bestRoute = waypoints
    let mutable bestDistance = startingRouteDistance
    let mutable retry = true
    let mutable tryCount = 0
    while retry && tryCount < 5000 do
        tryCount <- tryCount + 1
        let newRoute = OptimizeOrderByDistance waypoints startingTemp finalTemp coolingRate rand
        let newDistance = getEarthDistance newRoute
        if newDistance < bestDistance then
            retry <- false
            bestRoute <- newRoute
            bestDistance <- newDistance
    if bestDistance <> startingRouteDistance then
        for coordinate in bestRoute do
            printfn "%s: %f, %f" coordinate.Name coordinate.LatitudeDeg coordinate.LongitudeDeg
    printfn "Final distance:   %A" bestDistance

    0 // return an integer exit code

