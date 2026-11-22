using System;
using System.Collections.Generic;

namespace Backgammon.Core.Types;

// The values of one roll (four values on a double) and which of them are used.
public class DiceRoll
{
    private readonly int[] values;
    private readonly bool[] used;

    public DiceRoll(int first, int second)
    {
        values = first == second
            ? new[] { first, first, first, first }
            : new[] { first, second };
        used = new bool[values.Length];
    }

    public IReadOnlyList<int> Values => values;

    public bool IsDouble => values.Length == 4;

    public bool IsUsed(int index) => used[index];

    public List<int> Remaining()
    {
        var remaining = new List<int>();
        for (int i = 0; i < values.Length; i++)
        {
            if (!used[i])
            {
                remaining.Add(values[i]);
            }
        }
        return remaining;
    }

    public void Use(int die)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == die && !used[i])
            {
                used[i] = true;
                return;
            }
        }
        throw new InvalidOperationException($"No unused die with value {die}.");
    }

    // A new roll with the same values and nothing used yet.
    public DiceRoll Copy() => new DiceRoll(values[0], values[1]);
}
