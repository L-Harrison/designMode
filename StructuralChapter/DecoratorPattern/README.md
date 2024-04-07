## 08装饰器模式

装饰器模式（decorator pattern)：这种模式可以将不同功能的单个模块规划到不同的装饰器中，每加一层装饰就会有新的特性体现出来，让功能可以层层叠加，最终使原始对象的特性动态地得到增强。
***特点：自由嵌套***

```/// <summary>
/// 组件通用接口
/// </summary>
public interface IShowable
{
    void Show();
}
/// <summary>
/// 组件实现
/// </summary>
public class Girl : IShowable
{
    public void Show()
    {
        Console.Write("Girl");
    }
}
/// <summary>
/// 装饰器
/// </summary>
public abstract class Decorator : IShowable
{
    protected IShowable showable;
    public Decorator(IShowable showable)
    {
        this.showable = showable;
    }
    public abstract void Show();
}
#region 装饰器实现
public class FoundationMakeUp : Decorator
{
    public FoundationMakeUp(IShowable showable) : base(showable) { }
    public override void Show()
    {
        Console.Write("打粉底【");
        this.showable.Show();
        Console.Write("】");
    }
}
public class LipStick : Decorator
{
    public LipStick(IShowable showable) : base(showable) { }
    public override void Show()
    {
        Console.Write("涂口红【");
        this.showable.Show();
        Console.Write("】");
    }
}
#endregion
```
调用方调用：

```   Girl girl = new Girl();
      IShowable makeupGirl = new FoundationMakeUp(new LipStick(girl));
      makeupGirl.Show();
```