namespace VisitorPattern;

public class VisitorCtor
{
    public void DO()
    {
        //Shopping Cart购物车
        List<Acceptable> shoppingCart = new List<Acceptable>();
        shoppingCart.Add(new Candy("大白兔", new DateTime(2011, 12, 12), 10));
        shoppingCart.Add(new Candy("徐福记", new DateTime(2022, 12, 1), 20));
        shoppingCart.Add(new Candy("三只松鼠", new DateTime(2022, 6, 12), 10));
        shoppingCart.Add(new Fruit("苹果", new DateTime(2022, 12, 3), 10));
        shoppingCart.Add(new Wine("茅台", new DateTime(1995, 11, 3), 2000));

        DiscountVisitor discountVisitor = new DiscountVisitor();
        foreach (var item in shoppingCart)
        {
            item.Accept(discountVisitor);
        }
    }
}

#region 访问者模式在超时结算系统的应用
#region 实现接待者接口的数据
#region 接待者接口
public interface Acceptable
{
    void Accept(Visitor visitor);
}
#endregion
public abstract class Product
{
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProductDate { get; set; }
    /// <summary>
    /// 价格
    /// </summary>
    public double Price { set; get; }

    public Product(string name, DateTime productDate, int price)
    {
        this.Name = name;
        this.ProductDate = productDate;
        this.Price = price;
    }
}
/// <summary>
/// 糖果类
/// </summary>
public class Candy : Product, Acceptable
{
    public Candy(string name, DateTime productDate, int price) : base(name, productDate, price) { }
    public void Accept(Visitor visitor)
    {
        visitor.Visit(this);
    }
}
/// <summary>
/// 酒类
/// </summary>
public class Wine : Product, Acceptable
{
    public Wine(string name, DateTime productDate, int price) : base(name, productDate, price) { }
    public void Accept(Visitor visitor)
    {
        visitor.Visit(this);
    }
}
/// <summary>
/// 水果类
/// </summary>
public class Fruit : Product, Acceptable
{
    public Fruit(string name, DateTime productDate, int price) : base(name, productDate, price) { }
    /// <summary>
    /// 重量
    /// </summary>
    public float Weight { set; get; }

    void Acceptable.Accept(Visitor visitor)
    {
        visitor.Visit(this);
    }
}

#endregion

#region 实现访问不同数据方法的访问者
public interface Visitor
{
    void Visit(Candy candy);
    void Visit(Fruit fruit);
    void Visit(Wine wine);
}

public class DiscountVisitor : Visitor
{
    void Visitor.Visit(Candy candy)
    {
        if (DateTime.Now.Subtract(candy.ProductDate).Days > 180)
        {
            Console.WriteLine(candy.Name + "超过半年的糖果,请勿食用");
        }
        else
        {
            double discountPrice = 0.9 * candy.Price;
            Console.WriteLine("糖果" + candy.Name + "打9折，打折后价格为" + discountPrice);
        }
    }
    void Visitor.Visit(Fruit fruit)
    {
        if (DateTime.Now.Subtract(fruit.ProductDate).Days > 7)
        {
            Console.WriteLine(fruit.Name + "超过7天的水果,请勿使用");
        }
        else
        {
            double discountPrice = 0.6 * fruit.Price;
            Console.WriteLine("水果" + fruit.Name + "打6折，打折后价格为" + discountPrice);
        }
    }
    void Visitor.Visit(Wine wine)
    {
        double discountPrice = 0.8 * wine.Price;
        Console.WriteLine("白酒" + wine.Name + "打8折，打折后价格为" + discountPrice);
    }
}
#endregion
#endregion