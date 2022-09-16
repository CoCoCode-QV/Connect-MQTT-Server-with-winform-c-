using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTlon
{
    public class dataMqtt
    {
    
        public class DataSensor
        {
            public string sensor_name { get; set; }
            public string sensor_key { get; set; }
            public string sensor_unit { get; set; }
            public string sensor_value { get; set; }
        }

        
       

        public string project_id { get; set; }
        public string project_name { get; set; }
        public string station_id { get; set; }
        public string station_name { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public double volt_battery { get; set; }
        public double volt_solar { get; set; }
        public List<DataSensor> data_sensor { get; set; }
        public int timestamp { get; set; }
        //private int Timestamp;
        //public int timestamp {
        //    get => Timestamp;
        //    set
        //    {
        //        Timestamp = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("timestamp"));
        //        }
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
