using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Menukit
{
    public interface IFormsAuth
    {
        bool Authenticate(string name, string password);
        void SetAuthCookie(string name, bool persistent);
    }

    public class FormsAuthWrapper : IFormsAuth
    {
        public bool Authenticate(string name, string password)
        {
            return FormsAuthentication.Authenticate(name, password);
        }

        public void SetAuthCookie(string name, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(name, persistent);
        }
    }
}
