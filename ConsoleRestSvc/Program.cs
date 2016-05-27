﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Timers;


namespace ConsoleRestSvc
{
    class Program
    {
        private static Timer timer = new Timer(1000);

        static void Main(string[] args)
        {
            WebServiceHost host = new WebServiceHost(typeof(Service), new Uri("http://localhost:8011/"));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "");
            ServiceDebugBehavior sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.HttpHelpPageEnabled = false;


            //RestClient cl = new RestClient();
            //string json = cl.GetData("http://10.11.51.52:8011/JSon/producent/1");
            //RootObject r = cl.Deserialize<RootObject>(json);
            //string s = Convert.ToString(r.JSONData2Result.benodigd);
            
            //updateDatabase.updatadatabase(s);

            host.Open();
            Console.WriteLine("Service is Opgestart");
            Console.WriteLine("Druk op enter om af te sluiten...");
            //Console.WriteLine("database updated");
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
            //while (true)
            //{

            //}
            
            Console.ReadLine();
            host.Close();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RestClient cl = new RestClient();
            //string json = cl.GetData("http://10.13.139.159:8011/JSon/producent/1");
            //RootObject r = cl.Deserialize<RootObject>(json);
            //string s = Convert.ToString(r.JSONData2Result.benodigd);
            ////int unitid = 1;
            string s = "10000,211";
            database updateDatabase = new database();
            
            //int maxproductie = 100000;
            double newproductie=0;
            int aantal = updateDatabase.getMaxdatadatabase()+1;
            double totaleProductie=0;
            int[] unitprijs = new int[aantal];
            double[] unitproductie = new double[aantal];

            for (int i = 0; i < aantal; i++)
            {
                totaleProductie += Convert.ToInt32(updateDatabase.getProductieDatabase(Convert.ToString(i)));
                unitprijs[i] = Convert.ToInt32(updateDatabase.getPrijsDatabase(Convert.ToString(i)));
                unitproductie[i] = Convert.ToInt32(updateDatabase.getProductieDatabase(Convert.ToString(i)));
            }

           
            if (Convert.ToDouble(s)< totaleProductie)
            {
                //minder produceren

                //werkt
                if (unitprijs[0] >= unitprijs[1])
                {
                    if (unitproductie[2]>=2)
                    {
                        newproductie = unitproductie[2] - 2;
                        updateDatabase.updatadatabase(Convert.ToString(newproductie), 2);
                    }
                    else
                    {
                        newproductie = unitproductie[1] - 2;
                        updateDatabase.updatadatabase(Convert.ToString(newproductie), 1);
                    }

                }
                else
                {
                    if (unitproductie[0] >= 2)
                    {
                        newproductie = unitproductie[1] - 2;
                        updateDatabase.updatadatabase(Convert.ToString(newproductie), 1);
                    }
                    else
                    {
                        newproductie = unitproductie[2] - 2;
                        updateDatabase.updatadatabase(Convert.ToString(newproductie), 2);
                    }

                }

            }
            else if (Convert.ToDouble(s) > totaleProductie)
            {
                //meer produceren
               

                if (unitprijs[0] >= unitprijs[1])
                {
                    newproductie = unitproductie[1] + 2;
                    updateDatabase.updatadatabase(Convert.ToString(newproductie), 1);
                }
                else
                {
                    newproductie = unitproductie[2] + 2;
                    updateDatabase.updatadatabase(Convert.ToString(newproductie), 2);
                }


            }
 
            timer.Stop();
            timer.Start();
 
            Console.WriteLine(newproductie);
            Console.WriteLine("totaal gevraagd = "+s);
            Console.WriteLine("totaal geproduceerd =" + totaleProductie);
            Console.WriteLine("unit0 ="+ unitproductie[0]);
            Console.WriteLine("unit1 ="+ unitproductie[1]);
            Console.WriteLine("unit2 ="+ unitproductie[2]);
        }   
    }
}
