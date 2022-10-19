using Bussi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        MessageQueue queue = null;
        public Form1()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            string path = @".\private$\phongkehoach";
            //string path = @"hbmnl\private$\phongkehoach";
            if (MessageQueue.Exists(path))
            {
                queue = new MessageQueue(path, QueueAccessMode.Send);
            }
            else
                queue = MessageQueue.Create(path, true);
            queue.Label = "queue cho phong ke hoach";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ma = Int32.Parse(textBox1.Text);
            string ten = textBox2.Text;
            string diachi = textBox3.Text;
            string sdt = textBox4.Text;
            //bool gioitinh = checkBox1.Checked;
            //bool gioitinh = true;
            //if (radioButton2.Checked)
            //{
            //  gioitinh = false;
            //}
            //else
            //  gioitinh = true;
            bool gioitinh = true;
            if (comboBox1.SelectedIndex == 1)
            {
               gioitinh = false;
            }
            else
            {
                gioitinh=true;
            }

            Student st = new Student(ma, ten,diachi,sdt,gioitinh, new DateTime(1999, 10, 15));
            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
            queue.Send(st, transaction);
            transaction.Commit();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
