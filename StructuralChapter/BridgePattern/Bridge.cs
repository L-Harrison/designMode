using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern;

internal class Bridge
{
    public void DO()
    {
        //根据不同分工的类进行任意的组合桥接，实现了所有类型的笛卡尔积
        Red red1 = new Red(new Circle());
        Red red2 = new Red(new Rectangle());
        Red red3 = new Red(new Triangle());

        Green green1 = new Green(new Circle());
        Green green2 = new Green(new Rectangle());
        Green green3 = new Green(new Triangle());

        Black black1 = new Black(new Circle());
        Black black2 = new Black(new Rectangle());
        Black black3 = new Black(new Triangle());
    }
}

#region 将颜色和形状分开，允许客户端根据自己的需求进行任意的组合
#region 形状
public abstract class Shape
{
    public abstract void DrawShape();
}
public class Triangle : Shape
{
    public override void DrawShape()
    {
        Console.WriteLine("绘制三角形");
    }
}
public class Rectangle : Shape
{
    public override void DrawShape()
    {
        Console.WriteLine("绘制矩形");
    }
}
public class Circle : Shape
{
    public override void DrawShape()
    {
        Console.WriteLine("绘制圆形");
    }
}
#endregion
#region 颜色
public abstract class ShapeColor
{
    protected Shape Shape;
    public ShapeColor(Shape shape)
    {
        this.Shape = shape;
    }
    public abstract void DrawShapeColored();
}
public class Black : ShapeColor
{
    public Black(Shape shape) : base(shape) { }
    public override void DrawShapeColored()
    {
        Console.Write("使用黑色");
        this.Shape.DrawShape();
    }
}
public class Red : ShapeColor
{
    public Red(Shape shape) : base(shape) { }
    public override void DrawShapeColored()
    {
        Console.Write("使用红色");
        this.Shape.DrawShape();
    }
}
public class Green : ShapeColor
{
    public Green(Shape shape) : base(shape) { }
    public override void DrawShapeColored()
    {
        Console.Write("使用绿色");
        this.Shape.DrawShape();
    }
}
#endregion
#endregion
