using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Drawing.Printing;
using System.Net.NetworkInformation;
namespace word
{
    public partial class Form1 : Form
    {
        ColorDialog colorDialog1 = new ColorDialog();
        private PrintDocument printDocument;
        private float zoomLevel = 1.0f;
        private Font baseFont;
        public Form1()
        {
            InitializeComponent();
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            baseFont = rtbox.Font;
        }



        // cắt text
        void cutText()
        {
            if (rtbox.SelectedText != "")
            {
                // lưu vào clipboard  window + v 
                Clipboard.SetText(rtbox.SelectedText);
                // làm cho chuỗi thành rỗng
                rtbox.SelectedText = "";
                //
            }
        }
        // copy text
        void copyText()
        {

            Clipboard.SetText(rtbox.SelectedText);
        }
        // dán text
        void pasteText()
        {


            rtbox.Text = Clipboard.GetText();
        }
        private void btnCut_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyText();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            pasteText();
        }


        private void NewFile_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Text = "Document";
            newForm.Size = this.Size;


            // Tạo một Label và đặt nội dung văn bản
            Label label = new Label();
            //label.Text = "Chữ bên trong cửa sổ mới";
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(50, 50);

            // Thêm Label vào cửa sổ mới
            newForm.Controls.Add(label);
            newForm.StartPosition = this.StartPosition;
            newForm.WindowState = this.WindowState;
            newForm.Show();


        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format|*.rtf|All Files|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                rtbox.BackColor = colorDialog1.Color;

                string filePath = saveFileDialog.FileName;
                // Lấy tên file từ đường dẫn đầy đủ
                rtbox.SaveFile(filePath, RichTextBoxStreamType.RichText);
                MessageBox.Show("File đã được lưu thành công!");

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format|*.rtf|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Đọc nội dung từ file và hiển thị trong RichTextBox
                rtbox.LoadFile(filePath, RichTextBoxStreamType.RichText);

