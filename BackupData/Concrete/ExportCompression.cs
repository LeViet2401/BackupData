using BackupData.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData.Concrete
{
    internal class ExportCompression : IExport
    {
        public void ExportFile(DataInfo dataInfo)
        {
            using (var connection = new SqlConnection(dataInfo.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = dataInfo.QueryString;
                    using (var dataReader = command.ExecuteReader())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                            {
                                var tempFile = archive.CreateEntry(dataInfo.FileName + "." + dataInfo.Extension);
                                using (var entryStream = tempFile.Open())
                                using (StreamWriter sw = new StreamWriter(entryStream))
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

                                using (var fileStream = new FileStream(dataInfo.GetPathFile(), FileMode.Create))
                                {
                                    memoryStream.Seek(0, SeekOrigin.Begin);
                                    memoryStream.CopyTo(fileStream);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
