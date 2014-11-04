namespace Ext.Direct.Mvc.Fsharp

open System.Reflection
open System.Web.Mvc
open Newtonsoft.Json



type Class1() = 
    member this.X = "F#"

module DirectMethod =
    
    let getAttribute<'T> (mb: MethodBase) =
        let attrs = mb.GetCustomAttributes(typeof<'T>, true)
        match attrs with
        | [||] -> None
        | _ -> attrs.[0] :?> 'T |> Some

    module DirectMethodTests =

        type BooleanAttribute () =
            inherit System.Attribute()
        
        type Test() =
            [<BooleanAttribute>]
            member public this.Any () = true

        let test = new Test()

        test.GetType().GetMethods()
        |> Array.map (fun m -> m.GetCustomAttributes(typeof<BooleanAttribute>, true))
        