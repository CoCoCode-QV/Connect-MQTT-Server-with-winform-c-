
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace BTlon
{
    public partial class MqttLoading:Form
    {
        public MqttClient client;
        public bool status = false;
        public List<dataMqtt> data = new List<dataMqtt>();
        public List<Sensor> sensor = new List<Sensor>();
        string m_broker = "mqttserver.tk";
        int m_port = 1883;
        string m_topic = "/tram_chim_monitoring/dong_thap/";
        string m_clientId = Guid.NewGuid().ToString();
        string m_username = "tram_chim_sub";
        string m_password = "TramChimMQTT...";
        List<string> AzureID = new List<string>();

        public MqttLoading()
        {

            Setup();
            InitializeComponent();
        }
        //
        public void Setup()
        {
            var json = new WebClient().DownloadString("https://ubc.sgp1.digitaloceanspaces.com/TramChimPark/Config/config.json");
            sensor = JsonConvert.DeserializeObject<List<Sensor>>(json);

            client = new MqttClient(m_broker, m_port, false, null, null, MqttSslProtocols.None);
            client.Connect(m_clientId, m_username, m_password);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            foreach (var item in sensor)
            {
                if (item.AzureID.Contains("water-sensor"))
                {
                    client.Subscribe(new string[] { m_topic + item.AzureID }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                }
            }
        }
        //
        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (byte qosLevel in e.GrantedQoSLevels)
            {
                sb.Append(qosLevel);
                sb.Append(" ");
            }
        }
        //
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = new string(Encoding.UTF8.GetChars(e.Message));
            data.Add(JsonConvert.DeserializeObject<dataMqtt>(message));
            
        }
        //loading
        int x = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            x++;
            if (x == 5)
            {
                pictureBox1.Hide();
                label1.Hide();
                client.Disconnect();
                this.Close();
            }
        }
    }
}

