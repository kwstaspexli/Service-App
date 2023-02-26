using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace MySQL_CRUD
{
    class PostgressStudent
    {
        public static NpgsqlConnection GetNpgsqlConnection()
        {
            string sql = "Host=dpg-cf2hqoarrk0bppbhri7g-a.frankfurt-postgres.render.com;Port=5432;Username=sakalakis_service_user;Password=zyHtVjYNEbrQLj4Dbx18GLJ3hGmzBLmd;Database=sakalakis_service";
            NpgsqlConnection con = new(sql);
            try
            {
                con.Open();
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show("MySQL Connection! \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }
        public static void AddStudent(Student std)
        {
            string sql = "INSERT INTO student_table(name,reg,class,section) VALUES(@StudentName, @StudentReg, @StudentClass, @StudentSection)";
            NpgsqlConnection con = GetNpgsqlConnection();
            NpgsqlCommand cmd = new(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@StudentName", std.Name);
            cmd.Parameters.AddWithValue("@StudentReg", std.Reg);
            cmd.Parameters.AddWithValue("@StudentClass", std.Class);
            cmd.Parameters.AddWithValue("@StudentSection", std.Section);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show("Student not inserted. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void UpdateStudent(Student std, string id)
        {
            string sql = "update student_table set Name='" + std.Name + "',Reg='" + std.Reg + "',Class='" + std.Class + "',Section='" + std.Section + "' where id='" + id + "' ";
            NpgsqlConnection con = GetNpgsqlConnection();
            NpgsqlCommand cmd = new(sql, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show("Student not updated. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DeleteStudent(string id)
        {
            string sql = "DELETE FROM student_table where id='" + id + "' ";
            NpgsqlConnection con = GetNpgsqlConnection();
            NpgsqlCommand cmd = new(sql, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show("Student not Deleted. \n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DisplayAndSearch(string query, DataGridView dgv)
        {
            string sql = query;
            NpgsqlConnection con = GetNpgsqlConnection();
            NpgsqlCommand cmd = new(sql, con);
            NpgsqlDataAdapter adp = new(cmd);
            DataTable tbl = new();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            con.Close();
        }
    }
}