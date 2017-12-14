using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Game
{
    public partial class RTBE : UserControl
    {
        public RTBE()
        {
            InitializeComponent();
        }
        public String FileName = "";
        private void RTBE_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = LineCol;
        }
        [DllImport("kernel32.dll")]
        static extern void OutputDebugString(string lpOutputString);
        public void SetText(String text, Boolean richTextBoxLogDateTime)
        {
            if (richTextBoxLogDateTime) text = "[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + text + "\r\n";
            //SpeechSynthesis(text);
            try
            {
                if (this.richTextBox1.InvokeRequired)
                {
                    richTextBox1.BeginInvoke(new Action<string>((msg) =>
                    {
                        richTextBox1.AppendText(msg);
                        richTextBox1.ScrollToCaret();
                        richTextBox1.Focus();
                        UpdateCursorPos();
                    }), text);
                }
                else
                {
                    richTextBox1.AppendText(text);
                    richTextBox1.ScrollToCaret();
                    richTextBox1.Focus();
                    UpdateCursorPos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);    
            }
            OutputDebugString(text);
            System.Diagnostics.Debug.WriteLine(text);

        }
        public void M_SelectAll_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectAll();
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectAll();
            }
        }

        public void M_cut_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.Cut();
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.Cut();
            }
            UpdateCursorPos();
        }

        public void M_copy_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.Copy();
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.Copy();
            }
            UpdateCursorPos();
        }

        public void M_paste_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.Paste();
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.Paste();
            }
            UpdateCursorPos();
        }

        public void M_Delete_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectedText = "";
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectedText = "";
            }
            UpdateCursorPos();
        }

        public void M_Undo_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.Undo();
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.Undo();
            }
            UpdateCursorPos();
        }

        public void M_Redo_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.Redo();
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.Redo();
            }
            UpdateCursorPos();
        }

        public void M_Bold_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                if (rtb.SelectionFont.Bold)
                {
                    rtb.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Regular);
                }
                else
                {
                    rtb.Font = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Bold);
                }
            }
            else
            {
                richTextBox1.Select();
                if (richTextBox1.SelectionFont.Bold)
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Regular);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Bold);
                }
            }
        }

        public void M_Italic_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                if (rtb.SelectionFont.Italic)
                {
                    rtb.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Regular);
                }
                else
                {
                    rtb.Font = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Italic);
                }
            }
            else
            {
                richTextBox1.Select();
                if (richTextBox1.SelectionFont.Italic)
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Regular);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Italic);
                }
            }
        }

        public void M_Underline_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                if (rtb.SelectionFont.Underline)
                {
                    rtb.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Regular);
                }
                else
                {
                    rtb.Font = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Underline);
                }
            }
            else
            {
                richTextBox1.Select();
                if (richTextBox1.SelectionFont.Underline)
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Regular);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, richTextBox1.SelectionFont.Size, FontStyle.Underline);
                }
            }
        }

        public void M_Font_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                if (M_ContextMenuStrip.IsAccessible)
                {
                    this.M_ContextMenuStrip.SourceControl.Select();
                    RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                    rtb.SelectionFont = fontDialog1.Font;
                }
                else
                {
                    richTextBox1.Select();
                    richTextBox1.SelectionFont = fontDialog1.Font;
                }
            }
        }

        public void M_GrowFont_Click(object sender, EventArgs e)
        {
            float ss = richTextBox1.SelectionFont.Size;
            ss += richTextBox1.Font.SizeInPoints;
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectionFont = new Font(richTextBox1.SelectionFont.Name, ss, richTextBox1.SelectionFont.Style);
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, ss, richTextBox1.SelectionFont.Style);
            }
        }

        public void M_ShrinkFont_Click(object sender, EventArgs e)
        {
            float ss = richTextBox1.SelectionFont.Size;
            ss -= richTextBox1.Font.SizeInPoints;
            if (ss <= richTextBox1.Font.SizeInPoints)
                ss = richTextBox1.Font.SizeInPoints;
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectionFont = new Font(richTextBox1.SelectionFont.Name, ss, richTextBox1.SelectionFont.Style);
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, ss, richTextBox1.SelectionFont.Style);
            }
        }

        public void M_Bullets_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                if (rtb.SelectionBullet)
                {
                    rtb.SelectionBullet = false;
                }
                else
                {
                    rtb.SelectionBullet = true;
                }
            }
            else
            {
                richTextBox1.Select();
                if (richTextBox1.SelectionBullet)
                    richTextBox1.SelectionBullet = false;
                else
                    richTextBox1.SelectionBullet = true;
            }
        }

        public void M_Numbering_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                if (rtb.SelectionBullet)
                {
                    rtb.SelectionBullet = false;
                }
                else
                {
                    rtb.SelectionBullet = true;
                }
            }
            else
            {
                richTextBox1.Select();
                if (richTextBox1.SelectionBullet)
                    richTextBox1.SelectionBullet = false;
                else
                    richTextBox1.SelectionBullet = true;
            }
        }

        public void M_AlignLeft_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectionAlignment = HorizontalAlignment.Left;
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            }
        }

        public void M_AlignCenter_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectionAlignment = HorizontalAlignment.Center;
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        public void M_AlignRight_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectionAlignment = HorizontalAlignment.Right;
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            }
        }

        public void M_AlignJustify_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                rtb.SelectionAlignment = HorizontalAlignment.Center;
            }
            else
            {
                richTextBox1.Select();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                //SendKeys.Send("{ENTER}");
            }
        }

        public void M_IncreaseIndent_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                int bs = rtb.SelectionIndent;
                rtb.SelectionIndent = bs + 9;
            }
            else
            {
                richTextBox1.Select();
                int bs = richTextBox1.SelectionIndent;
                richTextBox1.SelectionIndent = bs + 9;
            }
        }

        public void M_DecreaseIndent_Click(object sender, EventArgs e)
        {
            if (M_ContextMenuStrip.IsAccessible)
            {
                this.M_ContextMenuStrip.SourceControl.Select();
                RichTextBox rtb = (RichTextBox)this.M_ContextMenuStrip.SourceControl;
                int bs = rtb.SelectionIndent;
                if (bs <= 9)
                    rtb.SelectionIndent = 0;
                else
                    rtb.SelectionIndent = bs - 9;
            }
            else
            {
                richTextBox1.Select();
                int bs = richTextBox1.SelectionIndent;
                if (bs <= 9)
                    richTextBox1.SelectionIndent = 0;
                else
                    richTextBox1.SelectionIndent = bs - 9;
            }
        }
        public void M_Open_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "rtf files (*.rtf)|*.rtf|OLE rtf files (*.rtf)|*.rtf|OLE Text rtf files (*.rtf)|*.rtf|Unicode rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = FileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog1.FileName;
                /*
                try
                {
                    richTextBox1.LoadFile(FileName, RichTextBoxStreamType.PlainText);
                }
                catch
                {
                    try
                    {
                        richTextBox1.LoadFile(FileName, RichTextBoxStreamType.UnicodePlainText);
                    }
                    catch
                    {
                        try
                        {
                            richTextBox1.LoadFile(FileName, RichTextBoxStreamType.TextTextOleObjs);
                        }
                        catch
                        {
                            try
                            {
                                richTextBox1.LoadFile(FileName, RichTextBoxStreamType.RichNoOleObjs);
                            }
                            catch
                            {
                                richTextBox1.LoadFile(FileName,RichTextBoxStreamType.RichText);
                            }
                        }
                    }
                }
                */
                switch (openFileDialog1.FilterIndex)
                {
                    case 2:
                        try
                        {
                            richTextBox1.LoadFile(FileName, RichTextBoxStreamType.RichText);
                        }
                        catch
                        {
                            richTextBox1.LoadFile(FileName, RichTextBoxStreamType.PlainText);
                        }
                        break;
                    /*
                case 2:
                    richTextBox1.LoadFile(FileName,RichTextBoxStreamType.RichNoOleObjs);
                    break;
                case 3:
                    richTextBox1.LoadFile(FileName,RichTextBoxStreamType.TextTextOleObjs);
                    break;
                case 4:
                    richTextBox1.LoadFile(FileName,RichTextBoxStreamType.UnicodePlainText);
                    break;
                     */
                    default:
                        richTextBox1.LoadFile(FileName, RichTextBoxStreamType.PlainText);
                        break;
                }
                toolStripStatusLabel1.Text = String.Format("Open:[{0}]", FileName);
                UpdateCursorPos();
            }
        }

        public void M_Save_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.Filter = "rtf files (*.rtf)|*.rtf|OLE rtf files (*.rtf)|*.rtf|OLE Text rtf files (*.rtf)|*.rtf|Unicode rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFileDialog1.FileName;
                /*
        try
        {
            richTextBox1.SaveFile(FileName, RichTextBoxStreamType.PlainText);
        }
        catch
        {
            try
            {
                richTextBox1.SaveFile(FileName, RichTextBoxStreamType.UnicodePlainText);
            }
            catch
            {
                try
                {
                    richTextBox1.SaveFile(FileName, RichTextBoxStreamType.TextTextOleObjs);
                }
                catch
                {
                    try
                    {
                        richTextBox1.SaveFile(FileName, RichTextBoxStreamType.RichNoOleObjs);
                    }
                    catch
                    {
                        richTextBox1.SaveFile(FileName, RichTextBoxStreamType.RichText);
                    }
                }
            }
        }
                 */


                switch (openFileDialog1.FilterIndex)
                {
                    case 2:
                        richTextBox1.SaveFile(FileName, RichTextBoxStreamType.RichText);
                        break;
                    /*
                case 2:
                    richTextBox1.SaveFile(FileName, RichTextBoxStreamType.RichNoOleObjs);
                    break;
                case 3:
                    richTextBox1.SaveFile(FileName, RichTextBoxStreamType.TextTextOleObjs);
                    break;
                case 4:
                    richTextBox1.SaveFile(FileName, RichTextBoxStreamType.UnicodePlainText);
                    break;
                     */
                    default:
                        richTextBox1.SaveFile(FileName, RichTextBoxStreamType.PlainText);
                        break;
                }
                    toolStripStatusLabel1.Text = String.Format("Save:[{0}]", FileName);
            }

        }

        public String FontName
        {
            set { richTextBox1.SelectionFont = new Font(value, richTextBox1.SelectionFont.Size, richTextBox1.SelectionFont.Style); }
            get { return richTextBox1.SelectionFont.Name; }
        }
        public String LineCol = String.Format("Line : 1          Col : 1      ");
        public void UpdateCursorPos()
        {
            try
            {
                Int32 begin = 0;
                Int32 fronting = richTextBox1.SelectionStart;
                Int32 row = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
                while (begin < fronting)
                {
                    if (row == richTextBox1.GetLineFromCharIndex(begin))
                    //richTextBox的GetLineFormCharIndex()方法，获取行号   
                    {
                        break;
                    }
                    else
                    {
                        begin++;
                    }
                }
                Int32 field = fronting - begin;
                row++;
                field++;
                LineCol = String.Format("Line : {0}          Col : {1}      ", row, field);
                toolStripStatusLabel2.Text = LineCol;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateCursorPos();
        }

        public void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdateCursorPos();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateCursorPos();
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateCursorPos();
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateCursorPos();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCursorPos();
        }
    }
}
