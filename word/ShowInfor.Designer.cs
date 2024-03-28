namespace word
{
    partial class ShowInfor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listView = new ListView();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Location = new Point(12, 12);
            listView.Name = "listView";
            listView.Size = new Size(484, 121);
            listView.TabIndex = 1;
            listView.UseCompatibleStateImageBehavior = false;
            // 
            // ShowInfor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 143);
            Controls.Add(listView);
            Name = "ShowInfor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShowInfor";
            Load += ShowInfor_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView;
    }
}