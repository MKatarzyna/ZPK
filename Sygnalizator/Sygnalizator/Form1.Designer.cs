namespace Sygnalizator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.trafficLightsComponent4 = new TrafficLightsComponent.TrafficLightsComponent();
            this.trafficLightsComponent3 = new TrafficLightsComponent.TrafficLightsComponent();
            this.trafficLightsComponent2 = new TrafficLightsComponent.TrafficLightsComponent();
            this.trafficLightsComponent1 = new TrafficLightsComponent.TrafficLightsComponent();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(453, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trafficLightsComponent4
            // 
            this.trafficLightsComponent4.Location = new System.Drawing.Point(346, 175);
            this.trafficLightsComponent4.Name = "trafficLightsComponent4";
            this.trafficLightsComponent4.NonCollisionMode = false;
            this.trafficLightsComponent4.QueueNumber = 1;
            this.trafficLightsComponent4.Size = new System.Drawing.Size(100, 150);
            this.trafficLightsComponent4.SizeOfComponent = 5;
            this.trafficLightsComponent4.TabIndex = 5;
            this.trafficLightsComponent4.TimeForGreenLight = 5;
            this.trafficLightsComponent4.TimeForRedLight = 3;
            // 
            // trafficLightsComponent3
            // 
            this.trafficLightsComponent3.Location = new System.Drawing.Point(221, 282);
            this.trafficLightsComponent3.Name = "trafficLightsComponent3";
            this.trafficLightsComponent3.NonCollisionMode = false;
            this.trafficLightsComponent3.QueueNumber = 1;
            this.trafficLightsComponent3.Size = new System.Drawing.Size(100, 150);
            this.trafficLightsComponent3.SizeOfComponent = 5;
            this.trafficLightsComponent3.TabIndex = 4;
            this.trafficLightsComponent3.TimeForGreenLight = 5;
            this.trafficLightsComponent3.TimeForRedLight = 3;
            // 
            // trafficLightsComponent2
            // 
            this.trafficLightsComponent2.Location = new System.Drawing.Point(55, 159);
            this.trafficLightsComponent2.Name = "trafficLightsComponent2";
            this.trafficLightsComponent2.NonCollisionMode = true;
            this.trafficLightsComponent2.QueueNumber = 2;
            this.trafficLightsComponent2.Size = new System.Drawing.Size(100, 150);
            this.trafficLightsComponent2.SizeOfComponent = 5;
            this.trafficLightsComponent2.TabIndex = 3;
            this.trafficLightsComponent2.TimeForGreenLight = 5;
            this.trafficLightsComponent2.TimeForRedLight = 3;
            // 
            // trafficLightsComponent1
            // 
            this.trafficLightsComponent1.Location = new System.Drawing.Point(221, 45);
            this.trafficLightsComponent1.Name = "trafficLightsComponent1";
            this.trafficLightsComponent1.NonCollisionMode = true;
            this.trafficLightsComponent1.QueueNumber = 1;
            this.trafficLightsComponent1.Size = new System.Drawing.Size(100, 150);
            this.trafficLightsComponent1.SizeOfComponent = 5;
            this.trafficLightsComponent1.TabIndex = 2;
            this.trafficLightsComponent1.TimeForGreenLight = 5;
            this.trafficLightsComponent1.TimeForRedLight = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 477);
            this.Controls.Add(this.trafficLightsComponent4);
            this.Controls.Add(this.trafficLightsComponent3);
            this.Controls.Add(this.trafficLightsComponent2);
            this.Controls.Add(this.trafficLightsComponent1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private TrafficLightsComponent.TrafficLightsComponent trafficLightsComponent1;
        private TrafficLightsComponent.TrafficLightsComponent trafficLightsComponent2;
        private TrafficLightsComponent.TrafficLightsComponent trafficLightsComponent3;
        private TrafficLightsComponent.TrafficLightsComponent trafficLightsComponent4;
    }
}

