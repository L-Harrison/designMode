namespace TempletePattern;

public class Templete
{
    public void DO()
    {
        PM pM = new HRProject();
        pM.KickOff();
    }
}

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