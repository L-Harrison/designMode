namespace StatePattern;

//注释：这里虽然看上去代码量相似，但是当思路不同。 状态模式将状态抽离设计很巧妙。

//状态模式与策略模式实现方式非常相似，与之不同之处在于，策略模式是将策略算法抽离出来由外部注入，从而引发不同的系统行为，其可扩展性更好。
//而状态模式则将状态及其响应机制抽离出来，这能让系统行为与响应有更好的逻辑控制能力，并且实现系统状态主动式的自我切换。
//状态模式和策略模式的侧重点不同，所以适用于的场景不同。

//总之，如果系统中堆积了大量的状态判断语句，那么就可以考虑应用状态模式，它能让系统原本复杂的状态响应及维护逻辑变得异常简单。
public class StateCtor
{
    public void UnUseState()
    {
        #region 未使用状态模式下的交通灯
        TrafficLight trafficLight = new TrafficLight();
        trafficLight.SwitchToGreen();
        trafficLight.SwitchToRed();
        trafficLight.SwitchToYellow();
        trafficLight.SwitchToYellow();
        #endregion
    }
    public void UseState()
    {
        #region 使用状态模式下的交通灯
        TrafficLightPlus trafficLightplus = new TrafficLightPlus();
        trafficLightplus.SwitchToGreen();
        trafficLightplus.SwitchToRed();
        trafficLightplus.SwitchToYellow();
        trafficLightplus.SwitchToYellow();
        #endregion
    }
}

#region 未使用状态前的交通灯
public class TrafficLight
{
    string state = "红";

    public void SwitchToGreen()
    {
        switch (state)
        {
            case "红":
                Console.WriteLine("当前状态是红灯,切换到绿灯");
                this.state = "绿";
                break;
            case "黄":
                Console.WriteLine("当前状态是黄灯,切换到绿灯");
                this.state = "绿";
                break;
            case "绿":
                Console.WriteLine("当前状态已是绿灯不切换");
                break;
        }
    }
    public void SwitchToRed()
    {
        switch (state)
        {
            case "红":
                Console.WriteLine("当前状态已是红灯不切换");
                break;
            case "黄":
                Console.WriteLine("当前状态是黄灯,切换到红灯");
                this.state = "红";
                break;
            case "绿":
                Console.WriteLine("当前状态是绿灯,切换到红灯");
                this.state = "红";
                break;
        }

    }
    public void SwitchToYellow()
    {
        switch (state)
        {
            case "红":
                Console.WriteLine("当前状态是红灯,切换到黄灯");
                this.state = "黄";
                break;
            case "黄":
                Console.WriteLine("当前状态已是黄灯不切换");
                break;
            case "绿":
                Console.WriteLine("当前状态是绿灯,切换到黄灯");
                this.state = "黄";
                break;
        }
    }
}
#endregion

#region 状态模式在交通灯中的使用
#region 交通灯状态
public interface State
{
    void SwitchToGreen(TrafficLightPlus trafficLight);
    void SwitchToRed(TrafficLightPlus trafficLight);
    void SwitchToYellow(TrafficLightPlus trafficLight);
}
public class Red : State
{
    public void SwitchToGreen(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态是红灯,切换到绿灯");
        trafficLight.SetState(new Green());
    }

    public void SwitchToRed(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态已是红灯不切换");
    }

    public void SwitchToYellow(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态是红灯,切换到黄灯");
        trafficLight.SetState(new Yellow());
    }
}
public class Green : State
{
    public void SwitchToGreen(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态已是绿灯不切换");
    }

    public void SwitchToRed(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态是绿灯,切换到红灯");
        trafficLight.SetState(new Red());
    }

    public void SwitchToYellow(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态是绿灯,切换到黄灯");
        trafficLight.SetState(new Yellow());
    }
}
public class Yellow : State
{
    public void SwitchToGreen(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态是黄灯,切换到绿灯");
        trafficLight.SetState(new Green());
    }

    public void SwitchToRed(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态是黄灯,切换到红灯");
        trafficLight.SetState(new Red());
    }

    public void SwitchToYellow(TrafficLightPlus trafficLight)
    {
        Console.WriteLine("当前状态已是黄灯不切换");
    }
}
#endregion

#region 交通灯
/// <summary>
/// 交通灯类
/// </summary>
public class TrafficLightPlus
{
    /// <summary>
    /// 当前交通灯的状态
    /// </summary>
    private State state = new Red();

    public void SetState(State state)
    {
        this.state = state;
    }
    public void SwitchToGreen()
    {
        this.state.SwitchToGreen(this);
    }
    public void SwitchToRed()
    {
        this.state.SwitchToRed(this);
    }
    public void SwitchToYellow()
    {
        this.state.SwitchToYellow(this);
    }
}
#endregion
#endregion
