// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<ICalculator, StringCalculator>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var calculator = serviceProvider.GetService<ICalculator>();

try
{
    Console.Write("Enter numbers: ");
    string input = Console.ReadLine();

    Console.Write("Enter operation (+, -, *, /): ");
    string operation = Console.ReadLine();

    string delimiter = ","; // Default delimiter
    bool allowNegatives = false; // Default: don't allow negatives
    int upperBound = 1000; // Default upper bound

    // Perform calculation with custom operation
    int result = calculator.Calculate(input, operation, delimiter, allowNegatives, upperBound);
    Console.WriteLine($"Result: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
public interface ICalculator
{
    int Calculate(string numbers, string operation = "+", string delimiter = ",", bool allowNegatives = false, int upperBound = 1000);
}

public class StringCalculator : ICalculator
{
    public int Calculate(string numbers, string operation = "+", string delimiter = ",", bool allowNegatives = false, int upperBound = 1000)
    {
        if (string.IsNullOrEmpty(numbers)) return 0;

        // Set custom delimiters, including newline as default alternative delimiter
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

        // Replace newline with the provided delimiter
        numbers = numbers.Replace("\n", delimiter);

        // Split the input string into individual numbers based on the delimiters
        var splitNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);

        // Parse numbers, convert invalid entries to 0
        List<int> parsedNumbers = splitNumbers.Select(n => int.TryParse(n, out int result) ? result : 0).ToList();

        // If negatives are not allowed, check for negative numbers
        if (!allowNegatives)
        {
            var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }
        }

        // Ignore numbers greater than the upper bound
        parsedNumbers = parsedNumbers.Where(n => n <= upperBound).ToList();

        // Perform the operation
        int result = 0;
        switch (operation)
        {
            case "+":
                result = parsedNumbers.Sum();
                break;
            case "-":
                result = parsedNumbers.Aggregate((a, b) => a - b); // Subtract all numbers sequentially
                break;
            case "*":
                result = parsedNumbers.Aggregate(1, (a, b) => a * b); // Multiply all numbers sequentially
                break;
            case "/":
                result = parsedNumbers.Aggregate((a, b) =>
                {
                    if (b == 0) throw new DivideByZeroException("Division by zero is not allowed");
                    return a / b;
                }); // Divide all numbers sequentially
                break;
            default:
                throw new InvalidOperationException($"Unsupported operation: {operation}");
        }

        return result;
    }

}



