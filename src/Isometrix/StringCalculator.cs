using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Isometrix;

public class StringCalculator
{
    private static readonly ImmutableList<char> Delimiters = new[] {',', '\n'}.ToImmutableList();

    public int Add(string numbers)
    {
        if (numbers is not {Length: > 0})
            return 0;

        if (TryGetExtraDelimiter(numbers, out var extraDelimiter))
        {
            numbers = RemoveDelimiterDefinition(numbers);
        }

        var ints = numbers.Split(Delimiters.Add(extraDelimiter).ToArray()).Select(int.Parse).ToImmutableList();
        
        if (ints.Any(x => x < 0))
            throw new InvalidOperationException("Negatives not allowed");
        
        return ints.Sum();
    }

    private static bool HasDelimiterDefinition(string numbers) => numbers.StartsWith("//");
    
    private static bool TryGetExtraDelimiter(string numbers, out char extraDelimiter)
    {
        if (HasDelimiterDefinition(numbers))
        {
            extraDelimiter = numbers[2];
            return true;
        }

        extraDelimiter = default;
        return false;
    }

    private static string RemoveDelimiterDefinition(string numbers)
    {
        var firstNewLine = numbers.IndexOf('\n');
        return numbers[(firstNewLine + 1)..];
    }
}