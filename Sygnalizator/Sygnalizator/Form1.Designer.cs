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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.trafficLightsComponent4 = new TrafficLightsComponent.TrafficLightsComponent();
            this.trafficLightsComponent3 = new TrafficLightsComponent.TrafficLightsComponent();
            this.trafficLightsComponent2 = new TrafficLightsComponent.TrafficLightsComponent();
            this.trafficLightsComponent1 = new TrafficLightsComponent.TrafficLightsComponent();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(447, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "START";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Non Collision";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(447, 175);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Standard";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(447, 86);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "STOP";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // trafficLightsComponent4
            // 
            this.trafficLightsComponent4.Location = new System.Drawing.Point(341, 187);
            this.trafficLightsComponent4.Name = "trafficLightsComponent4";
            this.trafficLightsComponent4.NonCollisionMode = false;
            this.trafficLightsComponent4.QueueNumber = 1;
            this.trafficLightsComponent4.Size = new System.Drawing.Size(100, 200);
            this.trafficLightsComponent4.SizeOfComponent = 5;
            this.trafficLightsComponent4.TabIndex = 9;
            this.trafficLightsComponent4.TimeForGreenLight = 5;
            this.trafficLightsComponent4.TimeForRedLight = 3;
            // 
            // trafficLightsComponent3
            // 
            this.trafficLightsComponent3.Location = new System.Drawing.Point(221, 329);
            this.trafficLightsComponent3.Name = "trafficLightsComponent3";
            this.trafficLightsComponent3.NonCollisionMode = true;
            this.trafficLightsComponent3.QueueNumber = 2;
            this.trafficLightsComponent3.Size = new System.Drawing.Size(100, 200);
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
            this.trafficLightsComponent2.QueueNumber = 3;
            this.trafficLightsComponent2.Size = new System.Drawing.Size(100, 200);
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
            this.trafficLightsComponent1.QueueNumber = 4;
            this.trafficLightsComponent1.Size = new System.Drawing.Size(100, 200);
            this.trafficLightsComponent1.SizeOfComponent = 5;
            this.trafficLightsComponent1.TabIndex = 2;
            this.trafficLightsComponent1.TimeForGreenLight = 5;
            this.trafficLightsComponent1.TimeForRedLight = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 570);
            this.Controls.Add(this.trafficLightsComponent4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private TrafficLightsComponent.TrafficLightsComponent trafficLightsComponent4;
    }
}

