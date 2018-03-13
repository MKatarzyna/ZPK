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
            button1.Enabled = false;
            button4.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            trafficLightsComponent1.StartCounter();
            trafficLightsComponent2.StartCounter();
            trafficLightsComponent3.StartCounter();
            trafficLightsComponent4.StartCounter();
            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trafficLightsComponent4.RefreshUI();
            trafficLightsComponent3.RefreshUI();
            trafficLightsComponent2.RefreshUI();
            trafficLightsComponent1.RefreshUI();

            trafficLightsComponent1.NonCollisionMode = true;
            trafficLightsComponent2.NonCollisionMode = true;
            trafficLightsComponent3.NonCollisionMode = true;
            trafficLightsComponent4.NonCollisionMode = true;

            trafficLightsComponent1.IsArrowLightening = false;
            trafficLightsComponent2.IsArrowLightening = false;
            trafficLightsComponent3.IsArrowLightening = false;
            trafficLightsComponent4.IsArrowLightening = false;

            trafficLightsComponent4.QueueNumber = 1;
            trafficLightsComponent3.QueueNumber = 2;
            trafficLightsComponent2.QueueNumber = 3;
            trafficLightsComponent1.QueueNumber = 4;

            button1.Enabled = true;
            button4.Enabled = true; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trafficLightsComponent4.RefreshUI();
            trafficLightsComponent3.RefreshUI();
            trafficLightsComponent2.RefreshUI();
            trafficLightsComponent1.RefreshUI();

            trafficLightsComponent1.NonCollisionMode = false;
            trafficLightsComponent2.NonCollisionMode = false;
            trafficLightsComponent3.NonCollisionMode = false;
            trafficLightsComponent4.NonCollisionMode = false;

            trafficLightsComponent1.IsArrowLightening = false;
            trafficLightsComponent2.IsArrowLightening = false;
            trafficLightsComponent3.IsArrowLightening = false;
            trafficLightsComponent4.IsArrowLightening = false;

            trafficLightsComponent4.QueueNumber = 1;
            trafficLightsComponent3.QueueNumber = 2;
            trafficLightsComponent2.QueueNumber = 1;
            trafficLightsComponent1.QueueNumber = 2;

            button1.Enabled = true;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trafficLightsComponent1.StopCounter();
            trafficLightsComponent2.StopCounter();
            trafficLightsComponent3.StopCounter();
            trafficLightsComponent4.StopCounter();
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            trafficLightsComponent4.RefreshUI();
            trafficLightsComponent3.RefreshUI();
            trafficLightsComponent2.RefreshUI();
            trafficLightsComponent1.RefreshUI();

            trafficLightsComponent1.NonCollisionMode = false;
            trafficLightsComponent2.NonCollisionMode = false;
            trafficLightsComponent3.NonCollisionMode = false;
            trafficLightsComponent4.NonCollisionMode = false;

            trafficLightsComponent1.IsArrowLightening = true;
            trafficLightsComponent2.IsArrowLightening = true;
            trafficLightsComponent3.IsArrowLightening = true;
            trafficLightsComponent4.IsArrowLightening = true;

            trafficLightsComponent4.QueueNumber = 1;
            trafficLightsComponent3.QueueNumber = 2;
            trafficLightsComponent2.QueueNumber = 1;
            trafficLightsComponent1.QueueNumber = 2;

            button1.Enabled = true;
            button4.Enabled = true;


        }
    }
}
