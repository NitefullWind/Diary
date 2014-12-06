using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    class User
    {
        private string userName;
        private string userPassword;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
    }
}
