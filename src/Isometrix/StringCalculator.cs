namespace Isometrix;

public class StringCalculator
{
    public int Add(string numbers) => numbers is {Length: > 0} ? int.Parse(numbers): 0;
}