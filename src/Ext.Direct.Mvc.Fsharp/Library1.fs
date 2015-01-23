namespace Ext.Direct.Mvc.Fsharp


module ProviderConfiguration =
    
    open System.Configuration

    let nullInt (n: obj) = 
        if n = null then System.Nullable<int>()
        else System.Nullable<int>(n :?> int)

    type T () =
        inherit ConfigurationSection ()
        
        [<ConfigurationProperty("name", IsRequired = false, DefaultValue = "Ext.app.REMOTING_API")>]
        member public this.Name with get () = string this.["name"]


        // Optional namespace for generated proxy methods.
        [<ConfigurationProperty("namespace", IsRequired = false)>]
        member public this.Namespace with get () = string this.["namespace"]
        

        // One or more names of assemblies to generate proxy for, separated by a comma.
        [<ConfigurationProperty("assembly", IsRequired = false)>]
        member public this.Assembly with get () = string this.["assembly"] 
        

        // Number that specifies the amount of time in milliseconds to wait before sending a batched request.
        // If not specified then the default value, configured by Ext JS will be used, which is 10
        [<ConfigurationProperty("buffer", IsRequired = false, DefaultValue = null)>]
        member public this.Buffer with get () = nullInt this.["buffer"]
        

        // Number of times to re-attempt delivery on failure of a call.
        // If not specified then the default value, configured by Ext JS will be used, which is 1
        [<ConfigurationProperty("maxRetries", IsRequired = false, DefaultValue = null)>]
        member public this.MaxRetries with get () = nullInt this.["maxRetries"]
        

        // The timeout to use for each request.
        // If not specified then the default value, configured by Ext JS will be used, which is I don't remember
        [<ConfigurationProperty("timeout", IsRequired = false, DefaultValue = null)>]
        member public this.Timeout with  get () =  nullInt this.["timeout"] 
        

        // The format in which DateTime objects should be returned. Valid values are "Iso", "JS" or "JavaScript". All case insensitive.
        [<ConfigurationProperty("dateFormat", IsRequired = false, DefaultValue = "Iso")>]
        member public this.DateFormat with get () = string this.["dateFormat"] 
        

        // Turns debug mode on if set to true. For development only! Set it to false on production environment.
        [<ConfigurationProperty("debug", IsRequired = false, DefaultValue = false)>]
        member public this.Debug with get () = this.["debug"] 


    let private cache = 
        lazy (
            let c = ConfigurationManager.GetSection("ext.direct")
            if c = null then new T ()
            else c :?> T )

    let GetConfiguration () = cache.Value
    

module JsonExtensions =

    open Newtonsoft.Json

    let writeProperty n v (wr: JsonWriter) =
        wr.WritePropertyName(n)
        wr.WriteRawValue(JsonConvert.SerializeObject(v))
        

module MethodExtions = 

    open System
    open System.Reflection

    let getAttribute<'T when 'T:> Attribute> (mb: MethodBase) =
        let attrs = mb.GetCustomAttributes(typeof<'T>, true)
        if attrs |> Seq.length > 0 then attrs.[0] :?> 'T |> Some
        else None


    let hasAttribute<'T when 'T:> Attribute> =
        getAttribute<'T> >> Option.isSome

module Attributes =

    open System
    
    [<AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)>]
    type DirectEventAttribute (name: string) =
        inherit Attribute ()

        let name = name

        member this.Name with get () = name


    [<AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)>]
    type NamedArgumentAttribute () =
        inherit Attribute ()


    [<AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)>]
    type FormHandlerAttribute () =
        inherit Attribute ()


