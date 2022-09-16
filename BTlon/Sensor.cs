using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTlon
{
    public class Sensor
    {

        public class SensorDatum
        {
            public string sensorName { get; set; }
            public string sensorUnit { get; set; }
            public string sensorMapKey { get; set; }
            public double sensorCalib { get; set; }
            public List<int> sensorData { get; set; }
        }
        public string CPUSerial { get; set; }
        public string AzureID { get; set; }
        public string AzureToken { get; set; }
        public string StationType { get; set; }
        public List<SensorDatum> SensorData { get; set; }
        public int? TimeOutPump { get; set; }
        public int? TimeOutFlush { get; set; }
    }
}
