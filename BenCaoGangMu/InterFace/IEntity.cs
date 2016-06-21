using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//建立实体接口
namespace BenCaoGangMu.InterFace
{
    public interface IEntity
    {
        void setValue(int index, Object obj);   //为属性设置值
        int getMaxFieldNum();                   //获取实体属性的总个数
        Object getID();                         //获取区分实体的唯一标识
    }
}
