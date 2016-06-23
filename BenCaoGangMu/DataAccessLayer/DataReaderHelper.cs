using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

//DataReaderHelper 是将 IDateReader 对象转换成为对应实体类对象（集合）的辅助类。
//原因1：将查询结果保存到 DataSet 中，虽然不需要保持同数据库的连接，但 DataSet 对象占用很多系统资源（内存）。
//原因2：将查询结果保存到 DataReader 对象中，需要一直保持同数据库的连接，占用系统资源。
//综合：通过 DataReaderHelper 将查询结果保存到对应的实体对象（集合）中，就不需要保持同数据库的连接，也不用占用内存。
namespace BenCaoGangMu.DataAccessLayer
{
    /// <summary>
    ///功能：将 IDateReader 对象转换成为对应实体类对象（集合）的辅助类。
    /// </summary>
    public static class DataReaderHelper
    {
        /// <summary>
        /// 转换成为实体类
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dr">IDataReader接口</param>
        /// <returns></returns>
        public static T ReaderToEntity<T>(IDataReader dr)
        {
            //  try
            //  {
            using (dr)
            {
                if (dr.Read())
                {
                    Type modelType = typeof(T);//获取对象的类型
                    int count = dr.FieldCount;//获取datareader的列总数
                    T model = Activator.CreateInstance<T>();//反射创建对象
                    for (int i = 0; i < count; i++)
                    {
                        if (!IsNullOrDBNull(dr[i]))//不为空才设定值
                        {
                            //获取reader[i]所对应对象的属性
                            PropertyInfo pi = modelType.GetProperty(GetPropertyName(dr.GetName(i)), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //设定属性对应的值
                                pi.SetValue(model, HackType(dr[i], pi.PropertyType), null);
                            }
                        }
                    }
                    return model;
                }
            }
            return default(T);
            //   }
            //  catch (Exception ex)
            //  {
            //      return default(T);
            //   }
        }
        /// <summary>
        /// 转换成为实体类集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dr">IDataReader接口</param>
        /// <returns></returns>
        public static IList<T> ReaderToList<T>(IDataReader dr)
        {
            using (dr)
            {
                List<T> list = new List<T>();
                Type modelType = typeof(T);
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < count; i++)
                    {
                        if (!IsNullOrDBNull(dr[i]))
                        {
                            PropertyInfo pi = modelType.GetProperty(GetPropertyName(dr.GetName(i)), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                pi.SetValue(model, HackType(dr[i], pi.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }
        /// <summary>
        /// 类型转换，把数据库的类型转换成为C#中的类型
        ///这个类对可空类型进行判断转换，要不然会报错  
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="conversionType">对象中的类型</param>
        /// <returns></returns>
        private static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
        private static bool IsNullOrDBNull(object obj)
        {
            return (obj == null || (obj is DBNull)) ? true : false;
        }
        //取得DB的列对应bean的属性名
        private static string GetPropertyName(string column)
        {
            column = column.ToLower();
            string[] narr = column.Split('_');
            column = "";
            for (int i = 0; i < narr.Length; i++)
            {
                if (narr[i].Length > 1)
                {
                    column += narr[i].Substring(0, 1).ToUpper() + narr[i].Substring(1);
                }
                else
                {
                    column += narr[i].Substring(0, 1).ToUpper();
                }
            }
            return column;
        }
    }
}