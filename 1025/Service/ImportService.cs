using OpenDataImport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OpenDataImport.Service
{
    public class ImportService
    {

        private Repository.OpenDataRepository _repository;
        public ImportService()
        {
            _repository = new Repository.OpenDataRepository();
        }
        public List<OpenData> FindOpenData()
        {
            List<OpenData> result = new List<OpenData>();

            string baseDir = Directory.GetCurrentDirectory();

            var xml = XElement.Load(@"C:/Users/Henry/source/repos/10182/10182/App_Data/O-A0001-001.xml");

            XNamespace aw = "urn:cwb:gov:tw:cwbcommon:0.1";
            var nodes = xml.Descendants(aw + "location").ToList();

            //XNamespace gml = @"http://www.opengis.net/gml/3.2";
            //XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";
            //var nodes = xml.Descendants("node").ToList();


            //result = nodes
            //    .Where(x => !x.IsEmpty).ToList()
            //    .Select(node =>
            //    {
            //        OpenData item = new OpenData();
            //        //item.id = int.Parse(getValue(node, "id"));
            //        item.地區 = getValue(node, "地區");
            //        item.經度 = getValue(node, "經度");
            //        item.緯度 = getValue(node, "緯度");
            //        return item;
            //    }).ToList();
            //return result;


            nodes.ToList().ForEach(node =>
            {
                OpenData item = new OpenData();

                item.地區 = getValue(node, aw + "locationName");
                item.經度 = getValue(node, aw + "lat");
                item.緯度 = getValue(node, aw + "lon");
                result.Add(item);

            });
            return result;
        }
        public List<OpenData> FindOpenDataFromXml()
        {
            List<OpenData> result = new List<OpenData>();

            string baseDir = Directory.GetCurrentDirectory();

            var xml = XElement.Load(@"C:/Users/Henry/Desktop/軟體工程/XML/O-A0001-001.xml");

            XNamespace aw = "urn:cwb:gov:tw:cwbcommon:0.1";
            var nodes = xml.Descendants(aw + "location").ToList();


            nodes.ToList().ForEach(node =>
            {
                OpenData item = new OpenData();

                item.地區 = getValue(node, aw + "locationName");
                item.經度 = getValue(node, aw + "lat");
                item.緯度 = getValue(node, aw + "lon");
                result.Add(item);

            });
            return result;
        }

        public List<OpenData> FindOpenDataFromDb(string name)
        {

            return _repository.SelectAll(name);
        }

        public void ImportToDb(List<OpenData> openDatas)
        {
            Repository.OpenDataRepository Repository = new Repository.OpenDataRepository();
            openDatas.ForEach(item =>
            {
                Repository.Insert(item);
            });

        }

        private static string getValue(XElement node, XName propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }

        //private string getValue(XElement node, string propertyName)
        //{
        //    return node.Element(propertyName)?.Value?.Trim();

        //}

    }
}
