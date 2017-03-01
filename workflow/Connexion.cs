using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace workflow
{
    public class Connexion : DataSet   
    {
        private string server;
        private string database;
        private string user; string password;
        private Boolean security = false;
        private List<SqlDataAdapter> AD = new List<SqlDataAdapter>();
        
        /* privates Inicilized of attributs */
        private void constrector(string database, string server)
        {
            this.server = server;
            this.database = database;
        }

        private void sécure(string user, string password)
        {
            this.security = true;
            this.user = user;
            this.password = password;
        }
        /* x2 construtcts DataSet */

        public Connexion(string database, string server)
        {
            AllTables(database, server);
        }

        public Connexion(string database, string server, string user, string password)
        {
            AllTables(database, server);
            sécure(user, password);
        }
        /* x2 construtct DataTable */

        public Connexion(DataTable T,string TableName, string database, string server)
        {
            OneTable(T, TableName, database, server);
        }

        public Connexion(DataTable T, string TableName, string database, string server, string user, string password)
        {
            OneTable(T, TableName, database, server);
            sécure(user, password);
        }
        /* Private Methodes */

        private void AllTables(string database, string server)
        {
            constrector(database, server);
            SqlDataAdapter da = new SqlDataAdapter();
            String[] TNames = this.TNames();
            for (int i = 0; i < TNames.Length; i++)
            {
                da = Load(TNames[i]);
                da.Fill(this, TNames[i]);
                AD.Add(da);
            }
        }

        private void OneTable(DataTable T,string TName, string database, string server)
        {
            constrector(database, server);
            SqlDataAdapter da = Load(TName);
            da.Fill(T);
            AD.Add(da);
        }

        private SqlDataAdapter Load(string TableName)
        {
            SqlDataAdapter da = import("select * from " + TableName);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            return da;
        }

        private SqlDataAdapter import(string query)
        {
            return new SqlDataAdapter(query, connection());
        }

        private SqlConnection connection()
        {
            string s;
            if (security == false) s = "Integrated Security=True";
            else s = "Persist Security Info=True;User ID=" + user + ";Password=" + password + "";
            return new SqlConnection("Data Source=" + server + ";Initial Catalog=" + database + ";" + s);
        }
 
        String[] TNames()
        {
            DataTable T = new DataTable();
            import("USE " + database + " SELECT name FROM sys.tables WHERE name != 'sysdiagrams'").Fill(T);
            int RowsCount = T.Rows.Count;
            String[] s = new String[RowsCount];
            for (int i = 0; i < RowsCount; i++)
            {
                s[i] = T.Rows[i]["name"].ToString();
            }
            return s;
        }

        Boolean TableExist(String TName)
        {
            String[] TNames = this.TNames();
            Boolean Exist = false;
            for (int i = 0; i < TNames.Length; i++)
                if (TNames[i] == TName) Exist = true;
            return Exist;
        }

        private bool Save_Dt(DataTable Dt, SqlDataAdapter da)
        {
            try
            {
                new SqlCommandBuilder(da);
                da.Update(Dt.Select("", "", DataViewRowState.Added));
                da.Update(Dt.Select("", "", DataViewRowState.ModifiedCurrent));
                da.Update(Dt.Select("", "", DataViewRowState.Deleted));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save_Ds()
        {
            try
            {
               new SqlCommandBuilder(AD[0]);
               AD[0].Update(this.Tables[0].Select("","",DataViewRowState.Added));
               AD[0].Update(this.Tables[0].Select("", "", DataViewRowState.ModifiedCurrent));
               AD[0].Update(this.Tables[0].Select("", "", DataViewRowState.Deleted));
                return true;
            }
            catch
            {
                return false;
            }
        }
        //END.
    }
}
