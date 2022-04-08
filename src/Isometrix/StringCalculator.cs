namespace Isometrix;

public class StringCalculator
{
    public int Add(string numbers)
    {
        if (numbers is {Length: > 0})
            return int.Parse(numbers);
        
        return 0;
    }
}