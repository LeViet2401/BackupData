using BackupData.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupData
{
    internal class OptionBuilder : IOptionBuilder
    {
        private DataInfo _dataInfo;
        public OptionBuilder(IConfiguration configuration) 
        {
            _dataInfo = new DataInfo();
            _dataInfo.ConnectionString = configuration.GetConnectionString("HRConnection");
        }
        public DataInfo Build()
        {
            return _dataInfo;
        }
        public IOptionBuilder AddExportTime()
        {
            _dataInfo.ExportTime = $"_{DateTime.Now:yyyyMMdd_HHmmss}";
            return this;
        }
        public IOptionBuilder ChangeExtension(string extension)
        {
            _dataInfo.Extension = extension;
            return this;
        }

        public IOptionBuilder ExportAddCompresion()
        {
            _dataInfo.Compression = true;
            return this;
        }
    }
}
