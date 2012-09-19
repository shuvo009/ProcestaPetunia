using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcestaAccountingFunction
{
    class AccountOtherFunctions
    {
        //present string
        public double PersentIs(string amount)
        {
            double doubleAmount = Convert.ToDouble(amount);
            return ((doubleAmount / 100) * 15);
        }
        //Present with Double
        public double PersentIs(double amount)
        {
            double doubleAmount = Convert.ToDouble(amount);
            return ((doubleAmount / 100) * 15);
        }
        //Number couple 
        public static string NumberCoupleIs(int number)
        {
            if (number>10)
            {
                return number.ToString("00");
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
