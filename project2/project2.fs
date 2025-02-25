//Programming Languages Project 2//
//Funcitonal Programming//
//Simulated Binary Logic and Arithmetic Operations of CPU Written in F#//

open System
    
//-------------------------------------------------//
//LOGICAL OPERATORS//

//NOT FUNCTION - flips all bits in binary list//
let NOT (listIn: list<int>) : list<int>   = 
    listIn |> List.map (fun bit -> if bit = 0 then 1 else 0)

//AND FUNCTION - takes list1, list2 as input, creates new list with 1s where list1 and list2 has a 1//
let AND (list1: list<int>) (list2: list<int>) : list<int> =
    List.map2 (fun bit1 bit2 -> if bit1 = 1 && bit2 = 1 then 1 else 0) list1 list2

//OR FUNCTION - takes list1, list2 as input, creates new list with 1s where list1 or list2 has a 1//
let OR (list1: list<int>) (list2: list<int>) : list<int> =
    List.map2 (fun bit1 bit2 -> if bit1 = 1 || bit2 = 1 then 1 else 0) list1 list2

//XOR FUNCTION - takes list1, list2 as input, creates new list with 1s where list1 does not equal list2//
let XOR (list1: list<int>) (list2: list<int>) : list<int> =
    List.map2 (fun bit1 bit2 -> if bit1 <> bit2 then 1 else 0) list1 list2

//-------------------------------------------------//

//ADDITION FUNCTION//
//-------------------------------------------------//

let ADD (list1: list<int>) (list2: list<int>) : list<int> =
    let rev1 = List.rev list1
    let rev2 = List.rev list2

    let rec adder (l1: list<int>) (l2: list<int>) (carry: int) (acc: list<int>) = 
        match l1, l2 with
        | head1::tail1, head2::tail2 ->
            let sum = head1 + head2 + carry
            let nextCarry = sum / 2
            let bit = sum % 2
            adder tail1 tail2 nextCarry (bit::acc)
        | [], [] -> 
            if carry = 1 then carry::acc else acc
        | [], _ -> 
            adder [] l2 carry acc
        | _, [] -> 
            adder l1 [] carry acc
    let result = adder rev1 rev2 0 []

    let truncated = 
        if List.length result > 8 then
            List.skip (List.length result - 8) result
        else
            List.replicate (8 - List.length result) 0 @ result

    truncated

//no subtraction function, it's handled in the mainloop
//-------------------------------------------------//


//CONVERSION FUNCTIONS//
//-------------------------------------------------//
//convert string inputs to ints of base 10 or 16//
let stringToInt (input: string) (toBase:int) = 
    System.Convert.ToInt32(input, toBase)


//convert positive int to negative with twoscompliment//
let twosCompliment (positiveList: list<int>) = 
    let inverted = NOT positiveList
    let one = [0; 0; 0; 0; 0; 0; 0; 1]
    ADD inverted one

//convert unsigned base 10 to binary//
let unsignedToBinaryList (n: int) : list<int> = 
    //first absvalue to binary, then later check if original num is negative, if so, output 2s compliment
    let absN = abs n

    let rec makeList l bitsLeft value = 
        if bitsLeft = 0 then
            l
        else
            let bit = value % 2
            makeList (bit :: l) (bitsLeft - 1) (value/2)

    let positiveList = makeList [] 8 absN
    if n>= 0 then 
        positiveList
    else
        twosCompliment positiveList


//convert binary to signed integer//
let binaryListToSigned (binaryList: list<int>) =
//multiplying the first bit by 0 rather than 128
    let pow2list = [ -128; 64; 32; 16; 8; 4; 2; 1 ]
    let result = List.fold2 (fun acc bit power -> acc + bit * power) 0 binaryList pow2list

    result

//-------------------------------------------------//


