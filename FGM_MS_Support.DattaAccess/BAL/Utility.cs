
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FGM_MS_Support.DattaAccess.BAL
{
    public class Utility
    {
        public static void ConvertDataTable<T>(DataTable dt, ref List<T> data)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                //List<T> data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                //return data;
            }
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        if (!DBNull.Value.Equals(dr[column.ColumnName]))
                        {
                            Type proptype = pro.PropertyType;
                            if (proptype.IsGenericType && proptype.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                proptype = new NullableConverter(pro.PropertyType).UnderlyingType;
                            }
                            if (proptype == dr[column.ColumnName].GetType())
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);

                            }
                            else
                            {

                                pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], proptype));
                            }
                        }
                        else
                            continue;
                }
                //temp.GetProperties().Where(x => x.Name == column.ColumnName).SingleOrDefault();
            }
            return obj;
        }


        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
