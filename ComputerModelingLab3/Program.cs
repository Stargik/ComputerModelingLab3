using System.Text.Json;

namespace ComputerModelingLab3;

record Point(double x, double y);

class Program
{
    static void Main(string[] args)
    {
        double k, x0, y0, ys, a, b, h; 
        Console.Write("Enter k: ");
        Double.TryParse(Console.ReadLine(), out k);

        Console.Write("Enter x0: ");
        Double.TryParse(Console.ReadLine(), out x0);

        Console.Write("Enter y0: ");
        Double.TryParse(Console.ReadLine(), out y0);

        Console.Write("Enter ys: ");
        Double.TryParse(Console.ReadLine(), out ys);

        Console.Write("Enter a: ");
        Double.TryParse(Console.ReadLine(), out a);

        Console.Write("Enter b: ");
        Double.TryParse(Console.ReadLine(), out b);

        Console.Write("Enter h: ");
        Double.TryParse(Console.ReadLine(), out h);

        //Func<double, double, double> f = (x, y) => x + y;
        Func<double, double, double> f = (x, y) => -k * (y - ys);

        Console.WriteLine($"f = -({k}) * (y - ({ys}))");

        //EulerMethod(0, 1, 0.1, 0, 0.5, f);
        //RungeKutta4(0, 1, 0.1, 0, 0.5, f);

        EulerMethod(x0, y0, h, a, b, f);
        RungeKutta4(x0, y0, h, a, b, f);
    }

    public static void EulerMethod(double x0, double y0, double h, double a, double b, Func<double, double, double> f)
    {
        List<Point> points = new List<Point>();
        List<double> xList = new List<double>();
        List<double> yList = new List<double>();
        double xi = x0;
        double yi = y0;
        while (xi <= b)
        {
            xList.Add(xi);
            xi = xi + h;
        }

        yList.Add(yi);
        Console.WriteLine($"{xList[0]} | {yList[0]}");
        for (int i = 1; i < xList.Count; i++)
        {
            yi = yi + h * f(xList[i - 1], yi);
            yList.Add(yi);
            Console.WriteLine($"{Math.Round(xList[i], 8)} | {Math.Round(yList[i], 8)}");
        }

        for (int i = 0; i < xList.Count; i++)
        {
            points.Add(new Point(Math.Round(xList[i], 8), Math.Round(yList[i], 8)));
        }

        string jsonString = JsonSerializer.Serialize(points);
        string fileName = "../../../EulerMethod.json";
        File.WriteAllText(fileName, jsonString);
        Console.WriteLine(File.ReadAllText(fileName));
    }

    public static void RungeKutta4(double x0, double y0, double h, double a, double b, Func<double, double, double> f)
    {
        List<Point> points = new List<Point>();
        List<double> xList = new List<double>();
        List<double> yList = new List<double>();
        double xi = x0;
        double yi = y0;
        while (xi <= b)
        {
            xList.Add(xi);
            xi = xi + h;
        }

        yList.Add(yi);

        Console.WriteLine($"{xList[0]} | {yList[0]}");
        for (int i = 1; i < xList.Count; i++)
        {
            double k1 = h * f(xList[i - 1], yi);
            double k2 = h * f(xList[i - 1] + h / 2, yi + k1 / 2);
            double k3 = h * f(xList[i - 1] + h / 2, yi + k2 / 2);
            double k4 = h * f(xList[i - 1] + h, yi + k3);
            yi = yi + (1.0 / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
            yList.Add(yi);
            Console.WriteLine($"{Math.Round(xList[i], 8)} | {Math.Round(yList[i], 8)}");
        }

        for (int i = 0; i < xList.Count; i++)
        {
            points.Add(new Point(Math.Round(xList[i], 8), Math.Round(yList[i], 8)));
        }

        string jsonString = JsonSerializer.Serialize(points);
        string fileName = "../../../RungeKutta4.json";
        File.WriteAllText(fileName, jsonString);
        Console.WriteLine(File.ReadAllText(fileName));
    }
}

