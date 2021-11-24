module CompPilots

open Fable.Core
open Feliz
open Thoth.Json
open Thoth.Fetch

type PilotStatus =
    { pilotId: string
    ; pilotName: string
    ; pilotStatus: string list
    }
    static member Null =
        { pilotId = ""
        ; pilotName = ""
        ; pilotStatus = []
        }
    static member Decoder =
        Decode.list (Decode.list Decode.string) |> Decode.map (function
            | [[id; name]; status] ->
                { pilotId = id
                ; pilotName = name
                ; pilotStatus = status
                }
            | _ -> PilotStatus.Null)

let getCompPilots (comp : string) : JS.Promise<PilotStatus list> = promise {
    let url = sprintf "http://%s.flaretiming.com/json/gap-point/pilots-status.json" comp
    return! Fetch.get(url, decoder = Decode.list PilotStatus.Decoder)
}

let pilotRow (pilot: PilotStatus) = Html.tr [
        Html.td [
            prop.className "td-pid"
            prop.text (string pilot.pilotId)
        ]
        Html.td pilot.pilotName
        yield!
            pilot.pilotStatus |> List.map (function
                | "" | "DF" -> Html.td []
                | s -> Html.td [ prop.text s ])
    ]

let pilotsTable (taskNames: string list, pilots: PilotStatus list) = Html.table [
        prop.classes [ "table"; "is-bordered"; "is-striped" ]
        prop.children [
            Html.thead [
                Html.tr [
                    Html.th [
                        prop.className "th-pid"
                        prop.text "Id"
                    ]
                    Html.th "Name"
                    yield! (taskNames |> List.map Html.th)
                ]
            ]
            Html.tbody (pilots |> List.map pilotRow)
        ]
    ]