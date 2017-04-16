using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Cells
{
    public partial class UcFileCell : ImageCell
    {
        public UcFileCell()
        {

            this.SetBinding(ImageCell.ImageSourceProperty, new Binding() { Source = HsFileData, Path = "FileIcon" });

            this.SetBinding(ImageCell.TextProperty, new Binding() { Source = HsFileData, Path = "FileName" });

            this.SetBinding(ImageCell.DetailProperty, new Binding() { Source = HsFileData, Path = "FileSize" });
        }

        #region 绑定属性


        public static readonly BindableProperty HsFileDataProperty =
          BindableProperty.Create("HsFileData", typeof(HsFile), typeof(UcFileCell), null);

        public HsFile HsFileData
        {
            get { return (HsFile)GetValue(HsFileDataProperty); }
            set { SetValue(HsFileDataProperty, value); }
        }

        #endregion 

    }
}
