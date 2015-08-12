using System;
using System.Collections.Generic;
using System.Text;

namespace NextMatch
{
    class TimeZone
    {
        string _location;

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        double _time;

        public double Time
        {
            get { return _time; }
            set { _time = value; }
        }

        void Add(string loc, double t){
            this.Location=loc;
            this.Time=t;
        }
        public TimeZone(string loc, double t) {
            this.Location = loc;
            this.Time = t;
        }

        public TimeZone()
        {
            // TODO: Complete member initialization
        }
        void setList() {
            List<TimeZone> list = new List<TimeZone>();
            list.Add(new TimeZone("International Date Line West",-12));
            list.Add(new TimeZone("Coordinated Universal Time -11",-11));
            list.Add(new TimeZone("Samoa", -11));
            list.Add(new TimeZone("Hawaii", -10));
            list.Add(new TimeZone("Alaska", -9));
            list.Add(new TimeZone("Baja California", -8));
            list.Add(new TimeZone("Pacific Time (US & Canada)",-8 ));
            list.Add(new TimeZone("Arizona", -7));
            list.Add(new TimeZone("Chihuahua, La Paz, Mazatlan", -7));
            list.Add(new TimeZone("Mountain Time (US & Canada)", -7));
            list.Add(new TimeZone("Central America", -6));
            list.Add(new TimeZone("Central Time (US & Canada)", -6));
            list.Add(new TimeZone("Guadalajara, Mexico City, Monterrey",-6 ));
            list.Add(new TimeZone("Saskatchewan", -6));
            list.Add(new TimeZone("Bogota, Lima, Quito", -5));
            list.Add(new TimeZone("Eastern Time (US & Canada)", -5));
            list.Add(new TimeZone("Indiana (West)", -5));
            list.Add(new TimeZone("Caracas", -4.5));
            list.Add(new TimeZone("Asuncion",-4 ));
            list.Add(new TimeZone("Atlantic Time (Canada)", -4));
            list.Add(new TimeZone("Cuiaba",-4 ));
            list.Add(new TimeZone("Georgetown, La Paz, Manaus, San Juan",-4 ));
            list.Add(new TimeZone("Santiago", -4));
            list.Add(new TimeZone("Newfoundland", -3));
            list.Add(new TimeZone("Brasilia", -3));
            list.Add(new TimeZone("Buenos Aires",-3 ));
            list.Add(new TimeZone("Cayenne, Fortaleza", -3));
            list.Add(new TimeZone("Greenland",-3 ));
            list.Add(new TimeZone("Montevideo",-3 ));
            list.Add(new TimeZone("Coordinated Universal Time-02",-2 ));
            list.Add(new TimeZone("Mid-Atlantic", -2));
            ////list.Add(new TimeZone("Azores", ""));
            ////list.Add(new TimeZone("Cape Verde Is.", ""));
            ////list.Add(new TimeZone("Casablanca", ""));
            ////list.Add(new TimeZone("Coordinated Universal Time", ""));
            ////list.Add(new TimeZone("Dublin, Edinburgh, Lisbon, London", ""));
            ////list.Add(new TimeZone("Monrovia, Reykjavik", ""));
            ////list.Add(new TimeZone("Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", ""));
            ////list.Add(new TimeZone("Belgrade, Bratislava, Budapest, Ljubljana, Prague", ""));
            ////list.Add(new TimeZone("Brussels, Copenhagen, Madrid, Paris", ""));
            ////list.Add(new TimeZone("Sarajevo, Skopje, Warsaw, Zagreb", ""));
            ////list.Add(new TimeZone("West Central Africa", ""));
            ////list.Add(new TimeZone("Windhoek", ""));
            ////list.Add(new TimeZone("Amman", ""));
            ////list.Add(new TimeZone("Athens, Bucharest, Istanbul", ""));
            ////list.Add(new TimeZone("Beirut", ""));
            ////list.Add(new TimeZone("Cairo", ""));
            ////list.Add(new TimeZone("Damascus", ""));
            ////list.Add(new TimeZone("Harare, Pretoria", ""));
            ////list.Add(new TimeZone("Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius", ""));
            ////list.Add(new TimeZone("Jerusalem", ""));
            ////list.Add(new TimeZone("Minsk", ""));
            ////list.Add(new TimeZone("Baghdad", ""));
            ////list.Add(new TimeZone("Kuwait, Riyadh", ""));
            ////list.Add(new TimeZone("Moscow, St. Petersburg, Volgograd", ""));
            ////list.Add(new TimeZone("Nairobi", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            ////list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));
            //list.Add(new TimeZone("", ""));

            
        }
      
    }
    
}
