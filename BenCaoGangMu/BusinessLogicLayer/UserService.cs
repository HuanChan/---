using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenCaoGangMu.Entity;
using BenCaoGangMu.DataAccessLayer;

namespace BenCaoGangMu.BusinessLogicLayer
{
    /// <summary>
    /// 用户信息的修改
    /// </summary>
    class UserService
    {
        //1 插入记录
        public static int insert(User user)
        {
            return new UserDao().insert(user);
        }
        //2 修改记录
        public static int update(User user)
        {
            return new UserDao().update(user);
        }
        //2_1 只修改用户密码
        public static int update(string userId, string pwd)
        {
            return new UserDao().update(userId, pwd);
        }
        //3 删除记录:删除User表中记录的同时、UserRole表对应的记录
        public static bool delete(string userId)
        {
            return new UserDao().delete(userId);
        }
        //4 根据用户编号，查询用户信息
        public static User queryById(string userid)
        {
            return new UserDao().queryById(userid);
        }
        //5 根据用户名称，查询用户信息
        public static User queryByUserName(string userName)
        {
            return new UserDao().queryByUserName(userName);
        }
        //6 查询所有用户的信息
        public static IList<User> query()
        {
            return new UserDao().query();
        }
        //7  查询一个用户拥有的所有角色的信息
        public static IList<Role> query(string userId)
        {
            return new UserDao().query(userId);
        }
        //8 根据用户编号，查询一个用户拥有的所有权限的信息
        public static IList<Actions> queryActions(string userId)
        {
            return new UserDao().queryActions(userId);
        }
    }
}
