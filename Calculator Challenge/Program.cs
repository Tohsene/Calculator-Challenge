// See https://aka.ms/new-console-template for more information

using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

var calculator = new StringCalculator();

while (true)
{
    Console.WriteLine("Enter numbers to add (or type 'exit' to quit):");
    string input = Console.ReadLine();

    if (input?.ToLower() == "exit")
        break;

    try
    {
        int result = calculator.Add(input);
        Console.WriteLine($"Result: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

//public class StringCalculator
//{
//    public int Add(string numbers)
//    {
//        if (string.IsNullOrEmpty(numbers)) return 0;

//        // Split on both commas and newline characters
//        var splitNumbers = numbers.Split(new[] { ',', '\n' }, StringSplitOptions.None);

//        // Convert and sum valid numbers
//        return splitNumbers.Sum(n => int.TryParse(n, out int result) ? result : 0);
//    }

//}
public class StringCalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers)) return 0;

        numbers = numbers.Replace("\n", ",");
        var splitNumbers = numbers.Split(new[] { ',' }, StringSplitOptions.None);

        return splitNumbers.Sum(n => int.TryParse(n, out int result) ? result : 0);
    }
}


