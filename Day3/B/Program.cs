using System.Text;

var lines = File.ReadAllLines("input.txt");

var runningTotal = 0L;
foreach (var line in lines)
{
    var span = line.AsSpan();
    var sb = new StringBuilder();

    var lastIndex = -1;
    while (sb.Length < 12)
    {
        var missingNumbersCount = 12-sb.Length;

        var minIndex = lastIndex + 1;
        var maxIndex = line.Length - missingNumbersCount;
        var highestNumber = FindHighestNumber(span, minIndex, maxIndex);
        
        sb.Append(highestNumber.Value);
        lastIndex = highestNumber.Index;
    }

    runningTotal += long.Parse(sb.ToString());
}

Console.WriteLine(runningTotal);

NumberLocation FindHighestNumber(ReadOnlySpan<char> span, int minIndex, int maxIndex)
{
    var highestNumber = span.ToArray().Select((x, i) => new NumberLocation{Value = int.Parse($"{x}"), Index = i}).Where(x => x.Index >= minIndex).Where(x => x.Index <= maxIndex).OrderByDescending(x => x.Value).First();
    return highestNumber;
}

class NumberLocation
{
    public int Index { get; set; }        
    public int Value { get; set; }        
}                                         
                                          
                                          