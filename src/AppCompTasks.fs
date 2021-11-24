module AppCompTasks

open Feliz

let taskRow (task: {|taskName: string; tps: string; distance: string; stopped: bool; cancelled: bool|}) = Html.tr [
        Html.td "1"
        Html.td [
            prop.className "td-task-name"
            prop.text task.taskName
        ]
        Html.td [
            prop.className "td-task-tps"
            prop.text task.tps
        ]
        Html.td [
            prop.className "td-task-dist"
            prop.text task.distance
        ]
        Html.td [
            prop.className "td-task-stopped"
            prop.text (if task.stopped then "STOPPED" else "")
        ]
        Html.td [
            prop.className "td-task-cancelled"
            prop.text (if task.cancelled then "CANCELLED" else "")
        ]
    ]

let tasksTable (tasks: {|taskName: string; tps: string; distance: string; stopped: bool; cancelled: bool|} list)= Html.table [
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
            Html.tbody (List.map taskRow tasks)
        ]
    ]