using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    public abstract class AbstractFactory
    {
        /// <summary>
        /// 生产轿车
        /// </summary>
        /// <returns></returns>
        public abstract Limousine ManufactureLimosine();
        /// <summary>
        /// 生产越野车
        /// </summary>
        /// <returns></returns>
        public abstract CrossCountry ManufactureCrossCountry();
        /// <summary>
        /// 生产跑车
        /// </summary>
        /// <returns></returns>
        public abstract Sports ManufactureSports();

    }

    #region 工厂A
    public class FactoryA : AbstractFactory
    {
        public override CrossCountry ManufactureCrossCountry()
        {
            return new CrossCountryA();
        }

        public override Limousine ManufactureLimosine()
        {
            return new LimousineA();
        }

        public override Sports ManufactureSports()
        {
            return new SportsA();
        }
    }
    #endregion

    #region 工厂B
    public class FactoryB : AbstractFactory
    {
        public override CrossCountry ManufactureCrossCountry()
        {
            return new CrossCountryB();
        }

        public override Limousine ManufactureLimosine()
        {
            return new LimousineB();
        }

        public override Sports ManufactureSports()
        {
            return new SportsC();
        }
    }
    #endregion

    #region 工厂C
    public class FactoryC : AbstractFactory
    {
        public override CrossCountry ManufactureCrossCountry()
        {
            return new CrossCountryA();
        }

        public override Limousine ManufactureLimosine()
        {
            return new LimousineB();
        }

        public override Sports ManufactureSports()
        {
            return new SportsC();
        }
    }
    #endregion
}
