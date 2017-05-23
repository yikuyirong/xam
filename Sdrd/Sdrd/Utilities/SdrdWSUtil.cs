using Hungsum.Framework.Utilities;
using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Hungsum.Framework.Extentsions;

namespace Hungsum.Sdrd.Utilities
{
	public class SdrdWSUtil : SysWSUtil
	{
		#region 客户维护

		public async Task<List<HsLabelValue>> ShowKhs(string progressId)
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId));

			string data = await postByName("ShowKhs", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();
		}


		public async Task<string> UpdateKh(string progressId, string khId, string khmc, string dz, string yzbm, string bz, int flag)
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("KhId", khId),
				new XElement("Khmc", khmc),
				new XElement("Dz", dz),
				new XElement("Yzbm", yzbm),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateKh", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}

		#endregion

		#region 联系人维护

		public async Task<List<HsLabelValue>> ShowLxrs(string progressId, string khId)
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
			new XElement("KhId", khId));

			string data = await postByName("ShowLxrs", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();
		}

		public async Task<string> UpdateLxr(string progressId, string lxrId, string khId, string name, string zwzc,
			string sex, string birthday, string phone, string phone2, string email, string qq, string we, string hobby, string bz, int flag)
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("LxrId", lxrId),
				new XElement("KhId", khId),
				new XElement("Name", name),
				new XElement("Zwzc", zwzc),
				new XElement("Sex", sex),
				new XElement("Birthday", birthday),
				new XElement("Phone", phone),
				new XElement("Phone2", phone2),
				new XElement("Email", email),
				new XElement("QQ", qq),
				new XElement("We", we),
				new XElement("Hobby", hobby),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateLxr", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}


		#endregion

		#region 客户交流记录维护

		public async Task<List<HsLabelValue>> ShowKhjljls(string progressId, string beginDate, string endDate, string zdr = "")
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("BeginDate", beginDate),
				new XElement("EndDate", endDate),
				new XElement("Zdr", zdr));


			string data = await postByName("ShowKhjljls", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();

		}

		public async Task<string> UpdateKhjljl(string progressId, string jlId, string xmId, string lxrId, string jlrq,
			string jllx, string jlpj, string jlzy, string bz, int flag)
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("JlId", jlId),
				new XElement("XmId", xmId),
				new XElement("LxrId", lxrId),
				new XElement("Jlrq", jlrq),
				new XElement("Jllx", jllx),
				new XElement("Jlpj", jlpj),
				new XElement("Jlzy", jlzy),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateKhjljl", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}

		#endregion

		#region 项目

		public async Task<List<HsLabelValue>> ShowXms(string progressId, string beginDate, string endDate, string xmjd, string xmzt, string zdr)
		{
			XElement xData = new XElement("Data",
										  new XElement("ProgressId", progressId),
										  new XElement("BeginDate", beginDate),
										  new XElement("EndDate", endDate),
										  new XElement("Xmjd", xmjd),
										  new XElement("Xmzt", xmzt),
										  new XElement("Zdr", zdr));


			string data = await postByName("ShowXms", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();

		}

		public async Task<string> UpdateXm(string progressId, string xmId, string xmmc, string xmly,
			string khId, string rq, string lxr, string zy, string xmjd, string bz, int flag)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("XmId", xmId),
				new XElement("Xmmc", xmmc),
				new XElement("Xmly", xmly),
				new XElement("KhId", khId),
				new XElement("Rq", rq),
				new XElement("Lxr", lxr),
				new XElement("Zy", zy),
				new XElement("Xmjd", xmjd),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateXm", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}

		#endregion

		#region 合同

		public async Task<List<HsLabelValue>> ShowHts(string progressId, string beginDate, string endDate, string htzt, string zdr = "")
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("BeginDate", beginDate),
				new XElement("EndDate", endDate),
				new XElement("Htzt", htzt),
				new XElement("Zdr", zdr));


			string data = await postByName("ShowHts", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();

		}

		public async Task<string> GetHt(string progressId, string htId)
		{
			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("HtId", htId));

			string data = await postByName("GetHt", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data;
		}

		public async Task<string> UpdateHt(string progressId, string htId, string xmId, string khId, string htrq, string htbh, string htmc, string htje, string fkfs, string htqx, string zytk, string bz, int flag)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("HtId", htId),
				new XElement("XmId", xmId),
				new XElement("KhId", khId),
				new XElement("Htrq", htrq),
				new XElement("Htbh", htbh),
				new XElement("Htmc", htmc),
				new XElement("Htje", htje),
				new XElement("Fkfs", fkfs),
				new XElement("Htqx", htqx),
				new XElement("Zytk", zytk),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateHt", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}

		#endregion

		#region 合同回款记录

		public async Task<List<HsLabelValue>> ShowHthkjls(string progressId, string beginDate, string endDate, string jlzt)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("BeginDate", beginDate),
				new XElement("EndDate", endDate),
				new XElement("Jlzt", jlzt));


			string data = await postByName("ShowHthkjls", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();

		}

		public async Task<List<HsLabelValue>> ShowHthkjls(string progressId, string htId)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("HtId", htId));


			string data = await postByName("ShowHthkjls", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();

		}


		public async Task<string> UpdateHthkjl(string progressId, string jlId, string htId, string jlrq, string hksm, string hkje, string bz, int flag)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("JlId", jlId),
				new XElement("HtId", htId),
				new XElement("Jlrq", jlrq),
				new XElement("Hksm", hksm),
				new XElement("Hkje", hkje),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateHthkjl", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}
		#endregion

		#region 合同考核记录

		public async Task<List<HsLabelValue>> ShowHtkhjls(string progressId, string beginDate, string endDate, string jlzt)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("BeginDate", beginDate),
				new XElement("EndDate", endDate),
				new XElement("Jlzt", jlzt));


			string data = await postByName("ShowHtkhjls", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			data = HsGZip.DecompressString(data);

			return data.ToHsLabelValues();

		}

		public async Task<string> UpdateHtkhjl(string progressId, string jlId, string htId, string jlrq, string khmyd, string sjzlyll, string xmjd, string xmrcgl, string bmjdxz, string kzxzb, string bz, int flag)
		{

			XElement xData = new XElement("Data",
				new XElement("ProgressId", progressId),
				new XElement("JlId", jlId),
				new XElement("HtId", htId),
				new XElement("Jlrq", jlrq),
				new XElement("Khmyd", khmyd),
				new XElement("Sjzlyll", sjzlyll),
				new XElement("Xmjd", xmjd),
				new XElement("Xmrcgl", xmrcgl),
				new XElement("Bmjdxz", bmjdxz),
				new XElement("Kzxzb", kzxzb),
				new XElement("Bz", bz),
				new XElement("Flag", flag));

			string data = await postByName("UpdateHtkhjl", HsGZip.CompressString(xData.ToString(SaveOptions.DisableFormatting)));

			return data;
		}

		#endregion

	}
}
