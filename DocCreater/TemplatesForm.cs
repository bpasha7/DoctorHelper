using Entities;
using MetroFramework;
using MetroFramework.Forms;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocCreater
{
    public partial class TemplatesForm : MetroFramework.Forms.MetroForm
    {
        //private MyRepository _db;

        private List<Template> _currentTempaltes;

        public TemplatesForm()
        {
            //_db = new MyRepository();
            InitializeComponent();
        }

        private /*async*/ void TemplatesForm_Load(object sender, EventArgs e)
        {
            RefreshExaminations();

            //var t = await _db.GetAllTreeNodesAsync();
            //var listNodes =  t.OrderBy(x=>x.pId).ToList();

            //while(listNodes.Count() != 0)
            //{
            //    var r = treeView1.Nodes.Add(listNodes[0].Name);
            //    AddNodes(r.Nodes, listNodes[0], listNodes);
            //}
        }

        void AddNodes(TreeNodeCollection collection, Entities.TreeNode rootNode, List<Entities.TreeNode> ListTreeNodes)
        {
            var children = ListTreeNodes.Where(n => n.pId == rootNode.Id).OrderBy(r => r.Name).ToList();
            foreach (var node in children)
            {
                var col = collection.Add($"{node.Id}", node.Name);

                AddNodes(col.Nodes, node, ListTreeNodes);

                ListTreeNodes.Remove(node);
            }
            ListTreeNodes.Remove(rootNode);
        }

        private /*async*/ void RefreshExaminations()
        {
            //var t = await _db.GetAllExamitationsAsync();
            //t.ToList();
            //ExaminationsBox.DataSource = t.ToList();
            //ExaminationsBox.DisplayMember = "Name";
            //ExaminationsBox.ValueMember = "Id";
        }

        private /*async*/ void RefreshExaminationsProperties()
        {
            //try
            //{
            //    var t = await _db.GetAllExamitationsPropertiesByIdAsync((int)ExaminationsBox.SelectedValue);
            //    t.ToList();
            //    FieldBox.DataSource = t.ToList();
            //    FieldBox.DisplayMember = "Name";
            //    FieldBox.ValueMember = "Id";
            //}
            //catch (Exception ex) { }
        }

        private /*async*/ void RefreshTemplates()
        {
            //try
            //{
            //    var t = await _db.GetAllTemplatesAsync((int)FieldBox.SelectedValue);
            //    t.ToList();
            //    _currentTempaltes = t.ToList();
            //    _currentTempaltes.Sort((t1, t2) => t1.Name.CompareTo(t2.Name));
            //    listView1.Items.Clear();
            //    foreach (var template in _currentTempaltes)
            //    {
            //        listView1.Items.Add(template.Name).SubItems.Add(template.Id.ToString());
            //    }
            //}
            //catch (Exception ex) { }
        }

        private void showExaminePanelBtn_Click(object sender, EventArgs e)
        {
            newExaminationPanel.Visible = true;
        }

        private void closeExpaminationPanelBtn_Click(object sender, EventArgs e)
        {
            newExaminationPanel.Visible = false;
            newExamineBox.Text = "";
        }

        private /*async*/ void submitNewExamine_Click(object sender, EventArgs e)
        {
            //if (await _db.CreateExaminationAsync(newExamineBox.Text))
            //    MessageBox.Show("Данные добавлены", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    MessageBox.Show("Данные не добавлены", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //newExaminationPanel.Visible = false;
            //newExamineBox.Text = "";
            //RefreshExaminations();
        }

        private void showFieldPanelBtn_Click(object sender, EventArgs e)
        {
            newFieldPanel.Visible = true;
        }

        private void closeFieldPanel_Click(object sender, EventArgs e)
        {
            newFieldPanel.Visible = false;
            newFieldBox.Text = "";
        }

        private void ExaminationsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var t = ExaminationsBox.SelectedValue;
            RefreshExaminationsProperties();
        }

        private /*async*/ void submitNewField_Click(object sender, EventArgs e)
        {
            //DialogResult dr = MetroMessageB MetroMessageBox.Show("Your message here.", "Title Here", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if (await _db.CreateExaminationPropertyAsync((int)ExaminationsBox.SelectedValue, newFieldBox.Text))
            //    MessageBox.Show("Данные добавлены", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    MessageBox.Show("Данные не добавлены", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //newFieldPanel.Visible = false;
            //newFieldBox.Text = "";
        }

        private /*async*/ void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //try
            //{
            //    if (listView1.SelectedItems[0].SubItems[1].Text == "")
            //    {
            //        await _db.CreateTempalteAsync((int)FieldBox.SelectedValue, e.Label);
            //    }
            //    else
            //    {
            //        await _db.UpdateTempalteAsync(Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text), listView1.SelectedItems[0].Text, valueTextBox.Text);
            //    }
            //    RefreshTemplates();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Шаблон не добавлен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Add("Введите название").SubItems.Add("");
            listView1.Items[listView1.Items.Count - 1].BeginEdit();
        }

        private /*async*/ void editTextToggle_CheckedChanged(object sender, EventArgs e)
        {
            //valueTextBox.Enabled = editTextToggle.Checked;
            //if (editTextToggle.Checked == false && valueTextBox.Text.Length != 0)
            //{
            //    try
            //    {

            //        if(await _db.UpdateTempalteAsync(Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text), listView1.SelectedItems[0].Text, valueTextBox.Text))
            //        {
            //            RefreshTemplates();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Шаблон не обновлен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0].SubItems[1].Text == "")
                    return;
                var selectedId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
                valueTextBox.Text = _currentTempaltes.Where(t => t.Id == selectedId).SingleOrDefault()?.Value;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Шаблон не обновлен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FieldBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTemplates();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;

            var sb = new StringBuilder();
            while(node != null)
            {
                sb.Insert(0, node.Text +" ");
                node = node.Parent;
            }
            MessageBox.Show(sb.ToString());
        }
    }
}
