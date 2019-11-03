using System;
using System.Collections.Generic;
using System.Text;

namespace Navigation
{
    public class ipMAC
    {
        public string _ip;
        public string _mac;

        public ipMAC()
        {
            _ip = "0.0.0.0";
            _mac = "00:00:00:00:00:00";
        }

        public ipMAC( string ip, string mac)
        {
            _ip = ip;
            _mac = mac;
        }

        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        public string MAC
        {
            get { return _mac; }
            set { _mac = value; }
        }





    }
}
