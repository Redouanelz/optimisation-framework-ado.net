using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

//using MySql.Data.MySqlClient;


namespace workflow
{
     public class Table : DataTable
    {        
        Connexion c;
        private void constrector(string table, string database, string server, string user, string password, DataTable T, Boolean sécurity)
        {
            c = (sécurity == true) ? new Connexion(this,table, database, server, user, password) : new Connexion(this,table, database, server);
        }

        public Table(string table, string database, string server)
        {
            constrector(table, database, server, "", "", this, false);
        }
        public Table(string table, string database, string server, string user, string password)
        {
            constrector(table, database, server, user, password, this, true);
        }

        public string value(int row, int col)
        {
            try
            {
                return this.Rows[row][col].ToString();
            }
            catch
            {
                return "Introuvable";
            }
        }

        public int columns_count()
        {
            return this.Columns.Count;
        }
        public int rows_count()
        {
            return this.Rows.Count;
        }

        public string insert(string[] values, string msg, string error)
        {
           
            try
            {
                DataRow row = this.NewRow();
                for (int i = 0; i < values.Length; i++) row[i] = values[i];
                this.Rows.Add(row);
                return msg;
            }
            catch
            {
                return error;
            }
        }


        private string delete(int row, string msg, string error)
        {
            try
            {
                this.Rows[row].Delete();
                return msg;
            }
            catch
            {
                return error;
            }
        }

        public string delete(int column, string value, string msg, string error)
        {

            /* delete a */
            for (int i = 0; i < rows_count(); i++)
                if (value == this.value(i, column)) delete(i, msg, error);
            return msg;

        }

        public string delete(string KeyValue, string msg, string error)
        {
            /* delete a row by his index , using prmary key */
            return delete(this.Rows.IndexOf(this.Rows.Find(KeyValue)), msg, error);

        }

        public string delete(int column, string[] values, string msg, string error)
        {
            /* delete an array of values from a single column */
            return delete(column, values[column], msg, error);
        }

        public string update(int column, string SearchValue, string[] values, string msg, string error)
        {
            try
            {
                for (int i = 0; i < this.rows_count(); i++)

                    if (SearchValue == value(i, column).ToString())
                    {
                        DataRow row = this.Rows.Find(this.value(i, this.Columns.IndexOf(this.PrimaryKey[0])));
                        row.BeginEdit();
                        for (int j = 0; j < values.Length; j++) if (j != this.Columns.IndexOf(this.PrimaryKey[0])) row[j] = values[j];
                        row.EndEdit();
                    }
                    else msg = "Not Found";
                return msg;
            }
            catch { return error; }
        }

        public string update(string key, string[] values, string msg, string error)
        {
            try
            {
                DataRow row = this.Rows.Find(key);
                row.BeginEdit();
                for (int j = 0; j < values.Length; j++) if (j != this.Columns.IndexOf(this.PrimaryKey[0])) row[j] = values[j];
                row.EndEdit();
                return msg;
            }
            catch { return error; }
        }

        public void Save() {
            c.Save_Ds();
        }      
        
    }
}
