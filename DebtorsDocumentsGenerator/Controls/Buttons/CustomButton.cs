using System.Windows.Forms;

namespace DebtorsDocumentsGenerator
{
    public partial class CustomButton : Button
    {
        public CustomButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
