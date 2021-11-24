module App

open Fable.Core
open Feliz
open Feliz.Router
open Thoth.Fetch

type UtcOffset = { timeZoneMinutes: int }

type Comp =
    { compName: string
    ; scoreBack: string option
    ; utcOffset: UtcOffset
    ; from: string
    ; ``to``: string
    ; location: string
    }
    static member Null =
        { compName = ""
        ; scoreBack = None
        ; utcOffset = { timeZoneMinutes = 0 }
        ; from = ""
        ; ``to`` = ""
        ; location = ""
        }
    member x.compSlug = sprintf "%s to %s, %s" x.from x.``to`` x.location

type Nominals =
    { free: string
    ; distance: string
    ; time: string
    ; goal: float
    }
    static member Null =
        { free = ""
        ; distance = ""
        ; time = ""
        ; goal = 0.0
        }

let getComp (comp : string) : JS.Promise<Comp> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/comp-input/comps.json" comp
    return! Fetch.get url
}

let getNominals (comp : string) : JS.Promise<Nominals> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/comp-input/nominals.json" comp
    return! Fetch.get url
}

let (|StringSegment|_|) (input: string) =
    if System.String.IsNullOrWhiteSpace input && not (input.Contains "/") then None else Some input

type Components =
    [<ReactComponent(import="Index", from="./jsx/index.jsx")>]
    static member Index () = React.imported()
    [<ReactComponent(import="CompHeader", from="./jsx/comp-header.jsx")>]
    static member CompHeader (props: {| comp: Comp; nominals: Nominals |}) = React.imported()

[<ReactComponent>]
let Router() =
    let (url, setUrl) = React.useState(Router.currentUrl())
    let (comp, setComp) = React.useState(Comp.Null)
    let (nominals, setNominals) = React.useState(Nominals.Null)
    React.router [
        router.onUrlChanged setUrl
        router.children [
            match url with
            | [ ] -> Components.Index()
            | [ "comp" ] ->
                Html.div
                    [ Html.pre (sprintf "%A" comp)
                    ; Html.pre (sprintf "%A" nominals)
                    ; Components.CompHeader({| comp = comp; nominals = nominals |})
                    ]
            | [ "comp-prefix"; StringSegment compPrefix ] ->
                Router.navigate "comp"
                ignore <| Promise.Parallel [
                    promise {
                        let! comp = getComp compPrefix
                        setComp comp
                    }
                    promise {
                        let! nominals = getNominals compPrefix
                        setNominals nominals
                    }
                ]
            | [ "settings" ] -> Html.h1 "settings page"
            | [ "pilots" ] -> Html.h1 "pilots page"
            | otherwise -> Html.h1 "Not found"
        ]
    ]

open Browser.Dom

ReactDOM.render (Router(), document.getElementById "feliz")
