using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//自定义错误信息
namespace BenCaoGangMu.Common
{
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
