using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern;

public class TankFactory : IFactory
{
    public Enemy Create()
    {
        #region 模拟复杂的构造、实例化、初始化过程
        #region 构造和实例化
        //准备Tank的组装部件等等
        Tank tank = new Tank();
        tank.Name = "虎式坦克001";
        tank.UsefulLife = 2;
        tank.UsefulLife = 5;
        tank.Color = Color.Green;
        #endregion
        #region 初始化
        tank.Refuel();
        tank.RefreshTime();
        tank.Maintain();
        for (int i = 0; i < 4; i++)
        {
            tank.CheckTirePressure(i);
        }
        #endregion
        #endregion
        return tank;
    }
}
public class Tank : Enemy
{
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { set; get; }
    /// <summary>
    /// 使用年限
    /// </summary>
    public int UsefulLife { set; get; }
    /// <summary>
    /// 最大使用年限
    /// </summary>
    public int MaxLife { set; get; }
    /// <summary>
    /// 颜色
    /// </summary>
    public Color Color { set; get; }

    /// <summary>
    /// 加油
    /// </summary>
    public void Refuel()
    {

    }
    /// <summary>
    /// 刷新仪器计时
    /// </summary>
    public void RefreshTime()
    {

    }
    /// <summary>
    /// 装配弹药
    /// </summary>
    public void LoadAmmo()
    {

    }
    /// <summary>
    /// 维护
    /// </summary>
    public void Maintain()
    {

    }
    /// <summary>
    /// 检查胎压
    /// </summary>
    public void CheckTirePressure(int i)
    {

    }

}

