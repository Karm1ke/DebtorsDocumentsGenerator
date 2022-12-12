using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedFunctions
{
    public class FtpResult
    {
        public bool result;
        public string errorText;

        public FtpResult()
        {

        }

        public FtpResult(bool result, string errorText)
        {
            this.result = result;
            this.errorText = errorText;
        }
    }
}
