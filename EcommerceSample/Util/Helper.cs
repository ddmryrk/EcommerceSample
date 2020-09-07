using System.Collections.Generic;
using System.Linq;
using EcommerceSample.Interfaces;

namespace EcommerceSample.Util
{
    public class Helper
    {
        public static int GenerateIDByList<T>(List<T> list) where T : PrivateObjectBase
        {
            var lastItemByID = list.OrderByDescending(item => item.ID).FirstOrDefault();

            return lastItemByID == null ? 0 : lastItemByID.ID + 1;
        }
    }
}
