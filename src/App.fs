module App

open Fable.Core
open Feliz
open Feliz.Router
open Thoth.Fetch

type Comp =
    { compName: string }
    static member Null = { compName = "" }

let getComp (comp : string) : JS.Promise<Comp> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/comp-input/comps.json" comp
    return! Fetch.get url
}

let (|StringSegment|_|) (input: string) =
    if System.String.IsNullOrWhiteSpace input && not (input.Contains "/") then None else Some input

type Components =
    [<ReactComponent(import="Index", from="./Index.jsx")>]
    static member Index () = React.imported()

[<ReactComponent>]
let Router() =
    let (url, setUrl) = React.useState(Router.currentUrl())
    let (compPrefix, setCompPrefix) = React.useState("2017-dalby")
    let (comp, setComp) = React.useState(Comp.Null)
    React.router [
        router.onUrlChanged setUrl
        router.children [
            match url with
            | [ ] -> Components.Index()
            | [ "comp" ] ->
                Html.h1 ("comp page " + compPrefix + " " + comp.compName)
            | [ "comp-prefix"; StringSegment compPrefix ] ->
                setCompPrefix compPrefix
                Router.navigate "comp"
                ignore <| promise {
                    let! comp = getComp compPrefix
                    setComp comp
                }
            | [ "settings" ] -> Html.h1 "settings page"
            | [ "pilots" ] -> Html.h1 "pilots page"
            | otherwise -> Html.h1 "Not found"
        ]
    ]

open Browser.Dom

ReactDOM.render (Router(), document.getElementById "feliz")
