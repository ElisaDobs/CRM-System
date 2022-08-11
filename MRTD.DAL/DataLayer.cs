using MRTD.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MRTD.DAL
{
    public class DataLayer : IDisposable
    {
        private readonly bool disposing = false;

        private readonly SqlConnection connection;
        public DataLayer(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            try
            {
                connection?.Open();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        public bool BulkUpload(string destinationTable, DataTable table)
        {
            using (SqlBulkCopy bulk = new SqlBulkCopy(connection))
            {
                try
                {
                    bulk.DestinationTableName = destinationTable;
                    for (int index = 0; index < table.Columns.Count; index++)
                    {
                        bulk.ColumnMappings.Add(table.Columns[index].ColumnName, table.Columns[index].ColumnName);
                    }
                    bulk.WriteToServerAsync(table);
                }
                catch(Exception exception)
                {
                    throw exception;
                }
                return true;
            }   
        }
        public int Execute(CommandType commandType, string CommandText, List<SqlParameter> listParam = null)
        {
            using (SqlCommand command = new SqlCommand(CommandText, connection))
            {
                try
                {
                    command.CommandType = commandType;
                    if (listParam != null)
                        command.Parameters.AddRange(listParam.ToArray());

                    return command.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
        }

        public List<T> GetData<T>(CommandType commandType, string CommandText, List<SqlParameter> listParam = null)
        {
            using (SqlCommand command = new SqlCommand(CommandText, connection))
            {
                try
                {
                    command.CommandType = commandType;
                    if (listParam != null)
                    {
                        command.Parameters.AddRange(listParam.ToArray());
                    }
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        return dt.ToList<T>();
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
        }

        public object Get(CommandType commandType, string CommandText, List<SqlParameter> listParam = null)
        {
            using (SqlCommand command = new SqlCommand(CommandText, connection))
            {
                try
                {
                    command.CommandType = commandType;
                    command.Parameters.AddRange(listParam.ToArray());
                    object value = command.ExecuteScalar();

                    return value;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
        }

        public void Close()
        {
            try
            {
                connection?.Close();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        private void Dispose(bool dispose)
        {
            if (!disposing)
            {
                if (dispose)
                {
                    connection?.Close();
                    connection?.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

