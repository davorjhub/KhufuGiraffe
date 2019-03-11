namespace firstGiraffe

module Controller =

    open Microsoft.AspNetCore.Http
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open Giraffe
    open firstGiraffe.Models
    open MongoDB.Bson
    open MongoCRUD
(*
    let handleGetHello =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                Text = "Hello world, from Giraffe!"
                return json Text next ctx
            }
*)
(*
    let handleObj =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = {
                    Id = 201
                    Text = "Obj is here..."
                }
                return! json response next ctx
            }
*)
    let getMessage =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = MongoCRUD.readOnId 
                return! json response next ctx
            }
(*
    let getMessagefromid =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = MongoCRUD.readAll
                return! json response next ctx
            }
            *)
(*
    let insertMessage text =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let lilmessage = {
                    Id = BsonObjectId.Create 
                    Text = text
                }
                let response = MongoCRUD.create lilmessage

                return! json response next ctx
            } 
*)
