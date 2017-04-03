namespace Pokestop.Core

open Microsoft.FSharp.Collections

module SimulatedAnnealing =

    let Reorder<'T when 'T : comparison> (list:List<'T>) (index1:int) (index2:int) =
        let item1 = list.Item index1
        let item2 = list.Item index2

        let transform arg =
            if arg = item1 then item2
            elif arg = item2 then item1
            else arg
        
        list |> List.map transform