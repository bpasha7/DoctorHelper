using DocumentTemplates;
using DocumentTemplates.Templates;
using Entities;
using Entities.TDS;
using MetroFramework.Controls;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DocCreater
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Текущий документ
        /// </summary>
        private IDocumentTemplate _currentDocument;
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, int> _examinations;
        /// <summary>
        /// 
        /// </summary>
        private Patient _selectedPatient;
        /// <summary>
        /// Флаг, создан ли префью файл
        /// </summary>
        private bool _canPreview = false;
        /// <summary>
        /// DB
        /// </summary>
        private MyLocalRepository _db;
        /// <summary>
        /// Last entered text field, where will be past tempaltes from tree
        /// </summary>
        private MetroTextBox _enteredEditText;

        private string _owner = "";

        private List<Entities.TreeNode> _toAddNodes;
        private List<Entities.TreeNode> _toDeleteNodes;
        private List<Entities.TreeNode> _toEditNodes;

        private List<Patient> _foundedPatients;

        private int _selectedTab = 0;

        public Form1(string Owner)
        {
            _db = new MyLocalRepository();
            _toAddNodes = new List<Entities.TreeNode>();
            _toDeleteNodes = new List<Entities.TreeNode>();
            _toEditNodes = new List<Entities.TreeNode>();
            _examinations = _db.GetAllExamitations();
            InitializeComponent();
            _owner = Owner;
            врачToolStripMenuItem.Text += _owner;
        }

        private void ClearForm()
        {
            PatientBox.Hide();
            ClientTextBox.Text = "";
            HistoryTextBox.Text = "";
            cancelSearchbutton.Hide();
            findPatientbutton.Show();
            печатьToolStripMenuItem1.Enabled = false;
            просмотретьToolStripMenuItem1.Enabled = false;
            LoadTab(0);
            LoadTab(1);
            LoadTab(2);
            _selectedPatient = null;

            // LoadTab2();
            //  LoadTab3();
        }

        void AddNodes(TreeNodeCollection collection, Entities.TreeNode rootNode, List<Entities.TreeNode> ListTreeNodes)
        {
            var children = ListTreeNodes.Where(n => n.pId == rootNode.Id).OrderBy(r => r.Name).ToList();
            foreach (var node in children)
            {
                var newNode = new System.Windows.Forms.TreeNode
                {
                    ToolTipText = node.Name,
                    Name = $"{node.Id},{node.epId}",
                    Text = node.Name
                };
                var col = collection.Add(newNode/*$"{node.Id},{node.epId}", node.Name*/);

                AddNodes(newNode.Nodes/*collection*/, node, ListTreeNodes);

                ListTreeNodes.Remove(node);
            }
            ListTreeNodes.Remove(rootNode);
        }

        private void SaveNodesChanges()
        {
            if (_toAddNodes.Count != 0  || _toEditNodes.Count != 0 || _toDeleteNodes.Count != 0)
            {
                var res = MessageBox.Show("Внесены изменения, применить?", "Выберите действие", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                    return;
                if (res == DialogResult.Yes)
                {
                    var saves = _db.SaveNodes(_toAddNodes);
                    var edits = _db.UpdateNodes(_toEditNodes);
                    var deletes = _db.DeleteNodes(_toDeleteNodes);
                    MessageBox.Show($"Сохранено {saves} из {_toAddNodes.Count}, изменено {edits} из {_toEditNodes.Count}, удалено {deletes} из {_toDeleteNodes.Count}.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowTemplates(string FieldName)
        {
            try
            {
                treeView1.SelectedNode = null;
                if (TreeName.Text == FieldName)
                    return;
                SaveNodesChanges();
                _toAddNodes.Clear();
                _toEditNodes.Clear();
                _toDeleteNodes.Clear();
                treeView1.Nodes.Clear();
                _enteredEditText.Style = MetroFramework.MetroColorStyle.Red;
                TreeName.Text = FieldName;
                var t = _db.GetAllTreeNodes(_examinations[metroTabControl1.SelectedTab.Text], FieldName);
                var listNodes = t.OrderBy(x => x.pId).ToList();

                while (listNodes.Count() != 0)
                {
                    //_currentExId = listNodes[0].epId;
                    var r = treeView1.Nodes.Add($"{listNodes[0].Id},{listNodes[0].epId}", listNodes[0].Name);
                    AddNodes(r.Nodes, listNodes[0], listNodes);
                }
                //_vf.ShowTemplates(metroTabControl1.SelectedTab.Text, FieldName);
                //_vf.Show();
            }
            catch (Exception ex)
            {

            }
        }
        #region TabLoads
        public void LoadTab(int index)
        {
            switch (index)
            {
                case 0:
                    var ds1 = new List<Criteria>();
                    ds1.Add(new Criteria("ТИМ, мм"));
                    ds1.Add(new Criteria("Vps, см/сек"));
                    ds1.Add(new Criteria("D, мм"));
                    ds1.Add(new Criteria("Ri"));
                    ds1.Add(new Criteria("Нарушение хода, извитости"));
                    gridTDS1.DataSource = ds1;
                    gridTDS1.BringToFront();

                    var ds2 = new List<Criteria>();
                    ds2.Add(new Criteria("ВЯВ(d)") { Val1 = "мм;  Vps", Val3 = "см/сек" });
                    ds2.Add(new Criteria("ВЯВ(s)") { Val1 = "мм;  Vps", Val3 = "см/сек" });
                    ds2.Add(new Criteria("ВП(d)") { Val1 = "мм;  Vps", Val3 = "см/сек" });
                    ds2.Add(new Criteria("ВП(s)") { Val1 = "мм;  Vps", Val3 = "см/сек" });
                    gridTDS2.DataSource = ds2;

                    var ds3 = new List<Criteria>();
                    ds3.Add(new Criteria("Vps см/сек"));
                    ds3.Add(new Criteria("RI"));
                    gridTDS3.DataSource = ds3;
                    break;
                case 1:
                    var ds31 = new List<Criteria>();
                    ds31.Add(new Criteria("устье"));
                    ds31.Add(new Criteria("ворота"));
                    ds31.Add(new Criteria("сегментарные ветви"));

                    ds31.Add(new Criteria("устье"));
                    ds31.Add(new Criteria("ворота"));
                    ds31.Add(new Criteria("сегментарные ветви"));
                    gridControl2.DataSource = ds31;
                    break;
                case 2:
                    var ds11 = new List<Criteria>();
                    ds11.Add(new Criteria("Черный ствол"));
                    ds11.Add(new Criteria("Общая печеночная артерия"));
                    ds11.Add(new Criteria("Селезеночная артерия"));
                    ds11.Add(new Criteria("Верхняя брыжеечная артерия"));
                    gridControl1.DataSource = ds11;
                    break;
                default:
                    break;
            }

        }
        public void LoadTab2()
        {

        }
        public void LoadTab3()
        {

        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            var devices = _db.GetDevices();
            devicesBox.DataSource = new BindingSource(devices, null);
            devicesBox.DisplayMember = "Value";
            devicesBox.ValueMember = "Key";

        }

        private void просмотретьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //_currentDocument?.ShowOrHide();
            var pf = new PreviewForm();
            pf.ShowDialog();
            //просмотретьToolStripMenuItem1.Text = просмотретьToolStripMenuItem1.Text == "Просмотреть" ? "Скрыть" : "Просмотреть";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ClientTextBox.Text == "")
                return;
            _canPreview = false;
            metroProgressBar1.Value = 0;
            ProgressPanel.BringToFront();
            ProgressPanel.Visible = true;
            menuStrip1.Enabled = false;
            metroTabControl1.Enabled = false;
            _selectedTab = metroTabControl1.SelectedIndex;
            FileWorker.RunWorkerAsync();
        }
        /// <summary>
        /// Получение выбранного текста из панели
        /// </summary>
        /// <param name="Panel"></param>
        /// <returns></returns>
        private string GetCheckedValueFromPanel(MetroPanel Panel)
        {
            foreach (var ctrl in Panel.Controls)
            {
                try
                {
                    if (ctrl.GetType().Name == "MetroRadioButton")
                    {
                        var m = ctrl as MetroRadioButton;
                        if (m.Checked)
                            return m.Text;
                    }
                }
                finally
                {

                }
            }
            return "";
        }

        private void FormatingDocument()
        {
            try
            {
                var client = new Client
                {
                    Age = (int)((dateTimePicker1.Value - DateTime.Now).Duration().Days / 365.2425),
                    Name = ClientTextBox.Text,
                    HistoryNumber = HistoryTextBox.Text
                };
                FileWorker.ReportProgress(5);


                switch (_selectedTab)
                {
                    case 0:
                        var tdsData = new DataTDS(gridTDS1.DataSource as List<Criteria>, gridTDS2.DataSource as List<Criteria>, gridTDS3.DataSource as List<Criteria>);
                        FileWorker.ReportProgress(20);
                        tdsData.P1 = metroTextBox1.Text;
                        tdsData.P2 = metroTextBox2.Text;
                        tdsData.P3 = metroTextBox3.Text;
                        _currentDocument = new TDS(tdsData, client, FileWorker);
                        break;
                    case 1:
                        var tds1 = new DataTDS1(gridControl2.DataSource as List<Criteria>);
                        FileWorker.ReportProgress(20);
                        var paragraphs1 = new string[7];
                        paragraphs1[0] = $"Диаметр на уровне висцеральных ветвей {metroTextBox14.Text.Trim()} мм, на уровне бифуркации {metroTextBox13.Text.Trim()} мм.";
                        var wall1 = metroCheckBox2.Checked ? metroCheckBox2.Text + ", " : "";
                        paragraphs1[1] = $"Стенка: {wall1}АБС ({GetCheckedValueFromPanel(metroPanel4)}).";
                        paragraphs1[2] = $"Просвет: {GetCheckedValueFromPanel(metroPanel5)}.";
                        paragraphs1[3] = $"Кровоток Vps {metroTextBox12.Text.Trim()} см/сек, магистральный {metroTextBox11.Text.Trim()} см/сек.";
                        paragraphs1[4] = metroTextBox10.Text.Trim();
                        paragraphs1[5] = $"RAR = {metroTextBox16.Text.Trim()}.";
                        paragraphs1[6] = metroTextBox15.Text.Trim();
                        tds1.Paragraphs = paragraphs1;
                        _currentDocument = new TDS1(tds1, client, FileWorker);
                        break;
                    case 2:
                        var tds2 = new DataTDS2(gridControl1.DataSource as List<Criteria>);
                        FileWorker.ReportProgress(20);
                        var paragraphs2 = new string[6];
                        paragraphs2[0] = $"Диаметр на уровне висцеральных ветвей {metroTextBox4.Text.Trim()} мм, на уровне бифуркации {metroTextBox5.Text.Trim()} мм.";
                        var wall = metroCheckBox1.Checked ? metroCheckBox1.Text + ", " : "";
                        paragraphs2[1] = $"Стенка: {wall}АБС ({GetCheckedValueFromPanel(metroPanel2)}).";
                        paragraphs2[2] = $"Просвет: {GetCheckedValueFromPanel(metroPanel3)}.";
                        paragraphs2[3] = $"Кровоток Vps {metroTextBox6.Text.Trim()} см/сек, магистральный {metroTextBox7.Text.Trim()} см/сек.";
                        paragraphs2[4] = metroTextBox8.Text.Trim();
                        paragraphs2[5] = metroTextBox9.Text.Trim();
                        tds2.Paragraphs = paragraphs2;
                        _currentDocument = new TDS2(tds2, client, FileWorker);
                        break;
                    default:
                        break;
                }


                FileWorker.ReportProgress(95);
                _canPreview = _currentDocument.SavePreview();
                FileWorker.ReportProgress(100);
            }
            catch (Exception ex)
            {

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FormatingDocument();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            menuStrip1.Enabled = true;
            metroTabControl1.Enabled = true;
            ProgressPanel.Visible = false;
            просмотретьToolStripMenuItem1.Enabled = _canPreview;
            печатьToolStripMenuItem1.Enabled = _canPreview;
            if (!_canPreview)
            {
                MessageBox.Show("Ошибка формирования файла!");
                return;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            metroProgressBar1.Value += e.ProgressPercentage;
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _currentDocument?.Save();
        }

        private void metroTextBox1_Enter(object sender, EventArgs e)
        {
            _enteredEditText = metroTextBox1;
            ShowTemplates("Атеросклеротическая бляшка");
        }

        private void metroTextBox2_Enter(object sender, EventArgs e)
        {
            _enteredEditText = metroTextBox2;
            ShowTemplates("Дополнительные данные");
        }

        private void metroTextBox3_Enter(object sender, EventArgs e)
        {
            _enteredEditText = metroTextBox3;
            ShowTemplates("Заключение");
        }
        #region TDS Grids Custom cells
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Drawing.Rectangle r = e.Bounds;
            Brush ellipseBrush = Brushes.DimGray;
            e.Graphics.DrawRectangle(new Pen(ellipseBrush, 0.5f), r);
            r.Width -= 12;
            e.Appearance.DrawString(e.Cache, e.DisplayText, r);
            e.Handled = true;
            if (gridView1.IsEditing && gridView1.FocusedColumn == e.Column && gridView1.FocusedRowHandle == e.RowHandle)
            {
                System.Drawing.Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Handled = true;
            }
        }
        private void gridView3_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Drawing.Rectangle r = e.Bounds;
            Brush ellipseBrush = Brushes.DimGray;
            e.Graphics.DrawRectangle(new Pen(ellipseBrush, 0.5f), r);
            r.Width -= 12;
            e.Appearance.DrawString(e.Cache, e.DisplayText, r);
            e.Handled = true;
            if (gridView3.IsEditing && gridView3.FocusedColumn == e.Column && gridView3.FocusedRowHandle == e.RowHandle)
            {
                System.Drawing.Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Handled = true;
            }
        }

        private void gridView4_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Drawing.Rectangle r = e.Bounds;
            Brush ellipseBrush = Brushes.DimGray;
            e.Graphics.DrawRectangle(new Pen(ellipseBrush, 0.5f), r);
            r.Width -= 12;
            e.Appearance.DrawString(e.Cache, e.DisplayText, r);
            e.Handled = true;
            if (gridView4.IsEditing && gridView4.FocusedColumn == e.Column && gridView4.FocusedRowHandle == e.RowHandle)
            {
                System.Drawing.Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Handled = true;
            }
        }
        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Drawing.Rectangle r = e.Bounds;
            Brush ellipseBrush = Brushes.DimGray;
            e.Graphics.DrawRectangle(new Pen(ellipseBrush, 0.5f), r);
            r.Width -= 12;
            e.Appearance.DrawString(e.Cache, e.DisplayText, r);
            e.Handled = true;
            if (gridView2.IsEditing && gridView2.FocusedColumn == e.Column && gridView2.FocusedRowHandle == e.RowHandle)
            {
                System.Drawing.Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Handled = true;
            }
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Drawing.Rectangle r = e.Bounds;
            Brush ellipseBrush = Brushes.DimGray;
            e.Graphics.DrawRectangle(new Pen(ellipseBrush, 0.5f), r);
            r.Width -= 12;
            e.Appearance.DrawString(e.Cache, e.DisplayText, r);
            e.Handled = true;
            if (bandedGridView1.IsEditing && bandedGridView1.FocusedColumn == e.Column && bandedGridView1.FocusedRowHandle == e.RowHandle)
            {
                System.Drawing.Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Handled = true;
            }
        }
        #endregion
        /// <summary>
        /// Добавление фарзы из дерева в используемый текст бокс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _enteredEditText.Text += treeView1.SelectedNode.Text + " ";//sb.ToString();
                _enteredEditText.Style = MetroFramework.MetroColorStyle.Blue;
            }
            catch (Exception ex) { }
        }

        private void metroTextBox10_Enter(object sender, EventArgs e)
        {
            _enteredEditText = metroTextBox10;
            ShowTemplates("Дополнительные данные");
        }
        /// <summary>
        /// Печать документа и сохранениепациента в БД если его нету
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void печатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_currentDocument.Print())
            {
                if (_selectedPatient == null)
                {
                    var lName = ClientTextBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    var P = new Patient
                    {
                        Lname = lName,
                        BDate = dateTimePicker1.Value.Date,
                        Gender = mRadioButton.Checked ? "М" : "Ж",
                        Name = ClientTextBox.Text.Replace($"{lName} ", "")
                    };
                    //MessageBox.Show("Добавить ациента в базу?", "Выберите действие", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    var patientId = _db.SavePatient(P);
                    if (patientId > 0)
                    {
                        _selectedPatient = P;
                        _selectedPatient.Id = patientId;
                    }
                    else if (patientId == -1)
                    {
                        MessageBox.Show("Выберите пациента из базы!", "Пациент существует в базе", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Пациент не добавлен в базу!", "Ошибка добавления пациента в базу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                var fileName = _currentDocument.Save();
                if (fileName != null)
                {
                    var D = new Docs
                    {
                        Date = DateTime.Now.Date,
                        Name = fileName,
                        exId = _examinations[metroTabControl1.SelectedTab.Text],
                        Pid = _selectedPatient.Id
                    };
                    if (_db.SaveDoc(D) == 1)
                    {
                        _selectedPatient = null;
                        MessageBox.Show("Документ распечатан и сохранен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                }
                else
                {
                    MessageBox.Show("Документ не был сохранен в базу!", "Ошибка сохранения документа в базу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void addNodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ids;

                if (treeView1.SelectedNode == null)
                {
                    ids = treeView1.Nodes[0].Name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ids[0] = "0";
                }
                else
                    ids = treeView1.SelectedNode.Name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var newNode = new Entities.TreeNode
                {
                    Name = _enteredEditText.Text,
                    pId = Convert.ToInt32(ids[0]),
                    epId = Convert.ToInt32(ids[1])
                };
                _toAddNodes.Add(newNode);
                if (newNode.pId == 0)
                    treeView1.Nodes.Add(_enteredEditText.Text);
                else
                    treeView1.SelectedNode.Nodes.Add(_enteredEditText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя добавить к несохраненному объекту новый шаблон! Сохраните и продолжите.", "Добавление нового шаблона", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void deleteNodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var ids = treeView1.SelectedNode.Name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var node = new Entities.TreeNode
                {
                    Id = Convert.ToInt32(ids[0]),
                    epId = Convert.ToInt32(ids[1])
                };
                if (MessageBox.Show($"Удалить <{ treeView1.SelectedNode.Text}>?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                _toDeleteNodes.Add(node);
                treeView1.SelectedNode.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить несохраненный шаблон! Сохраните или отмените сохранение шаблонов.", "Удаление шаблона", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void metroTextBox10_Leave(object sender, EventArgs e)
        {
            metroTextBox10.Style = MetroFramework.MetroColorStyle.Blue;
        }

        private void metroTextBox1_Leave(object sender, EventArgs e)
        {
            metroTextBox1.Style = MetroFramework.MetroColorStyle.Blue;
        }

        private void metroTextBox2_Leave(object sender, EventArgs e)
        {
            metroTextBox2.Style = MetroFramework.MetroColorStyle.Blue;
        }

        private void metroTextBox3_Leave(object sender, EventArgs e)
        {
            metroTextBox3.Style = MetroFramework.MetroColorStyle.Blue;
        }

        private void metroTabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            treeView1.Nodes.Clear();
            LoadTab(metroTabControl1.SelectedIndex);
            TreeName.Text = "";
        }

        private void findPatientbutton_Click(object sender, EventArgs e)
        {
            try
            {
                var lName = ClientTextBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                _foundedPatients = _db.GetPatients(lName).ToList();
                if (_foundedPatients.Count == 0)
                {
                    MessageBox.Show($"В базе не найдена фамилия {ClientTextBox.Text.Trim()}.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                findPatientbutton.Hide();
                cancelSearchbutton.Show();
                PatientBox.Show();
                PatientBox.DisplayMember = "Info";
                PatientBox.ValueMember = "Id";
                PatientBox.DataSource = _foundedPatients;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска в базе фамилии {ClientTextBox.Text.Trim()}.", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void PatientBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedPatient = PatientBox.SelectedItem as Patient;
                ClientTextBox.Text = _selectedPatient.FullName;
                dateTimePicker1.Value = _selectedPatient.BDate;
                //_patientId = _selectedPatient.Id;
                if (_selectedPatient.Gender == "М")
                    mRadioButton.Checked = true;
                else
                    wRadioButton.Checked = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void cancelSearchbutton_Click(object sender, EventArgs e)
        {
            ClientTextBox.Text = "";
            cancelSearchbutton.Hide();
            findPatientbutton.Show();
            PatientBox.Hide();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                Opacity = i * 0.1;
                System.Threading.Thread.Sleep(150 - i * 10);
            }
            metroTabControl1.Show();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Подтвердите закрытие", "Закрыть программу?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            e.Cancel = (window == DialogResult.No);
        }

        private void metroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroTextBox8_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox8_Enter(object sender, EventArgs e)
        {
            _enteredEditText = metroTextBox8;
            ShowTemplates("Дополнительные данные");
        }

        private void metroTextBox9_Enter(object sender, EventArgs e)
        {
            _enteredEditText = metroTextBox9;
            ShowTemplates("Заключение");
        }

        private void saveNodesButton_Click(object sender, EventArgs e)
        {
            SaveNodesChanges();
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
                return;
            try
            {
                var ids = selectedNode.Name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var editedNode = new Entities.TreeNode
                {
                    Name = e.Label,
                    Id = Convert.ToInt32(ids[0])
                };
                _toEditNodes.Add(editedNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя изменить несохраненный шаблон!", "Изменение несохраненного шаблона", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
