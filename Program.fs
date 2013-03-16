// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System;

type Game = {
    ActualNumber : int
    GuessedNumber : int
    NumberOfGuesses : int
}


let getGuess () =
    stdout.Write "Guess the number: "
    stdin.ReadLine() |> int


let (|Low|High|Equal|) (guess, actual) =
    if (guess < actual) then Low
    elif (guess > actual) then High
    else Equal
 

let getDesc guess actual : string =
    match (guess, actual) with
    | Low -> "Too low!"
    | High -> "Too high!"
    | Equal -> "Correct!"


let rec runGame game =
    if (game.GuessedNumber = game.ActualNumber) then game
    else
        let nextIter = { game with GuessedNumber = getGuess (); NumberOfGuesses = game.NumberOfGuesses + 1 }
        getDesc nextIter.GuessedNumber nextIter.ActualNumber |> stdout.WriteLine |> ignore  // why cant i printfn here instead?
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
