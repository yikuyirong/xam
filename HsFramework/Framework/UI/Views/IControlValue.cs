using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hungsum.Framework.Events;

namespace Hungsum.Framework.UI.Views
{
    public interface IControlValue
    {
        event EventHandler<HsEventArgs<string>> DataChanged;

        string CName { get; set; }

        string ControlId { get; set; }

        string ControlType { get; }

        string ControlLabel { get; }

        string ControlValue { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        bool AllowEdit { get; set; }

        /// <summary>
        /// 是否允许空
        /// </summary>
        bool AllowEmpty { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        void Validate();

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();
    }
}
