namespace PrototypePattern;

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