using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NextMatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        xuly x;
        TimeSpan timeSpan = new TimeSpan(0,0,0);
        bool summer = false, live=false;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Check your network connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                GetTimeSpan();
                x = new xuly();
                x.GetTeams("teams.txt");
                string text=timeSpan.ToString();
                if(text.IndexOf("-")<0)
                    text="+"+timeSpan;
                this.Text = "Premier League "+xuly.season+". Your location: UTC" + text + ". Copyright© baotroy@gmail.com";
                lblNot.Text = "Your system time zone is set to UTC" + text+". And the matches time will be shown as this setting.";
                Decoration();
            
               
                //System.Threading.Thread matchThread =       new System.Threading.Thread(loadMatches);
                //System.Threading.Thread fixThread =           new System.Threading.Thread(LoadPLFix);
                //System.Threading.Thread tableThread = new System.Threading.Thread(LoadTable);

                //matchThread.Start();
                ////if (!matchThread.IsAlive) {
                //    fixThread.Start();
                //    tableThread.Start();
                ////}

              //  loadMatches();

                LoadPLFix();

              //  LoadTable();
        }
        
        void LoadTable() {
            
            XmlDocument doc = new XmlDocument();
            try
            {
                    doc.LoadXml(x.tableParseXML(x.loadWebpage("http://www.premierleague.com/en-gb/matchday/league-table.html")));
            }
            catch (Exception e)
            {
                MessageBox.Show("Load failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lvTable.Items.Clear();
            XmlNodeList bigNodes = doc.SelectNodes("//tr");
            ListViewItem l;
            foreach (XmlNode node in bigNodes) {
                l=new ListViewItem();
                int pos =int.Parse( node.FirstChild.InnerText);
                int lastPos = int.Parse(node.ChildNodes[2].InnerText.Substring(1, node.ChildNodes[2].InnerText.Length-2));
                if (pos < lastPos && pos==1)
                    l.ImageIndex = 1;//up white
                else if (pos < lastPos)
                    l.ImageIndex = 2;//up blue
                else if (pos == lastPos)
                    l.ImageIndex = 3;//still
                else
                    l.ImageIndex = 4;//down grey
                //l.SubItems.Add("");
                l.SubItems.Add(pos.ToString());
                l.SubItems.Add("("+lastPos.ToString()+")");
                string club = node.ChildNodes[3].InnerText;

                l.SubItems.Add(node.ChildNodes[3].InnerText);
                l.SubItems.Add(node.ChildNodes[4].InnerText);
                l.SubItems.Add(node.ChildNodes[5].InnerText);
                l.SubItems.Add(node.ChildNodes[6].InnerText);
                l.SubItems.Add(node.ChildNodes[7].InnerText);
                l.SubItems.Add(node.ChildNodes[8].InnerText);
                l.SubItems.Add(node.ChildNodes[9].InnerText);
                l.SubItems.Add(node.ChildNodes[10].InnerText);
                l.SubItems.Add(node.ChildNodes[11].InnerText);
                if(pos==1) {
                    l.BackColor = Color.FromArgb(140, 189, 224);
                    l.ForeColor = Color.White;
                }
                else if (pos == 2 || pos == 3 || pos == 4) {
                    l.BackColor = Color.FromArgb(200, 220, 235);
                }
                else if (pos == 5)
                {
                    l.BackColor = Color.FromArgb(237, 245, 250);
                }
                else if (pos == 18 || pos == 19 || pos == 20)
                {
                    l.BackColor = Color.FromArgb(224, 230, 233);
                }
                lvTable.Items.Add(l);
            }
        }


        void Decoration() {
      
            tabPage2.Text = "Fixtures & Results";
            tabPage3.Text = "Table";

        }
        string strdate = "";
        void LoadPLFix() {
            lvFix.Items.Clear();

            XmlDocument xmlPL = new XmlDocument();
            try
            {
              
                    xmlPL.LoadXml(x.eplParseXML(x.loadWebpage("http://www.premierleague.com/en-gb.html")));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
          // xmlPL.Load("fi.xml");
            XmlNodeList nodes = xmlPL.SelectNodes("//li");
            for (int i = 0; i < nodes.Count; i++) {
                string strDate = getTime(nodes[i]);
                //string strDate = getDate(nodes[i]);
                string strTime = strDate.Substring(strDate.Length - 5);
                strDate = strDate.Substring(0, strDate.Length - 5).Trim();
                string strMatch = getMatch(nodes[i]);

                if (strMatch.IndexOf('-') > -1)
                {
                    loadResult(strDate,strTime , strMatch);
                }
                else {
                    loadFixMatch(strDate, strTime, strMatch);
                }
            }
            Living();
           LiveFirst(ref lvFix);
        }
        void Living() {
            if (live)
            {
                timer1.Interval = 60000;
            }
            else
            {
                timer1.Interval = 300000;
            }
            timer1.Start();
        }
        
        void loadFixMatch(string d,string t, string m) {
            string team1= m.Substring(0, m.IndexOf(' '));
            string team2=m.Substring(m.LastIndexOf(' ')+1);
            ListViewItem l=new ListViewItem();

            if (d != "TODAY")
            {
                DateTime dt = new DateTime();

                dt = DateTime.Parse(d + ' ' + t + ":00");
                int hourSpan = timeSpan.Hours;
                int minSpan = timeSpan.Minutes;
                if (timeSpan != new TimeSpan(0, 0, 0))
                {
                    if (summer)
                    {
                        dt = dt.AddHours(hourSpan - 1).AddMinutes(minSpan);
                        summer = true;
                    }
                    else
                        dt = dt.AddHours(hourSpan).AddMinutes(minSpan);
                }
                l.Text =dt.DayOfWeek.ToString()+", "+ dt.Date.ToShortDateString();
                string min = dt.Minute.ToString().Length == 1 ? "00" : dt.Minute.ToString();

                l.SubItems.Add(dt.Hour.ToString() + ':' + min);
            }
               
            else
            {
                l.Text = d;
                DateTime time=DateTime.Parse(t+":00");
                if (summer) time = time.AddHours(6);
                else time = time.AddHours(7);
                l.SubItems.Add(time.ToString("HH:mm"));
            }
           


            l.SubItems.Add(getTeam(team1));
            l.SubItems.Add(" v");
            l.SubItems.Add(getTeam(team2));
            l.SubItems.Add(getStadium(team1));
            
            l.BackColor = Color.FromArgb(250, 255, 189);
            lvFix.Items.Add(l);
        }
        
        void loadResult(string d, string t, string m)
        {
            string team1 = m.Substring(0, m.IndexOf(' '));
            string team2 = m.Substring(m.LastIndexOf(' ') + 1);
            string score = m.Substring(m.IndexOf(' '), m.LastIndexOf(' ') - m.IndexOf(' '));
            ListViewItem l = new ListViewItem();
            DateTime dt=new DateTime();
            if (t != "FT" && t!= "LIVE" && t!="HT")
            {
                dt = DateTime.Parse(d + ' ' + t + ":00");
                int hourSpan = timeSpan.Hours;
                int minSpan = timeSpan.Minutes;
                if (timeSpan != new TimeSpan(0, 0, 0))
                {
                    if (summer)
                    {
                        dt = dt.AddHours(hourSpan - 1).AddMinutes(minSpan);
                        summer = true;
                    }
                    else
                        dt = dt.AddHours(hourSpan).AddMinutes(minSpan);
                }
                if (d=="TODAY")
                {
                    l.Text = "TODAY";
                    l.SubItems.Add(DateTime.Parse(t).ToString("HH:mm"));
                    live = false;
                }
                else
                {
                    l.Text = dt.Date.ToShortDateString();
                    string min = dt.Minute.ToString().Length == 1 ? "00" : dt.Minute.ToString();
                    l.SubItems.Add(dt.Hour.ToString() + ':' + min);
                    live = false;
                }
            }
            else { //time la ft
                
                    l.Text = d;
                
                    l.SubItems.Add(t);
                if(t=="LIVE")
                    live = true;
                else live=false;            

            }
            
            l.SubItems.Add(getTeam(team1));
            l.SubItems.Add(score);
            l.SubItems.Add(getTeam(team2));
            l.SubItems.Add(getStadium(team1));
            lvFix.Items.Add(l);

            
        }
        string getTime(XmlNode n)
        { //li tag
            XmlNode timeNode = n.SelectSingleNode("div[@class='megamenu-date']").SelectSingleNode("span");
            return timeNode.InnerText;
        }
        string getDate(XmlNode n)
        { //li tag
            XmlNode timeNode = n.SelectSingleNode("div[@class='ticker-date']").SelectSingleNode("span");
            return timeNode.InnerText;
        }
        string getMatch(XmlNode n)
        { //li tag
            XmlNode timeNode = n.SelectSingleNode("div[@class='megamenu-matchName']").SelectSingleNode("span");
            return timeNode.InnerText;
        }

        void loadMatches() {
           XmlDocument xmlDoc = new XmlDocument();
           try
           {
                
                     xmlDoc.LoadXml(x.parseXML(x.loadWebpage("http://www.manutd.com/en/Fixtures-And-Results.aspx")));
           }
            catch(Exception e){
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            XmlNodeList nodes = xmlDoc.SelectNodes("//tr");
            
            int ipanel = 0;
            //Panel pan;
            //PictureBox pType, pHome, pAway;
            Label Ldate, LScore, LTeams, lVersus;
            
            for(int i=0;i<nodes.Count;i++)//tr tag
            {
                //lVersus = new Label();
                //lVersus.Text = "V";
                //lVersus.AutoSize = true;
                //lVersus.Location = new Point(310, 5 * (ipanel + 1)+5);

                //pHome = new PictureBox();
                //pHome.Size = new System.Drawing.Size(30, 30);
                //pHome.Location = new Point(250, 5 * (ipanel + 1));
                //pHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

                //pAway = new PictureBox();
                //pAway.Size = new System.Drawing.Size(30, 30);
                //pAway.Location = new Point(348, 5* (ipanel + 1));
                //pAway.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

                //LScore = new Label();
                //LScore.Location = new Point(440, 5 * (ipanel + 1)+10);
                //LScore.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));

                
                XmlNodeList tdNodes = nodes[i].ChildNodes;//get td tag
                strdate =tdNodes[0].InnerText;
                //string imgTypeUrl ="http://www.manutd.com"+ tdNodes[1].FirstChild.Attributes["src"].Value;
                //pType = new PictureBox();
                //pType.Size = new System.Drawing.Size(100, 100);
                //pType.Location = new Point(118, 5 * (ipanel + 1)+ 10);
                //pType.ImageLocation = imgTypeUrl;
               
                //XmlNodeList logoNodes = tdNodes[2].ChildNodes;
                //string imgLogo1 ="http://www.manutd.com"+ logoNodes[0].Attributes["src"].Value;
                
                //pHome.ImageLocation = imgLogo1;



                //string imgLogo2 = "http://www.manutd.com" + logoNodes[2].Attributes["src"].Value;
                //PictureBox pLogo2 = new PictureBox();
                //pAway.ImageLocation = imgLogo2;

                //LTeams = new Label();
                //string teams = logoNodes[3].InnerText.Replace("&nbsp;","");
                //LTeams.AutoSize = true;
                //LTeams.Text = teams.ToUpper();
                //LTeams.Location = new Point(250, 40+(4*ipanel));

                string score = getScoreKickOff(tdNodes[4]);
               // LScore.Text = score;
                
                
                //Ldate = new Label();
                //Ldate.Name = "date" + ipanel.ToString();
                //Ldate.Location = new Point(10, 5 * (ipanel + 1)+10);
                //Ldate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                //Ldate.Text = strdate;
                //Label tem = new Label();
                //tem = Ldate;
              
                //pan = new Panel();
                //pan.Name = "pan" + ipanel;
                //pan.Location=new Point(10, 58 * ipanel+10); ipanel++;
                //pan.Size = new System.Drawing.Size(633, 68);
                //pan.Controls.Add(Ldate);
                //pan.Controls.Add(lVersus);
                //pan.Controls.Add(pType);
                //pan.Controls.Add(pHome);
                //pan.Controls.Add(pAway);
                //pan.Controls.Add(LScore);
                //pan.Controls.Add(LTeams);
             //   tabPage1.Controls.Add(pan);
            }
        }

        void GetTimeSpan() {
            DateTime sys = DateTime.Now;
            DateTime utc = sys.ToUniversalTime();
            if(sys!=utc)
                timeSpan = sys - utc;
        }
        void convertTime(string date,ref string hour, string strXML) {
            
            string dt = date +" "+ hour+":00";
            
            DateTime dtime = DateTime.Parse(dt);

            int hourSpan = timeSpan.Hours;
            int minSpan = timeSpan.Minutes;
            if (timeSpan != new TimeSpan(0, 0, 0))
            {
                if (isBTS(strXML))
                {
                    dtime = dtime.AddHours(hourSpan - 1).AddMinutes(minSpan);
                    summer = true;
                }
                else
                    dtime = dtime.AddHours(hourSpan).AddMinutes(minSpan);
            }
            strdate = dtime.Date.ToString();
            string min=dtime.Minute.ToString().Length==2?dtime.Minute.ToString():dtime.Minute.ToString()+"0";
            hour = dtime.Hour + ":" + min;
            
        }
        string getScoreKickOff(XmlNode node) {
            string att = node.Attributes["class"].Value;
            string value;
            if (att == "score")
            {
                value = node.FirstChild.InnerText;
                value = value.Replace("&nbsp;", " ");
                string time="00:00";
                convertTime(strdate, ref time, "");
                
            }
            else
            {
                value = node.InnerText;
                int start=value.IndexOf("KO");
                int end=value.LastIndexOf("BST");
                value = value.Substring(start+2, end - start-2);
                convertTime(strdate,ref value, node.InnerXml);
            }
            return value;
        }

        bool isBTS(string s) { 
            int pos=s.IndexOf("&amp;t=1");
            if (pos > 0)
                return true;//summer time
            return false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadPLFix();
        }
        string getTeam(string t)
        {
            int team;
            for(team=0;team<xuly.ID.Length;team++)
            {
                if (t == xuly.ID[team].ToString())
                    break;
            }

            return xuly.Teams[team];
        }
        string getStadium(string t)
        {
            int team;
            for (team = 0; team < xuly.ID.Length; team++)
            {
                if (t == xuly.ID[team].ToString())
                    break;
            }

            return xuly.Stadiums[team];
        }

      

        void LiveFirst(ref ListView lv)
        {
            DateTime d1=new DateTime();
            DateTime d2 = new DateTime();
            bool endToday = false;
            for (int i = 0; i < lv.Items.Count - 1; i++) {
                for (int j = i+1; j < lv.Items.Count ; j++)
                {
                    if (lv.Items[j].Text == "TODAY" && lv.Items[i].Text != "TODAY")
                    {
                        swap(lv.Items[i], lv.Items[j]);
                        endToday = true;
                        //  DecoreLine(lv.Items[i]);
                    }

                    else if (DateTime.TryParse(lv.Items[i].Text, out d1) && DateTime.TryParse(lv.Items[j].Text, out d2))
                    {
                        if (d1 > d2)
                            swap(lv.Items[i], lv.Items[j]);
                        //  DecoreLine(lv.Items[i]);
                    }
                }
                DecoreLine(lv.Items[i]);
            }
        }

        void DecoreLine(ListViewItem i) {
            if (i.SubItems[1].Text == "LIVE" || i.SubItems[1].Text == "HT")
            {
                i.ForeColor = Color.Red;
                i.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            }
            else if (i.SubItems[1].Text == "FT") {
                i.ForeColor = Color.Gold;
                i.BackColor = Color.FromArgb(50, 50, 78);
            }
            else if (i.Text == "TODAY")
            {
                i.BackColor = Color.FromArgb(50, 50, 78);
                i.ForeColor = Color.White;
            }
            else if (i.SubItems[3].Text.IndexOf('-') > 0)
            {
                i.ForeColor = Color.Silver;
                i.BackColor = Color.White;
            }
        }

       
        void swap(ListViewItem la, ListViewItem lb)
        {
            ListViewItem li = new ListViewItem();
            li.Text = la.Text;
            li.SubItems.Add(la.SubItems[1].Text);
            li.SubItems.Add(la.SubItems[2].Text);
            li.SubItems.Add(la.SubItems[3].Text);
            li.SubItems.Add(la.SubItems[4].Text);
            li.SubItems.Add(la.SubItems[5].Text);

            la.Text = lb.Text;
            la.SubItems[1].Text=  lb.SubItems[1].Text;
            la.SubItems[2].Text = lb.SubItems[2].Text;
            la.SubItems[3].Text = lb.SubItems[3].Text;
            la.SubItems[4].Text = lb.SubItems[4].Text;
            la.SubItems[5].Text = lb.SubItems[5].Text;

            lb.Text = li.Text;
            lb.SubItems[1].Text = li.SubItems[1].Text;
            lb.SubItems[2].Text = li.SubItems[2].Text;
            lb.SubItems[3].Text = li.SubItems[3].Text;
            lb.SubItems[4].Text = li.SubItems[4].Text;
            lb.SubItems[5].Text = li.SubItems[5].Text;
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabControl c=(TabControl)sender;
            if(c.SelectedIndex==1){
                if (lvTable.Items.Count == 0)
                {
                    this.Enabled = false;
                    LoadTable();
                    this.Enabled = true;
                }
            }
            else if(c.SelectedIndex==2){
                if (lvPlayer.Items.Count == 0)
                {
                    this.Enabled = false;
                    LoadTopScorer();
                    this.Enabled = true;
                }
            }

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LoadPLFix();
            this.Enabled = true;
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LoadTable();
            this.Enabled = true;
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            LoadTopScorer();
           
        }
       void LoadTopScorer(){
           if (lvPlayer.Items.Count == 0)
            {
                try
                {
                    Scorer s = new Scorer();
                    s.LoadTopScorer(ref lvPlayer, "http://www.premierleague.com/en-gb/players.html");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Load failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
             }
       }
    }
}
