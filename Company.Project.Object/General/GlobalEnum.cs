using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Object.General
{
    public class GlobalEnum
    {
        public static partial class RowStatus
        {
            private enum _rowStatus
            {
                Active = 0,
                Delete = -1,
                Other = 1
            }

            public static int Active
            {
                get
                {
                    return (int)_rowStatus.Active;
                }
            }

            public static int Delete
            {
                get
                {
                    return (int)_rowStatus.Delete;
                }
            }

            public static int Other
            {
                get
                {
                    return (int)_rowStatus.Other;
                }
            }
        }

        public static partial class SystemName
        {
            private enum _systemName
            {
                SYSTEM = 1,
                THIRDPARTY = 2
            }

            public static string System
            {
                get
                {
                    return _systemName.SYSTEM.ToString();
                }
            }

            public static string API
            {
                get
                {
                    return _systemName.THIRDPARTY.ToString();
                }
            }
        }

        public static partial class DatetimNowRange
        {
            public static DateTime TimeNowStart
            {
                get
                {
                    return DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd") + " 00:00:00", "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }

            public static DateTime TimeNowEnd
            {
                get
                {
                    return DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd") + " 23:59:59", "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
