namespace firstGiraffe

open MongoDB.Bson
module Controller =

    open Microsoft.AspNetCore.Http
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open Giraffe
    open firstGiraffe.Models

    let getProfile =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = MongoCRUD.readAll 
                return! json response next ctx
            }
    
    let insertProfile=
        fun (next : HttpFunc) (ctx : HttpContext)  ->
             task {
                let newProfile = {
                    Id = BsonObjectId.Create(ObjectId())
                    Title = "Is Redirection Working????"
                    Discipline = ".NET Intern"
                    ProjectName = "Khufu" 
                    Grade = "Intern"
                    ProjectRole = "Fullstack Developer"
                    Priority = "High"
                    NumberOfPositions = 3
                    Recruiter = "Camilo Gamba"
                }
                let response = MongoCRUD.create newProfile 
                return! json response next ctx 
            } 
    let getProfileFromId (id : string)=
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let objid = BsonObjectId(ObjectId.Parse(id))
                let response = MongoCRUD.readOnId(objid) 
                if response.ToJson() = "[]" then return! text("400") next ctx
                else return! json response next ctx
            }

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
   
