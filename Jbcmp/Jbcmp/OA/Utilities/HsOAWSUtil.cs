using Hungsum.Framework.Extentsions;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using Hungsum.Jbcmp.OA.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hungsum.OA.Utilities
{
    public class HsOAWSUtil : SysWSUtil
    {
        #region 工作流

        public async Task<List<HsLabelValue>> ShowDbsxs(string progressId, string begindate, string enddate, string jlzt)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("BeginDate", begindate),
                new XElement("EndDate", enddate),
                new XElement("Jlzt", jlzt));

            string data = await postByName("ShowDbsxs", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }

        public async Task<List<HsLabelValue>> ShowHsLcspjls(string progressId, string djlx, string djId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Djlx", djlx),
                new XElement("DjId", djId));

            string data = await postByName("ShowHsLcspjls", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }

        public async Task<string> UpdateHsLcspjl(string progressId, string jlId, string spyj, string jlzt, string zdthspId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("JlId", jlId),
                new XElement("Spyj", spyj),
                new XElement("Jlzt", jlzt),
                new XElement("ZdthspId", zdthspId));

            return await postByName("UpdateHsLcspjl", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));
        }

        public async Task<List<HsLabelValue>> ShowHsLcbzfzs(string progressId, string jlId, string spyj, string jlzt, string zdthspId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("JlId", jlId),
                new XElement("Spyj", spyj),
                new XElement("Jlzt", jlzt),
                new XElement("ZdthspId", zdthspId));

            string data = await postByName("ShowHsLcbzfzs", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }

    }

    #endregion
}
