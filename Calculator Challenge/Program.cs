// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<ICalculator, StringCalculator>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var calculator = serviceProvider.GetService<ICalculator>();

while (true)
{
    try
    {
        Console.Write("Enter numbers: ");
        string input = Console.ReadLine();
        int result = calculator.Add(input);
        Console.WriteLine($"Result: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
public interface ICalculator
{
    int Add(string numbers, string delimiter = ",", bool allowNegatives = false, int upperBound = 1000);
}

public class StringCalculator : ICalculator
{
    public int Add(string numbers, string delimiter = ",", bool allowNegatives = false, int upperBound = 1000)
    {
        if (string.IsNullOrEmpty(numbers)) return 0;

        List<string> delimiters = new List<string> { delimiter, "\n" };

        if (numbers.StartsWith("//"))
        {
            int delimiterEndIndex = numbers.IndexOf("\n");
            string delimiterSection = numbers.Substring(2, delimiterEndIndex - 2);

            // Support custom delimiters with varying lengths
            if (delimiterSection.StartsWith("[") && delimiterSection.EndsWith("]"))
            {
                var delimiterMatches = delimiterSection.Trim('[', ']').Split("][");
                delimiters = delimiterMatches.ToList();
            }
            else
            {
                delimiters = new List<string> { delimiterSection };
            }

            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        numbers = numbers.Replace("\n", delimiter);

        var splitNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);

        List<int> parsedNumbers = splitNumbers.Select(n => int.TryParse(n, out int result) ? result : 0).ToList();

        if (!allowNegatives)
        {
            var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }
        }

        parsedNumbers = parsedNumbers.Where(n => n <= upperBound).ToList();

        return parsedNumbers.Sum();
    }
}



