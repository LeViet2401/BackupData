using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData
{
    internal class DataInfo
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DirectoryPath { get; set; } = string.Empty;
        public string FileName { get; set; } = "export";
        public string Extension { get; set; } = "csv";
        public string ExportTime { get; set; } = string.Empty;
        public bool Compression { get; set; } = false;
        public string QueryString { get; set; } = string.Empty;

        public string GetPathFile()
        {
            if(Compression)
            {
                return Path.Combine(DirectoryPath, FileName + ExportTime + ".zip");
            }
            return Path.Combine(DirectoryPath, FileName + ExportTime + "." + Extension);
        }

    }
}
