using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using Hungsum.Framework.Extentsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Hungsum.Framework.Utilities
{
    public abstract class SysWSUtil : HsCoreWSHelper
    {
        #region Upgrade

        public async Task<string> GetIOSClientInfo()
        {
            string data = await postByName("GetIOSClientInfo", "");

            return data;
        }

        #endregion

        #region GetDBs

        public async Task<string> GetDbs()
        {
            string data = await postByName("GetDbs", "");

            data = HsGZip.DecompressString(data);

            return data;
        }

        #endregion

        #region Login

        public async Task<string> Login(string username, string password, string connString)
        {
            XElement xData = new XElement("Data",
                new XElement("UserName", username),
                new XElement("Password", password),
                new XElement("ConnString", connString),
                new XElement("ClientType", "2")); //标记为IOS

            string data = await postByName("Login", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data;
        }

        #endregion

        #region Logout

        public async Task Logout(string progressId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId));

            await postByName("Logout", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));
        }

        #endregion

        #region Password

        public async Task ChangePassword(string progressId, string oldpassword, string newpassword)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("OldPassword", oldpassword),
                new XElement("NewPassword", newpassword));

            await postByName("ChangePassword", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));
        }

        #endregion

        #region GetQueryNameAndArgs

        public async Task<string> GetQueryNameAndArgs(string progressId, string queryName)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("QueryName", queryName),
                new XElement("QueryFlag", 2));

            string data = await postByName("GetQueryNameAndArgs", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data;
        }

        #endregion

        #region QueryResult

        public async Task<List<HsLabelValue>> QueryResult(string progressId, string queryName, XElement args)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("QueryName", queryName),
                args,
                new XElement("QueryFlag", 2));

            string data = await postByName("QueryResult", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }

        #endregion

        #region GetAutoCompleteData

        public async Task<string> GetAutoCompleteData(string progressId, string flag, string args = "")
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Flag", flag),
                new XElement("Args", args));

            string data = await postByName("GetAutoCompleteData", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data;
        }

        #endregion


        #region DoDatas

        public async Task<string> DoDatas(string progressId, string bhs, string actionFlag)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Bhs", bhs),
                new XElement("ActionFlag", actionFlag));

            string data = await postByName("DoDatas", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return data;
        }

        #endregion

        #region 图像

        public async Task<List<HsLabelValue>> GetSysImageHashDatas(string progressId, string djlx, string djId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Class", djlx),
                new XElement("ClassId", djId));

            string data = await postByName("GetSysImageHashDatas", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }

        public async Task<string> GetSysImageById(string progressId, string imageId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("ImageId", imageId));

            string data = await postByName("GetSysImageById", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data;
        }

        public async Task<int> UpdateSysImageByHashData(string progressId, string djlx, string djId, string hashData)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Class", djlx),
                new XElement("ClassId", djId),
                new XElement("HashData", hashData));

            string data = await postByName("UpdateSysImageByHashData", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return int.Parse(data);
        }

        public async Task<int> UpdateSysImage(string progressId, string djlx, string djId, string imageData)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Class", djlx),
                new XElement("ClassId", djId),
                new XElement("ImageData", imageData));

            string data = await postByName("UpdateSysImage", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return int.Parse(data);
        }

        public async Task<string> DeleteSysImages(string progressId, string djlx, string djId, int needDeleteImageMaxId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Class", djlx),
                new XElement("ClassId", djId),
                new XElement("MaxId", needDeleteImageMaxId));

            string data = await postByName("DeleteSysImages", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return data;
        }

        #endregion



        #region 文件

        public async Task<List<HsLabelValue>> GetSysFiles(string progressId, string djlx, string djId)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("Class", djlx),
                new XElement("ClassId", djId));

            string data = await postByName("GetSysFiles", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            data = HsGZip.DecompressString(data);

            return data.ToHsLabelValues();
        }


        public async Task<HsLabelValue> GetSysFileSeg(string progressId, string hashdata, int order = 0)
        {
            XElement xData = new XElement("Data",
                new XElement("ProgressId", progressId),
                new XElement("HashData", hashdata),
                new XElement("Order", order));

            string data = await postByName("GetSysFileSeg", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

            return string.IsNullOrEmpty(data) ? null : XElement.Parse(HsGZip.DecompressString(data)).ToHsLabelValue();
        }

        #endregion
    }
}