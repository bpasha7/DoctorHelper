namespace DocCreater
{
    partial class TemplatesForm
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
            this.components = new System.ComponentModel.Container();
            this.ExaminationsBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.valueTextBox = new MetroFramework.Controls.MetroTextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.FieldBox = new MetroFramework.Controls.MetroComboBox();
            this.newExaminationPanel = new MetroFramework.Controls.MetroPanel();
            this.submitNewExamine = new System.Windows.Forms.Button();
            this.closeExpaminationPanelBtn = new System.Windows.Forms.Button();
            this.newExamineBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.showFieldPanelBtn = new System.Windows.Forms.Button();
            this.showExaminePanelBtn = new System.Windows.Forms.Button();
            this.newFieldPanel = new MetroFramework.Controls.MetroPanel();
            this.submitNewField = new System.Windows.Forms.Button();
            this.closeFieldPanel = new System.Windows.Forms.Button();
            this.newFieldBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.editTextToggle = new MetroFramework.Controls.MetroToggle();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1.SuspendLayout();
            this.newExaminationPanel.SuspendLayout();
            this.newFieldPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExaminationsBox
            // 
            this.ExaminationsBox.FormattingEnabled = true;
            this.ExaminationsBox.ItemHeight = 23;
            this.ExaminationsBox.Location = new System.Drawing.Point(157, 63);
            this.ExaminationsBox.Name = "ExaminationsBox";
            this.ExaminationsBox.Size = new System.Drawing.Size(354, 29);
            this.ExaminationsBox.TabIndex = 0;
            this.ExaminationsBox.SelectedIndexChanged += new System.EventHandler(this.ExaminationsBox_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(23, 63);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(128, 25);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Обследование";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Enabled = false;
            this.valueTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.valueTextBox.Location = new System.Drawing.Point(248, 178);
            this.valueTextBox.Multiline = true;
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(304, 140);
            this.valueTextBox.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader3});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(23, 146);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(219, 172);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 1;
            this.columnHeader4.Text = "Шаблон";
            this.columnHeader4.Width = 209;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 0;
            this.columnHeader3.Text = "Id";
            this.columnHeader3.Width = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(23, 103);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(52, 25);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Поле";
            // 
            // FieldBox
            // 
            this.FieldBox.FormattingEnabled = true;
            this.FieldBox.ItemHeight = 23;
            this.FieldBox.Location = new System.Drawing.Point(107, 103);
            this.FieldBox.Name = "FieldBox";
            this.FieldBox.Size = new System.Drawing.Size(404, 29);
            this.FieldBox.TabIndex = 5;
            this.FieldBox.SelectedIndexChanged += new System.EventHandler(this.FieldBox_SelectedIndexChanged);
            // 
            // newExaminationPanel
            // 
            this.newExaminationPanel.Controls.Add(this.submitNewExamine);
            this.newExaminationPanel.Controls.Add(this.closeExpaminationPanelBtn);
            this.newExaminationPanel.Controls.Add(this.newExamineBox);
            this.newExaminationPanel.Controls.Add(this.metroLabel3);
            this.newExaminationPanel.HorizontalScrollbarBarColor = true;
            this.newExaminationPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.newExaminationPanel.HorizontalScrollbarSize = 10;
            this.newExaminationPanel.Location = new System.Drawing.Point(192, 199);
            this.newExaminationPanel.Name = "newExaminationPanel";
            this.newExaminationPanel.Size = new System.Drawing.Size(529, 84);
            this.newExaminationPanel.TabIndex = 8;
            this.newExaminationPanel.VerticalScrollbarBarColor = true;
            this.newExaminationPanel.VerticalScrollbarHighlightOnWheel = false;
            this.newExaminationPanel.VerticalScrollbarSize = 10;
            this.newExaminationPanel.Visible = false;
            // 
            // submitNewExamine
            // 
            this.submitNewExamine.BackColor = System.Drawing.Color.White;
            this.submitNewExamine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitNewExamine.FlatAppearance.BorderSize = 0;
            this.submitNewExamine.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitNewExamine.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitNewExamine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitNewExamine.Image = global::DocCreater.Properties.Resources.checked__1_;
            this.submitNewExamine.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.submitNewExamine.Location = new System.Drawing.Point(488, 27);
            this.submitNewExamine.Name = "submitNewExamine";
            this.submitNewExamine.Size = new System.Drawing.Size(32, 36);
            this.submitNewExamine.TabIndex = 10;
            this.submitNewExamine.UseVisualStyleBackColor = false;
            this.submitNewExamine.Click += new System.EventHandler(this.submitNewExamine_Click);
            // 
            // closeExpaminationPanelBtn
            // 
            this.closeExpaminationPanelBtn.BackColor = System.Drawing.Color.White;
            this.closeExpaminationPanelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeExpaminationPanelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.closeExpaminationPanelBtn.Image = global::DocCreater.Properties.Resources.delete;
            this.closeExpaminationPanelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.closeExpaminationPanelBtn.Location = new System.Drawing.Point(505, 0);
            this.closeExpaminationPanelBtn.Name = "closeExpaminationPanelBtn";
            this.closeExpaminationPanelBtn.Size = new System.Drawing.Size(24, 24);
            this.closeExpaminationPanelBtn.TabIndex = 9;
            this.closeExpaminationPanelBtn.UseVisualStyleBackColor = false;
            this.closeExpaminationPanelBtn.Click += new System.EventHandler(this.closeExpaminationPanelBtn_Click);
            // 
            // newExamineBox
            // 
            this.newExamineBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.newExamineBox.Location = new System.Drawing.Point(163, 35);
            this.newExamineBox.Name = "newExamineBox";
            this.newExamineBox.Size = new System.Drawing.Size(317, 23);
            this.newExamineBox.TabIndex = 3;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(17, 35);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(140, 19);
            this.metroLabel3.TabIndex = 2;
            this.metroLabel3.Text = "Новое обследование";
            // 
            // showFieldPanelBtn
            // 
            this.showFieldPanelBtn.BackColor = System.Drawing.Color.White;
            this.showFieldPanelBtn.FlatAppearance.BorderSize = 0;
            this.showFieldPanelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showFieldPanelBtn.Image = global::DocCreater.Properties.Resources.add__1_;
            this.showFieldPanelBtn.Location = new System.Drawing.Point(522, 103);
            this.showFieldPanelBtn.Name = "showFieldPanelBtn";
            this.showFieldPanelBtn.Size = new System.Drawing.Size(30, 30);
            this.showFieldPanelBtn.TabIndex = 7;
            this.showFieldPanelBtn.UseVisualStyleBackColor = false;
            this.showFieldPanelBtn.Click += new System.EventHandler(this.showFieldPanelBtn_Click);
            // 
            // showExaminePanelBtn
            // 
            this.showExaminePanelBtn.BackColor = System.Drawing.Color.White;
            this.showExaminePanelBtn.FlatAppearance.BorderSize = 0;
            this.showExaminePanelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showExaminePanelBtn.Image = global::DocCreater.Properties.Resources.add__1_;
            this.showExaminePanelBtn.Location = new System.Drawing.Point(522, 63);
            this.showExaminePanelBtn.Name = "showExaminePanelBtn";
            this.showExaminePanelBtn.Size = new System.Drawing.Size(30, 30);
            this.showExaminePanelBtn.TabIndex = 4;
            this.showExaminePanelBtn.UseVisualStyleBackColor = false;
            this.showExaminePanelBtn.Click += new System.EventHandler(this.showExaminePanelBtn_Click);
            // 
            // newFieldPanel
            // 
            this.newFieldPanel.Controls.Add(this.submitNewField);
            this.newFieldPanel.Controls.Add(this.closeFieldPanel);
            this.newFieldPanel.Controls.Add(this.newFieldBox);
            this.newFieldPanel.Controls.Add(this.metroLabel4);
            this.newFieldPanel.HorizontalScrollbarBarColor = true;
            this.newFieldPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.newFieldPanel.HorizontalScrollbarSize = 10;
            this.newFieldPanel.Location = new System.Drawing.Point(73, 277);
            this.newFieldPanel.Name = "newFieldPanel";
            this.newFieldPanel.Size = new System.Drawing.Size(529, 84);
            this.newFieldPanel.TabIndex = 11;
            this.newFieldPanel.VerticalScrollbarBarColor = true;
            this.newFieldPanel.VerticalScrollbarHighlightOnWheel = false;
            this.newFieldPanel.VerticalScrollbarSize = 10;
            this.newFieldPanel.Visible = false;
            // 
            // submitNewField
            // 
            this.submitNewField.BackColor = System.Drawing.Color.White;
            this.submitNewField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitNewField.FlatAppearance.BorderSize = 0;
            this.submitNewField.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitNewField.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitNewField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitNewField.Image = global::DocCreater.Properties.Resources.checked__1_;
            this.submitNewField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.submitNewField.Location = new System.Drawing.Point(488, 27);
            this.submitNewField.Name = "submitNewField";
            this.submitNewField.Size = new System.Drawing.Size(32, 36);
            this.submitNewField.TabIndex = 10;
            this.submitNewField.UseVisualStyleBackColor = false;
            this.submitNewField.Click += new System.EventHandler(this.submitNewField_Click);
            // 
            // closeFieldPanel
            // 
            this.closeFieldPanel.BackColor = System.Drawing.Color.White;
            this.closeFieldPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeFieldPanel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.closeFieldPanel.Image = global::DocCreater.Properties.Resources.delete;
            this.closeFieldPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.closeFieldPanel.Location = new System.Drawing.Point(505, 0);
            this.closeFieldPanel.Name = "closeFieldPanel";
            this.closeFieldPanel.Size = new System.Drawing.Size(24, 24);
            this.closeFieldPanel.TabIndex = 9;
            this.closeFieldPanel.UseVisualStyleBackColor = false;
            this.closeFieldPanel.Click += new System.EventHandler(this.closeFieldPanel_Click);
            // 
            // newFieldBox
            // 
            this.newFieldBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.newFieldBox.Location = new System.Drawing.Point(105, 35);
            this.newFieldBox.Name = "newFieldBox";
            this.newFieldBox.Size = new System.Drawing.Size(375, 23);
            this.newFieldBox.TabIndex = 3;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(17, 35);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(82, 19);
            this.metroLabel4.TabIndex = 2;
            this.metroLabel4.Text = "Новое поле";
            // 
            // editTextToggle
            // 
            this.editTextToggle.AutoSize = true;
            this.editTextToggle.Location = new System.Drawing.Point(463, 153);
            this.editTextToggle.Name = "editTextToggle";
            this.editTextToggle.Size = new System.Drawing.Size(80, 17);
            this.editTextToggle.TabIndex = 14;
            this.editTextToggle.Text = "Off";
            this.editTextToggle.UseVisualStyleBackColor = true;
            this.editTextToggle.CheckedChanged += new System.EventHandler(this.editTextToggle_CheckedChanged);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(248, 153);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(119, 19);
            this.metroLabel5.TabIndex = 11;
            this.metroLabel5.Text = "Изменение текста";
            // 
            // treeView1
            // 
            this.treeView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.treeView1.Location = new System.Drawing.Point(597, 57);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(162, 261);
            this.treeView1.TabIndex = 15;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // TemplatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 384);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.editTextToggle);
            this.Controls.Add(this.newFieldPanel);
            this.Controls.Add(this.newExaminationPanel);
            this.Controls.Add(this.showFieldPanelBtn);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.FieldBox);
            this.Controls.Add(this.showExaminePanelBtn);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ExaminationsBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemplatesForm";
            this.ShowIcon = false;
            this.Text = "Шаблоны для обследований";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TemplatesForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.newExaminationPanel.ResumeLayout(false);
            this.newExaminationPanel.PerformLayout();
            this.newFieldPanel.ResumeLayout(false);
            this.newFieldPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox ExaminationsBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox valueTextBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button showExaminePanelBtn;
        private System.Windows.Forms.Button showFieldPanelBtn;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox FieldBox;
        private MetroFramework.Controls.MetroPanel newExaminationPanel;
        private System.Windows.Forms.Button closeExpaminationPanelBtn;
        private MetroFramework.Controls.MetroTextBox newExamineBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.Button submitNewExamine;
        private MetroFramework.Controls.MetroPanel newFieldPanel;
        private System.Windows.Forms.Button submitNewField;
        private System.Windows.Forms.Button closeFieldPanel;
        private MetroFramework.Controls.MetroTextBox newFieldBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private MetroFramework.Controls.MetroToggle editTextToggle;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private System.Windows.Forms.TreeView treeView1;
    }
}