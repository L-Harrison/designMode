namespace AbstractFactoryPattern
{
    public class Creater
    {
        public void FactoryCreater()
        {
            //创建工厂
            FactoryA factoryA = new FactoryA();
            FactoryB factoryB = new FactoryB();
            FactoryC factoryC = new FactoryC();

            //生产产品
            CrossCountryA crossCountryA = (CrossCountryA)factoryA.ManufactureCrossCountry();
            CrossCountryB crossCountryB = (CrossCountryB)factoryB.ManufactureCrossCountry();
            CrossCountryC crossCountryC = (CrossCountryC)factoryC.ManufactureCrossCountry();

            LimousineA limousineA = (LimousineA)factoryA.ManufactureLimosine();
            LimousineB limousineB = (LimousineB)factoryB.ManufactureLimosine();
            LimousineC limousineC = (LimousineC)factoryC.ManufactureLimosine();

            SportsA sportsA = (SportsA)factoryA.ManufactureSports();
            SportsB sportsB = (SportsB)factoryB.ManufactureSports();
            SportsC sportsC = (SportsC)factoryC.ManufactureSports();
        }
    }
}