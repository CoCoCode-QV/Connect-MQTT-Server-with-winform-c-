using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTlon
{
    public partial class MqttMain : Form
    {
        MqttLoading frmLoading = new MqttLoading();

        public MqttMain()
        {
            frmLoading.ShowDialog();
            InitializeComponent();
            addStation();

        }
        //
        private void addStation()
        {
            foreach (var item in frmLoading.data)
            {
                Button btn = new Button();
                string s = item.station_name;
                string name_Station = s.Substring(s.LastIndexOf('-') + 1);
                btn.Text = "Trạm " + $"{name_Station}";
                btn.Name = $"{item.station_name}";

                string pathToIcoFile = AppDomain.CurrentDomain.BaseDirectory + @"\Image\btn.png";
                btn.Image = System.Drawing.Image.FromFile(pathToIcoFile);
                btn.Size = new Size(130, 130);
                //handle time
                DateTime convert_Timestamp = ConvertDate(item.timestamp);
                ToolTip tool = new ToolTip();
                tool.ToolTipTitle = "Thông tin trạm";
                tool.SetToolTip(btn,"Tên trạm: " + $"{item.station_name}" + "\r\nUpdate: " + $"{convert_Timestamp}");
                tool.IsBalloon = true;
                //
                flowPanel.Controls.Add(btn);
                btn.Click += (sender, args) =>
                {
                    MqttShow frmData = new MqttShow(btn.Name);
                    this.Hide();
                    frmData.ShowDialog(); 
                    this.Close();
                };
            }
        }
        //
        private DateTime ConvertDate(double timestmp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timestmp).ToLocalTime();
            return dateTime;
        }  
        //
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Exit();
        }
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        //
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString(); 
        }
        //
        private void timer2_Tick(object sender, EventArgs e)
        {   
            double timesTampNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            foreach (var item in frmLoading.data)
            {
                double seconds = timesTampNow - item.timestamp;
                foreach (Control c in flowPanel.Controls)
                {
                    Button btn = (Button)c;
                    if (item.station_name == btn.Name)
                    {
                        if (seconds > 1800)
                        {
                            btn.Enabled = false;
                        }
                    }
                }
            }
        }
        //
    }
}