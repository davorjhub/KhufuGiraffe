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
                let response = MongoCRUD.readAll 
                return! json response next ctx
            }
(*
    let getMessagefromid =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = MongoCRUD.readOnId
                return! json response next ctx
            }
            *)

    let insertMessage =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                //let save = ctx.GetService<Message>()
                let! mess = ctx.BindJsonAsync<Message>()
                let mess = {mess with Id = BsonObjectId.GenerateNewId()}
                let response = MongoCRUD.create mess
                return! json response next ctx
            } 

