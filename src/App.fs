module App

open Browser.Dom
open Feliz

[<ReactComponent>]
let Counter () =
    let (count, setCount) = React.useState (0)

    Html.div
        [ Html.pre count
          Html.button [ prop.text "+"; prop.onClick (fun _ -> setCount (count + 1)) ]
          Html.button [ prop.text "-"; prop.onClick (fun _ -> setCount (count - 1)) ]
        ]

[<ReactComponent>]
let App () = React.fragment [ Counter() ]

ReactDOM.render (App(), document.getElementById "feliz")
