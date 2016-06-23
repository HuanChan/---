using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BenCaoGangMu.Entity;
using BenCaoGangMu.Tools;
using BenCaoGangMu.DataAccessLayer;

namespace BenCaoGangMu.DataAccessLayer
{
    /// <summary>
    /// 用户信息加密
    /// </summary>
    class UserDao
    {
        //加密密钥,要求为8位
        //private string  key= "ab123456";
        private string key = "12345678";
        //1 插入记录
        public int insert(User user)
        {
            //生成SQL语句
            string sql = "insert [User]  values (@UserId,@UserName,@UserPwd,@DepartmentId)";
            //定义参数数组
            SqlParameter[] parameters = new SqlParameter[4];
            //给参数数组赋值
            parameters[0] = new SqlParameter("@UserId", SqlDbType.Char, 3);
            parameters[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
            parameters[2] = new SqlParameter("@UserPwd", SqlDbType.VarChar, 60);
            parameters[3] = new SqlParameter("@DepartmentId", SqlDbType.Char, 3);

            //给参数数组中的每一个元素赋值
            parameters[0].Value = user.UserId;
            parameters[1].Value = user.UserName;


            parameters[2].Value = DES.EncryptDES(user.UserPwd.Trim(), key);  //加密密码

            parameters[3].Value = "1";


            DB db = new DB();
            int i = db.executeSQL(sql, parameters);
            return i;
        }



        //2 修改记录
        public int update(User user)
        {
            string sql = "update [User]  set UserName=@UserName,DepartmentId=@DepartmentId where UserId=@UserId";
            //定义参数数组
            //定义参数数组
            SqlParameter[] parameters = new SqlParameter[3];
            //给参数数组赋值           
            parameters[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
            parameters[1] = new SqlParameter("@DepartmentId", SqlDbType.Char, 3);
            parameters[2] = new SqlParameter("@UserId", SqlDbType.Char, 3);

            //给参数数组中的每一个元素赋值          
            parameters[0].Value = user.UserName;
            parameters[1].Value = "1";
            parameters[2].Value = user.UserId;

            DB db = new DB();
            int i = db.executeSQL(sql, parameters);
            return i;
        }
        //2_1 只修改用户密码
        public int update(string userId, string pwd)
        {
            string sql = "update [User]  set UserPwd=@UserPwd where UserId=@UserId";
            //定义参数数组
            SqlParameter[] parameters = new SqlParameter[2];
            //给参数数组赋值           
            parameters[0] = new SqlParameter("@UserPwd", SqlDbType.VarChar, 60);
            parameters[1] = new SqlParameter("@UserId", SqlDbType.Char, 3);

            //给参数数组中的每一个元素赋值   

            parameters[0].Value = DES.EncryptDES(pwd.Trim(), key);
            parameters[1].Value = userId;

            DB db = new DB();
            int i = db.executeSQL(sql, parameters);
            return i;
        }
        //3 删除记录:删除User表中记录的同时、UserRole表对应的记录
        public bool delete(string userId)
        {
            string sql_user = "delete [User] where userId=@userId";
            string sql_userrole = "delete UserRole where userId=@userId";

            SqlParameter[] para_user = new SqlParameter[1];
            para_user[0] = new SqlParameter("@userId", SqlDbType.Char, 3);
            para_user[0].Value = userId;

            //SqlParameter para_userrole = new SqlParameter("@userId", SqlDbType.Char, 3);
            //para_userrole.Value = userId;

            ArrayList sqls = new ArrayList();
            sqls.Add(sql_user);
            sqls.Add(sql_userrole);

            ArrayList parameters = new ArrayList();
            parameters.Add(para_user);
            parameters.Add(para_user);

            return new DB().executeTransaction(sqls, parameters);

        }


        //4 根据用户编号，查询用户信息
        public User queryById(string userid)
        {
            string sql = "select * from [User]  where UserId=@UserId";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserId", SqlDbType.Char, 3);
            parameters[0].Value = userid;

            User user = new DB().GeEntity<User>(sql, parameters);
            if (user != null)
            {
                user.UserPwd = DES.DecryptDES(user.UserPwd, key);  //解密密码
            }
            return user;
        }
        //5 根据用户名称，查询用户信息
        public User queryByUserName(string userName)
        {
            string sql = "select * from [User]  where UserName=@UserName";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
            parameters[0].Value = userName;

            User user = new DB().GeEntity<User>(sql, parameters);
            if (user != null)
            {
                user.UserPwd = DES.DecryptDES(user.UserPwd.Trim(), key);  //解密密码
            }
            return user;
        }

        //6 查询所有用户的信息
        public IList<User> query()
        {
            string sql = "select * from [User] ";

            return new DB().GeEntityList<User>(sql, null);
        }
        //7 根据用户编号，查询一个用户拥有的所有角色的信息
        public IList<Role> query(string userId)
        {
            string sql = "select r.RoleId,RoleName from  [Role] r inner join UserRole ur on r.RoleId=ur.RoleId  where UserId=@UserId";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserId", SqlDbType.Char, 3);
            parameters[0].Value = userId;

            return new DB().GeEntityList<Role>(sql, parameters);
        }
        //
        //8 根据用户编号，查询一个用户拥有的所有权限的信息
        public IList<Actions> queryActions(string userId)
        {
            string sql = "select * from view_user_action   where UserId=@UserId";// view_user_action 为视图

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserId", SqlDbType.Char, 3);
            parameters[0].Value = userId;

            return new DB().GeEntityList<Actions>(sql, parameters);
        }
    }
}
