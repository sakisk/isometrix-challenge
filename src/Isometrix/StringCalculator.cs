using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Isometrix;

public class StringCalculator
{
    private static readonly ImmutableList<char> DefaultDelimiters = new[] {',', '\n'}.ToImmutableList();

    public int Add(string numbers)
    {
        if (numbers is not {Length: > 0})
            return 0;

        numbers = NomarmaliseDelimitedNumbers(numbers);

        var ints = numbers.Split(DefaultDelimiters.ToArray()).Select(int.Parse).ToImmutableList();

        if (HasNegativeNumbers(ints, out var negatives))
            throw new InvalidOperationException($"Negatives not allowed: {string.Join(',', negatives)}");

        return ints.Where(number => number <= 1000).Sum();
    }

    private string NomarmaliseDelimitedNumbers(string numbers)
    {
        if (!TryParseDelimiterDefinition(numbers, out var parsedDelimiter)) 
            return numbers;
        
        numbers = RemoveDelimiterDefinition(numbers);
        return numbers.Replace(parsedDelimiter!, ",");
    }
    
    private static bool HasNegativeNumbers(IReadOnlyList<int> numbers, out IReadOnlyList<int> negatives)
    {
        negatives = numbers.Where(number => number < 0).ToImmutableList();

        return negatives.Any();
    }

    private static bool HasDelimiterDefinition(string numbers) => numbers.StartsWith("//");

    private static bool TryParseDelimiterDefinition(string numbers, out string? extraDelimiter)
    {
        if (HasDelimiterDefinition(numbers))
        {
            extraDelimiter = TryGetMultipleLengthDelimiter(numbers, out var delimiter)
                ? delimiter
                : numbers[2].ToString();
            return true;
        }

        extraDelimiter = default;
        return false;
    }

    private static bool TryGetMultipleLengthDelimiter(string numbers, out string? delimiter)
    {
        if (numbers[2] == '[')
        {
            delimiter = numbers[3..numbers.IndexOf(']')];
            return true;
        }
        
        delimiter = default;
        return false;
    }

    private static string RemoveDelimiterDefinition(string numbers)
    {
        var firstNewLine = numbers.IndexOf('\n');
        return numbers[(firstNewLine + 1)..];
    }
}