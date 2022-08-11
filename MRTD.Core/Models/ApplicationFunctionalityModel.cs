using MRTD.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MRTD.Core.Models
{
    public class ApplicationFunctionalityModel
    {
        private CommandType _commandType;
        private DataReturnType _returnType;
        public int FunctionalityID { get; set; }
        public string ApplicationMethod { get; set; }
        public string ApplicationAlgorithm { get; set; }
        public CommandType CommandType
        {
            get
            {
                return _commandType;
            }
            set
            {
                _commandType = (CommandType)Enum.Parse(typeof(CommandType), value.ToString());
            }
        }
        public DataReturnType ReturnType
        {
            get
            {
                return _returnType;
            }
            set
            {
                _returnType = (DataReturnType)Enum.Parse(typeof(DataReturnType), value.ToString());
            }
        }
        public ApplicationSession ApplicationParameter { get; set; }
    }
}
