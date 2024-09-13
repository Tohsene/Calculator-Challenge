// See https://aka.ms/new-console-template for more information

using System;

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

public class StringCalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers)) return 0;

        var splitNumbers = numbers.Split(new[] { ',', '\n' }, StringSplitOptions.None);
        if (splitNumbers.Length > 2)
            throw new ArgumentException("More than two numbers provided");

        return splitNumbers.Sum(n => int.TryParse(n, out int result) ? result : 0);
    }
}


