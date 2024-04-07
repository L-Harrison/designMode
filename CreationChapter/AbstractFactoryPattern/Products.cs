using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    #region A厂产品
    public class LimousineA : Limousine
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class CrossCountryA : CrossCountry
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class SportsA : Sports
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region B厂产品
    public class LimousineB : Limousine
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class CrossCountryB : CrossCountry
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class SportsB : Sports
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region C厂产品
    public class LimousineC : Limousine
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class CrossCountryC : CrossCountry
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }

    public class SportsC : Sports
    {
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
