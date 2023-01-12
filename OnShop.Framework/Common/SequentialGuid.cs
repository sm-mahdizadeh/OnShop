using System;
using System.Threading;

namespace OnShop.Framework.Common
{
    public static class SequentialGuid
    {
        public static Guid GetSequentialGuid()
        {
            var tempGuid = Guid.NewGuid();
            var bytes = tempGuid.ToByteArray();
            var time = DateTime.Now;
            bytes[3] = (byte)time.Year;
            bytes[2] = (byte)time.Month;
            bytes[1] = (byte)time.Day;
            bytes[0] = (byte)time.Hour;
            bytes[5] = (byte)time.Minute;
            bytes[4] = (byte)time.Second;
            var currentGuid = new Guid(bytes);
            return currentGuid;
        }
    }
}