namespace DocCreater
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.prgSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.SuspendLayout();
            // 
            // prgSpinner
            // 
            this.prgSpinner.Location = new System.Drawing.Point(37, 58);
            this.prgSpinner.Maximum = 100;
            this.prgSpinner.Name = "prgSpinner";
            this.prgSpinner.Size = new System.Drawing.Size(128, 128);
            this.prgSpinner.Style = MetroFramework.MetroColorStyle.Red;
            this.prgSpinner.TabIndex = 0;
            this.prgSpinner.UseWaitCursor = true;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 220);
            this.Controls.Add(this.prgSpinner);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(210, 220);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(210, 220);
            this.Name = "Menu";
            this.Padding = new System.Windows.Forms.Padding(34, 92, 34, 31);
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Запуск";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.Menu_Load);
            this.Shown += new System.EventHandler(this.Menu_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroProgressSpinner prgSpinner;
    }
}