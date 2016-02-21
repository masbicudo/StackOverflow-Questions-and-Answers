using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SOPTRichTextBoxApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Colorir("if", this.richTextBox1, Color.Blue);
            Colorir("_[0-Z]+", this.richTextBox1, Color.DarkRed);
            Colorir("PRESSED!TRUE", this.richTextBox1, Color.DarkBlue);
        }

        static void Colorir(string filter, RichTextBox rtb, Color color)
        {
            var matches = Regex.Matches(rtb.Text, filter);

            Color orig = rtb.SelectionColor;
            int start = rtb.SelectionStart;
            int length = rtb.SelectionLength;

            foreach (Match m in matches)
            {
                rtb.SelectionStart = m.Index;
                rtb.SelectionLength = m.Length;
                rtb.SelectionColor = color;
            }

            rtb.SelectionStart = start;
            rtb.SelectionLength = length;
            rtb.SelectionColor = orig;
        }
    }
}
