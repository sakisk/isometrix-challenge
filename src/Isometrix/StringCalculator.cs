using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace Isometrix;

public class StringCalculator
{
    private const char Comma = ',';
    private const char NewLine = '\n';
    private const string DelimiterDefinitionStart = "//";

    public int Add(string numbers)
    {
        if (numbers is not {Length: > 0})
            return 0;

        numbers = NomarmaliseDelimiters(numbers);

        var ints = numbers.Split(Comma).Select(int.Parse).ToImmutableList();

        if (HasNegativeNumbers(ints, out var negatives))
            throw new InvalidOperationException($"Negatives not allowed: {negatives}");

        return ints.Where(number => number <= 1000).Sum();
    }

    private string NomarmaliseDelimiters(string numbers)
    {
        if (!TryParseDelimiterDefinition(numbers, out var parsedDelimiter)) 
            return numbers.Replace(NewLine, Comma);
        
        numbers = RemoveDelimiterDefinition(numbers);
        return numbers
            .Replace(parsedDelimiter!, Comma.ToString())
            .Replace(NewLine, Comma);
    }
    
    private static bool HasNegativeNumbers(IEnumerable<int> numbers, out string negatives)
    {
        negatives = string.Join(Comma, numbers.Where(number => number < 0));

        return negatives is {Length: > 0};
    }

    private static bool HasDelimiterDefinition(string numbers) => numbers.StartsWith(DelimiterDefinitionStart);

    private static bool TryParseDelimiterDefinition(string numbers, out string? parsedDelimiter)
    {
        if (HasDelimiterDefinition(numbers))
        {
            parsedDelimiter = TryGetMultipleLengthDelimiter(numbers, out var delimiter)
                ? delimiter
                : numbers[2].ToString();
            return true;
        }

        parsedDelimiter = default;
        return false;
    }

    private static bool TryGetMultipleLengthDelimiter(string numbers, out string? delimiter)
    {
        var regex = Regex.Match(numbers, @"\/\/\[(?'delimiter'.*)\]\n");
        if (regex.Success)
        {
            delimiter = regex.Groups["delimiter"].Value;
            return true;
        }
        
        delimiter = default;
        return false;
    }

    private static string RemoveDelimiterDefinition(string numbers)
    {
        var firstNewLine = numbers.IndexOf(NewLine);
        return numbers[(firstNewLine + 1)..];
    }
}