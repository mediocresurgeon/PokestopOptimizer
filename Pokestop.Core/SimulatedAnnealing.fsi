namespace Pokestop.Core

    module SimulatedAnnealing =

        val Reorder<'T when 'T : comparison> : list : List<'T> -> index1 : int -> index2 : int -> List<'T>