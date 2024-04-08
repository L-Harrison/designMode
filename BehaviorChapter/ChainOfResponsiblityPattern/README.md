## 15 责任链模式

责任链模式（chain of responsiblity pattern)：允许业务请求者将责任链视为一个整体对其发起请求，而不必关心链条内部具体的业务逻辑与流程走向。

拦截的类都是先统一的接口，每个接收者包含对下一个接收者的引用。将这些对象连接成一条链，并沿着这条链传递请求，直到有对象处理它为止。

> ***适用场景：当一个业务需要经历一系列对象去处理时，我们可以把这些业务对象串联起来成为一条业务责任链，请求者可以直接通过访问业务责任链来完成业务的处理，最终实现请求者与响应者的解耦。***

> ***责任链模式：本质是处理某种连续的工作流，并确保业务能够被传递至相应的责任节点上得到处理。***
    
***举例说明：***

- **未设置责任链**

```
#region 未设计责任链时，各自负责自己的工作
public class Staff
{
    private string name;
    public Staff(string name)
    {
        this.name = name;
    }
    public bool Approve(int amount)
    {
        if (amount <= 1000)
        {
            Console.WriteLine("审批通过" + "【专员" + name + "】");
            return true;
        }
        else
        {
            Console.WriteLine("无权审批,请找上级" + "【专员" + name + "】");
            return false;
        }
    }
}
public class Manager
{
    private string name;
    public Manager(string name)
    {
        this.name = name;
    }
    public bool Approve(int amount)
    {
        if (amount <= 5000)
        {
            Console.WriteLine("审批通过" + "【经理" + name + "】");
            return true;
        }
        else
        {
            Console.WriteLine("无权审批,请找上级" + "【经理" + name + "】");
            return false;
        }
    }
}
public class CFO
{
    private string name;
    public CFO(string name)
    {
        this.name = name;
    }
    public bool Approve(int amount)
    {
        if (amount <= 10000)
        {
            Console.WriteLine("审批通过" + "【总监" + name + "】");
            return true;
        }
        else
        {
            Console.WriteLine("审批驳回" + "【总监" + name + "】");
            return false;
        }
    }
}
#endregion
```

未设置时调用者调用

```
 public void DoUnChain()
    {
        #region 未设计责任链(过程繁琐，需要客户东奔西跑，找各种人)
        int Amount = 8000;
        //找专员进行审批
        Staff staff = new Staff("小美");
        if (!staff.Approve(Amount))
        {
            //审批失败后,找经理进行审批
            Manager manager = new Manager("张经理");
            if (!manager.Approve(Amount))
            {
                //审批失败后,找财务总监审批
                CFO cFO = new CFO("Lisa");
                if (!cFO.Approve(Amount))
                {
                    Console.WriteLine("申请失败");
                }
            }
        }
        #endregion
    }
```

- **设置责任链**

```
#region 设计责任链
public abstract class Approver
{
    protected string name;
    protected Approver nextApprover;
    public Approver(string name)
    {
        this.name = name;
    }
    /// <summary>
    /// 设置上一级审批人员(此方法用于设置责任链)
    /// </summary>
    /// <param name="nextApprover"></param>
    /// <returns></returns>
    public Approver SetNextApprover(Approver nextApprover)
    {
        this.nextApprover = nextApprover;
        return this.nextApprover;
    }
    /// <summary>
    /// 审批
    /// </summary>
    /// <param name="amount"></param>
    public abstract void Approve(int amount);
}
public class StaffPlus : Approver
{
    public StaffPlus(string name) : base(name) { }
    public override void Approve(int amount)
    {
        if (amount <= 1000)
        {
            Console.WriteLine("审批通过" + "【专员:" + name + "】");
            return;
        }
        else
        {
            if (this.nextApprover != null)
            {
                this.nextApprover.Approve(amount);
                return;
            }
            else
            {
                Console.WriteLine("审批驳回");
                return;
            }
        }
    }
}
public class ManagerPlus : Approver
{
    public ManagerPlus(string name) : base(name) { }
    public override void Approve(int amount)
    {
        if (amount <= 5000)
        {
            Console.WriteLine("审批通过" + "【经理:" + name + "】");
            return;
        }
        else
        {
            if (this.nextApprover != null)
            {
                this.nextApprover.Approve(amount);
                return;
            }
            else
            {
                Console.WriteLine("审批驳回");
                return;
            }
        }
    }
}
public class CFOPlus : Approver
{
    public CFOPlus(string name) : base(name) { }
    public override void Approve(int amount)
    {
        if (amount <= 10000)
        {
            Console.WriteLine("审批通过" + "【总监:" + name + "】");
            return;
        }
        else
        {
            if (this.nextApprover != null)
            {
                this.nextApprover.Approve(amount);
                return;
            }
            else
            {
                Console.WriteLine("审批驳回");
                return;
            }
        }
    }
}
#endregion
```
**设计责任链时调用**

```
  public void DOChain()
    {
        #region 设计责任链
        Approver staffPlus = new StaffPlus("小美");
        //链式编程
        staffPlus.SetNextApprover(new ManagerPlus("张经理")).SetNextApprover(new CFOPlus("Lisa"));
        int Amount1 = 8000;
        staffPlus.Approve(Amount1);
        #endregion
    }
```

> 我们甚至可以让一位审批人将业务传递给多位审批人，或者加入更加复杂的业务逻辑以完善工作流，最终使不同业务有不同的传递方向。

不管以何种形式呈现，设计者都可以根据具体的业务场景，更灵活、恰当地运用责任链模式，而不是照本宣科、生搬硬套。