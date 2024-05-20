using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PDB_Test;

namespace PDB_Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] p = new string[1];
            p[2] = "a";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DieClass die = new DieClass();
            die.Die();
        }
    }
}
