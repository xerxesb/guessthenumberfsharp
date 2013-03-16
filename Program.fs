// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System;

type Game = {
    ActualNumber : int
    GuessedNumber : int
    NumberOfGuesses : int
}

type Guess =
    | Low
    | Equal
    | High


let getGuess () =
    stdout.Write "Guess the number: "
    stdin.ReadLine() |> int


let (|GuessType|_|) (guess :int) (actual : int) =
    if (guess < actual) then Some Low
    elif (guess > actual) then Some High
    else Some Equal
 

let getDesc guess actual =
    match actual with
    | GuessType guess Low -> printfn "Too low!" |> ignore
    | GuessType guess High -> printfn "Too high!" |> ignore
    | GuessType guess Equal -> printfn "Correct!" |> ignore
    | _ -> ()


(*
//example of parameterized active pattern
let (|MultipleOf|_|) (multiplicand : int) (n : int) =
    Some (n / multiplicand, n % multiplicand)


let mult (n : int) (x :int) =
    match n with
    | MultipleOf x (_,0) -> printfn "%A is multiple of %A" n x
    | MultipleOf x (m,_) -> printfn "%A is not multiple of %A" n x
    | _ -> ()
*)

let rec runGame game =
    if (game.GuessedNumber = game.ActualNumber) then game
    else
        let nextIter = { game with GuessedNumber = getGuess (); NumberOfGuesses = game.NumberOfGuesses + 1 }
        getDesc nextIter.GuessedNumber nextIter.ActualNumber
        runGame nextIter 


[<EntryPoint>]
let main argv = 
    let rand = (new Random()).Next(1, 10)
    let game = { Game.ActualNumber = rand; GuessedNumber = 0; NumberOfGuesses = 0 }
    let x = runGame game
    printfn "You guessed in %A turn(s)" x.NumberOfGuesses

    printfn "Press any key to exit..." |> ignore
    stdin.Read() |> ignore
    0 
