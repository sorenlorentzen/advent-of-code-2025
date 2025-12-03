// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

var lines = File.ReadAllLines("input.txt");

var runningTotal = 0;

foreach(var line in lines)
{
    var span = line.AsSpan().Slice(0, line.Length-1);
    var firstNumber = FindHighestNumber(span);

    var subSpan = line.AsSpan().Slice(line.IndexOf(firstNumber.ToString())+1);
    var secondNumber = FindHighestNumber(subSpan);

    var numberString = $"{firstNumber}{secondNumber}";
    var parsedNumber = int.Parse(numberString);

    Console.WriteLine(parsedNumber);
    runningTotal += parsedNumber;
}

Console.WriteLine();
Console.WriteLine(runningTotal);


int FindHighestNumber(ReadOnlySpan<char> span)
{
    var highestNumber = span.ToArray().Select(x => int.Parse($"{x}")).OrderByDescending(x => x).First();
    return highestNumber;
}