//MAIN LOOPING FUNTION//
//-------------------------------------------------//
let rec mainLoop () = 
    printf "\nEnter Desired Operation: NOT, OR, AND, XOR, ADD, SUB, QUIT: "
    match Console.ReadLine() with
    | "NOT" ->
        printf "\nEnter Hex Value: "
        let n1 = Console.ReadLine()
        let notN1 = NOT (stringToInt n1 16 |> unsignedToBinaryList)
        let hexResult = binaryListToSigned notN1
        let hexString = sprintf "%02X" hexResult  //string to print only last 2 digits of hex
        printfn "Result of NOT %s = %A = 0x%s" n1 notN1 (hexString.Substring(hexString.Length - 2))

        mainLoop()
    | "OR" ->
        printf "\n Enter first hex value: "
        let n1String = Console.ReadLine()
        let n1 = (stringToInt n1String 16 |> unsignedToBinaryList)
        printf "\n Enter second hex value: "
        let n2String = Console.ReadLine()
        let n2 = (stringToInt n2String 16 |> unsignedToBinaryList)
        let orN1N2 = (OR n1 n2)
        printfn "Result of %s OR %s = %A = 0X%X" n1String n2String orN1N2 (binaryListToSigned orN1N2)

        mainLoop()
    | "AND" ->
        printf "\n Enter first hex value: "
        let n1String = Console.ReadLine()
        let n1 = (stringToInt n1String 16 |> unsignedToBinaryList)
        printf "\n Enter second hex value: "
        let n2String = Console.ReadLine()
        let n2 = (stringToInt n2String 16 |> unsignedToBinaryList)
        let andN1N2 = (AND n1 n2)
        printfn "Result of %s AND %s = %A = 0X%X" n1String n2String andN1N2 (binaryListToSigned andN1N2)

        mainLoop()
    | "XOR" ->
        printf "\n Enter first hex value: "
        let n1String = Console.ReadLine()
        let n1 = (stringToInt n1String 16 |> unsignedToBinaryList)
        printf "\n Enter second hex value: "
        let n2String = Console.ReadLine()
        let n2 = (stringToInt n2String 16 |> unsignedToBinaryList)
        let xorN1N2 = (XOR n1 n2)
        printfn "Result of %s or %s = %A = 0X%X" n1String n2String xorN1N2 (binaryListToSigned xorN1N2)

        mainLoop()
    | "ADD" ->
        printf "\n Enter first decimal value: "
        let n1String = Console.ReadLine()
        let n1 = (stringToInt n1String 10 |> unsignedToBinaryList)
        printf "\n Enter second decimal value: "
        let n2String = Console.ReadLine()
        let n2 = (stringToInt n2String 10 |> unsignedToBinaryList)
        let addedN1N2 = (ADD n1 n2)
        printfn "Result of %s PLUS %s = %A = %d" n1String n2String addedN1N2 (binaryListToSigned addedN1N2)

        mainLoop()
    | "SUB" ->
        printf "\n Subtracting n2 from n1, enter n1 as a decimal: "
        let n1String = Console.ReadLine()
        let n1 = (stringToInt n1String 10 |> unsignedToBinaryList)
        printf "\n Enter n2 as a decimal: "
        let n2String = Console.ReadLine()
        let n2temp = (stringToInt n2String 10)
        let n2 = (-1 * n2temp |> unsignedToBinaryList)
        let subbedN1N2 = (ADD n1 n2)
        printfn "Result of %s MINUS %s = %A = %d" n1String n2String subbedN1N2 (binaryListToSigned subbedN1N2)

        mainLoop()
    | "QUIT" ->
        printf "\n Quitting Program...\n"
    | _ ->
        printf "\nInvalid input, please type one of the operations listed below exactly as they are written:\n"
        mainLoop()

//-------------------------------------------------//


//TESTING FUNCITON
// let test () =
//     let number1 = unsignedToBinaryList -100
//     let number2 = unsignedToBinaryList 29
//     printfn "\nNUM 1: %A" number1
//     printfn "\nNUM 2: %A" number2
//     let flipped = NOT number1
//     printfn "\nNOT NUM 1: %A" flipped
//     let anded = AND number1 number2
//     printfn "\nANDED: %A" anded
//     let ored = OR number1 number2
//     printfn "\nORED: %A" ored
//     let xored = XOR number1 number2
//     printfn "\nXORED %A" xored
//     let newInt = binaryListToSigned number1
//     printfn "\n n as a decimal: %d" newInt
//     let added = ADD number1 number2
//     printfn "\n n1 and n2 added: %A" added
//     let addedBackToInt = binaryListToSigned added
//     printfn "\n n1+n2 as an int %d" addedBackToInt


//MAIN//

[<EntryPoint>]
let main args = 
    mainLoop()
    0