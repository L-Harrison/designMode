using Castle.DynamicProxy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern;

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