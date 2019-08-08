using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace Ideas.Data
{
    public abstract class RepositoryBase
    {
        public void AddParameterWithValue(IDbCommand cmd, string paramName, object value)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value;
            parameter.DbType = GetDbType(value.GetType());

            cmd.Parameters.Add(parameter);
        }

        public DataTable CreateIntegerIDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            return dt;
        }

        private DbType GetDbType(Type theType)
        {
            var param = new SqlParameter();
            var tc = TypeDescriptor.GetConverter(param.DbType);
            if (tc.CanConvertFrom(theType))
            {
                var convertFrom = tc.ConvertFrom(theType.Name);
                if (convertFrom != null)
                {
                    param.DbType = (DbType)convertFrom;
                }
            }
            else
            {
                try
                {
                    var convertFrom = tc.ConvertFrom(theType.Name);
                    if (convertFrom != null)
                    {
                        param.DbType = (DbType)convertFrom;
                    }
                }
                catch
                {
                    // ignore the exception
                }
            }
            return param.DbType;
        }
    }
}