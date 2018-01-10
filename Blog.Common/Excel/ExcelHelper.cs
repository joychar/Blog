using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Excel
{
    public static class ExcelHelper
    {
        public static DataTable ListToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            //dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            foreach (PropertyInfo pi in props)
            {
                Type colType = pi.PropertyType;//得到属性的类型
                                               //如果属性为泛型类型
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                == typeof(Nullable<>)))
                {   //获取泛型类型的参数
                    colType = colType.GetGenericArguments()[0];
                }
                //将类型的属性名称与属性类型作为DataTable的列数据
                dt.Columns.Add(new DataColumn(pi.Name, colType));
            }

            if (collection.Count() > 0)
            {
                int count = collection.Count();
                for (int i = 0; i < count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null) == null ? DBNull.Value : pi.GetValue(collection.ElementAt(i), null); ;
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        public static DataTable LinqToDataTable<T>(IEnumerable<T> varlist)
        {   //定义要返回的DataTable对象
            DataTable dtReturn = new DataTable();
            // 保存列集合的属性信息数组
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;//安全性检查
                                                 //循环遍历集合，使用反射获取类型的属性信息
            foreach (T rec in varlist)
            {
                //使用反射获取T类型的属性信息，返回一个PropertyInfo类型的集合
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    //循环PropertyInfo数组
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;//得到属性的类型
                                                       //如果属性为泛型类型
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {   //获取泛型类型的参数
                            colType = colType.GetGenericArguments()[0];
                        }
                        //将类型的属性名称与属性类型作为DataTable的列数据
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                //新建一个用于添加到DataTable中的DataRow对象
                DataRow dr = dtReturn.NewRow();
                //循环遍历属性集合
                foreach (PropertyInfo pi in oProps)
                {   //为DataRow中的指定列赋值
                    dr[pi.Name] = pi.GetValue(rec, null) == null ?
                        DBNull.Value : pi.GetValue(rec, null);
                }
                //将具有结果值的DataRow添加到DataTable集合中
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;//返回DataTable对象
        }
    }
}
