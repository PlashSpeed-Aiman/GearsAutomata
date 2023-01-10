namespace MathsCalc
open System
open MathNet.Numerics
open MathNet.Numerics.LinearAlgebra
 
module Formulaes =

   [<Struct>]
   type GearSpeedFive = {
        LowestGear:double
        SecondGear:double
        ThirdGear :double
        FourthGear: double
        HighestGear : double}
    type GearSpeedSix = {
        LowestGear:double
        SecondGear:double
        ThirdGear :double
        FourthGear: double
        FifthGear :double
        HighestGear : double }

    let calculateCGP(lowestGear)(highestGear)(sqrVal)=
        let ratio = lowestGear/highestGear
        let cgp = Math.Pow (ratio,sqrVal)
        cgp

    let CGP5 (highestGear:double) cgp=
        
        let gear4 = highestGear * cgp
        let gear3 = gear4 * cgp
        let gear2 = gear3 * cgp
  
        gear2,gear3,gear4

    let CGP6 (highestGear:double) cgp=
        let gear5 = highestGear * cgp
        let gear4 = gear5 * cgp
        let gear3 = gear4 * cgp
        let gear2 = gear3 * cgp

        gear2,gear3,gear4,gear5

    let ProgressiveFiveSpeed k (lowestGear:double) cgp =
        let c1 = cgp * Math.Pow (k,-1.5)
        let c2 = c1 * k
        let c3 = c2 * k
        let gear2 = (double) lowestGear/c1
        let gear3 = (double) lowestGear/(c1*c2)
        let gear4 = (double) lowestGear/(c1*c2*c3)

        gear2,gear3,gear4
        

    let ProgressiveSixSpeed k (lowestGear:double) cgp =
        let d1 = cgp * Math.Pow(k,-2)
        let d2 = d1  * k
        let d3 = d2  * k
        let d4 = d3  * k
        let gear2 = (double)lowestGear/d1
        let gear3 = (double)lowestGear/(d1*d2)
        let gear4 = (double)lowestGear/(d1*d2*d3)
        let gear5 = (double)lowestGear/(d1*d2*d3*d4)

        gear2,gear3,gear4,gear5

    let LowestGear (driveTypeCoefficient:double) (rollingRadius:double) (drivelineEfficiency:double) (engineTorque:double) (adhesionCoefficient:double) (vehicleWeight:double) =
        driveTypeCoefficient * (rollingRadius * (adhesionCoefficient * (vehicleWeight * 9.81))/(drivelineEfficiency * engineTorque)) 

    let GenericTopSpeed (engineRpm:double)(rollingRadius:double)(topSpeed:double) =
        (engineRpm* (Math.PI/30.0)) * (rollingRadius/(topSpeed))

    let MaxTopSpeedHelper (maxPower:double)(driveLine:double)(rollingResistance)(weight)=
        let d = (maxPower * driveLine) * (-1.0)
        let c = rollingResistance * weight * 9.81
        let b = 0
        let a = 0.35
        let roots = FindRoots.Cubic( d ,c, b, a)
        let a,b,c = roots.ToTuple() 
        let V = 
            match a,b,c with
                | (a,b,c) when a.IsRealNonNegative() = true -> (double) a.Real 
                | (a,b,c) when b.IsRealNonNegative() = true -> b.Real
                | (a,b,c) when c.IsRealNonNegative() = true -> c.Real
                | _ -> 0.0
        V

    
        