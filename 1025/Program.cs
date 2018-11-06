using OpenDataImport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace OpenDataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            Service.ImportService importService = new Service.ImportService();

            //var nodes = importService.FindOpenData();
            var nodes = importService.FindOpenDataFromDb("鶯歌");
            //importService.ImportToDb(nodes);

            showOpenData(nodes);
            Console.ReadKey();

        }

        public static void showOpenData(List<OpenData> nodes)
        {
            // Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            /*nodes.GroupBy(node => node.地區).ToList()
                 .ForEach(group =>
                 {
                     var key = group.Key;
                     var groupDatas = group.ToList();
                     var message = $"服務分類:{key},共有{groupDatas.Count()}筆資料";
                     Console.WriteLine(message);
                 });*/
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                Console.WriteLine(string.Format("{0}.{1}", i + 1, node.地區));
                Console.WriteLine(string.Format("\t經度:{0}", node.經度));
                Console.WriteLine(string.Format("\t緯度:{0}", node.緯度));
            }

        }
    }

}
