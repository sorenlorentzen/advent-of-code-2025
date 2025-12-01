using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var dial = new Dial(50);

var timesDialIsPointingAtZero = 0;

foreach (var line in input)
{
    var instruction = RotationInstruction.ParseInstruction(line);
    var timesDialPointedAtZeroDuringRotation = dial.Rotate(instruction.Direction, instruction.Steps);
    timesDialIsPointingAtZero += timesDialPointedAtZeroDuringRotation;

}

Console.WriteLine($"The dial pointed at 0 a total of {timesDialIsPointingAtZero} times.");


public class Dial
{
    public int Position { get; private set; }

    public Dial(int startingPosition)
    {
        Position = startingPosition;
    }

    public int Rotate(Direction direction, int steps)
    {
        var retVal = 0;

        for (var i = 0; i < steps; i++)
        {
            RotateInternally(direction);
            if (Position == 0)
            {
                retVal++;
            }
        }

        Console.WriteLine($"Rotating {direction} by {steps} steps. New position: {Position}.");
        return retVal;
    }

    private void RotateInternally(Direction direction)
    {
        if (direction == Direction.Left)
        {
            Position = (Position + 99) % 100;
        }
        else
        {
            Position = (Position + 1) % 100;
        }
    }

}


public class RotationInstruction
{
    private static readonly Regex _instructionRegex = new(@"^(L|R)(\d+)$");
    public Direction Direction { get; private set; }
    public int Steps { get; private set; }

    public static RotationInstruction ParseInstruction(string instruction)
    {
        var match = _instructionRegex.Match(instruction);
        var parts = match.Groups.Values.Skip(1).Select(g => g.Value).ToArray();
        Direction direction = parts[0] == "L" ? Direction.Left : Direction.Right;
        int steps = int.Parse(parts[1]);
        return new RotationInstruction { Direction = direction, Steps = steps };
    }

}

public enum Direction
{
    Left,
    Right
}