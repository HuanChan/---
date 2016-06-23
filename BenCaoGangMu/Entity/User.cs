using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenCaoGangMu.Entity
{
    /// <summary>
    /// 设置 User* 的属性
    /// </summary>
    public class User
    {
        private string userId;
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string userPwd;
        public string UserPwd
        {
            get { return userPwd; }
            set { userPwd = value; }
        }

        private string departmentId;
        public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }
    }
}
