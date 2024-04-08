## 13 模板模式

模板模式（Templete Pattern)：可以将总结出来的规律纪律沉淀为一种既定格式，并固化于模板中以供子类继承，对未确立下来的步骤方法进行抽象化，使其得到延续、多态化，最终架构一个平台，使系统实现在不改变预设规则的前提下，对每个分步骤进行个性化定义的目的。
        
> ***虚实结合***：模板方法模式巧妙地结合了抽象类虚部方法和实部方法，分别定义可变部分与不可变部分，其中前者留给子类去实现，保证了系统的可扩展性；而后者则包含一系列对前者的逻辑调用，为子类提供一种固有的应用指导规范，从而达到虚中带实、虚实结合的状态。 这样做的好处是编程人员不用为编写内容，先后顺序，格式而感到困惑，只需要按照模板的标准去填写特定的代码即可。

***举例如下：***

```
/// <summary>
/// 瀑布流型项目管理类模板
/// </summary>
public abstract class PM
{
    #region 虚
    /// <summary>
    /// 需求分析
    /// </summary>
    /// <returns></returns>
    public abstract string Analyze();
    /// <summary>
    /// 软件设计
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public abstract string Design(string project);
    /// <summary>
    /// 代码开发
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public abstract string Develop(string project);
    /// <summary>
    /// 质量测试
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public abstract bool Test(string project);
    /// <summary>
    /// 上线发布
    /// </summary>
    /// <param name="project"></param>
    public abstract void Release(string project);
    #endregion

    #region 实
    /// <summary>
    /// 开始
    /// </summary>
    public void KickOff()
    {
        string requirement = this.Analyze();
        string DesignCode = this.Design(requirement);
        do
        {
            DesignCode = this.Develop(DesignCode);
        }
        while (!this.Test(DesignCode));
        this.Release(DesignCode);
    }
    #endregion
}

#region 利用模板实现新类
public class HRProject : PM
{
    public override string Analyze()
    {
        Console.WriteLine("需求分析师：分析需求");
        return "人力资源管理项目";
    }

    public override string Design(string project)
    {
        Console.WriteLine("架构师:程序设计");
        return project + "在" + DateTime.Now.ToString() + "写好的架构";
    }

    public override string Develop(string project)
    {
        Console.WriteLine("开发：Bug修复");
        return project + "在" + DateTime.Now.ToString() + "修复的代码";
    }

    public override void Release(string project)
    {
        Console.WriteLine("管理员：上线发布");
        return;
    }
    public override bool Test(string project)
    {
        Console.WriteLine("测试人员:测试代码");
        ///测试成功返回true
        return true;
    }
}
#endregion
```

***调用方调用：***

```
public class Templete
{
    public void DO()
    {
        PM pM = new HRProject();
        pM.KickOff();
    }
}
```