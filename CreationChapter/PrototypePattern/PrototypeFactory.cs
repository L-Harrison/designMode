using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern;

public class PrototypeFactory
{
    List<EnemyPlane> enemyPlanes = new List<EnemyPlane>();
    public (bool res,double cotrTime, double factoryTime) CtorTakeLongTime()
    {
        var res = false;
        var ctorTime = 0d;
        var factoryTime = 0d;
        int num = 1_000_000;
        var stopWatch=new Stopwatch();
        stopWatch.Restart();
        for (int i = 0; i < num; i++)
        {
            enemyPlanes.Add(new EnemyPlane());
        }
        stopWatch.Stop();
        ctorTime = stopWatch.ElapsedMilliseconds;
        stopWatch.Restart();
        for (int i = 0; i < num; i++)
        {
            enemyPlanes.Add(EnemyPlaneFactory.GetEnemyPlane());
        }
        stopWatch.Stop();
        factoryTime = stopWatch.ElapsedMilliseconds;
        return (ctorTime > factoryTime, ctorTime, factoryTime);
    }
}
