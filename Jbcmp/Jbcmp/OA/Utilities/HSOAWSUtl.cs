using Hungsum.Framework.Extentsions;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hungsum.OA.Utilities
{
    public class HSOAWSUtl : SysWSUtil
    {
        #region 工作流

        public async Task<List<HsLabelValue>> ShowDbsxs(string progressId,string begindate,string enddate,string jlzt)
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

        //public async Task<string> UpdateKh(string progressId, string khId, string khmc, string dz, string yzbm, string bz, int flag)
        //{
        //    XElement xData = new XElement("Data",
        //        new XElement("ProgressId", progressId),
        //        new XElement("KhId", khId),
        //        new XElement("Khmc", khmc),
        //        new XElement("Dz", dz),
        //        new XElement("Yzbm", yzbm),
        //        new XElement("Bz", bz),
        //        new XElement("Flag", flag));

        //    string data = await postByName("UpdateKh", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

        //    return data;
        //}

        #endregion
    }
}
