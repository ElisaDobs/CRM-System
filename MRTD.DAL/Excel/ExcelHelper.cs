using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MRTD.Core.Models;
using System.Reflection;
using System.IO;

namespace MRTD.DAL.Excel
{
    public  static class ExcelHelper
    {
        public static DataTable ProcessExcelData<T>(string filePath, Guid importID, int UserID)
        {
            try
            {
                
                DataTable data = new DataTable();

                var columns = typeof(T).GetProperties();

                foreach (PropertyInfo info in columns)
                {
                    data.Columns.Add(info.Name, info.PropertyType);
                }
                string[] rangeRows = File.ReadAllLines(filePath);

                for (int row = 1; row < rangeRows.Length; row++)
                {
                    DataRow dataRow = data.NewRow();
                    string[] line = rangeRows[row].Split(';'); 
                    dataRow[data.Columns[0].ColumnName] = line[0];
                    dataRow[data.Columns[1].ColumnName] = line[1];
                    dataRow[data.Columns[2].ColumnName] = line[2];
                    dataRow[data.Columns[3].ColumnName] = Convert.ToInt32(line[3]);
                    dataRow[data.Columns[4].ColumnName] = importID;
                    dataRow[data.Columns[5].ColumnName] = UserID;
                    dataRow[data.Columns[6].ColumnName] = line[4];
                    data.Rows.Add(dataRow);
                }

                return data;
            }
            catch(Exception excpetion)
            {
                throw excpetion;
            }
        }
    }
}
