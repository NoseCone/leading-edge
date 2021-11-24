module AppCompSettings

open Feliz

let settingsTable (settings: {| giveFraction: float; earthRadius: string; earthMath: string |})= Html.table [
        prop.className "table is-bordered"
        prop.children [
            Html.thead [
                Html.tr [
                    Html.th [
                        prop.colSpan 3
                    ]
                    Html.th "Value"
                ]
            ]
            Html.tbody [
                Html.tr [
                    Html.th "* Give"
                    Html.td [
                        prop.colSpan 2
                        prop.text "give fraction only, no give distance"
                    ]
                    Html.td (string settings.giveFraction)
                ]
                Html.tr [
                    Html.th "Earth model"
                    Html.td [
                        prop.colSpan 2
                        prop.text "Sphere with radius"
                    ]
                    Html.td settings.earthRadius
                ]
                Html.tr [
                    Html.th [
                        prop.colSpan 3
                        prop.text "Earth math"
                    ]
                    Html.td settings.earthMath
                ]
            ]
            Html.tfoot [
                Html.tr [
                    Html.td [
                        prop.colSpan 4
                        prop.text "* Adjusting the turnpoint radius with some give for pilots just short of the control zone"
                    ]
                ]
            ]
        ]
    ]