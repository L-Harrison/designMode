## 01单例模式

单例模式：指一个系统中只存在一个实例，同时提供集中统一的接口，以使系统行为保持一致。

> singleton一词在逻辑学中指“有且仅有一个元素的集合”，这非常恰当地概括了单例的概念

```
/// <summary>
/// 饿汉方式
/// </summary>
public class Patterns
{
    private static Patterns patterns = new Patterns();
    private Patterns() { }
    public static Patterns Instance()
        => patterns;
}
/// <summary>
/// 懒汉方式
/// </summary>
public class Patters2
{
    private static Patters2? patterns;
    private static object obj=new object();
    private Patters2() { }
    public static Patters2 GetInstance()
    {
        if (patterns == null)
           lock (obj)
              if (patterns == null)
                    patterns = new Patters2();
        return patterns;
    }
}
```
