using Hungsum.Framework.Extentsions;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using Hungsum.OA.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hungsum.Jbcmp.Utilities
{
    public class JbcmpWSUtil : HsOAWSUtil
    {
        #region 采购审批单

        public async Task<List<HsLabelValue>> ShowJbCgspds(string progressId,string beginDate,string endDate,string spzt)
        {
            return await ShowJbCgspds(progressId, null, beginDate, endDate, spzt);
        }

        public async Task<List<HsLabelValue>> ShowJbCgspds(string progressId,string dwbh, string beginDate, string endDate, string spzt)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("BeginDate", beginDate),
                new XElement("EndDate", endDate),
                new XElement("Spzt", spzt));

            if (dwbh != null)
            {
                xData.Add(new XElement("Dwbh", dwbh));
            }

            string data = await postByName("ShowJbCgspds", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }


        public async Task<string> UpdateJbCgspd(string progressId, string djId, string djrq, string cgbt, string sfjj, string cgyy, string bz,string strmx, int flag)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("DjId", djId),
                new XElement("Cgbt", cgbt),
                new XElement("Sfjj", sfjj),
                new XElement("Cgyy", cgyy),
                new XElement("Bz", bz),
                new XElement("Strmx", strmx),
                new XElement("Flag", flag));

            string data = await postByName("UpdateJbCgspd", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return data;
        }

        #endregion
    }
}
