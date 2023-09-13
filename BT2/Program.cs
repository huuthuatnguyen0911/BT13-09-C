using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Nhap vao chuoi so");
        List<int> numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
        List<int> result = new List<int>();

        int start = 0;
        int end = numbers.Count - 1;

        while (start <= end)
        {
            if (start == end)
            {
                result.Add(numbers[start]);
            }
            else
            {
                result.Add(numbers[start] + numbers[end]);
            }
            start++;
            end--;
        }

        Console.WriteLine(string.Join(" ", result));
    }
}
