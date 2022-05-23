using Kumeleme_Solution_WinForm.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kumeleme_Solution_WinForm.Datasets
{
    public static class Datas
    {
        public static List<Data> Items { get; private set; }

        static Datas()
        {
            Items = new List<Data>();
        }

        public static void SetCSVData(string path)
        {
            Items.Clear();

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int counter = 1;
                
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    string name = fields[0];
                    int hastaYas = int.Parse(fields[1]);
                    int lenfNodAdet = int.Parse(fields[2]);
                    string hayattaKalmaSuresi = fields[3];

                    Data newData = new Data
                    {
                        Name = name,
                        OperasyondakiHastaYasi = hastaYas,
                        PozitifAksillerLenfNoduAdeti = lenfNodAdet,
                        HayattaKalmaSuresi = hayattaKalmaSuresi,
                        Cluster = "C" + counter
                    };

                    counter = counter == 2 ? 1 : counter + 1;
                    
                    Add(newData);
                }
            }
        }

        public static void Add(Data newData)
        {
            if (String.IsNullOrWhiteSpace(newData.Cluster))
            {
                Distance distance = Distances.FindMinDistance(newData);
                
                if(distance != null)
                {
                    Distances.NewDistance(distance);

                    newData.Cluster = distance.Data.Cluster;
                }
            }
            else
            {
                Clusters.AddNewDataToExistingCluster(newData);
            }

            Items.Add(newData);

            Clusters.RefreshClusters();
            Distances.CreateDistances();
        }

        public static void ChangeCluster(Data data, string clusterName)
        {
            Items.FirstOrDefault(x => x == data).Cluster = clusterName;
            Clusters.RefreshClusters();
            Centers.CreateCenters();
            Distances.CreateDistances();
        }
    }
}
