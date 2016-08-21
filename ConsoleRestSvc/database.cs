using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using MySql.Data.MySqlClient;


namespace ConsoleRestSvc
{
    public class database
    {
        MySqlConnection myConnection = new MySqlConnection("datasource=localhost;port=3306;username=root;");
        MySqlCommand command;

        public void updatadatabase(string s, int userid)
        {
            string querry ="UPDATE Producent.unitdata SET productie ='"+ s+"' WHERE unitID = '"+userid+"'";
            Executequerry(querry);
        }

        public string getProductieDatabase(string userid)
        {            
            string querry = "select productie from Producent.unitdata where unitID= '"+userid+"'";
            Executequerry(querry);
            string iets = reader(querry, "productie");
            return iets;
        }

        public string getPrijsDatabase(string userid)
        {
            string querry = "select prijs from Producent.unitdata where unitID= '" + userid + "'";
            Executequerry(querry);
            return reader(querry, "prijs");
        }

        public int getMaxdatadatabase()
        {
            string querry = "SELECT max(unitID) FROM Producent.unitdata";
            Executequerry(querry);
            return Convert.ToInt32( reader(querry, "max(unitID)"));
        }

        private void openConnection()
        {
            if (myConnection.State == ConnectionState.Closed)
            {
                myConnection.Open();
            }
        }

        private void Executequerry(string query)
        {
            try
            {
                openConnection();
                command = new MySqlCommand(query, myConnection);

                if (command.ExecuteNonQuery() == 1)
                {
                    // querry gedaan
                }
                else
                {
                    //query niet gedaan
                }

            }
            catch (Exception ex)
            {}

            finally { myConnection.Close(); }
        }

        private string reader(string query, string zoek)
        {
            string value = "";

            try
            {
                openConnection();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value = reader[zoek].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {}

            finally{myConnection.Close();}

            return value;
        }
    }
}
