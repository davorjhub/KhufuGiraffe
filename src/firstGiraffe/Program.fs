module firstGiraffe.App

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open firstGiraffe.Controller

// ---------------------------------
// Web app
// ---------------------------------

let webApp =
    choose [
        subRoute "/api"
            (choose [
                GET >=> choose [
                    

                    
                    route "/profiles/">=> getProfile
                    subRoutef "/%s" (fun lang ->
                        choose [
                            routef "/%s" (fun n -> getProfileFromId n)
                        ])
                    
                    
                    
                    (*//route "/newProfile" >=> redirectTo true "/profiles" 
                    //route "/hello" >=> handleGetHello
                    //route "/obj" >=> handleObj
                    //route "/profiles">=> getProfile
                    //route "/searchID" >=> getProfileFromId *)
                    
                ]
                
                POST >=> choose [
                    route "/newProfile" >=> insertProfile 
                    //routef "/%s " >=> getProfileFromId
                    //routef "/searchID" >=> getProfileFromId
                   //routef "/newProfile/%s" >=> insertProfile %s
                ]
                
            ])
        setStatusCode 404 >=> text "Not Found" ]

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureCors (builder : CorsPolicyBuilder) =
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyMethod()
           .AllowAnyHeader()
           |> ignore

let configureApp (app : IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IHostingEnvironment>()
    (match env.IsDevelopment() with
    | true  -> app.UseDeveloperExceptionPage()
    | false -> app.UseGiraffeErrorHandler errorHandler)
        .UseHttpsRedirection()
        .UseCors(configureCors)
        .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    builder.AddFilter(fun l -> l.Equals LogLevel.Error)
           .AddConsole()
           .AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .UseIISIntegration()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
    0