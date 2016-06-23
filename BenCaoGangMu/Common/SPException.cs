using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenCaoGangMu.Common
{
    /// <summary>
    /// 自定义错误信息
    /// </summary>
    class SPException : Exception
    {
        public SPException()
        {
        }

        //先执行base()，也就是 Exception() 的构造函数
        public SPException(string msg)
            : base(msg)
        {

        }
    }
}
