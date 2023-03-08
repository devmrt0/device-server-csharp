using System;
using System.Collections.Generic;

namespace device_server_c_
{
    public static class Common
    {
        public static bool IsEmptyStr(string str)
        {
            return String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str);
        }
    }








}