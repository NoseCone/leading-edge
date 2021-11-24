module AppTasks

open Feliz

let tasks =
    Html.table [
        prop.classes [ "table"; "is-striped" ]
        prop.children [
            Html.thead [
                Html.tr [
                    Html.th "#"
                    Html.th [
                        prop.className "th-task-name"
                        prop.text "Name"
                    ]
                    Html.th [
                        prop.className "th-task-tps"
                        prop.text "Turnpoints"
                    ]
                    Html.th [
                        prop.className "th-task-dist"
                        prop.text "Distance"
                    ]
                    Html.th [
                        prop.className "th-task-stopped"
                        prop.text "Stopped"
                    ]
                    Html.th [
                        prop.className "th-task-cancelled"
                        prop.text "Cancelled"
                    ]
                ]
            ]
            Html.tbody [
                Html.tr [
                    Html.td "1"
                    Html.td [
                        prop.className "td-task-name"
                        prop.text "day one"
                    ]
                    Html.td [
                        prop.className "td-task-tps"
                        prop.text "1DALBY-JANDOW-WARRA-1DALBY"
                    ]
                    Html.td [
                        prop.className "td-task-dist"
                        prop.text "99.561444 km"
                    ]
                    Html.td [
                        prop.className "td-task-stopped"
                    ]
                    Html.td [
                        prop.className "td-task-cancelled"
                    ]
                ]
                Html.tr [
                    Html.td "2"
                    Html.td [
                        prop.className "td-task-name"
                        prop.text "day four T2"
                    ]
                    Html.td [
                        prop.className "td-task-tps"
                        prop.text "1DALBY-MACALI-JIMBOU-WARRA-JANDOW-BRIGAL"
                    ]
                    Html.td [
                        prop.className "td-task-dist"
                        prop.text "111.259762 km"
                    ]
                    Html.td [
                        prop.className "td-task-stopped"
                    ]
                    Html.td [
                        prop.className "td-task-cancelled"
                    ]
                ]
                Html.tr [
                    Html.td "3"
                    Html.td [
                        prop.className "td-task-name"
                        prop.text "day five"
                    ]
                    Html.td [
                        prop.className "td-task-tps"
                        prop.text "1DALBY-BRIGAL-IRON-1DALBY"
                    ]
                    Html.td [
                        prop.className "td-task-dist"
                        prop.text "96.889354 km"
                    ]
                    Html.td [
                        prop.className "td-task-stopped"
                    ]
                    Html.td [
                        prop.className "td-task-cancelled"
                    ]
                ]
                Html.tr [
                    Html.td "4"
                    Html.td [
                        prop.className "td-task-name"
                        prop.text "day six"
                    ]
                    Html.td [
                        prop.className "td-task-tps"
                        prop.text "1DALBY-WARRA-BELL-1DALBY"
                    ]
                    Html.td [
                        prop.className "td-task-dist"
                        prop.text "114.373717 km"
                    ]
                    Html.td [
                        prop.className "td-task-stopped"
                    ]
                    Html.td [
                        prop.className "td-task-cancelled"
                    ]
                ]
                Html.tr [
                    Html.td "5"
                    Html.td [
                        prop.className "td-task-name"
                        prop.text "day seven"
                    ]
                    Html.td [
                        prop.className "td-task-tps"
                        prop.text "1DALBY-BROADW-JIMBOU-MACALI-1DALBY"
                    ]
                    Html.td [
                        prop.className "td-task-dist"
                        prop.text "88.969527 km"
                    ]
                    Html.td [
                        prop.className "td-task-stopped"
                    ]
                    Html.td [
                        prop.className "td-task-cancelled"
                    ]
                ]
            ]
        ]
    ]