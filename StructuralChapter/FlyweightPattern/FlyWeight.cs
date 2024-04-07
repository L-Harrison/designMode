using System.Drawing;

namespace FlyweightPattern;

public class FlyWeight
{
}

public interface IDrawable
{
    void Draw(int x, int y);
}

#region 享元对象
public class River : IDrawable
{
    public River()
    {
        riverbitmap = new Bitmap(640, 480);
    }
    private Bitmap riverbitmap;

    public void Draw(int x, int y)
    {
        Console.WriteLine("绘制河流");
    }
}
public class Grass : IDrawable
{
    public Grass()
    {
        Grassbitmap = new Bitmap(640, 480);
    }
    private Bitmap Grassbitmap;

    public void Draw(int x, int y)
    {
        Console.WriteLine("绘制草坪");
    }
}
public class Stone : IDrawable
{
    public Stone()
    {
        Stonebitmap = new Bitmap(640, 480);
    }
    private Bitmap Stonebitmap;

    public void Draw(int x, int y)
    {
        Console.WriteLine("绘制石头路");
    }
}
public class House : IDrawable
{
    private Bitmap riverbitmap;
    public void Draw(int x, int y)
    {
        Console.WriteLine("绘制房屋");
    }
}
#endregion

#region 享元对象工厂
public class TileFactory
{
    private Dictionary<string, IDrawable> Images;
    public TileFactory()
    {
        Images = new Dictionary<string, IDrawable>();
    }

    public IDrawable GetImage(string image)
    {
        if (this.Images.ContainsKey(image))
        {
            return this.Images[image];
        }
        else
        {
            switch (image)
            {
                case "河流":
                    this.Images.Add(image, new River());
                    return this.Images[image];
                case "房屋":
                    this.Images.Add(image, new House());
                    return this.Images[image];
                case "石头路":
                    this.Images.Add(image, new Stone());
                    return this.Images[image];
                case "草坪":
                    this.Images.Add(image, new Grass());
                    return this.Images[image];
                default:
                    this.Images.Add(image, new Grass());
                    return this.Images[image];
            }
        }
    }
}
#endregion