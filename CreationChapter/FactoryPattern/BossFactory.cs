using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern;

public class BossFactory : IFactory
{
    public Enemy Create()
    {

        Boss boss = new Boss();
        boss.Age = 1111;
        boss.Level = 100;
        boss.Skill = "地爆天星";
        boss.Name = "哥布林" + DateTime.Now.Ticks;
        return boss;
    }
}
public class Boss : Enemy
{
    public int Age { set; get; }
    public string Name { set; get; }
    public int Level { set; get; }
    public string Skill { set; get; }
}

