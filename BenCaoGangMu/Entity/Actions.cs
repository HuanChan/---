using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenCaoGangMu.Entity
{
    /// <summary>
    /// 设置 Actions 的属性
    /// </summary>
    class Actions
    {
        private string actionId;
        public string ActionId
        {
            get { return actionId; }
            set { actionId = value; }
        }
        private string actionName;
        public string ActionName
        {
            get { return actionName; }
            set { actionName = value; }
        }
    }
}
