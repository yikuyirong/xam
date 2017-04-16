using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Models
{
    public class LoginData
    {
        public readonly string ProgressId;

        public readonly string Userbh;

        public readonly string Username;

        public readonly string Deptbh;

        public readonly string Deptmc;

        public readonly string Rolebhs;

        public readonly string Rolemcs;

        public LoginData(string progressId, string userbh, string username, string deptbh, string deptmc, string rolebhs, string rolemcs)
        {
            this.ProgressId = progressId;

            this.Userbh = userbh;

            this.Username = username;

            this.Deptbh = deptbh;

            this.Deptmc = deptmc;

            this.Rolebhs = rolebhs;

            this.Rolemcs = rolemcs;
        }
    }
}
