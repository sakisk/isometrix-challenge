namespace Isometrix;

public class StringCalculator
{
    public int Add(string numbers)
    {
        if (numbers is not {Length: > 0})
        {
            return 0;
        }

        var result = 0;
        foreach (var number in numbers.Split(','))
        {
            result += int.Parse(number);
        }

        return result;
    }
}