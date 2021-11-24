module App

open Browser.Dom
open Feliz

type Components =
    [<ReactComponent(import="Index", from="./Index.jsx")>]
    static member Index () = React.imported()

[<ReactComponent>]
let App () = React.fragment [
    Components.Index()
  ]

ReactDOM.render (App(), document.getElementById "feliz")
