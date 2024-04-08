## 16 策略模式
 
策略模式（strategy pattern):封装不同的算法，算法间能相互替换。

> ***策略模式强调行为的灵活切换。*** 比如：一个类的多个方法有着类似的行为接口，可以将他们抽离出来作为一系列策略类，在运行时灵活对接不同的策略类，以适应不同的场景。

优点(即插即用)
        
    策略模式：让策略与系统环境彻底解耦，通过对算法策略的抽象、拆分，再拼装，接入外设，使系统行为可塑性提高。
    策略接口的引入也让各种策略实现彻底解放，最终实现算法独立，即插即用。
       

***举例如下***

```

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
```

***调用者调用***

```
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
```
  //优点(即插即用)
        //策略模式：让策略与系统环境彻底解耦，通过对算法策略的抽象、拆分，再拼装，接入外设，使系统行为可塑性提高。
        //策略接口的引入也让各种策略实现彻底解放，最终实现算法独立，即插即用。
        //