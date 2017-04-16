using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.Models
{
    public class HsFile
    {
        public string FileName { get; set; }

        public string FileHash { get; set; }

        public string FileType { get; set; }

        public ImageSource FileIcon { get; set; }

        public string FileSize { get; set; }
    }
}
