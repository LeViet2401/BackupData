using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData.Interface
{
    internal interface IExport
    {
        void ExportFile(DataInfo dataInfo);
    }
}
