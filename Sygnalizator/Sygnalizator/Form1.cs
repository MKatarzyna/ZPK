using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sygnalizator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            trafficLightsComponent1.StartCounter();
            trafficLightsComponent2.StartCounter();
            trafficLightsComponent3.StartCounter();
            trafficLightsComponent4.StartCounter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trafficLightsComponent1.NonCollisionMode = true;
            trafficLightsComponent2.NonCollisionMode = true;
            trafficLightsComponent3.NonCollisionMode = true;
            trafficLightsComponent4.NonCollisionMode = true;

            trafficLightsComponent4.QueueNumber = 1;
            trafficLightsComponent3.QueueNumber = 2;
            trafficLightsComponent2.QueueNumber = 3;
            trafficLightsComponent1.QueueNumber = 4;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trafficLightsComponent1.NonCollisionMode = false;
            trafficLightsComponent2.NonCollisionMode = false;
            trafficLightsComponent3.NonCollisionMode = false;
            trafficLightsComponent4.NonCollisionMode = false;

            trafficLightsComponent4.QueueNumber = 1;
            trafficLightsComponent3.QueueNumber = 2;
            trafficLightsComponent2.QueueNumber = 1;
            trafficLightsComponent1.QueueNumber = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trafficLightsComponent1.StopCounter();
            trafficLightsComponent2.StopCounter();
            trafficLightsComponent3.StopCounter();
            trafficLightsComponent4.StopCounter();
        }
    }
}
