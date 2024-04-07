## 11代理模式

代理模式（ProxyPattern):  为其他对象提供一种代理以控制对这个对象的访问。

> ***代理与装饰器的区别：虽然两者的理念和实现方式有点类似，但是装饰器模式更加关注为其他对象增加功能，让客户端更加灵活地进行组件搭配。而代理器模式更加强调的是对被代理对象访问的管控，将被代理对象完全封装起来，使被代理对象对客户端完全透明。***

> 在很多软件框架中大量应用动态代理的理念，如面向切面编程技术AOP,我们只需要定义好一个切面，并声明切入点

> 标记出被代理的对象的哪些接口的方法，类似于本Demo中的"HttpAccess"和"fileAccess"接口，并写好被切入的代码块(要增加的逻辑，前置/后置/异常处理)
    
> 框架会自动为我们生成代理并切入，并根据我们设计好的方式进行代理目标。

> 常常的地方是：动态加入日志信息，数据库事物控制。 

举例 [Castle DynamicProxy_引用](https://www.cnblogs.com/youring2/p/10962573.html )

举例 [Castle Core](https://github.com/castleproject/Core/blob/master/docs/dynamicproxy-introduction.md ,target)


***举例：***

```
#region 基本的代理器
public interface IInternet
{
    void HttpAccess(string url);
}

/// <summary>
/// 调制解调器(猫)
/// </summary>
public class Modem : IInternet
{
    public Modem(string password)
    {
        if (password != "123456")
        {
            throw new ArgumentException("拨号错误");
        }
    }
    public void HttpAccess(string url)
    {
        Console.WriteLine($"通过调制解调器访问{url}网站");
    }
}

/// <summary>
/// 路由器(调制解调器的代理)
/// </summary>
public class RouterProxy : IInternet
{
    private Modem modem;
    private List<string> blackList = new List<string> { "电影", "游戏", "音乐", "小说" };
    public RouterProxy()
    {
        modem = new Modem("123456");
    }
    public void HttpAccess(string url)
    {
        foreach (var item in blackList)
        {
            if (url.Contains(item))
            {
                Console.WriteLine($"对{url}的访问被路由器代理禁止");
                return;
            }

        }
        modem.HttpAccess(url);
    }
}

//这里的代理方式有点像装饰器方式，虽然两者的理念和实现方式有点类似，但是装饰器模式更加关注为其他对象增加功能，让客户端更加灵活地进行组件搭配。
//而代理器模式更加强调的是对被代理对象访问的管控，将被代理对象完全封装起来，使被代理对象对客户端完全透明。
//心法：大可不必被概念所束缚，最适合系统需求的设计就是好的设计。
#endregion

#region 万能的动态代理器
public interface Intranet
{
    void fileAccess(string path);
}
public class Switch : Intranet
{
    public void fileAccess(string path)
    {
        Console.WriteLine($"通过交换机访问了“{path}”下的文件");
    }
}

public class BlackFilter : IInterceptor
{
    /// <summary>
    /// 黑名单
    /// </summary>
    private List<string> blackList = new List<string>() { "电影", "游戏", "音乐", "小说" };

    public void Intercept(IInvocation invocation)
    {
        //黑名单过滤
        foreach (var item in this.blackList)
        {
            if (invocation.Arguments[0].ToString().Contains(item))
            {
                Console.WriteLine($"对{invocation.Arguments[0].ToString()}的访问被拒绝");
                return;
            }
        }
        invocation.Proceed();

        return;
    }


}
#endregion
```

***调度这调度：***

```
public class Proxy
{
    public void Do()
    {
        #region 基本的代理器
        IInternet proxy = new RouterProxy();
        proxy.HttpAccess("电影");
        proxy.HttpAccess("学习网站");
        proxy.HttpAccess("小说");
        proxy.HttpAccess("电视剧");
        #endregion

        #region 万能的动态代理器(使用Castle Core实现)
        Intranet target = new Switch();
        //黑名单过滤器代理交换机
        IInterceptor blackFilter = new BlackFilter();
        ProxyGenerator generator = new ProxyGenerator();
        Intranet targetProxy1 = generator.CreateInterfaceProxyWithTarget(target, blackFilter);

        targetProxy1.fileAccess("电影");
        targetProxy1.fileAccess("小说");
        targetProxy1.fileAccess("游戏");
        targetProxy1.fileAccess("图书馆");


        IInternet modem = new Modem("123456");
        //黑名单过滤器代理调制解调器(猫)
        IInterceptor routerEpoxy = new BlackFilter();
        IInternet targetProxy2 = generator.CreateInterfaceProxyWithTarget(modem, blackFilter);

        targetProxy2.HttpAccess("电影");
        targetProxy2.HttpAccess("小说");
        targetProxy2.HttpAccess("游戏");
        targetProxy2.HttpAccess("图书馆");
        #endregion

        #region 根据输入类型，返回对应的输出类型,我认为这也是实现动态代理的核心技术，所以在这里进行Demo演示
        int i = 10;
        i = GetValue(i, 2);
        string s = "asdasda";
        s = GetValue(s, 3);
        #endregion
    }

    public static T GetValue<T, T1>(T t, T1 t1)
    {
        return t;
    }
}
```