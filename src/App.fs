module App

open Fable.Core
open Feliz
open Feliz.Router
open Thoth.Fetch

type UtcOffset = { timeZoneMinutes: int }
type Radius = { radius: string }
type Give = { giveDistance: string option; giveFraction: float }
type Earth = { sphere: Radius }
type EarthMath = string

type Comp =
    { compName: string
    ; scoreBack: string option
    ; utcOffset: UtcOffset
    ; from: string
    ; ``to``: string
    ; location: string
    ; earth: Earth
    ; earthMath: EarthMath
    ; give: Give
    }
    static member Null =
        { compName = ""
        ; scoreBack = None
        ; utcOffset = { timeZoneMinutes = 0 }
        ; from = ""
        ; ``to`` = ""
        ; location = ""
        ; earth = { sphere = {radius = "" }}
        ; earthMath = ""
        ; give = { giveDistance = None; giveFraction = 0.0 }
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

type TaskLength = string
type RawZone = { zoneName: string }
type Stopped = { announced: string; retroactive: string}

type Task =
    { taskName: string
    ; zones: {| raw: RawZone list |}
    ; stopped: Stopped option
    ; cancelled: bool option
    }

let mkTaskRows (tasks: Task list) (taskLengths: TaskLength list) =
    (tasks, taskLengths)
    ||> List.map2 (fun {taskName = t; zones = zs; stopped = s; cancelled = c} d ->
        {| taskName = t
        ; tps = zs.raw |> List.map (fun z -> z.zoneName) |> String.concat "-"
        ; distance = d
        ; stopped = if Option.isSome s then true else false
        ; cancelled = Option.defaultValue false c
        |})

let getComp (comp : string) : JS.Promise<Comp> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/comp-input/comps.json" comp
    return! Fetch.get url
}

let getNominals (comp : string) : JS.Promise<Nominals> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/comp-input/nominals.json" comp
    return! Fetch.get url
}

let getTaskLengths (comp : string) : JS.Promise<TaskLength list> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/task-length/task-lengths.json" comp
    return! Fetch.get url
}

let getCompTasks (comp : string) : JS.Promise<Task list> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/comp-input/tasks.json" comp
    return! Fetch.get url
}

let (|StringSegment|_|) (input: string) =
    if System.String.IsNullOrWhiteSpace input && not (input.Contains "/") then None else Some input

type Components =
    [<ReactComponent(import="Index", from="./jsx/index.jsx")>]
    static member Index () = React.imported()

    [<ReactComponent(import="CompHeader", from="./jsx/comp-header.jsx")>]
    static member CompHeader (_: {| comp: Comp; nominals: Nominals |}) = React.imported()

// SEE: https://thisfunctionaltom.github.io/Html2Feliz/
let breadcrumb (compName: string) = Html.nav [
        prop.className "breadcrumb"
        prop.children [
            Html.ul [
                Html.li [
                    Html.a [
                        prop.href "/"
                        prop.text "Leading Edge (Feliz)"
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

open CompTasks
open CompSettings
open CompPilots
    
[<ReactComponent>]
let Router() =
    let (url, setUrl) = React.useState(Router.currentUrl())
    let (comp, setComp) = React.useState(Comp.Null)
    let (nominals, setNominals) = React.useState(Nominals.Null)
    let (taskLengths, setTaskLengths) = React.useState([])
    let (compTasks, setCompTasks) = React.useState([])
    let (compPilots, setCompPilots) = React.useState([])
    let (activeTab, setActiveTab) = React.useState(Tasks)
    let navigateToTab = function
        | Settings -> Router.navigate "settings"
        | Tasks -> Router.navigate "comp"
        | Pilots -> Router.navigate "pilots"
    React.router [
        router.onUrlChanged setUrl
        router.children [
            match url with
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
                    promise {
                        let! xs = getCompTasks compPrefix
                        setCompTasks xs
                    }
                    promise {
                        let! xs = getCompPilots compPrefix
                        setCompPilots xs
                    }
                    promise {
                        let! xs = getTaskLengths compPrefix
                        setTaskLengths xs
                    }
                ]
            | [ ] -> Html.div [ spacer; Components.Index() ]
            | [ "comp" ] ->
                Html.div
                    [ spacer
                    ; Components.CompHeader({| comp = comp; nominals = nominals |})
                    ; spacer
                    ; breadcrumb comp.compName
                    ; compTabs activeTab (fun x -> setActiveTab x; navigateToTab x)
                    ; tasksTable (mkTaskRows compTasks taskLengths)
                    ]
            | [ "settings" ] ->
                Html.div
                    [ spacer
                    ; Components.CompHeader({| comp = comp; nominals = nominals |})
                    ; spacer
                    ; breadcrumb comp.compName
                    ; compTabs activeTab (fun x -> setActiveTab x; navigateToTab x)
                    ; settingsTable ({| giveFraction = comp.give.giveFraction; earthRadius = comp.earth.sphere.radius; earthMath = comp.earthMath |})
                    ]
            | [ "pilots" ] ->
                Html.div
                    [ spacer
                    ; Components.CompHeader({| comp = comp; nominals = nominals |})
                    ; spacer
                    ; breadcrumb comp.compName
                    ; compTabs activeTab (fun x -> setActiveTab x; navigateToTab x)
                    ; Html.h1 "pilots page"
                    ; pilotsTable (compTasks |> List.map (fun x -> x.taskName), compPilots)
                    ]
            | otherwise -> Html.h1 "Not found"
        ]
    ]

open Browser.Dom

ReactDOM.render (Router(), (document.getElementById "feliz").parentElement)