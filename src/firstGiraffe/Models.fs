namespace firstGiraffe.Models

open MongoDB.Bson

[<CLIMutable>]
(*type Message =
    {
        Id: BsonObjectId
        Text : string
    }*)
 type Profile =
    {
        Id: BsonObjectId
        Title : string
        Discipline : string
        ProjectName : string
        Grade : string
        ProjectRole : string
        Priority : string
        NumberOfPositions : int32
        Recruiter : string
    }