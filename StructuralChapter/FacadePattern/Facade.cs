namespace FacadePattern;

public class Facade
{
    //门面模式：将多个不同的子系统接口封装起来，你并对外提供统一的高层接口，使复杂的子系统变得更易使用,保证了用户访问的便利性。
    //对子系统进行整合，对外暴露统一接口，使结构内繁外简，最终达到资源共享，简化操作的目的。
    //高内聚低耦合
    static void Do(string[] args)
    {
        Facade1 facade1 = new Facade1();
        Facade2 facade2 = new Facade2();
        Customer customer = new Customer();
        facade1.Order(customer);
        facade2.Order(customer);
    }
}

/// <summary>
/// 蔬菜商类
/// </summary>
public class VegVendor
{
    public void Purchase()
    {
        Console.WriteLine("商店购买蔬菜");
    }
}
/// <summary>
/// 厨师类
/// </summary>
public class Chef
{
    public void Cook()
    {
        Console.WriteLine("厨师做饭");
    }
}
/// <summary>
/// 清洁工类
/// </summary>
public class Cleaner
{
    public void Clean()
    {
        Console.WriteLine("清洁工清洁洗碗");
    }
}
/// <summary>
/// 服务员类
/// </summary>
public class Waiter
{
    public void Order()
    {
        Console.WriteLine("服务员点菜");
    }
    public void Serve()
    {
        Console.WriteLine("服务员上菜");
    }
}

/// <summary>
/// 统一的门面接口
/// </summary>
public interface IFacade
{
    void Order(Customer customer);
}

/// <summary>
/// 门面类
/// </summary>
public class Facade1 : IFacade
{
    public Chef Chef { get; set; }
    public VegVendor VegVendor { get; set; }
    public Waiter Waiter { get; set; }
    public Cleaner Cleaner { get; set; }

    public Facade1()
    {
        Chef = new Chef();
        VegVendor = new VegVendor();
        //购买蔬菜
        VegVendor.Purchase();
        Waiter = new Waiter();
        Cleaner = new Cleaner();
    }
    public void Order(Customer customer)
    {
        Waiter.Order();
        Chef.Cook();
        Waiter.Serve();
        customer.Eat();
        Cleaner.Clean();
    }
}

public class Facade2 : IFacade
{
    public Chef Chef1 { get; set; }
    public Chef Chef2 { get; set; }
    public VegVendor VegVendor { get; set; }
    public Waiter Waiter1 { get; set; }
    public Waiter Waiter2 { get; set; }
    public Cleaner Cleaner1 { get; set; }
    public Cleaner Cleaner2 { get; set; }

    public Facade2()
    {
        Chef1 = new Chef();
        Chef2 = new Chef();
        VegVendor = new VegVendor();
        //购买蔬菜
        VegVendor.Purchase();
        Waiter1 = new Waiter();
        Waiter2 = new Waiter();
        Cleaner1 = new Cleaner();
        Cleaner2 = new Cleaner();
    }
    public void Order(Customer customer)
    {
        Waiter1.Order();
        Chef1.Cook();
        Chef2.Cook();
        Waiter1.Serve();
        Waiter2.Serve();
        customer.Eat();
        Cleaner1.Clean();
        Cleaner2.Clean();
    }
}

/// <summary>
/// 顾客类
/// </summary>
public class Customer
{
    public void Eat()
    {
        Console.WriteLine("客户吃饭");
    }
}
