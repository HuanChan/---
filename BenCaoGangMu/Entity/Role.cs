using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenCaoGangMu.Entity
{
    /// <summary>
    /// 设置 Role* 的属性
    /// </summary>
    class Role
    {
        private string roleId;
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
    }
}
