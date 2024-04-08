namespace ChainOfResponsiblityPattern;

public class ChainOfResponsiblity
{
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
}

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