module DirectMethod =

    open System
    open System.Reflection
    open System.Web.Mvc
    open Newtonsoft.Json

    module AT = Attributes
    module JE = JsonExtensions

    let getAttribute<'T> (mb: MethodBase) =
        let attrs = mb.GetCustomAttributes(typeof<'T>, true)
        match attrs with
        | [||] -> None
        | _ -> attrs.[0] :?> 'T |> Some

    let getMethod<'T> (o: obj) =
        let ms = o.GetType().GetMethods()
        match ms |> Array.tryFind (fun m -> m |> getAttribute<'T> |> Option.isSome) with
        | Some m -> Some m
        | None ->   None

    let actionNameMethod  = getMethod<ActionNameAttribute>
    let directEventMethod = getMethod<AT.DirectEventAttribute>
    let namedArgsMethod   = getMethod<AT.NamedArgumentAttribute>
    let formHandMethod    = getMethod<AT.FormHandlerAttribute>

    let actionNameAttr  = getAttribute<ActionNameAttribute>
    let directEventAttr = getAttribute<AT.DirectEventAttribute>
    let namedArgsAttr   = getAttribute<AT.NamedArgumentAttribute>
    let formHandAttr    = getAttribute<AT.FormHandlerAttribute>

    let actionName m = 
        match m |> actionNameAttr with
        | Some n -> n.Name
        | None   -> m.Name

    let eventName m = 
        match m |> directEventAttr with
        | Some n -> n.Name
        | None   -> m.Name

    let len m = 
        match m |> namedArgsAttr with
        | Some _ -> m.GetParameters().Length
        | None   -> 0 

    let parms m =
        match m |> namedArgsAttr with
        | Some _ -> 
            m.GetParameters()
            |> Array.map (fun p -> p.Name)
        | None -> [||]

    let isFormHandler m =
        match m |> formHandAttr with 
        | Some _ -> true
        | None   -> false

    let usesNamedArgs m =
        match m |> namedArgsAttr with 
        | Some _ -> true
        | None   -> false

    let writeJson (wr: JsonWriter) m =
        wr.WriteStartObject()
        wr |> JE.writeProperty "name" (actionName m)

        match m |> parms with
        | [||] -> wr |> JE.writeProperty "len" (len m)
        | ps   -> wr |> JE.writeProperty "params" ps

        wr |> JE.writeProperty "formHandler" (isFormHandler m)

        wr.WriteEndObject ()

    
    module DirectMethodTests =

        open NUnit.Framework
        open FsUnit
        open Swensen.Unquote

        open Attributes
        
        type TestObject() =
            [<DirectEvent("DirectEvent")>]
            member public this.DirectEvent () = ()

            [<ActionName("ActionName")>]
            member public this.Action () = ()

            [<NamedArgument>]
            member public this.NamedArgs (arg1, arg2) = ()


        let testObj = new TestObject()
        let testMethods = 
            testObj.GetType().GetMethods()

        let isTrue = (=) true
        let isFalse = (=) false
        
        let testDirectEventAttr () =
            test <@ testMethods 
                    |> Array.exists (fun m -> 
                                        m 
                                        |> getAttribute<DirectEventAttribute>
                                        |> Option.isSome)
                    |> isTrue @> 

        let testDirectEventName () =
            test <@ testMethods
                    |> Array.exists (fun m -> 
                                        m
                                        |> eventName = "DirectEvent")
                    |> isTrue @>

        let testActionNameName () =
            test <@ testMethods
                    |> Array.exists (fun m -> m |> actionName = "ActionName")
                    |> isTrue @>


        [<TestFixture>]
        type ``Given an object with a directevent attribute`` () =

            [<Test>]
            member x.``getAttribute should return some direct event attribute`` () =
                testDirectEventAttr ()
        
            [<Test>]
            member x.``eventName should be Test Direct Event`` () =
                testDirectEventName ()

        [<TestFixture>]
        type ``Given a test object with an action name method`` () =
            
            [<Test>]
            member x.``action name should return ActionName`` () =
                testActionNameName ()

        [<TestFixture>]
        type ``Given a test object with a method with 2 named arguments`` () =
            
            [<Test>]
            member x.``len should return 2`` () =
                testActionNameName ()


module DirectAction = 

    open System
    open System.Reflection
    open System.Web.Mvc

    open Newtonsoft.Json

    type T = 
        {
            Name: string
            Methods: Map<string, MethodInfo>
        }

    let create name methods = { Name = name; Methods = methods }

    
    let containsDuplicates same xs =
        let rec find xs =
            match xs with 
            | [] -> None
            | x::tail ->
                if tail |> List.exists (same x) then x |> Some
                else tail |> find
        xs
        |> Seq.toList 
        |> find


    let getActionMethods (tp: Type) =
        if tp.Name.EndsWith("Controller") then
            let name = tp.Name.Substring(0, tp.Name.IndexOf("Controller", StringComparison.InvariantCultureIgnoreCase))
            let methods =
                let filter (m: MethodInfo) =
                    m.IsPublic &&
                    (m.ReturnType = typeof<ActionResult> || 
                     m.ReturnType.IsSubclassOf(typeof<ActionResult>)) &&
                    (m |> MethodExtions.hasAttribute)

                tp.GetMethods()
                |> Seq.filter filter

            match methods |> containsDuplicates (fun x1 x2 -> x1.Name = x2.Name) with
            | Some m -> "duplicate method" + m.Name |> failwith
            | _ ->
                methods
                |> Seq.map (fun m -> m.Name, m)
                |> Map.ofSeq
                |> create name
                |> Some

        else None

    let writeJson (writer: JsonWriter) (actions: T) =
        writer.WritePropertyName actions.Name
        writer.WriteStartArray()

        actions.Methods
        |> Seq.map (fun kv -> kv.Value)
        |> Seq.iter (DirectMethod.writeJson writer)


module DirectRequest =

    open System
    open System.Web
    open Newtonsoft.Json

    let isNull x = x = Unchecked.defaultof<_>
    let (?=) right left =
        if right |> isNull then left else right
    
    [<Literal>]
    let _key = "Ext.Direct.Mvc.DirectRequest"
    [<Literal>]
    let _formTransactionId = "extTID"
    [<Literal>]
    let _formType = "extType"
    [<Literal>]
    let _action = "extAction"
    [<Literal>]
    let _method = "extMethod"
    [<Literal>]
    let _fileUpLoad = "extUpLoad"

    type T () = 
        member val Action = "" with get, set
        member val Method = "" with get, set
        member val Data = Array.empty : obj[] with get, set
        member val Type = "" with get, set
        member val IsFormPost = true with get, set
        member val IsFileUpload = false with get, set
        [<JsonProperty("tid")>]
        member val TransactionId = 0 with get, set
        [<JsonProperty("id")>]
        member val Id = 0 with get, set

    let create (httpReq: HttpRequestBase) =
        let req = new T()
        req.Action <- httpReq.[_action] ?= String.Empty
        req.Method <- httpReq.[_method] ?= String.Empty
        req.Type <- httpReq.[_formType] ?= String.Empty
        req.IsFileUpload <- Convert.ToBoolean httpReq.[_fileUpLoad]
        req.TransactionId <- Convert.ToInt32 httpReq.[_formTransactionId]
        req.Id <- req.TransactionId
        req
