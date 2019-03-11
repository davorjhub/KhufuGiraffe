namespace firstGiraffe.Models

open MongoDB.Bson

[<CLIMutable>]
type Message =
    {
        Id: BsonObjectId
        Text : string
    }