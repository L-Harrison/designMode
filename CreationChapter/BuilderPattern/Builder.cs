using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    #region 开发商需求接口
    /// <summary>
    /// 开发商的需要的施工方标准接口
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// 建造地基
        /// </summary>
        void buildBasement();
        /// <summary>
        /// 建造墙体
        /// </summary>
        void buildWall();
        /// <summary>
        /// 建造屋顶
        /// </summary>
        void buildRoof();
        /// <summary>
        /// 建造房屋
        /// </summary>
        /// <returns></returns>
        Building GetBuilding();
    }
    #endregion

    #region 实现接口的施工方
    public class HouseBuilder : IBuilder
    {
        Building building;
        public HouseBuilder()
        {
            building = new Building();
        }

        public void buildBasement()
        {
            building.SetBasement("00000000");
        }

        public void buildRoof()
        {
            building.SetRoof("//——————————\\\\");
        }

        public void buildWall()
        {
            building.SetRoof("|||||||||");
        }

        public Building GetBuilding()
        {
            return building;
        }
    }

    public class ApartmentBuilder : IBuilder
    {
        Building building;
        public ApartmentBuilder()
        {
            building = new Building();
        }

        public void buildBasement()
        {
            building.SetBasement("00000000\n00000000");
        }

        public void buildRoof()
        {
            building.SetRoof("//——————————\\\\");
        }

        public void buildWall()
        {
            building.SetRoof("||||||||||\n————————\n||||||||||\n————————\n||||||||||");
        }

        public Building GetBuilding()
        {
            return building;
        }
    }
    #endregion

    #region 房子
    /// <summary>
    /// 房子
    /// </summary>
    public class Building
    {
        List<string> buildingComponent = new List<string>();

        public void SetBasement(string basement)
        {
            buildingComponent.Add(basement);
        }
        public void SetWall(string wall)
        {
            buildingComponent.Add(wall);
        }
        public void SetRoof(string roof)
        {
            buildingComponent.Add(roof);
        }
        public void ShowBuild()
        {
            for (int i = 0; i < buildingComponent.Count; i++)
            {
                Console.WriteLine(buildingComponent[i]);
            }
        }
    }
    #endregion

    #region 工程监理
    public class Director
    {
        IBuilder builder;

        public void SetBuilder(IBuilder builder)
        {
            this.builder = builder;
        }

        public Building Direct()
        {
            Console.WriteLine("===工程项目启动===");
            //第一步，打好地基
            builder.buildBasement();
            //第二步,建造框架、墙体
            builder.buildWall();
            //第三步,封顶
            builder.buildRoof();
            Console.WriteLine("===项目竣工===");
            return builder.GetBuilding();

        }
    }
    #endregion 
}
