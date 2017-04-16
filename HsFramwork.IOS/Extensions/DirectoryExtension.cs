using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hungsum.Extensions
{
    public static class DirectoryExtension
    {
        public static long GetTotolSize(this DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(r => r.Length) + dir.GetDirectories().Sum(r => r.GetTotolSize());
        }

    }
}
