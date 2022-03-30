using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WebBanSmartphones
{
    public class Common
    {
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textFileld)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (DataRow row in table.Rows)
            {
                List.Add(new SelectListItem()
                {
                    Text = row[textFileld].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(List, "Value", "Text");
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
        }
        public class ProductType
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
    }
}