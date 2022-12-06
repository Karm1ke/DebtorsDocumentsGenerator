using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedFunctions
{
    public static class InformOperations
    {
        public delegate void SetInform(int currentColumn, int columnCount);
        public static SetInform sInform;

        public delegate void SetProgress(int count);
        public static SetProgress sProgress;

        public delegate void SetProgressStyle(ProgressBarStyle pbStyle);
        public static SetProgressStyle sProgressStyle;

        public delegate void SetMessage(string message);
        public static SetMessage setErrorMessage;
        public static SetMessage setInformMessage;

        public delegate void SetDisplayMessage(string message, string title = "Генератор документов: возникла проблема", ToolTipIcon icon = ToolTipIcon.Error);
        public static SetDisplayMessage setDisplayMessage;
    }
}
