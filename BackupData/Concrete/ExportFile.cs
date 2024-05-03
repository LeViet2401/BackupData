using BackupData.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData.Concrete
{
    internal class ExportFile : IExport
    {
        void IExport.ExportFile(DataInfo dataInfo)
        {
            using (var connection = new SqlConnection(dataInfo.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = dataInfo.QueryString;
                    using (var dataReader = command.ExecuteReader())
                    {
                        using (StreamWriter sw = new StreamWriter(dataInfo.GetPathFile()))
                        {
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                if (i > 0)
                                {
                                    sw.Write(",");
                                }
                                sw.Write(dataReader.GetName(i));
                            }
                            sw.WriteLine();
                            while (dataReader.Read())
                            {
                                for (int i = 0; i < dataReader.FieldCount; i++)
                                {
                                    if (i > 0)
                                    {
                                        sw.Write(",");
                                    }
                                    sw.Write(dataReader.GetValue(i));
                                }
                                sw.WriteLine();
                            }
                        }
                    }
                }
            }
        }
    }
}
