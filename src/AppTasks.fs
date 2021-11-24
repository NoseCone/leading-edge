module AppTasks

open Feliz

let taskRow taskName tps distance stopped cancelled = Html.tr [
        Html.td "1"
        Html.td [
            prop.className "td-task-name"
            prop.text (taskName: string)
        ]
        Html.td [
            prop.className "td-task-tps"
            prop.text (tps: string)
        ]
        Html.td [
            prop.className "td-task-dist"
            prop.text (distance: string)
        ]
        Html.td [
            prop.className "td-task-stopped"
            prop.text (if stopped then "STOPPED" else "")
        ]
        Html.td [
            prop.className "td-task-cancelled"
            prop.text (if cancelled then "CANCELLED" else "")
        ]
    ]

let tasksTable = Html.table [
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
            Html.tbody [ taskRow "day one" "1DALBY-JANDOW-WARRA-1DALBY" "99.561444 km" false false ]
        ]
    ]