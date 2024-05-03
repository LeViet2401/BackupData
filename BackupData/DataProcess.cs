using BackupData.Concrete;
using BackupData.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData
{
    internal class DataProcess
    {
        private DataInfo dataInfo;

        public DataProcess(DataInfo dataInfo)
        {
            this.dataInfo = dataInfo;
        }

        public string ExportData()
        {
            IExport export;
            if(dataInfo.Compression)
            {
                export = new ExportCompression();
            }
            else
            {
                export = new ExportFile();
            }
            export.ExportFile(dataInfo);
            return "Export Completed";
        }

        public string ImportData()
        {
            return "Import Data Completed";
        }
    }
}
