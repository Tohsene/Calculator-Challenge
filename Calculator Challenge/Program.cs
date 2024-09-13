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

        string delimiter = ",";  
        List<string> delimiters = new List<string> { delimiter };  

        if (numbers.StartsWith("//"))
        {
            int delimiterEndIndex = numbers.IndexOf("\n");
            string delimiterSection = numbers.Substring(2, delimiterEndIndex - 2);

            if (delimiterSection.StartsWith("[") && delimiterSection.EndsWith("]"))
            {
                delimiterSection = delimiterSection.Trim('[', ']');
                delimiters = new List<string> { delimiterSection };
            }
            else
            {
                delimiters = new List<string> { delimiterSection };
            }

            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        foreach (var del in delimiters)
        {
            numbers = numbers.Replace("\n", del);
        }

        var splitNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);

        List<int> parsedNumbers = splitNumbers.Select(n => int.TryParse(n, out int result) ? result : 0).ToList();

        var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();

        if (negativeNumbers.Any())
        {
            throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
        }

        parsedNumbers = parsedNumbers.Where(n => n <= 1000).ToList();

        return parsedNumbers.Sum();
    }
}


