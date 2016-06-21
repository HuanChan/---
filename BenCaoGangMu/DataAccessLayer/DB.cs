using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;       //命名空间提供类，使您能够与系统进程、事件日志和性能计数器进行交互。
using System.Reflection;        //命名空间包含通过检查托管代码中程序集、模块、成员、参数和其他实体的元数据来检索其相关信息的类型。
using System.Configuration;     //命名空间包含提供用于处理配置数据的编程模型的类型。
using BenCaoGangMu.InterFace;   //接口命名空间

// 功能：提供访问数据库的基本操作，事务管理的实现等
namespace BenCaoGangMu.DataAccessLayer
{
    /// <summary>
    ///DB 的摘要说明
    ///本类的作用：提供访问数据库的各种操作
    /// </summary>
    public class DB : IDisposable
    {
        #region 1.数据库的打开和连接，并释放资源
        public string constring = null;
        SqlConnection con = null;

        //不带参数的构造函数：与数据库的连接信息直接从配置文件app.config 中读取。
        public DB()
        {
            //添加了using Configuration; 还是无法引用 类ConfigurationManager。
            //解决方案资源管理器\引用\右键\添加\程序集\框架\.net\找到System.Configuration把它加上去就OK啦
            constring = ConfigurationManager.ConnectionStrings["conString"].ToString();
        }

        //带参数的构造函数
        public DB(string constr)
        {
            constring = constr;
        }

