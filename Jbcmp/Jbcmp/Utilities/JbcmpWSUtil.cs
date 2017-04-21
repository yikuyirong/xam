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

        public async Task<List<HsLabelValue>> ShowJbCgspds(string progressId, string beginDate, string endDate, string spzt)
        {
            return await ShowJbCgspds(progressId, null, beginDate, endDate, spzt);
        }

        public async Task<List<HsLabelValue>> ShowJbCgspds(string progressId, string dwbh, string beginDate, string endDate, string spzt)
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


        public async Task<string> UpdateJbCgspd(string progressId, string djId, string djrq, string cgbt, string sfjj, string cgyy, string bz, string strmx, int flag)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("DjId", djId),
                new XElement("Djrq", djrq),
                new XElement("Cgbt", cgbt),
                new XElement("Sfjj", sfjj),
                new XElement("Cgyy", cgyy),
                new XElement("Bz", bz),
                new XElement("StrMx", strmx),
                new XElement("Flag", flag));

            string data = await postByName("UpdateJbCgspd", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return data;
        }

        public async Task<HsLabelValue> GetJbCgspd(string progressId, string djId)
        {
            XElement xData = new XElement("Data",
                                new XElement("ProgressId", progressId),
                                new XElement("DjId", djId));

            string data = await postByName("GetJbCgspd", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return XElement.Parse(HsGZip.DecompressString(data)).ToHsLabelValue();
        }

        #endregion

        #region 合同评审表

        public async Task<string> UpdateJbHtpsb(string progressId, string djId, string qrdId, string djrq, string htdw, string htmc, string htbh, string jgly, string jtsp, string fjxm, string htje, string fkfs, string htqx, string zytk, string bz, int flag)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("DjId", djId),
                new XElement("QrdId", qrdId),
                new XElement("Djrq", djrq),
                new XElement("Htdw", htdw),
                new XElement("Htmc", htmc),
                new XElement("Htbh", htbh),
                new XElement("Jgly", jgly),
                new XElement("Jtsp", jtsp),
                new XElement("Fjxm", fjxm),
                new XElement("Htje", htje),
                new XElement("Fkfs", fkfs),
                new XElement("Htqx", htqx),
                new XElement("Zytk", zytk),
                new XElement("Bz", bz),
                new XElement("Flag", flag));

            string data = await postByName("UpdateJbHtpsb", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return data;
        }

        public async Task<HsLabelValue> GetJbHtpsb(string progressId, string djId)
        {
            XElement xData = new XElement("Data",
                                new XElement("ProgressId", progressId),
                                new XElement("DjId", djId));

            string data = await postByName("GetJbHtpsb", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return XElement.Parse(HsGZip.DecompressString(data)).ToHsLabelValue();
        }

        #endregion

        #region 招标文件评审表

        public async Task<string> UpdateJbZbwjpsb(string progressId, string djId, string djrq, string xmmc, string wjbh, string jtsp, string ysje, string fjxm, string sfjg, string jgsm, string qtsm, string bz, int flag)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("DjId", djId),
                new XElement("Djrq", djrq),
                new XElement("Xmmc", xmmc),
                new XElement("Wjbh", wjbh),
                new XElement("Jtsp", jtsp),
                new XElement("Ysje", ysje),
                new XElement("Fjxm", fjxm),
                new XElement("Sfjg", sfjg),
                new XElement("Jgsm", jgsm),
                new XElement("Qtsm", qtsm),
                new XElement("Bz", bz),
                new XElement("Flag", flag));

            string data = await postByName("UpdateJbZbwjpsb", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return data;
        }

        public async Task<HsLabelValue> GetJbZbwjpsb(string progressId, string djId)
        {
            XElement xData = new XElement("Data",
                                new XElement("ProgressId", progressId),
                                new XElement("DjId", djId));

            string data = await postByName("GetJbZbwjpsb", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return XElement.Parse(HsGZip.DecompressString(data)).ToHsLabelValue();
        }
        #endregion
    }
}
