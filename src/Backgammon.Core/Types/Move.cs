namespace Backgammon.Core.Types;

// One checker moved with one die.
public readonly record struct Move(int From, int To, int Die)
{
    public const int Bar = -1;
    public const int Off = 24;
}