                MessageBox.Show("File đã được mở thành công!");
            }
        }

        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Chèn hình ảnh vào RichTextBox
                using (Image image = Image.FromFile(imagePath))
                {
                    Clipboard.SetImage(image);
                    rtbox.Paste();
                }

                MessageBox.Show("Hình ảnh đã được chèn thành công!");
            }
        }

        private void BtnBullet_Click(object sender, EventArgs e)
        {
            // Đảo giá trị SelectionBullet giữa true và false
            rtbox.SelectionBullet = !rtbox.SelectionBullet;
        }

        private void removeBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Đảo giá trị SelectionBullet giữa true và false
            rtbox.SelectionBullet = false;

        }

        private void indentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            switch (clickedItem.Text)
            {
                case "None":
                    rtbox.SelectionIndent = 0;
                    break;
                case "5 pts":
                    rtbox.SelectionIndent = 50;

                    break;
                case "10 pts":
                    rtbox.SelectionIndent = 100;

                    break;
                case "15 pts":
                    rtbox.SelectionIndent = 150;

                    break;
                case "20 pts":
                    rtbox.SelectionIndent = 200;

                    break;
                default:
                    break;
            }

        }
        private ToolStripTextBox txtSearch;
        private ToolStripTextBox txtReplace;

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSearch = new ToolStripTextBox();
            txtReplace = new ToolStripTextBox();
            ToolStripButton btnReplace = new ToolStripButton("Thay thế");


            btnReplace.Click += (sender, e) =>
            {
                string searchText = txtSearch.Text;
                string replaceText = txtReplace.Text;

                // Thực hiện tìm kiếm trong RtbDoc
                int startIndex = rtbox.Find(searchText);
                if (startIndex != -1)
                {
                    // Tìm thấy văn bản, thực hiện thay thế
                    rtbox.SelectedText = replaceText;
                }

            };
            toolStrip1.Items.Add(new ToolStripLabel("Tìm: "));
            toolStrip1.Items.Add(txtSearch);
            toolStrip1.Items.Add(new ToolStripLabel("Thay thế: "));
            toolStrip1.Items.Add(txtReplace);
            toolStrip1.Items.Add(btnReplace);
        }


        private void BtnReplace_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            string replaceText = txtReplace.Text;

            // Thực hiện thay thế trong RtbDoc
            int startIndex = rtbox.Find(searchText);
            if (startIndex != -1)
            {
                rtbox.SelectedText = replaceText;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            string replaceText = txtReplace.Text;

            // Thực hiện tìm kiếm trong RtbDoc
            int startIndex = rtbox.Find(searchText);
            if (startIndex != -1)
            {
                rtbox.Select(startIndex, searchText.Length);
            }
        }
        private void rtbox_TextChanged(object sender, EventArgs e)
        {

        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rtbox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            rtbox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            rtbox.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            // Nếu chữ được chọn in đậm, thì bỏ in đậm, ngược lại thì in đậm
            if (rtbox.SelectionFont.Bold)
            {
                // Bỏ chữ in đậm
                rtbox.SelectionFont = new Font(rtbox.SelectionFont, rtbox.SelectionFont.Style & ~FontStyle.Bold);
            }
            else
            {
                // In đậm chữ
                rtbox.SelectionFont = new Font(rtbox.SelectionFont, rtbox.SelectionFont.Style | FontStyle.Bold);
            }

        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            // Nếu chữ được chọn có gạch chân, thì bỏ gạch chân, ngược lại thì có gạch chân
            if (rtbox.SelectionFont.Underline)
            {
                // Bỏ gạch chân
                rtbox.SelectionFont = new Font(rtbox.SelectionFont, rtbox.SelectionFont.Style & ~FontStyle.Underline);
            }
            else
            {
                // Có gạch chân
                rtbox.SelectionFont = new Font(rtbox.SelectionFont, rtbox.SelectionFont.Style | FontStyle.Underline);
            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            // Nếu chữ được chọn đã nghiêng, thì bỏ nghiêng, ngược lại thì nghiêng
            if (rtbox.SelectionFont.Italic)
            {
                // Bỏ nghiêng
                rtbox.SelectionFont = new Font(rtbox.SelectionFont, rtbox.SelectionFont.Style & ~FontStyle.Italic);
            }
            else
            {
                // Nghiêng
                rtbox.SelectionFont = new Font(rtbox.SelectionFont, rtbox.SelectionFont.Style | FontStyle.Italic);
            }
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                rtbox.SelectionColor = colorDialog1.Color;
        }

        private void selectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FontDialog fontDialog1 = new FontDialog();
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                rtbox.SelectionFont = fontDialog1.Font;
        }

        private void pageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                rtbox.BackColor = colorDialog1.Color;
        }

        private void print_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Lấy văn bản từ RichTextBox
            string text = rtbox.Text;
            Font font = rtbox.Font;
            SolidBrush brush = new SolidBrush(rtbox.ForeColor);
            RectangleF rect = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
            StringFormat format = new StringFormat();
            // In văn bản vào trang in
            e.Graphics.DrawString(text, font, brush, rect, format);
        }
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            zoomLevel += 0.1f;
            UpdateFont();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            zoomLevel -= 0.1f;
            UpdateFont();
        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            zoomLevel = 1.0f;
            UpdateFont();
        }

        private void UpdateFont()
        {
            float fontSize = baseFont.Size * zoomLevel;
            rtbox.Font = new Font(baseFont.FontFamily, fontSize, baseFont.Style);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInfor show = new ShowInfor();
            show.Show();
        }
        PageSetupDialog pageSetupDialog = new PageSetupDialog();

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            pageSetupDialog.Document = printDocument;
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                bool newIsLandscape = pageSetupDialog.PageSettings.Landscape;

                if (newIsLandscape)
                {
                    ApplyLandscapeMode(pageSetupDialog.PageSettings.Margins);
                }
                else
                {
                    ApplyPortraitMode(pageSetupDialog.PageSettings.Margins);
                }
            }
        }
        private void ApplyLandscapeMode(Margins margins)
        {
            rtbox.SelectionIndent = margins.Left;
            rtbox.SelectionHangingIndent = margins.Top;

            rtbox.Size = new Size(750, 450);
            rtbox.Left = (rtbox.Parent.ClientSize.Width - rtbox.Width) / 2;
            rtbox.Top = (rtbox.Parent.ClientSize.Height - rtbox.Height) / 2;
        }

        private void ApplyPortraitMode(Margins margins)
        {
            rtbox.SelectionIndent = margins.Right;
            rtbox.SelectionHangingIndent = margins.Bottom;
            rtbox.Size = new Size(847, 450);
            rtbox.Left = (rtbox.Parent.ClientSize.Width - rtbox.Width) / 2;
            rtbox.Top = (rtbox.Parent.ClientSize.Height - rtbox.Height) / 2;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripTextBox txtSearch = new ToolStripTextBox();
            ToolStripButton btnReplace = new ToolStripButton("Tìm kiếm");
            ToolStripButton btnDelete = new ToolStripButton("Xoá");

            btnReplace.Click += (sender, e) =>
            {
                string searchText = txtSearch.Text;
                if (!string.IsNullOrEmpty(searchText))
                {
                    rtbox.SelectAll();
                    rtbox.SelectionBackColor = rtbox.BackColor;

                    int startIndex = 0;
                    while (startIndex < rtbox.Text.Length)
                    {
                        startIndex = rtbox.Text.IndexOf(searchText, startIndex);
                        if (startIndex == -1)
                            break;

                        rtbox.Select(startIndex, searchText.Length);
                        rtbox.SelectionBackColor = Color.Yellow;

                        startIndex += searchText.Length;
                    }
                }
            };
            ToolStripLabel toolStripLabel = new ToolStripLabel("Tìm: ");


            toolStrip1.Items.Add(toolStripLabel);
            toolStrip1.Items.Add(txtSearch);
            toolStrip1.Items.Add(btnReplace);
            toolStrip1.Items.Add(btnDelete);

            btnDelete.Click += (sender, e) =>
            {

                toolStrip1.Items.Remove(toolStripLabel);
                toolStrip1.Items.Remove(txtSearch);
                toolStrip1.Items.Remove(btnReplace);
                toolStrip1.Items.Remove(btnDelete);
            };

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbox.CanUndo)
            {
                rtbox.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbox.CanRedo)
            {
                rtbox.Redo();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbox.SelectAll();
        }
    }

}
