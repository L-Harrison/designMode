namespace SingletonPatterns;

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