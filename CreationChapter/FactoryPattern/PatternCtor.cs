using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public class PatternCtor
    {
        public PatternCtor() {
            TankFactory tankFactory = new TankFactory();

            BossFactory bossFactory = new BossFactory();

            Tank tank1 = (Tank)tankFactory.Create();
            Tank tank2 = (Tank)tankFactory.Create();
            Tank tank3 = (Tank)tankFactory.Create();
            Tank tank4 = (Tank)tankFactory.Create();
            Tank tank5 = (Tank)tankFactory.Create();

            Boss boss1 = (Boss)bossFactory.Create();
            Boss boss2 = (Boss)bossFactory.Create();
            Boss boss3 = (Boss)bossFactory.Create();
            Boss boss4 = (Boss)bossFactory.Create();
            Boss boss5 = (Boss)bossFactory.Create();
        }
    }
}
