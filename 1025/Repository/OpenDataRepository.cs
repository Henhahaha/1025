
using OpenDataImport.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenDataImport.Repository
{
    public class OpenDataRepository
    {
        public string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Henry\source\repos\1025\1025\App_Data\Database1.mdf;Integrated Security=True";
                //return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ Directory.GetCurrentDirectory() + @"\App_Data\nodeDB.mdf;Integrated Security=True";
            }

        }

        public List<OpenData> SelectAll(string name)
        {
            var result = new List<OpenData>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();


            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"
Select Id,地區, 經度, 緯度
From OpenData");
            if (!string.IsNullOrEmpty(name))
                command.CommandText =
                    $"{command.CommandText} Where 地區=N'{name}'";
            //command.CommandText + "Where 服務分類=N'" + name+"'";
            //command.Parameters.Add(new SqlParameter("p1", name));

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var item = new OpenData();
                //item.id = reader.GetInt32(0);
                item.地區 = reader.GetString(1);
                item.經度 = reader.GetString(2);
                item.緯度 = reader.GetString(3);
                result.Add(item);
            }



            connection.Close();
            return result;
        }

        public void Insert(OpenData item)
        {
            var newItem = item;
            var connection = new SqlConnection(ConnectionString);
            connection.Open();


            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"
        INSERT INTO OpenData(地區, 經度, 緯度) 
        VALUES              (N'{0}',N'{1}',N'{2}')
            ", newItem.地區, newItem.經度, newItem.緯度);

            command.ExecuteNonQuery();


            connection.Close();
        }



        //        public object Update(object item)
        //        {
        //            var updateItem = item as YC.Models.OpenData;
        //            var connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //            connection.Open();


        //            var command = new System.Data.SqlClient.SqlCommand("", connection);
        //            command.CommandText = string.Format(@"
        //UPDATE [OpenData]
        //   SET 
        //      [資料集名稱] = N'{0}'
        //      ,[服務分類] = N'{1}'
        //      ,[資料集描述] = N'{2}'
        //      ,[DisplaySqe] = N'{3}'
        // WHERE ID=N'{4}'
        //            ", updateItem.服務分類, updateItem.資料集名稱, updateItem.資料集描述, updateItem.DisplaySqe, updateItem.ID);

        //            command.ExecuteNonQuery();


        //            connection.Close();
        //            return item;
        //        }

        //        public void Delete(string ID)
        //        {
        //            var connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //            connection.Open();


        //            var command = new System.Data.SqlClient.SqlCommand("", connection);
        //            command.CommandText = string.Format(@"
        //DELETE FROM [OpenData]
        // WHERE ID=N'{0}'
        //            ", ID);

        //            command.ExecuteNonQuery();


        //            connection.Close();
        //        }
    }
}
