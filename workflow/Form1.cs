using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace workflow
{
    public partial class Form1 : Form
    {

        int column;

        public Form1()
        {
            InitializeComponent();
        }

        //  Connexion C = new Connexion("RlzProject", @"RLZ-PC\RLZ");
        Table T = new Table("Client", "work", @"RLZ-PC\RLZ");   

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = T;
            /*
            dataGridView1.DataSource = C.Tables[0];
            dataGridView2.DataSource = C.Tables[1];
            dataGridView3.DataSource = C.Tables[2];
            dataGridView4.DataSource = C.Tables[3];
            dataGridView5.DataSource = C.Tables[4];             
            */
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            string[] v = { txtID.Text, txtNom.Text, txtPrenom.Text, txtAge.Text };
            MessageBox.Show(T.insert(v, "true", "false"));
            T.Save();
            dataGridView1.DataSource = T;
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            ////string[] values = { txtID.Text ,txtNom.Text , txtPrenom.Text,txtAge.Text};
            //T.delete(txtID.Text, "ok", "err");
            //dataGridView1.DataSource = T;
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
           //// T.save("saved done", "sorry, not saved!");
           // dataGridView1.DataSource = T;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            column = 0;
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            column = 1;
        }

        private void txtPrenom_TextChanged(object sender, EventArgs e)
        {
            column = 2;
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            column = 3;
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
        //    string[] v = { txtID.Text, txtNom.Text, txtPrenom.Text, txtAge.Text };
        //    T.update(txtID.Text, v, "oki", "not found");
        }

        private void button7_Click(object sender, EventArgs e)
        {/*
            DataRow row = T.NewRow();
            row[1] = "Louzi";
            row[2] = "redouane";
            row[3] = 23;
            T.Rows.Add(row);*/
        }

    }
}
