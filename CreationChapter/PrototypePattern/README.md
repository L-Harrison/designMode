## 02原型模式

原型模式(prototype pattern)：目的是以原型实例创建副本实例，并不需要知道其原始类；也就是说用对象创建对象，而不是用类创建对象，以此达到效率的提升。
> - ***适用对象：初始化过程比较复杂或者需要消耗大量资源的实例。***
> - 浅拷贝：只复制原始类型的值到克隆体，而引用类型只复制其引用地址
> - 深拷贝：不仅复制原始类型的值到克隆体，引用类型也复制到克隆体中

***例如：***

```
public class EnemyPlane:ICloneable
{
    public EnemyPlane()
    {
        for (int i = 0; i < 1000; i++)
        {
            if (MaxFlyHeight < 500)
                MaxFlyHeight++;
            if (MaxPlaneWidth < 500)
                MaxPlaneWidth++;
            if (MaxPlaneLenght>500)
                MaxPlaneLenght += 2;
        }
        PartPrice = new PlanePartPrice();
    }
    public int MaxFlyHeight { get; set; }
    public int MaxPlaneWidth { get; set; }
    public int MaxPlaneLenght { get; set; }
    public PlanePartPrice PartPrice { get; set; }
    public object Clone()
        =>(EnemyPlane)this.MemberwiseClone();
}
public class PlanePartPrice : ICloneable
{
    public int Price { get; set; }
    public string? PartName { get; set; }
    public object Clone()
        => (PlanePartPrice)this.MemberwiseClone();
}
public class EnemyPlaneFactory
{
    private static EnemyPlane?  enemyPlane;
    private static object obj=new object();
    public static void Initilize()
    {
        if (enemyPlane == null)
        {
            lock (obj)
            {
                if (enemyPlane==null)
                    enemyPlane=new EnemyPlane();
            }
        }
    }
    public static EnemyPlane GetEnemyPlane()
    {
        if (enemyPlane == null)
        {
            lock (obj)
            {
                if (enemyPlane == null)
                    enemyPlane = new EnemyPlane();
            }
        }
        EnemyPlane eplay =(EnemyPlane) enemyPlane.Clone();
        eplay.PartPrice = new PlanePartPrice();
        return eplay;
    }
}
```
***创建多个对象 ,体现出原型模式的作用。***

```
public class PrototypeFactory
{
    List<EnemyPlane> enemyPlanes = new List<EnemyPlane>();
    public (bool res,double cotrTime, double factoryTime) CtorTakeLongTime()
    {
        var res = false;
        var ctorTime = 0d;
        var factoryTime = 0d;
        int num = 1_000_000;
        var stopWatch=new Stopwatch();
        stopWatch.Restart();
        for (int i = 0; i < num; i++)
        {
            enemyPlanes.Add(new EnemyPlane());
        }
        stopWatch.Stop();
        ctorTime = stopWatch.ElapsedMilliseconds;
        stopWatch.Restart();
        for (int i = 0; i < num; i++)
        {
            enemyPlanes.Add(EnemyPlaneFactory.GetEnemyPlane());
        }
        stopWatch.Stop();
        factoryTime = stopWatch.ElapsedMilliseconds;
        return (ctorTime > factoryTime, ctorTime, factoryTime);
    }
}
```