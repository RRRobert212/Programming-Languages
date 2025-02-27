open System

//big problems here with negative numbers, the binary representation is actually right, but the conversion from binary back to dec is wrong
    
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




// let result () = 
//     printf "\nEnter Desired Operation: NOT, OR, AND, XOR, ADD, SUB, QUIT: "
//     match Console.ReadLine() with
//     | "NOT" ->
//         printf "\nEnter Hex Value: "
//         let input = Console.ReadLine()
//         let notInput = (NOT (stringToInt input 16 |> unsignedToBinaryList))
//         printfn "Result of NOT %s = %A = %X" input notInput (binaryListToUnsigned notInput)
//         true


let test () =
    let number1 = unsignedToBinaryList 100
    let number2 = unsignedToBinaryList 10
    printfn "\nNUM 1: %A" number1
    printfn "\nNUM 2: %A" number2
    let flipped = NOT number1
    printfn "\nNOT NUM 1: %A" flipped
    let anded = AND number1 number2
    printfn "\nANDED: %A" anded
    let ored = OR number1 number2
    printfn "\nORED: %A" ored
    let xored = XOR number1 number2
    printfn "\nXORED %A" xored
    let newInt = binaryListToSigned number1
    printfn "\n n as a decimal: %d" newInt
    let added = ADD number1 number2
    printfn "\n n1 and n2 added: %A" added




[<EntryPoint>]
let main args = 
    test()
    0