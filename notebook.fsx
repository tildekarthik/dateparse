#r "nuget:Deedle"
#r "nuget:FSharp.Charting"
#r "nuget:NodaTime"
// #load "Deedle.fsx"
// #load "FSharp.Charting.fsx"

open System
open System.IO
open Deedle
open FSharp.Charting
open NodaTime
open NodaTime.Text


let root = "."
let fname = "mc_5.csv"
let df = Frame.ReadCsv(Path.Combine(root,fname))
let stringDateTimeConvert (s:string) = 
    s.Replace("_","").Replace(".bin","")
let dts = df.GetColumn<string>("file_name").Values
            |> Seq.map stringDateTimeConvert
            |> Seq.map (fun x -> LocalDateTimePattern.CreateWithInvariantCulture("yyyyMMddHHmmss").Parse(x))
df?dt <- dts



let startDate = LocalDateTimePattern.CreateWithInvariantCulture("yyyyMMddHHmmss").Parse("20210801000000").Value

let newdf = df |> Frame.filterRowValues (fun c ->  c.GetAs<LocalDateTime>("dt") > startDate)