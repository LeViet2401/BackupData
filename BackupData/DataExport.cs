using BackupData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData
{
    internal class DataExport
    {
        private IExport Export;

        public DataExport(IExport Export)
        {
            this.Export = Export;
        }
        public void SetStrategy(IExport Export)
        {
            this.Export = Export;
        }
        public void CreateArchive(DataInfo dataInfo)
        {
            Export.ExportFile(dataInfo);
        }
    }
}
