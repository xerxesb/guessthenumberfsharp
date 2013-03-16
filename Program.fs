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


let getDesc guess actual =
    match guess with
    | x when x > actual -> printfn "Too high!" |> ignore 
    | x when x < actual -> printfn "Too low!" |> ignore 
    | _ -> printfn "Correct" |> ignore


let rec runGame game =
    if (game.GuessedNumber = game.ActualNumber) then game
    else
        getDesc(game.GuessedNumber, game.ActualNumber) |> ignore
        let nextIter = { game with GuessedNumber = getGuess (); NumberOfGuesses = game.NumberOfGuesses + 1 }
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
