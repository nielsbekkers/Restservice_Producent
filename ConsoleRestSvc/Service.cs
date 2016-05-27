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
    class Service :IService
    {

        public string EchoWithGet(string s)
        {
            return "You said " + s;
        }

        public string EchoWithPost(Productie p)
        {
            return "You said " + p.id;
        }
        //public Productie PostProductie(Stream p)
        //{
        //    StreamReader reader = new StreamReader(p);
        //    string res = reader.ReadToEnd();
        //    reader.Close();
        //    reader.Dispose();
        //    Productie demand = Deserialize<Productie>(res);
        //    MySqlCommand comm = new MySqlCommand();

        //    comm.Connection = new MySqlConnection(
        //       Properties.Settings.Default.ConnectionString);
        //    string sql = @"insert into Personen ( naam, voornaam) values(@naam,@voornaam)";
        //    comm.CommandText = sql;
        //    comm.CommandTimeout = 10000;



        //    comm.Parameters.Add("@pruductie", MySqlDbType.VarChar, 50);
        //    comm.Parameters["@productie"].Value = demand.productie;
        //    comm.Parameters["@productie"].IsNullable = false;
        //    //comm.Parameters.Add("@voornaam", MySqlDbType.VarChar, 50);
        //    //comm.Parameters["@voornaam"].Value = pers.voornaam;
        //    //comm.Parameters["@voornaam"].IsNullable = false;

        //    comm.Connection.Open();
        //    try
        //    {
        //        int result = comm.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    { }
        //    comm.Connection.Close();
        //    return pers;


        //}
        //public Persoon PutPerson(Stream p, string id)
        //{
        //    StreamReader reader = new StreamReader(p);
        //    string res = reader.ReadToEnd();
        //    reader.Close();
        //    reader.Dispose();
        //    Persoon pers = Deserialize<Persoon>(res);
        //    SqlCommand comm = new SqlCommand();

        //    comm.Connection = new SqlConnection(
        //       Properties.Settings.Default.ConnectionString);
        //    string sql = @"update  Personen set  naam=@naam, voornaam=@voornaam where id=@id";
        //    comm.CommandText = sql;
        //    comm.CommandTimeout = 10000;


        //    comm.Parameters.Add("@id", SqlDbType.NVarChar, 50);
        //    comm.Parameters["@id"].Value = id;
        //    comm.Parameters["@id"].IsNullable = false;
        //    comm.Parameters.Add("@naam", SqlDbType.NVarChar, 50);
        //    comm.Parameters["@naam"].Value = pers.naam;
        //    comm.Parameters["@naam"].IsNullable = false;
        //    comm.Parameters.Add("@voornaam", SqlDbType.NVarChar, 50);
        //    comm.Parameters["@voornaam"].Value = pers.voornaam;
        //    comm.Parameters["@voornaam"].IsNullable = false;

        //    comm.Connection.Open();
        //    try
        //    {
        //        int result = comm.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    { }
        //    comm.Connection.Close();
        //    return pers;
        //}
        //public void DeletePerson( string id)
        //{
        //    SqlCommand comm = new SqlCommand(); ;
        //    comm.Connection = new SqlConnection(
        //       Properties.Settings.Default.ConnectionString);
        //    string sql = @"delete from  Personen  where id=@id";
        //    comm.CommandText = sql;
        //    comm.CommandTimeout = 10000;


        //    comm.Parameters.Add("@id", SqlDbType.NVarChar, 50);
        //    comm.Parameters["@id"].Value = id;
        //    comm.Parameters["@id"].IsNullable = false;


        //    comm.Connection.Open();
        //    try
        //    {
        //        int result = comm.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    { }
        //    comm.Connection.Close();

        //}
        public Productie GetData(string s)
        {
            MySqlCommand comm = new MySqlCommand();
            Productie p = new Productie();
            comm.Connection = new MySqlConnection(
               Properties.Settings.Default.ConnectionString);

            string sql = @"SELECT unitID, productie, prijs from Producent.unitdata where unitID=@id";
            comm.CommandText = sql;
                comm.CommandTimeout = 10000;
            
                
                comm.Parameters.Add("@id", MySqlDbType.VarChar, 50);
                comm.Parameters["@id"].Value = s;
                comm.Parameters["@id"].IsNullable = false;
                
                comm.Connection.Open();
              
                MySqlDataReader cursor = comm.ExecuteReader();
                while (cursor.Read())
                {
                    p.id = (int)cursor["unitID"];
                    //p.tijdstip = (int)cursor["tijdstip"];
                    p.productie = (int)cursor["productie"];
                    p.prijs = (int)cursor["prijs"];
                }
                comm.Connection.Close();
            return p;
        }
        public T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
        public string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }
    }
}