        //打开连接
        protected void open()
        {
            if (con == null)
            {
                con = new SqlConnection(constring);
            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        //关闭连接
        protected void close()
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        //释放资源
        public void Dispose()
        {
            if (con != null)
            {
                //Dispose 方法使 Component（组件） 处于不可用状态。
                //调用完 Dispose 之后，必须释放对 Component 的所有引用。
                //这样垃圾回收器GC 才能收回 Component 占用的内存。
                con.Dispose();
                con = null;
            }
        }

        //析构函数
        ~DB()
        {
            close();   //关闭连接,后面会定义
            Dispose();  //释放非托管资源（如文件流、数据库连接等）
        }
        #endregion

        /// <summary>
        /// 2.功能：查询记录，获取一个 DataSet 对象
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <return>返回一个 DataSet对象</return>
        public DataSet getDataSet(string sql, SqlParameter[] parameters)
        {
            //SqlParameter：表示 SqlCommand 的参数，也可以是它到 DataSet 列的映射。
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    //为SelectCommand 命令对象添加参数
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
                //DataSet:数据内存中的缓存，对应DataReader；
                //DataAdapter：数据适配器，对应Command；
                //DataAdapter使用SelectCommand执行sql语句后，得到数据集adapter，然后将adapter填充Fill到ds中
                //所以，最后返回的是ds；
            }
            catch (Exception e)
            //Exception：表示程序执行过程中发生的错误
            //这是一个事件，用e存储
            {
                return null;
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// 3.功能：将一个 DataSet 对象写入到 XML 文件（在打印报表时，用到此方法）
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数数组</param>
        /// <param name="tableName">生成的内存数据表名称</param>
        /// <returns>操作成功：true，操作失败：false</returns>
        public bool getDataSetToXML(string sql, CommandType commandType, SqlParameter[] parameters, string tableName)
        {
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                    adapter.SelectCommand.CommandType = commandType;
                }
                DataSet ds = new DataSet();
                adapter.Fill(ds, tableName);
                ds.WriteXml(@"ds:/" + tableName + ".xml");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// 4.功能：查询，向 DataSet 对象中填充数据，选取从 m 开始的 n 个记录 
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">命令参数数组</param>
        /// <param name="m">开始记录号</param>
        /// <param name="n">（选取的） 记录个数</param>
        /// <param name="tablename">生成的内存数据表名称</param>
        /// <returns>执行成功返回生成的 DataSet 对象，否则为null</returns>
        public DataSet getDataSet(string sql, SqlParameter[] parameters, int m, int n, string tablename)
        {
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    //adapter执行sql语句，把获得的数组填充入parameters。range：排列。
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                DataSet ds = new DataSet();
                //把获得的数据，从 m 开始，一共 n 行的数据，填充到tablename
                adapter.Fill(ds, m, n, tablename);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// 5.功能：查询，返回一个 DataTable 对象
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行成功返回生成的 DataTable 对象，否则为null</returns>
        public DataTable getDataTable(string sql, SqlParameter[] parameters)
        {
            //不用连接：getDataSet()方法已经定义过连接关闭
            DataSet ds = getDataSet(sql, parameters);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 6.功能：查询，返回一个 DataRow 对象
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行成功返回生成的 DataRow 对象，否则为 null</returns>
        public DataRow getDataRow(string sql, SqlParameter[] parameters)
        {
            DataTable tb = getDataTable(sql, parameters);
            if (tb != null && tb.Rows.Count > 0)
            {
                return tb.Rows[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 7.1 功能：查询，返回一个 DataReader 对象，(调用后注意调用SqlDataReader.Close())
        /// </summary>
        /// <param name="sql">SQL 语句、存储过程名称</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行成功返回生成的 IDataReader 对象，否则为null</returns>
        public IDataReader getSqlDataReader(string sql,SqlParameter[] parameters)
        {   //parameter:参数，是一组数据类型，比如string，char等。
            //数据库会自动判断它属于什么类型，从而使用。
            try
            {
                open();
                SqlCommand cmd = new SqlCommand(sql, con);
                //CommandType:获取或设置一个值，该值指示如何解释 CommandText 属性。
                if (parameters != null)
                {
                    //如果 parameter 不为空，那么就自动在 sql 后面插入数组 parameters。
                    cmd.Parameters.AddRange(parameters);
                }
                //如果关闭关联的 DataReader 对象，则关联的Connection 对象也将关闭
                //用户并不直接创建 DataReader 类的实例， 而是通过使用 Command 对象的 ExecuteReader 方法获取 DataReader 实例。 
                //解释：生成一个接口的reader（也就是SqlDataReader），使用cmd执行读取的方法（Execute：执行）。
                IDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 7.2 功能：查询，返回一个 DataReader 对象（同上，多一个参数CommandType）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDataReader getSqlDataReader(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = commandType;

                //cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                IDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);//如果关闭关联的 DataReader 对象，则关联的 Connection 对象也将关闭
                return reader;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 8.功能：获得一个实体类的对象
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">命令参数数组</param>
        /// <returns>返回一个实体类的对象</returns>
        public T GetEntity<T>(string sql, SqlParameter[] parameters)
        {
            //泛型：generic，通用类型，使类型参数化，能够节省装箱拆箱的时间。
            //T就是参数类型,代编任意参数类型。
            //IEntity，自定义的、为属性设置值的一个接口，在BenCaoGangMu.InteFace
            try
            {
                IDataReader reader = getSqlDataReader(sql, parameters);
                T entity = DataReaderHelper.ReaderToEntity<T>(reader);
                reader.Close();
                return entity;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 9.1 功能：获取实体类对象的集合
        /// </summary>
        /// <typeparam name="T">类型参数，可以看成是占位符</typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>实体类对象的集合</returns>
        public IList<T> GeEntity<T>(string sql, SqlParameter[] parameters)
        {
            try
            {
                IDataReader reader = getSqlDataReader(sql, parameters);
                IList<T> list = DataReaderHelper.ReaderToList<T>(reader);
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 9.2 功能：获取实体类对象的集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="commandType">命令对象类型</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>返回实体类的对象集合</returns>
        public IList<T> GeEntity<T>(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                IDataReader reader = getSqlDataReader(sql, commandType, parameters);
                IList<T> list = DataReaderHelper.ReaderToList<T>(reader);
                reader.Close();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 10.获取实现 Ientity 接口的实体类对象集合（利用接口）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="entity">实现IEntity 接口的实体类</param>
        /// <returns>实现 IEntity 接口的实体类对象集合</returns>
        public IList<IEntity> GeIEntityList(string sql, SqlParameter[] parameters, IEntity entity)
        {
            try
            {
                IDataReader reader = getSqlDataReader(sql, parameters);
                IList<IEntity> list = new List<IEntity>();
                while (reader.Read())
                {
                    for (int i = 0; i < entity.getMaxFieldNum(); i++)
                    {
                        try
                        {
                            entity.setValue(i, reader[i]);
                        }
                        catch { }
                    }
                    list.Add(entity);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取 IEntity的实体类集合 (利用反射机制)                               类的完整字符串名称："SuperMarket.Entity. Customer_SalesMan_View"
        public IList<IEntity> GeIEntityList(string sql, SqlParameter[] parameters, string entityName)
        {
            try
            {
                IDataReader reader = getSqlDataReader(sql, parameters);
                IList<IEntity> list = new List<IEntity>();
                //Type t = Type.GetType("SuperMarket.Entity.Customer_SalesMan_View");
                Type t = Type.GetType(entityName);
                object obj = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod("getMaxFieldNum");
                int n = (int)method.Invoke(obj, null);

                while (reader.Read())
                {
                    obj = Activator.CreateInstance(t);
                    MethodInfo method2 = t.GetMethod("setValue");
                    for (int i = 0; i < n; i++)
                    {
                        object[] paras = new object[] { i, reader[i] };
                        try                              //处理日期型字段为空、数值型字段为空的情况
                        {
                            method2.Invoke(obj, paras);
                        }
                        catch { }

                    }

                    list.Add(obj as IEntity);

                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //可以判断 SqlCommand对象的命令类型.CommandType
        // 获取 IEntity的实体类集合 (利用反射机制)                               类的完整字符串名称："SuperMarket.Entity. Customer_SalesMan_View"
        public IList<IEntity> GeIEntityList(string sql, CommandType commandType, SqlParameter[] parameters, string entityName)
        {
            try
            {
                IDataReader reader = getSqlDataReader(sql, commandType, parameters);
                IList<IEntity> list = new List<IEntity>();
                //Type t = Type.GetType("SuperMarket.Entity.Customer_SalesMan_View");
                Type t = Type.GetType(entityName);
                object obj = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod("getMaxFieldNum");
                int n = (int)method.Invoke(obj, null);

                while (reader.Read())
                {
                    obj = Activator.CreateInstance(t);
                    MethodInfo method2 = t.GetMethod("setValue");
                    for (int i = 0; i < n; i++)
                    {
                        object[] paras = new object[] { i, reader[i] };
                        try                              //处理日期型字段为空、数值型字段为空的情况
                        {
                            method2.Invoke(obj, paras);
                        }
                        catch { }

                    }

                    list.Add(obj as IEntity);

                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //执行Insert、 Update 、Deelete等语句
        public int executeSQL(string sql, SqlParameter[] parameters)
        {
            try
            {
                open();
                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType=CommandType.StoredProcedure ;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteNonQuery();
                //cmd.ExecuteNonQuery();
                //return "OK";
            }
            catch (Exception e)
            {
                return 0;
                //return e.Message;
            }
            finally
            {
                close();
            }
        }

        //执行存储过程（Insert、 Update 、Deelete等语句）
        public int executeSQL_Proc(string sql, SqlParameter[] parameters)
        {
            try
            {
                open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                int i = cmd.ExecuteNonQuery();
                return i;
                //return cmd.ExecuteNonQuery();
                //cmd.ExecuteNonQuery();
                //return "OK";
            }
            catch (Exception e)
            {
                return 0;
                //return e.Message;
            }
            finally
            {
                close();
            }
        }

        //测试专用
        public string executeSQL1(string sql, SqlParameter[] parameters)
        {
            try
            {
                open();
                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType=CommandType.StoredProcedure ;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                //return cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                return "OK";
            }
            catch (Exception e)
            {
                //return 0;
                return e.Message;
            }
            finally
            {
                close();
            }
        }
        //执行带有返回值、输出参数的存储过程（含有于 SELECT语句）
        //注:作为学生调用存储过程的入门方法
        public Hashtable executeProcedure(string sql, SqlParameter[] parameters, int m, int n, string tablename)
        {
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);          //为SelectCommand命令对象添加参数
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                }
                DataSet ds = new DataSet();
                adapter.Fill(ds, m, n, tablename);
                if ((int)adapter.SelectCommand.Parameters["@returnValue"].Value == 1)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ds", ds);
                    ht.Add("count", adapter.SelectCommand.Parameters["@count"].Value);
                    return ht;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                close();
            }
        }
        //执行带有返回值、输出参数的存储过程（含有于 SELECT语句），
        public Hashtable newExecuteProcedure(string sql, SqlParameter[] parameters, int m, int n, string tablename)
        {
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);          //为SelectCommand命令对象添加参数
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                }
                DataSet ds = new DataSet();
                adapter.Fill(ds, m, n, tablename);
                if ((int)adapter.SelectCommand.Parameters["@returnValue"].Value == 1)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ds", ds);
                    //ht.Add("count", adapter.SelectCommand.Parameters["@count"].Value);
                    foreach (SqlParameter parameter in parameters)  //将输出参数的值保存到 Hashtable对象中
                    {
                        if (parameter.Direction == ParameterDirection.Output)
                        {
                            ht.Add(parameter.ParameterName, parameter.Value);
                        }
                    }
                    return ht;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                close();
            }
        }
        //测试用
        public string executeProcedure1(string sql, SqlParameter[] parameters, int m, int n, string tablename)
        {
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);          //为SelectCommand命令对象添加参数
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                }
                DataSet ds = new DataSet();
                adapter.Fill(ds, m, n, tablename);
                //sqlpara.Direction=ParameterDirection.ReturnValue
                if ((int)adapter.SelectCommand.Parameters["@returnValue"].Value == 1)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ds", ds);
                    ht.Add("count", adapter.SelectCommand.Parameters["@count"].Value);
                    return "OK";
                }
                else
                {
                    return "OK";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                close();
            }
        }
        // 执行存储过程，返回值类型为DataSet
        public DataSet executeProcedure_ds(string sql, SqlParameter[] parameters)
        {
            try
            {
                open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);          //为SelectCommand命令对象添加参数                    
                }
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    return ds; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                close();
            }
        }
        //功能:获取一个指定字段(表达式)的值:(总是返回第一行第一列)
        //参数:名称             类型        含义 
        //      sql             string      要执行的SQL语句
        //      parameters      SqlParameter[]   命令参数
        //返回值:执行成功返回生成的 值,否则为 null
        public object executeScalar(string sql, SqlParameter[] parameters)
        {
            try
            {
                open();
                SqlCommand cmd = new SqlCommand(sql, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                close();
            }
        }

        //在.NET的ADO.NET数据库编程中,使用SqlTransaction实现事务操作
        //参数:名称             类型                    含义 
        //    sqlarray        String 数组             要执行的一系列相关的SQL语句
        // 返回值:执行成功返回 true,否则为 false
        public bool executeTransaction(string[] sqlarray)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            try
            {
                foreach (string sql in sqlarray)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }

        //执行事务方法的重载（不带参数）
        public bool executeTransaction(ArrayList sqlarray)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            try
            {
                foreach (string sql in sqlarray)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }

        //执行事务方法的重载（带参数）
        public bool executeTransaction(ArrayList sqlarray, ArrayList parameters)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            try
            {
                for (int i = 0; i < sqlarray.Count; i++)
                {
                    cmd.CommandText = sqlarray[i].ToString();
                    if (parameters[i] != null)
                    {
                        cmd.Parameters.AddRange((SqlParameter[])parameters[i]);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }
                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }


        /// <summary>
        /// 执行事务方法的重载（带参数）  
        /// 公有方法，此方法具有针对性，无通用性(适用于采购单、销售单的管理：同时保存、修改、删除 主表--明细表记录)。
        /// </summary>
        /// <param name="sqls">要执行的SQL语句：类型：IList<string>，
        ///              sqls[0]：对主表执行的命令，
        ///              sqls[1]：对明细表执行的插入记录命令，无插入则为null,
        ///              sqls[2]：对明细表执行的修改记录命令，无修改则为null,
        ///              sqls[3]：对明细表执行的删除记录命令，无删除则为null,
        /// 
        /// </param>
        /// <param name="master_parameters">对主表执行命令需要的参数，无参数则为null，类型：IList<SqlParameter[]></param>
        /// <param name="insert_parameters">对明细表执行的插入记录命令需要的参数，无参数则为null，类型：IList<SqlParameter[]></param>
        /// <param name="update_parameters">对明细表执行的修改记录命令需要的参数，无参数则为null，类型：IList<SqlParameter[]></param>
        /// <param name=" del_parameters">对明细表执行的删除记录命令需要的参数，无参数则为null，类型：IList<SqlParameter></param>
        /// <returns>执行成功：true，失败：false</returns>

        public bool executeTransaction_few(IList<string> sqls, IList<SqlParameter[]> master_parameters, IList<SqlParameter[]> insert_parameters, IList<SqlParameter[]> update_parameters, IList<SqlParameter> del_parameters)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                ////1  添加、修改主表记录
                cmd.Parameters.Clear();
                cmd.CommandText = sqls[0];
                if (master_parameters != null)
                {
                    cmd.Parameters.AddRange(master_parameters[0]);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    return false;
                }
                //2 添加明细表记录               
                if (sqls[1] != null)
                {
                    cmd.CommandText = sqls[1];
                    if (insert_parameters != null)
                    {
                        for (int i = 0; i < insert_parameters.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(insert_parameters[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                //3 修改明细表记录  
                if (sqls[2] != null)
                {
                    cmd.CommandText = sqls[2];
                    if (update_parameters != null)
                    {
                        for (int i = 0; i < update_parameters.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(update_parameters[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // 4 删除明细记录
                if (sqls[3] != null)
                {
                    cmd.CommandText = sqls[3];
                    if (del_parameters != null)
                    {
                        for (int i = 0; i < del_parameters.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(del_parameters[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }

        //删除 主表--明细表中的记录 （如果删除数据库中记录的数据表之间的主外键关系，此方法和上一方法可以选择其中一个，否则，只能分为2个方法）
        public bool executeTransaction_few_del(IList<string> sqls, IList<SqlParameter[]> master_parameters, IList<SqlParameter[]> insert_parameters, IList<SqlParameter[]> update_parameters, IList<SqlParameter> del_parameters)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                ////1  添加、修改主表记录
                //cmd.Parameters.Clear();
                //cmd.CommandText = sqls[0];
                //if (master_parameters != null)
                //{
                //    cmd.Parameters.AddRange(master_parameters[0]);
                //    cmd.ExecuteNonQuery();
                //}
                //else
                //{
                //    return false;
                //}
                //2 添加明细表记录               
                if (sqls[1] != null)
                {
                    cmd.CommandText = sqls[1];
                    if (insert_parameters != null)
                    {
                        for (int i = 0; i < insert_parameters.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(insert_parameters[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                //3 修改明细表记录  
                if (sqls[2] != null)
                {
                    cmd.CommandText = sqls[2];
                    if (update_parameters != null)
                    {
                        for (int i = 0; i < update_parameters.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(update_parameters[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // 4 删除明细记录
                if (sqls[3] != null)
                {
                    cmd.CommandText = sqls[3];
                    if (del_parameters != null)
                    {
                        for (int i = 0; i < del_parameters.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(del_parameters[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                //1  添加、修改主表记录
                cmd.Parameters.Clear();
                cmd.CommandText = sqls[0];
                if (master_parameters != null)
                {
                    cmd.Parameters.AddRange(master_parameters[0]);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    return false;
                }

                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }



        //执行事务方法的重载（带参数,）
        //说明：此方法具有针对性，无通用性
        public bool executeTransaction_few(ArrayList sqlarray, ArrayList parameters)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            try
            {
                //插入订单表记录
                cmd.CommandText = sqlarray[0].ToString();
                if (parameters[0] != null)
                {
                    cmd.Parameters.AddRange((SqlParameter[])parameters[0]);
                }
                cmd.ExecuteNonQuery();

                //插入订单详细信息表记录
                for (int i = 1; i < parameters.Count; i++)
                {
                    if (parameters[i] != null)
                    {
                        cmd.Parameters.AddRange((SqlParameter[])parameters[i]);
                    }
                    cmd.ExecuteNonQuery();
                }
                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }

        //执行事务方法的重载（带参数,应用于页面 checkOut.aspx 的“提交订单按钮”）
        //说明：此方法具有针对性，无通用性
        public bool executeTransaction_few(string sql, ArrayList parameters)
        {
            open();
            SqlTransaction ts = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = ts;
            try
            {
                cmd.CommandText = sql;

                //插入订单详细信息表记录
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (parameters[i] != null)
                    {
                        cmd.Parameters.AddRange((SqlParameter[])parameters[i]);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                ts.Commit();
                return true;
            }
            catch (Exception e)
            {
                ts.Rollback();
                return false;
            }
            finally
            {
                ts.Dispose();
                cmd.Dispose();
                close();
            }
        }

        /// <summary>
        /// 公有方法，在一个数据表中插入一条记录。
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Cols">哈希表，键值为字段名，值为字段值</param>
        /// <returns>是否成功</returns>
        public bool Insert(String TableName, Hashtable Cols)
        {
            int Count = 0;

            if (Cols.Count <= 0)
            {
                return true;
            }

            String Fields = " (";
            String Values = " Values(";
            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                {
                    Fields += ",";
                    Values += ",";
                }
                Fields += "[" + item.Key.ToString() + "]";
                Values += item.Value.ToString();
                Count++;
            }
            Fields += ")";
            Values += ")";

            String sql = "Insert into " + TableName + Fields + Values;
            return executeSQL(sql, null) > 0 ? true : false;
        }


        /// <summary>
        /// 公有方法，更新一个数据表。
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Cols">哈希表，键值为字段名，值为字段值</param>
        /// <param name="Where">Where子句</param>
        /// <returns>是否成功</returns>
        public bool Update(String TableName, Hashtable Cols, String Where)
        {
            int Count = 0;
            if (Cols.Count <= 0)
            {
                return true;
            }
            String Fields = " ";
            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                {
                    Fields += ",";
                }
                Fields += "[" + item.Key.ToString() + "]";
                Fields += "=";
                Fields += item.Value.ToString();
                Count++;
            }
            Fields += " ";

            String sql = "Update " + TableName + " Set " + Fields + Where;
            return executeSQL(sql, null) > 0 ? true : false;
        }

        //向学生演示专用
        public string Update_demo(String TableName, Hashtable Cols, String Where)
        {
            int Count = 0;
            if (Cols.Count <= 0)
            {
                return null;
            }
            String Fields = " ";
            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                {
                    Fields += ",";
                }
                Fields += "[" + item.Key.ToString() + "]";
                Fields += "=";
                Fields += item.Value.ToString();
                Count++;
            }
            Fields += " ";

            String sql = "Update " + TableName + " Set " + Fields + Where;
            return sql;
        }
    }
}
