using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQL_CRUD
{
    public partial class Form1 : Form
    {
        FormStudent form;
        public Form1()
        {
            InitializeComponent();
            form = new FormStudent(this);
        }
        
        public void Display()
        {
            PostgressStudent.DisplayAndSearch("Select * FROM student_table", dataGridView1);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            form.Clear();
            form.saveInfo();
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            PostgressStudent.DisplayAndSearch("Select ID,Name,Reg,Class,Section FROM student_table WHERE Name LIKE '%" + textSearch.Text + "%'", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                form.Clear();
                form.id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.reg = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.@class = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.section = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.updateInfo();
                form.ShowDialog();
                return;
            }
            //delete a record
            if (e.ColumnIndex == 1)
            {
                if(MessageBox.Show("Are you sure you want to delete the student?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    PostgressStudent.DeleteStudent(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }
    }
}
