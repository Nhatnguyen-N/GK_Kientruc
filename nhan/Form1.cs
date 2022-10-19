using Bussi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace nhan
{

    public partial class Form1 : Form
    {
        private MessageQueue queue;
        private List<Student> obs = new List<Student>();
        public Form1()
        {
            InitializeComponent();
            init_queue();
        }
        void init_queue()
        {
            string path = @".\private$\phongkehoach";
            queue = new MessageQueue(path);
            queue.BeginReceive();
            queue.ReceiveCompleted += Queue_ReceiveCompleted;
        }
        private void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            //Khai bao de nhan message
            var msg = e.Message;
            int type = msg.BodyType;
            XmlMessageFormatter fmt = new XmlMessageFormatter(
            new System.Type[] { typeof(string), typeof(Student) }
            );

            //Danh dang cai Message
            msg.Formatter = fmt;
            //Lay cai Message
            var result = msg.Body;

            //Cat chuoi tao doi tuong de them  zo List
            String[] a = result.ToString().Split('-');
            Student student = new Student(Int32.Parse(a[0]), a[1] ,a[2], a[3], Boolean.Parse(a[4]), new DateTime());
            obs.Add(student);

            //Add ma vao List Box
            Thread th = new Thread(delegate () 
            {

                if (listBox1.InvokeRequired)
                {
                    System.Object[] i = new System.Object[1];
                    i[0] = a[0];

                    listBox1.Invoke(new MethodInvoker(delegate {
                        listBox1.Items.AddRange(i);
                    }));
                }
            });
            th.Start();

            //Su kien nghe MSMQ
            queue.BeginReceive();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            int i = listBox1.SelectedIndex;
            textBox1.AppendText(obs[i].BNid.ToString());
            textBox2.AppendText(obs[i].tenBN.ToString());
            textBox3.AppendText(obs[i].diaChi.ToString());
            textBox4.AppendText(obs[i].SDT.ToString());
            textBox5.AppendText(obs[i].DOB.ToString());
             if (obs[i].gioitinh)
                 label6.Text = "Nam";
             else label6.Text = "Nu";
            if (obs[i].gioitinh)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
            if (obs[i].gioitinh)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex=1;
            }
            
        }
    }
}
