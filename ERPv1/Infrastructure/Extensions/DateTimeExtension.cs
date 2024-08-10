using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ToEgyptionDate(this string date)
        {
            DateTime dt;

            var IsConverted =DateTime.TryParseExact(date, "dd/MM/yyyy",CultureInfo.InvariantCulture,
                DateTimeStyles.None,out dt);
                if(!IsConverted)
            {
                dt = DateTime.Parse(date);
            }
            return dt;

        }
    }
}
