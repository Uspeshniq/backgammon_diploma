using System;
using Backgammon.Core.Types;

namespace Backgammon.Core.Rules;

// The same seed always gives the same rolls, which replay depends on.
public class Dice
{
    private readonly Random rng;

    public Dice(int seed)
    {
        rng = new Random(seed);
    }

    public DiceRoll Roll() => new DiceRoll(rng.Next(1, 7), rng.Next(1, 7));
}
