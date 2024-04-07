## 09适配器模式

适配器模式（AdapterPattern）：当一个对象或类的接口不能匹配用户所期待的接口时，适配器就充当中间转换的角色，以达到兼容用户接口的目的。

> ***妙用：在出现兼容性问题时，我们不必再暴力修改类的接口，转而通过适配器的方式将两侧不兼容的接口进行对接。***

举例如下：

```
/// <summary>
/// 两孔接口
/// </summary>
public interface IDualPin
{
    void Electrify(int x, int y);
}
/// <summary>
/// 三孔接口
/// </summary>
public interface ITriplePin
{
    void Electrify(int x, int y, int z);
}

public class TV : IDualPin
{
    public void Electrify(int x, int y)
    {
        Console.WriteLine("TV连接到双孔插座上通电");
    }
}
#region 两孔电器适配为三孔电器的通用适配器
public class Adapter : ITriplePin
{
    IDualPin DualPin;
    public Adapter(IDualPin dualPin)
    {
        this.DualPin = dualPin;
    }

    public void Electrify(int x, int y, int z)
    {
        Console.WriteLine("适配器连接到三孔插座上");
        this.DualPin.Electrify(x, y);
    }
}
#endregion

#region 专属适配器(继承的方式实现)
public class TVAdapter : TV, ITriplePin
{
    public void Electrify(int x, int y, int z)
    {
        Console.WriteLine("专属适配器连接到三孔插座上");
        base.Electrify(x, y);
    }
}
#endregion
```
***通用适配器更加灵活，专用适配器更加简单***

***不论何种方式实现的适配器，从本质上都应该具备模块两侧的接口特性，如此才能呈上启下，促成双方的顺利对接与互动，实现接驳的作用。***

调用者调用：

```
#region 通用适配器
//TV是双孔插头，需要双孔插座，但是如果现在只有三孔插座呢？
TV tV = new TV();
//这是后就需要一个适配器(转接器)
ITriplePin triplePin = new Adapter(tV);  //这里有点类似于装饰器，将两孔插头的电视机装饰成三孔插头的电器
triplePin.Electrify(0, 0, 0);
#endregion

#region 专属适配器
TVAdapter tVAdapter = new TVAdapter(); //这里类似于使用装饰器中提到的不推荐的继承方式实现装饰效果
tVAdapter.Electrify(0, 0, 0);
#endregion
```