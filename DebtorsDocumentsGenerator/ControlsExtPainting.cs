using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DebtorsDocumentsGenerator
{
    /// <summary>
    /// Класс функций расширенной прорисовки контролом
    /// </summary>
    public static class ControlsExtPainting
    {
        public static void BaseButtonPaint(Button btn, PaintEventArgs e, string text)
        {
            // make sure Text is not also written on button
            btn.Text = string.Empty;
            dynamic drawBrush = new SolidBrush(btn.ForeColor);
            dynamic sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Gray, ButtonBorderStyle.Solid);

            e.Graphics.DrawString(text, btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();
        }

        /// <summary>
        /// Цвет/граница контролов формы задаются в соответствии с заданной темой
        /// </summary>
        /// <param name="name">Тема</param>
        /// <param name="form">Форма</param>
        public static void SetTheme(string name, Form form)
        {
            switch (name)
            {
                case "Dark":
                    form.BackColor = Color.FromArgb(54, 54, 54);
                    foreach (var lbl in form.Controls.OfType<Label>())
                    {
                        lbl.ForeColor = Color.White;
                    }
                    foreach (var button in form.Controls.OfType<Button>())
                    {
                        button.BackColor = button.Enabled ? Color.FromArgb(44, 44, 44) : Color.FromArgb(25, 25, 25);
                        button.ForeColor = button.Enabled ? Color.White : Color.Gray;
                    }
                    foreach (var tbox in form.Controls.OfType<TextBox>())
                    {
                        tbox.BackColor = Color.FromArgb(34, 34, 34);
                        tbox.ForeColor = Color.White;
                        tbox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    foreach (var mtbox in form.Controls.OfType<MaskedTextBox>())
                    {
                        mtbox.BackColor = Color.FromArgb(34, 34, 34);
                        mtbox.ForeColor = Color.White;
                        mtbox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    foreach (var panel in form.Controls.OfType<Panel>())
                    {
                        if (panel.Name == "cmdBoxPanel")
                        {
                            foreach (var rtbox in panel.Controls.OfType<RichTextBox>())
                            {
                                rtbox.BackColor = Color.FromArgb(34, 34, 34);
                                rtbox.ForeColor = Color.White;
                            }
                        }

                        foreach (var lbl in panel.Controls.OfType<LinkLabel>())
                        {
                            lbl.ForeColor = Color.White;
                            lbl.BackColor = Color.FromArgb(54, 54, 54);
                        }
                    }
                    foreach (var lbl in form.Controls.OfType<LinkLabel>())
                    {
                        lbl.ForeColor = Color.White;
                        lbl.BackColor = Color.FromArgb(54, 54, 54);
                    }
                    foreach (var cbox in form.Controls.OfType<ComboBox>())
                    {
                        cbox.BackColor = Color.FromArgb(34, 34, 34);
                        cbox.ForeColor = Color.White;
                    }
                    foreach (var gbox in form.Controls.OfType<CustomGroupBox>())
                    {
                        gbox.BorderColor = Color.FromArgb(54, 54, 54);
                        foreach (var lbl in gbox.Controls.OfType<Label>())
                        {
                            lbl.ForeColor = Color.White;
                        }
                    }
                    foreach (var lbl in form.Controls.OfType<StatusStrip>())
                    {
                        lbl.ForeColor = Color.White;
                        lbl.BackColor = Color.FromArgb(54, 54, 54);
                    }
                    break;
                case "Gray":
                    form.BackColor = Color.LightGray;
                    foreach (var lbl in form.Controls.OfType<Label>())
                    {
                        lbl.ForeColor = Color.Black;
                    }
                    foreach (var button in form.Controls.OfType<Button>())
                    {
                        button.BackColor = button.Enabled ? Color.FromArgb(44, 44, 44) : Color.FromArgb(25, 25, 25);
                        button.ForeColor = button.Enabled ? Color.White : Color.Gray;
                    }
                    foreach (var mtbox in form.Controls.OfType<MaskedTextBox>())
                    {
                        mtbox.BackColor = Color.LightGray;
                        mtbox.ForeColor = Color.Black;
                        mtbox.BorderStyle = BorderStyle.Fixed3D;
                    }
                    foreach (var tbox in form.Controls.OfType<TextBox>())
                    {
                        tbox.BackColor = Color.LightGray;
                        tbox.ForeColor = Color.Black;
                        tbox.BorderStyle = BorderStyle.Fixed3D;
                    }
                    foreach (var panel in form.Controls.OfType<Panel>())
                    {
                        if (panel.Name == "cmdBoxPanel")
                        {
                            foreach (var rtbox in form.Controls.OfType<RichTextBox>())
                            {
                                rtbox.BackColor = Color.LightGray;
                                rtbox.ForeColor = Color.Black;
                            }
                        }
                    }
                    foreach (var cbox in form.Controls.OfType<ComboBox>())
                    {
                        cbox.BackColor = Color.LightGray;
                        cbox.ForeColor = Color.Black;
                    }
                    foreach (var gbox in form.Controls.OfType<CustomGroupBox>())
                    {
                        gbox.BorderColor = SystemColors.WindowFrame;
                        foreach (var lbl in gbox.Controls.OfType<Label>())
                        {
                            lbl.ForeColor = Color.Black;
                        }
                    }
                    foreach (var lbl in form.Controls.OfType<StatusStrip>())
                    {
                        lbl.ForeColor = Color.White;
                        lbl.BackColor = Color.DimGray;
                    }
                    break;
            }
        }

        /// <summary>
        /// Цвет/граница контролов формы задаются в соответствии с заданной темой
        /// </summary>
        /// <param name="name">Тема</param>
        /// <param name="form">Форма</param>
        public static void SetTheme(string name, TableLayoutPanel layoutPanel)
        {
            switch (name)
            {
                case "Dark":
                    layoutPanel.BackColor = Color.FromArgb(54, 54, 54);
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var lbl in panel.Controls.OfType<Label>())
                        {
                            lbl.ForeColor = Color.White;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var button in panel.Controls.OfType<Button>())
                        {
                            button.BackColor = button.Enabled ? Color.FromArgb(44, 44, 44) : Color.FromArgb(25, 25, 25);
                            button.ForeColor = button.Enabled ? Color.White : Color.Gray;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var tbox in panel.Controls.OfType<TextBox>())
                        {
                            tbox.BackColor = Color.FromArgb(34, 34, 34);
                            tbox.ForeColor = Color.White;
                            tbox.BorderStyle = BorderStyle.FixedSingle;
                        }

                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var mtbox in panel.Controls.OfType<MaskedTextBox>())
                        {
                            mtbox.BackColor = Color.FromArgb(34, 34, 34);
                            mtbox.ForeColor = Color.White;
                            mtbox.BorderStyle = BorderStyle.FixedSingle;
                        }
                    }
                    foreach (var mpanel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var panel in mpanel.Controls.OfType<Panel>())
                        {
                            if (panel.Name == "cmdBoxPanel")
                            {
                                foreach (var rtbox in panel.Controls.OfType<RichTextBox>())
                                {
                                    rtbox.BackColor = Color.FromArgb(34, 34, 34);
                                    rtbox.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var cbox in panel.Controls.OfType<ComboBox>())
                        {
                            cbox.BackColor = Color.FromArgb(54, 54, 54);
                            cbox.ForeColor = Color.White;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var gbox in panel.Controls.OfType<CustomGroupBox>())
                        {
                            gbox.BorderColor = Color.FromArgb(54, 54, 54);
                            foreach (var lbl in gbox.Controls.OfType<Label>())
                            {
                                lbl.ForeColor = Color.White;
                            }
                        }
                    }
                    break;
                case "Gray":
                    layoutPanel.BackColor = Color.DimGray;
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var lbl in panel.Controls.OfType<Label>())
                        {
                            lbl.ForeColor = Color.Black;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var button in panel.Controls.OfType<Button>())
                        {
                            button.BackColor = Color.LightGray;
                            button.ForeColor = Color.Black;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var mtbox in panel.Controls.OfType<MaskedTextBox>())
                        {
                            mtbox.BackColor = Color.LightGray;
                            mtbox.ForeColor = Color.Black;
                            mtbox.BorderStyle = BorderStyle.Fixed3D;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var tbox in panel.Controls.OfType<TextBox>())
                        {
                            tbox.BackColor = Color.LightGray;
                            tbox.ForeColor = Color.Black;
                            tbox.BorderStyle = BorderStyle.Fixed3D;
                        }
                    }
                    foreach (var mpanel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var panel in mpanel.Controls.OfType<Panel>())
                        {
                            if (panel.Name == "cmdBoxPanel")
                            {
                                foreach (var rtbox in layoutPanel.Controls.OfType<RichTextBox>())
                                {
                                    rtbox.BackColor = Color.LightGray;
                                    rtbox.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var cbox in panel.Controls.OfType<ComboBox>())
                        {
                            cbox.BackColor = Color.LightGray;
                            cbox.ForeColor = Color.Black;
                        }
                    }
                    foreach (var panel in layoutPanel.Controls.OfType<Panel>())
                    {
                        foreach (var gbox in panel.Controls.OfType<CustomGroupBox>())
                        {
                            gbox.BorderColor = SystemColors.WindowFrame;
                            foreach (var lbl in gbox.Controls.OfType<Label>())
                            {
                                lbl.ForeColor = Color.Black;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
