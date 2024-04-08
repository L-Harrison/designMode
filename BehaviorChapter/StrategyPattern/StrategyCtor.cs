namespace StrategyPattern;

public class StrategyCtor
{
    public void USBStategy()
    {
        #region 计算机Demo
        Computer computer = new Computer();
        computer.SetUSB(new KeyBoard());
        computer.Read();
        computer.SetUSB(new Mouse());
        computer.Read();
        computer.SetUSB(new Camera());
        computer.Read();
        #endregion
    }
    public void CaculateStategy()
    {
        #region 计算器Demo
        int a = 3;
        int b = 4;
        Calculator calculator = new Calculator();
        calculator.SetStrategy(new Addition());
        Console.WriteLine(calculator.Calculate(a, b));
        calculator.SetStrategy(new Subtraction());
        Console.WriteLine(calculator.Calculate(a, b));
        calculator.SetStrategy(new Multiply());
        Console.WriteLine(calculator.Calculate(a, b));
        calculator.SetStrategy(new Division());
        Console.WriteLine(calculator.Calculate(a, b));
        Console.ReadKey();
        #endregion
    }
}

#region 策略模式在计算器上的应用

#region 计算策略
public interface Strategy
{
    double Calculate(double a, double b);
}
public class Addition : Strategy
{
    public double Calculate(double a, double b)
    {
        return a + b;
    }
}
public class Subtraction : Strategy
{
    public double Calculate(double a, double b)
    {
        return a - b;
    }
}
public class Division : Strategy
{
    public double Calculate(double a, double b)
    {
        return a / b;
    }
}
public class Multiply : Strategy
{
    public double Calculate(double a, double b)
    {
        return a * b;
    }
}
#endregion

#region 计算器类
public class Calculator
{
    Strategy Strategy;
    public void SetStrategy(Strategy strategy)
    {
        this.Strategy = strategy;
    }

    public double Calculate(double a, double b)
    {
        return this.Strategy.Calculate(a, b);
    }
}
#endregion
#endregion

#region 策略模式在计算机上的应用
#region USB策略
public interface USB
{
    void Read();
}
public class KeyBoard : USB
{
    public void Read()
    {
        Console.WriteLine("计算机读取到键盘输入信息");
    }
}
public class Mouse : USB
{
    public void Read()
    {
        Console.WriteLine("计算机读取到鼠标移动信息");
    }
}
public class Camera : USB
{
    public void Read()
    {
        Console.WriteLine("计算机读取到相机采图信息");
    }
}
#endregion

#region 计算机类
public class Computer
{
    private USB USB;
    public void SetUSB(USB usb)
    {
        this.USB = usb;
    }
    public void Read()
    {
        this.USB.Read();
    }
}
#endregion
#endregion