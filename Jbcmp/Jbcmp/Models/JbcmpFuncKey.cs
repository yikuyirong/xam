using Hungsum.Jbcmp.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Jbcmp.Models
{
    public class JbcmpFuncKey : HsOAFuncKey
    {
        /*****工作流*****/

        public const string JB采购审批单 = "JBAA0010";
		
		public const string JB采购审批单浏览 = "JBAA0030";
		
		public const string JB招标文件评审表 = "JBAA0040";
		
		public const string JB招标文件评审表浏览 = "JBAA0050";
		
		public const string JB合同评审表 = "JBAA0060";
		
		public const string JB合同评审表浏览 = "JBAA0070";
		
		
		
		public const string JB加班换休记录 = "JBAB0010";
		
		/*****银企直连*****/
		
		public const string JB直连客户维护 = "JBBB0010";
		
		public const string JB直连客户兑现绑定 = "JBBB0012";
		
		public const string JB直连客户在途绑定 = "JBBB0014";
		
		public const string JB核销在途欠款 = "JBBB0020";
		
		public const string JB客户资金记录维护 = "JBBB0030";
		
		public const string JB客户资金记录浏览 = "JBBB0040";
		
		public const string JB预收专送货款 = "JBBB0050";
		
		public const string JB专送预收款浏览 = "JBBB0051";
		
		/**前端上报单**/

		public const string JB前端数据_工业游报名 = "JBBC0010";
		
		public const string JB前端数据_公益课堂报名 = "JBBC0020";
		
		public const string JB前端数据_在线订奶 = "JBBC0030";
		

    }
}
