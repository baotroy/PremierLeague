using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
namespace NextMatch
{
   
    class xuly
    {
        public static string[] ID = new string[20];
        public static string[] Teams = new string[20];
        public static string[] Stadiums = new string[20];
        public static string season = "";
        public void GetTeams(string file) {
            string strfile = File.ReadAllText(file);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(strfile);

            XmlNodeList nodes = xdoc.SelectNodes("//team");
            int i=0;
            foreach (XmlNode node in nodes) {
                Teams[i] = node.FirstChild.InnerText;
                Stadiums[i] = node.LastChild.InnerText;
                ID[i] = node.Attributes["id"].Value.ToString();
                i++;
            }
            season = xdoc.SelectSingleNode("//teams").Attributes[0].Value;
        }

        public string loadWebpage(string address)
        {
            try
            {
                string url = address;
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                string contents = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    contents = reader.ReadToEnd();
                    reader.Close();
                }
                response.Close();

                return contents;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string parseXML(string htmlContent)
        {
            string find = htmlContent.Substring(htmlContent.IndexOf("<tbody>"));
            find = find.Substring(0, find.IndexOf("</table>"));
            return find;
        }
        public string eplParseXML(string htmlContent)
        {
            string find = htmlContent.Substring(htmlContent.IndexOf("megamenu-tape") + "megamenu-tape".Length + 2);
            find = find.Substring(0, find.IndexOf("</ul>"));
            return find + "</ul>".Replace("\n", "");
        }

        public string tableParseXML(string htmlContent)
        {
            string find = htmlContent.Substring(htmlContent.IndexOf("Up/Down"));
            find = find.Substring(find.IndexOf("<tbody>"), find.IndexOf("</tbody>") - find.IndexOf("<tbody>"));
            find = find + "</tbody>".Replace("\n", "").Replace("\r", "");
            return processString(find) ;

        }

        string processString(string s)
        {
            string result = "<tbody>";
            int i = 0;
            do
            {
                i++;
                int sTR = s.IndexOf("<tr");
                int eTR = s.IndexOf("</tr>");
                if (i % 2 == 1)
                    result += s.Substring(sTR, eTR - sTR + 5);
                s = s.Substring(eTR + 5);
            }
            while (s.IndexOf("<tr") > 0);
            result += "</tbody>";
            result = result.Replace("\n", "").Replace("\r", "");
            return result;
        }

        public string TopScorerParseXML(string htmlContent) {
            string find = htmlContent.Substring(htmlContent.IndexOf("<th>Goals</th>"));
            find = find.Substring(find.IndexOf("<tbody>"), find.IndexOf("</tbody>") - find.IndexOf("<tbody>"));
            find= find + "</tbody>";
            return find;
        }
    }
}
