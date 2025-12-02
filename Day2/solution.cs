using System.Diagnostics;
using System.Text;

var allText = File.ReadAllText("input.txt");
var ranges = allText.Split(',');

var runningSum = 0L;

foreach(var range in ranges)
{
    var invalidIds = FindInvalidIdsInRange(range);
    if (invalidIds.Length > 0) {
        Console.WriteLine($"Invalid IDs in range {range}:");
        foreach(var i in invalidIds)
        {
            runningSum += i;
            Console.WriteLine(i);
        }

        Console.WriteLine();
    }
}

Console.WriteLine("Sum of all invalid IDs:");
Console.WriteLine(runningSum);


long[] FindInvalidIdsInRange(string range)
{
    var splitted = range.Split('-');
    var rangeStart = long.Parse(splitted[0]);
    var rangeEnd = long.Parse(splitted[1]);

    var list = new List<long>();

    var i = rangeStart;
    while (i <= rangeEnd)
    {
        var str = i.ToString().AsSpan();
        i++;

        var spanSize = 1;
        while (spanSize <= str.Length/2)
        {
            if (str.Length % spanSize == 0)
            {
                var firstPart = str.Slice(0, spanSize);
                var a = IsSpanMadeOfRepeats(str, firstPart);
                if (a)
                {
                    list.Add(long.Parse(str));
                }
            }

            spanSize++;
        }
        
    }
    return list.Distinct().ToArray();
}

bool IsSpanMadeOfRepeats(ReadOnlySpan<char> str, ReadOnlySpan<char> firstPart)
{
    var timesToRepeat = str.Length/firstPart.Length;

    var start = firstPart.Length;
    while (start < str.Length)
    {
        var nextSlize = str.Slice(start, firstPart.Length);
        var isEqual = firstPart.SequenceEqual(nextSlize);
        if (isEqual == false)
        {
            return false;
        }

        start += firstPart.Length;
    }
    return true;
}