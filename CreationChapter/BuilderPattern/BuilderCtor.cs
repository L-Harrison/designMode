using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    internal class BuilderCtor
    {
        public void Build()
        {
            Director director = new Director();
            director.SetBuilder(new HouseBuilder());
            Building building = director.Direct();
            building.ShowBuild();

            director.SetBuilder(new ApartmentBuilder());
            building = director.Direct();
            building.ShowBuild();
        }
    }
}
