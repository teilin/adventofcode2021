using System.Diagnostics.CodeAnalysis;
using static adventofcode.Day05.HydrothermalVenture;

namespace adventofcode.Day05;

public sealed class HydrothermalVenture : ISolver
{
    public IList<Line> _lines = new List<Line>();

    public async ValueTask ExecutePart1(string inputFilePath)
    {
        _lines  = (await File.ReadAllLinesAsync(inputFilePath))
            .Select(s => s.ToLine())
            .ToList();

        var visited = new Dictionary<Point,int>();

        foreach(var line in _lines)
        {
            var points = PointsOnLine(line, false);
            foreach(var p in points)
            {
                if(visited.ContainsKey(p))
                {
                    visited[p] = visited[p]+1;
                }
                else
                {
                    visited.Add(p, 1);
                }
            }
        }
        var count = visited.Where(w => w.Value > 1).Count();
        Console.WriteLine(count);
    }

    public ValueTask ExecutePart2(string inputFilePath)
    {
        var visited = new Dictionary<Point,int>();

        foreach(var line in _lines)
        {
            var points = PointsOnLine(line, true);
            foreach(var p in points)
            {
                if(visited.ContainsKey(p))
                {
                    visited[p] = visited[p]+1;
                }
                else
                {
                    visited.Add(p, 1);
                }
            }
        }
        var count = visited.Where(w => w.Value > 1).Count();
        Console.WriteLine(count);
        return ValueTask.CompletedTask;
    }

    public bool ShouldExecute(int day)
    {
        return day == 5;
    }

    private Point[] PointsOnLine(Line line, bool includeDiagonals)
    {
        var points = new List<Point>();
        if(line.Start.X == line.End.X)
        {
            if(line.Start.Y <= line.End.Y)
            {
                for(var i = line.Start.Y;i<=line.End.Y;i++)
                {
                    var p = new Point(line.Start.X, i);
                    points.Add(p);
                }
            }
            else
            {
                for(var i = line.Start.Y;i>=line.End.Y;i--)
                {
                    var p = new Point(line.Start.X, i);
                    points.Add(p);
                }
            }
        }
        else if(line.Start.Y == line.End.Y)
        {
            if(line.Start.X <= line.End.X)
            {
                for(var i = line.Start.X;i<=line.End.X;i++)
                {
                    var p = new Point(i,line.Start.Y);
                    points.Add(p);
                }
            }
            else
            {
                for(var i = line.Start.X;i>=line.End.X;i--)
                {
                    var p = new Point(i,line.Start.Y);
                    points.Add(p);
                }
            }
        }
        else
        {
            if(includeDiagonals)
            {
                int initX = line.Start.X, initY = line.Start.Y;
                int dirX = line.End.X > line.Start.X ? 1 : -1;
                int dirY = line.End.Y > line.Start.Y ? 1 : -1;
                while(initX != line.End.X && initY != line.End.Y)
                {
                    var p = new Point(initX, initY);
                    points.Add(p);

                    initX += dirX;
                    initY += dirY;
                }
                var pend = new Point(line.End.X, line.End.Y);
                points.Add(pend);
            }
        }
        return points.ToArray();
    }

    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; init; }
        public int Y { get; init; }

        public bool Equals(Point other)
        {
            return X == other.X &&
                    Y == other.Y;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if(obj == null) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }
    }

    public struct Line
    {
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Point Start { get; init; }
        public Point End { get; init; }

        public bool Equals(Line other)
        {
            return Start.Equals(other.Start) &&
                    End.Equals(other.End);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if(obj == null) return false;
            return Equals((Line)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start.GetHashCode(), End.GetHashCode());
        }
    }
}

public static class Extensions
{
    public static Point ToPoint(this string str)
    {
        var array = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return new Point(int.Parse(array[0]), int.Parse(array[1]));
    }

    public static Line ToLine(this string str)
    {
        var array = str.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
        return new Line(array[0].ToPoint(),array[1].ToPoint());
    }
}