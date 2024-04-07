using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    public abstract class Car
    {
        /// <summary>
        /// 马力
        /// </summary>
        public int HorsePower { set; get; }

        /// <summary>
        /// 油耗
        /// </summary>
        public int FuelConsumption { set; get; }
        /// <summary>
        /// 空间
        /// </summary>
        public int Size { set; get; }

        public Car(int horsepower, int fuelConsumption, int size)
        {
            this.HorsePower = horsepower;
            this.FuelConsumption = fuelConsumption;
            this.Size = size;
        }

        /// <summary>
        /// 运行
        /// </summary>
        public abstract void Run();
        /// <summary>
        /// 刹车
        /// </summary>
        public abstract void Break();

    }

    /// <summary>
    /// 豪华轿车(空间大，马力油耗相对较低)
    /// </summary>
    public abstract class Limousine : Car
    {
        public Limousine() : base(6, 6, 10)
        {

        }

    }
    /// <summary>
    /// 越野车(空间偏大，马力偏大，油耗高)
    /// </summary>
    public abstract class CrossCountry : Car
    {
        public CrossCountry() : base(8, 10, 8)
        {

        }

    }
    /// <summary>
    /// 跑车(马力大，油耗偏大，空间小)
    /// </summary>
    public abstract class Sports : Car
    {
        public Sports() : base(10, 8, 3)
        {

        }
    }
}
