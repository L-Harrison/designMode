namespace ObserverPattern;

public class ObserverCtor
{
    public void DO()
    {
        Shop shop = new Shop();
        Buyer buyer1 = new PhoneFans("老王");
        Buyer buyer2 = new HandChopper("老张");
        shop.RegisterBuyer(buyer1);
        shop.RegisterBuyer(buyer2);
        shop.SetProduct("苹果手机");
        shop.SetProduct("猪肉炖粉条");

        Console.WriteLine("-----------------------------------分割线------------------------------------");


        Publisher<string> publisher = new ShopPlus("商店");
        Buyer buyer3 = new PhoneFans("小美");
        Buyer buyer4 = new HandChopper("小红");
        //订阅事件
        Action<string> action1 = new Action<string>(s =>
        {
            if (s.Contains("手机"))
            {
                buyer3.Inform(s);
            }

        });

        Action<string> action2 = new Action<string>(s =>
        {
            buyer4.Inform(s);
        });
        publisher.RegisterAction(action1);
        publisher.RegisterAction(action2);
        //发布
        publisher.Publish("苹果手机");
        publisher.Publish("猪肉炖粉条");
    }
}


#region 观察者模式在购买者和商店系统中的应用
#region 发布者
public class Shop
{
    string Product { set; get; }
    /// <summary>
    /// 订阅者列表
    /// </summary>
    List<Buyer> Buyers = new List<Buyer>();

    public void RegisterBuyer(Buyer buyer)
    {
        this.Buyers.Add(buyer);
    }

    public string GetProduct()
    {
        return this.Product;
    }

    public void SetProduct(string product)
    {
        this.Product = product;
        this.NotifyBuyers();
    }

    /// <summary>
    /// 通知订阅者
    /// </summary>
    private void NotifyBuyers()
    {
        foreach (var item in Buyers)
        {
            item.Inform(this.Product);
        }
    }
}
#endregion

#region 订阅者
public abstract class Buyer
{
    protected string Name { set; get; }
    public Buyer(string name)
    {
        this.Name = name;
    }

    public abstract void Inform(string product);

}

public class PhoneFans : Buyer
{
    public PhoneFans(string name) : base(name)
    {
    }

    public override void Inform(string product)
    {
        if (product.Contains("手机"))
        {
            Console.WriteLine(this.Name + "购买了" + product);
        }
    }
}

public class HandChopper : Buyer
{
    public HandChopper(string name) : base(name)
    {

    }

    public override void Inform(string product)
    {
        Console.WriteLine(this.Name + "购买了" + product);
    }
}


#endregion
#endregion


#region 订阅与发布
#region 订阅与发布
public class Publisher<T>
{
    private string Name;
    public Publisher(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    List<Action<T>> SubscribedActions = new List<Action<T>>();
    /// <summary>
    /// 订阅
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public Action<T> RegisterAction(Action<T> action)
    {
        if (!this.SubscribedActions.Contains(action))
        {
            this.SubscribedActions.Add(action);
        }
        return action;
    }

    /// <summary>
    /// 发布
    /// </summary>
    public void Publish(T value)
    {
        foreach (var item in SubscribedActions)
        {
            item.BeginInvoke(value, null, null);
        }
    }
}

public class ShopPlus : Publisher<string>
{
    public ShopPlus(string name) : base(name)
    {
    }
}

#endregion

#endregion