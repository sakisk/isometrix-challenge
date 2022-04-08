using System.Collections.Generic;
using System.Linq;

namespace Isometrix;

public class StringCalculator
{
    public int Add(string numbers)
    {
        if (numbers is not {Length: > 0})
            return 0;

        var delimiters = new List<char>(new[] {',', '\n'});
        
        if (numbers.StartsWith("//"))
        {
            delimiters.Add(numbers[2]);
            var firstNewLine = numbers.IndexOf('\n');
            numbers = numbers.Substring(firstNewLine + 1);
        }

        return numbers.Split(delimiters.ToArray()).Sum(int.Parse);
    }
}