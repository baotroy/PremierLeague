using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace NextMatch
{
    class Scorer
    {
        xuly x = new xuly();

       public  void LoadTopScorer(ref ListView lv, string address) {
           try
           {
               XmlDocument xml = new XmlDocument();
               xml.LoadXml(x.TopScorerParseXML(x.loadWebpage(address)));
               ListViewItem li;
               XmlNodeList nodes = xml.SelectNodes("//tr");

               foreach (XmlNode node in nodes)
               {
                   li = new ListViewItem(node.ChildNodes[0].InnerText);
                   li.SubItems.Add(node.ChildNodes[1].FirstChild.InnerText);
                   li.SubItems.Add(node.ChildNodes[2].FirstChild.InnerText);
                   li.SubItems.Add(node.ChildNodes[3].InnerText);
                   lv.Items.Add(li);
               }
           }
           catch (Exception e) {
               throw e;
           }
        }

      
    }
}
