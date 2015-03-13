// Example 0

type Customer = { Name : string; Age : int }





























// Example 1

let map f (x,y,z) = 
   (f x, f y, f z)
























// Example 2

type Expr = | True 
            | False 
            | And  of  Expr * Expr
            | Or   of  Expr * Expr
            | Not  of  Expr
            | Base of  bool
 
let  rec eval expr  = 
   match expr with 
   | True  -> true 
   | False -> false 
   | Base v  -> v
   | And  (x,y) -> eval x && eval y  
   | Or  (x,y)  -> eval x || eval y 
   | Not  x     -> not (eval x) 










// Example 3

let rec destutter l = 
  match l with 
  | []             -> [] 
  | x :: y :: rest -> 
    if  x = y then destutter (y :: rest) 
    else  x :: destutter (y :: rest)











// Example 4
open System
open System.Net

type ConnState = 
| Connecting 
| Connected 
| Disconnected 
 
type ConnInfo = { 
  State:                   ConnState; 
  Server:                  System.Net.IPAddress;
  LastPing:                option<DateTime>; 
  LastPingId:              option<int>; 
  SessionId:               string; 
  WhenInitiated:           option<DateTime>; 
  WhenDisconnected:        option<DateTime>; 
}




// Final Example

open System.IO 
open System.Text.RegularExpressions

// Download big text
let webClient = new WebClient()
let text = webClient.DownloadString(Uri("http://norvig.com/big.txt"))


// frequency count 
let NWORDS = 
    text |> (Regex "[a-zA-Z]+").Matches |> Seq.cast 
    |> Seq.map (fun (m:Match) -> m.Value.ToLower()) |> Seq.countBy id |> Map.ofSeq

let isKnown word = NWORDS.ContainsKey word 

/// Compute the 1-character edits of the word
let edits1 (word: string) = 
    let splits = [for i in 0 .. word.Length do yield (word.[0..i-1], word.[i..])]
    let deletes = [for a, b in splits do if b <> "" then yield a + b.[1..]]
    let transposes = [for a, b in splits do if b.Length > 1 then yield a + string b.[1] + string b.[0] + b.[2..]]
    let replaces = [for a, b in splits do for c in 'a'..'z' do if b <> "" then yield a + string c + b.[1..]]
    let inserts = [for a, b in splits do for c in 'a'..'z' do yield a + string c + b]
    deletes @ transposes @ replaces @ inserts |> Set.ofList

edits1 "speling"
edits1 "pgantom"

/// Compute the 1-character edits of the word which are actually words
let knownEdits1 word = 
    let result = [for w in edits1 word do if Map.containsKey w NWORDS then yield w] |> Set.ofList
    if result.IsEmpty then None else Some result 

knownEdits1 "fantom"
knownEdits1 "pgantom"

/// Compute the 2-character edits of the word which are actually words
let knownEdits2 word = 
    let result = [for e1 in edits1 word do for e2 in edits1 e1 do if Map.containsKey e2 NWORDS then yield e2] |> Set.ofList
    if result.IsEmpty then None else Some result 

knownEdits2 "pgantom"
knownEdits2 "quyck"


/// Find the best correction for a word, preferring 0-edit, over 1-edit, over 
/// 2-edit, and sorting by frequency.
let findBestCorrection (word: string) = 
    let words = 
        if isKnown word then Set.ofList [word] 
        else 
            match knownEdits1 word with
            | Some words -> words
            | None ->
            match knownEdits2 word with
            | Some words -> words
            | None -> Set.ofList [word]

    words |> Seq.sortBy (fun w -> -NWORDS.[w]) |> Seq.head

// Examples
findBestCorrection "speling"
findBestCorrection "korrecter"
findBestCorrection "fantom"
findBestCorrection "pgantom"