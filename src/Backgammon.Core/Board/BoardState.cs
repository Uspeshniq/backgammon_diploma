using System;
using Backgammon.Core.Types;

namespace Backgammon.Core.Board;

// Only remembers where the checkers are. RulesEngine decides what is allowed.
// White moves from point 23 down to 0 and its home is 0-5.
// Black moves from point 0 up to 23 and its home is 18-23.
public class BoardState
{
    public const int PointCount = 24;
    public const int CheckersPerPlayer = 15;

    // Positive numbers are white checkers, negative numbers are black checkers.
    private readonly sbyte[] points = new sbyte[PointCount];
    private readonly int[] bar = new int[2];
    private readonly int[] off = new int[2];

    // The standard starting position.
    public BoardState()
    {
        points[23] = 2;
        points[12] = 5;
        points[7] = 3;
        points[5] = 5;

        points[0] = -2;
        points[11] = -5;
        points[16] = -3;
        points[18] = -5;
    }

    // Any position. The arrays are copied.
    public BoardState(sbyte[] points, int[] bar, int[] off)
    {
        Array.Copy(points, this.points, PointCount);
        Array.Copy(bar, this.bar, 2);
        Array.Copy(off, this.off, 2);
    }

    public int Count(int point, PlayerColor color)
    {
        int checkers = points[point];
        return color == PlayerColor.White ? Math.Max(checkers, 0) : Math.Max(-checkers, 0);
    }

    public int BarCount(PlayerColor color) => bar[(int)color];

    public int OffCount(PlayerColor color) => off[(int)color];

    public bool AllInHome(PlayerColor color)
    {
        if (BarCount(color) > 0)
        {
            return false;
        }

        int firstOutside = color == PlayerColor.White ? 6 : 0;
        for (int point = firstOutside; point < firstOutside + 18; point++)
        {
            if (Count(point, color) > 0)
            {
                return false;
            }
        }
        return true;
    }

    // Changes the numbers without checking legality. The only place checkers move.
    public void MoveChecker(PlayerColor color, int from, int to)
    {
        sbyte one = color == PlayerColor.White ? (sbyte)1 : (sbyte)-1;

        if (from == Move.Bar)
        {
            bar[(int)color]--;
        }
        else
        {
            points[from] -= one;
        }

        if (to == Move.Bar)
        {
            bar[(int)color]++;
        }
        else if (to == Move.Off)
        {
            off[(int)color]++;
        }
        else
        {
            points[to] += one;
        }
    }

    public BoardState Clone() => new BoardState(points, bar, off);
}
