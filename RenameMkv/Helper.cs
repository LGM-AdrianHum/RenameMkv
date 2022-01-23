// ----------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" >
//   This file is part of the AudioBookPlayer distribution
// 
//   Copyright (c) 2022
// 
//   This program is distributed WITHOUT ANY WARRANTY; without even the implied
//   warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// <summary>
//   <developer>Casandra/Adrian Hum</developer>
//   <solution>AudioBookPlayer/BookLibrary/MainWindow.xaml.cs</solution>
//   <created>2022-01-15 3:43 PM</created>
//   <modified>2022-01-15 6:46 PM</modified>
// </summary>
// ----------------------------------------------------------------------------------

using System.IO;

namespace RenameMkv
{
    public static class Helper
    {
        public static string CleanName(this string s)
        {
            var ret = s.Replace(":", " ");
            foreach (var k in Path.GetInvalidFileNameChars())
                ret = ret.Replace(k, ' ');

            while (ret.Contains("  ")) ret = ret.Replace("  ", " ");
            return ret.Trim();
        }
    }
}