using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTlon
{
    public partial class MqttShow : Form
    {
        MqttMain frmMain = new MqttMain();
        MqttLoading frmLoading = new MqttLoading();
        string m_NameStation;
        public MqttShow(string btnName)
        {
            m_NameStation = btnName;
            InitializeComponent();
        }
        //
        private void showDataStation()
        {
            foreach (var item in frmLoading.data)
            {
                if (item.station_name == m_NameStation)
                {
                    string s = item.station_name;
                    string name_Station = s.Substring(s.LastIndexOf('-') + 1);
                    lbNameStation.Text = "Trạm " + $"{name_Station}";
                    foreach (var value in item.data_sensor)
                    {
                        switch (value.sensor_name)
                        {
                            case "TDS":
                                {
                                    lbTDSValue.Text = value.sensor_value;
                                    break;
                                }
                            case "Mực Nước":
                                {
                                    lbMucNuocValue.Text = value.sensor_value;
                                    break;
                                }
                            case "Độ Dẫn Điện":
                                {
                                    lbDoDanDienValue.Text = value.sensor_value;
                                    break;
                                }
                            case "Oxy":
                                {
                                    lbOxyValue.Text = value.sensor_value;
                                    break;
                                }
                            case "pH":
                                {
                                    lbPHValue.Text = value.sensor_value;
                                    break;

                                }
                            case "Nhiệt Độ":
                                {
                                    lbNhietDoValue.Text = value.sensor_value;
                                    break;
                                }
                        }
                    }
                }
            }
        }
        //
        private void MqttShow_Load(object sender, EventArgs e)
        {
            showDataStation();
            timer1.Start();
        }
        //
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbRealTime.Text = DateTime.Now.ToString();

        }
        //
        private void picBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();
             frmMain.ShowDialog();
            this.Close();
        }

    }
}
