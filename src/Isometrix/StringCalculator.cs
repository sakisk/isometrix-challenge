using System.Linq;

namespace Isometrix;

public class StringCalculator
{
    public int Add(string numbers) => 
        numbers is not {Length: > 0} 
            ? 0 
            : numbers.Split(',', '\n').Sum(int.Parse);
}