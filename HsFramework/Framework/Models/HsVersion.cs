using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Models
{
    public class HsVersion : IComparer<HsVersion>, IComparable, IComparable<HsVersion>
    {
        private string _version;

        public string Version
        {
            get { return _version; }
        }

        public static HsVersion Parse(string version)
        {
            return new HsVersion(version);
        }


        private HsVersion(string version)
        {
            _version = version;
        }

        public EType Type
        {
            get
            {
                try
                {
                    if (int.Parse(Version.Split('.').Last()) % 2 == 0)
                    {
                        return EType.Force;
                    } else
                    {
                        return EType.Normal;
                    }
                } catch
                {
                    return EType.Normal;
                }
            }
        }

        public override string ToString()
        {
            return _version;
        }

        #region Compare

        public static bool operator >(HsVersion x, HsVersion y)
        {
            return x.CompareTo(y) > 0;
        }

        public static bool operator <(HsVersion x, HsVersion y)
        {
            return x.CompareTo(y) < 0;
        }

        public int Compare(HsVersion x, HsVersion y)
        {
            string[] v1 = x.Version.Split('.');
            string[] v2 = y.Version.Split('.');

            int result = 0;

            for (int i = 0; i < Math.Max(v1.Length, v2.Length); i++)
            {
                if (v1.Length <= i)
                {
                    return -1;
                }

                if (v2.Length <= i)
                {
                    return 1;
                }

				//比较字符串长度，如果长度不同则前补零
				int comLength = Math.Max(v1[i].Length, v2[i].Length);

				result = v1[i].PadLeft(comLength, '0').CompareTo(v2[i].PadLeft(comLength, '0'));

				if (result != 0)
                {
                    return result;
                }
            }

            return result;
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo(obj as HsVersion);
        }

        public int CompareTo(HsVersion other)
        {
            return this.Compare(this, other);
        }

        #endregion

        public enum EType
        {
            Normal = 0, Force
        }
    }
}
