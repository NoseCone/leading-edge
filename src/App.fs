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

// SEE: https://thisfunctionaltom.github.io/Html2Feliz/
let breadcrumb (compName: string) = Html.nav [
        prop.className "breadcrumb"
        prop.children [
            Html.ul [
                Html.li [
                    Html.a [
                        prop.href "/"
                        prop.text "Variable Geometry (Svelte)"
                    ]
                ]
                Html.li [
                    prop.className "is-active"
                    prop.text compName
                ]
            ]
        ]
    ]

let spacer = Html.div [ prop.className "spacer" ]

type CompTab = Settings | Tasks | Pilots

let compTabs (tab: CompTab) (setTab: CompTab -> Unit) =
    let x = seq {"is-active"}
    let isActive expected = if tab = expected then "is-active" else ""
    Html.div [
        prop.className "tabs"
        prop.children [
            Html.ul [
                Html.li [
                    prop.className (isActive Settings)
                    prop.onClick (fun _ -> setTab Settings)
                    prop.children [ Html.a [ prop.text "Settings" ] ]
                ]
                Html.li [
                    prop.className (isActive Tasks)
                    prop.onClick (fun _ -> setTab Tasks)
                    prop.children [ Html.a [ prop.text "Tasks" ] ]
                ]
                Html.li [
                    prop.className (isActive Pilots)
                    prop.onClick (fun _ -> setTab Pilots)
                    prop.children [ Html.a [ prop.text "Pilots" ] ]
                ]
            ]
        ]
    ]

open AppTasks
    
[<ReactComponent>]
let Router() =
    let (url, setUrl) = React.useState(Router.currentUrl())
    let (comp, setComp) = React.useState(Comp.Null)
    let (nominals, setNominals) = React.useState(Nominals.Null)
    let (activeTab, setActiveTab) = React.useState(Tasks)
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
                    ; spacer
                    ; breadcrumb comp.compName
                    ; compTabs activeTab setActiveTab
                    ; tasksTable
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