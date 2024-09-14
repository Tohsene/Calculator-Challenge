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

        List<string> delimiters = new List<string> { "," };  // Default delimiter

        if (numbers.StartsWith("//"))
        {
            int delimiterEndIndex = numbers.IndexOf("\n");
            string delimiterSection = numbers.Substring(2, delimiterEndIndex - 2);

            if (delimiterSection.Contains("[") && delimiterSection.Contains("]"))
            {
                var delimiterMatches = delimiterSection.Split(new[] { "][" }, StringSplitOptions.None);
                delimiters = delimiterMatches.Select(d => d.Trim('[', ']')).ToList();
            }
            else
            {
                delimiters = new List<string> { delimiterSection };
            }
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        numbers = numbers.Replace("\n", delimiters[0]);
        var splitNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);

        List<int> parsedNumbers = splitNumbers.Select(n => int.TryParse(n, out int result) ? result : 0).ToList();
        var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();

        if (negativeNumbers.Any())
        {
            throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
        }

        parsedNumbers = parsedNumbers.Where(n => n <= 1000).ToList();
        string formula = string.Join("+", parsedNumbers);

        Console.WriteLine($"Formula: {formula} = {parsedNumbers.Sum()}");

        return parsedNumbers.Sum();
    }

}


