using System;

namespace NETStandard.Lib
{
    public static class Utils
    {
        public static string GetOutputMessage(string userName)
        {
            var currentTime = DateTime.Now.ToShortTimeString();
            return $"{currentTime} Hello, {userName}!";
        }
    }
}
