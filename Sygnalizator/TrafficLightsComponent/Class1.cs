using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace TrafficLightsComponent
{
    public class TrafficLightsComponent : UserControl
    {
        private System.Windows.Forms.Timer timer;
        private bool isArrowLightening;
        private int counter;
        private int timeForAllStates;
        private int timeForRedLight;
        private int timeForGreenLight;
        private int autoTime;

        private bool nonCollisionMode;
        private int queueNumber; // numer w kolejce
        
        private int chosenDirection;

        private int sizeOfComponent;
        int widthOfCircle;
        int heightOfCircle;
        int freeSpaceBar;

        Color brighterGreenColor = Color.FromArgb(255, 0, 255, 0);
        Color darkerGreenColor = Color.FromArgb(255, 0, 102, 0);
        Color brighterYellowColor = Color.FromArgb(255, 255, 255, 0);
        Color darkerYellowColor = Color.FromArgb(255, 102, 102, 0);
        Color brighterRedColor = Color.FromArgb(255, 255, 0, 0);
        Color darkerRedColor = Color.FromArgb(255, 102, 0, 0);

        SolidBrush brighterGreenBrush;
        SolidBrush darkerGreenBrush;
        SolidBrush brighterYellowBrush;
        SolidBrush darkerYellowBrush;
        SolidBrush brighterRedBrush;
        SolidBrush darkerRedBrush;
        SolidBrush backgroundOfComponent;

        Pen penLine = new Pen(Color.FromArgb(255, 0, 255, 0), 6); // Color.Black

        // dodanie do panelu przełącznika trybu standard i niekolizyjnego
        [Category("Layout")]
        public bool NonCollisionMode
        {
            get
            {
                return nonCollisionMode;
            }
            set
            {
                nonCollisionMode = value;

                if(nonCollisionMode == false)
                {
                    isArrowLightening = true;
                }else
                {
                    isArrowLightening = false;
                }

                Invalidate();
            }
        }

        [Category("Layout")] //dodanie do panelu czasu na czerwone światło
        public int TimeForRedLight
        {
            get
            {
                return timeForRedLight;
            }
            set
            {
                timeForRedLight = value;
                Invalidate();
            }
        }

        [Category("Layout")] //dodanie do panelu czasu na zielone światło
        public int TimeForGreenLight
        {
            get
            {
                return timeForGreenLight;
            }
            set
            {
                timeForGreenLight = value;
                Invalidate();
            }
        }

        [Category("Layout")] //dodanie do panelu proporcji rozmiaru komponentu
        public int SizeOfComponent
        {
            get
            {
                return sizeOfComponent;
            }
            set
            {
                sizeOfComponent = value;
                Size = new Size(sizeOfComponent * 10, sizeOfComponent * 30);
                Invalidate();
            }
        }

        [Category("Layout")] //dodanie do panelu kolejnosci dzialania świateł. Standard=1,1,2,2, Niekolizjny1,2,3,4. Zaczyna dzialac od 4
        public int QueueNumber
        {
            get
            {
                return queueNumber;
            }
            set
            {
                queueNumber = value;
                Invalidate();
            }
        }

        public TrafficLightsComponent() // kontruktor bezparametrów. Ustawianie zmiennych
        {
            isArrowLightening = true;
            nonCollisionMode = false;
            queueNumber = 1;
            timeForRedLight = 3;
            timeForGreenLight = 5;
            autoTime = 1; // czas na żółte światła
            sizeOfComponent = 5;
            Size = new Size(sizeOfComponent * 20, sizeOfComponent * 30); // ustawianie rozmiaru     
            chosenDirection = 0;
        }

        // namalowanie niezapalonych świateł
        private void defaultStateOfLights(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerRedBrush, freeSpaceBar, freeSpaceBar, widthOfCircle, heightOfCircle);
            g.FillEllipse(darkerYellowBrush, freeSpaceBar, ((int)(Size.Height * 1/3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
            g.FillEllipse(darkerGreenBrush, freeSpaceBar, ((int)(Size.Height * 2/3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo czerwonego swiatla
        private void turnOnRedLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterRedBrush, freeSpaceBar, freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo żółteg, czerwonego swiatla
        private void turnOnRedYellowLights(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterRedBrush, freeSpaceBar, freeSpaceBar, widthOfCircle, heightOfCircle);
            g.FillEllipse(brighterYellowBrush, freeSpaceBar, ((int)(Size.Height * 1/3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo zielonego swiatla
        private void turnOnGreenLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterGreenBrush, freeSpaceBar, ((int)(Size.Height * 2/3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo zoltego swiatla
        private void turnOnYellowLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterYellowBrush, freeSpaceBar, ((int)(Size.Height * 1/3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie włączonego strzalki i swiatla kierunkowego w prawo
        private void turnOnConditionalRightLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerGreenBrush, freeSpaceBar + Size.Width / 2, ((int)(Size.Height * 2 / 3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
            penLine.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka
            g.DrawLine(penLine, freeSpaceBar + widthOfCircle + (int)(Size.Width / 2), ((int)(Size.Height * 2 / 3)) + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + (int)(Size.Width / 2), ((int)(Size.Height * 2 / 3)) + freeSpaceBar + heightOfCircle / 2); // strzałka warunkowa w prawo
        }

        // namalowanie wylaczonego swiatla kierunkowego w prawo
        private void turnOffConditionalRightLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerGreenBrush, freeSpaceBar + Size.Width / 2, ((int)(Size.Height * 2 / 3)) + freeSpaceBar, widthOfCircle, heightOfCircle);

        }

        // namalowanie wlączonego swiatla bezkolizyjnego w lewo
        private void turnOnNonCollisionLeftLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerGreenBrush, freeSpaceBar, ((int)(Size.Height * 2/3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
            penLine.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka

            g.DrawLine(penLine, freeSpaceBar, ((int)(Size.Height * 2/3)) + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, ((int)(Size.Height * 2/3)) + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLine, freeSpaceBar + widthOfCircle / 2, ((int)(Size.Height * 2/3)) + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, ((int)(Size.Height * 2/3)) + freeSpaceBar + heightOfCircle);    // strzałka prosto
        }

        // cykliczne odmalowanie swiatel i namalowanie pierwszy raz
        protected override void OnPaint(PaintEventArgs e)
        {
            Width = sizeOfComponent * 20;
            Height = sizeOfComponent * 30;
            Graphics g = e.Graphics;
            widthOfCircle = (int)((Size.Width  / 2) * 0.8);
            heightOfCircle = widthOfCircle;
            freeSpaceBar = (int)((Size.Width / 2) * 0.1);

            brighterGreenBrush = new SolidBrush(brighterGreenColor);
            darkerGreenBrush = new SolidBrush(darkerGreenColor);
            brighterYellowBrush = new SolidBrush(brighterYellowColor);
            darkerYellowBrush = new SolidBrush(darkerYellowColor);
            brighterRedBrush = new SolidBrush(brighterRedColor);
            darkerRedBrush = new SolidBrush(darkerRedColor);
            backgroundOfComponent = new SolidBrush(Color.FromArgb(255, 64, 64, 64));
            
            // glowny czarny prostokat komponentu
            g.FillRectangle(backgroundOfComponent, 0, 0, (int)(Size.Width / 2), (int)Size.Height);

            if (nonCollisionMode == false)
            {
                // wyrysowanie bocznego kwadratu komponentu do strzałki warunkowej
                g.FillRectangle(backgroundOfComponent, (int)(Size.Width / 2), (int)((Size.Height / 3) * 2), (int)(Size.Width / 2), (int)(Size.Height / 3));
                g.FillEllipse(darkerGreenBrush, freeSpaceBar + Size.Width / 2, ((int)(Size.Height * 2 / 3)) + freeSpaceBar, widthOfCircle, heightOfCircle);
            }

           
            defaultStateOfLights(e, g);

            if (chosenDirection == 0)
            {
                defaultStateOfLights(e, g);
            }
            else if (chosenDirection == 1)
            {
                turnOnRedLight(e, g);
            }
            else if (chosenDirection == 2)
            {
                turnOnRedYellowLights(e, g);
            }
            else if (chosenDirection == 3)
            {
                turnOnGreenLight(e, g);
            }
            else if (chosenDirection == 4)
            {
                turnOnYellowLight(e, g);
            }
            else if (chosenDirection == 5)
            {
                turnOnConditionalRightLight(e, g);
            }
            else if (chosenDirection == 6)
            {
                turnOnNonCollisionLeftLight(e, g);
            }

        }

        //liczniki ustalenia czasu (dlugosci cyklu) dla trybu standard i non-collision
        public void  StartCounter()
        {
            timeForAllStates = timeForRedLight + timeForGreenLight + autoTime + autoTime;
            
           if (nonCollisionMode == false)
             {
                counter = timeForAllStates * 2;
             }
           else if (nonCollisionMode == true)
             {
                counter = timeForAllStates * 4;
            }
    
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = 1000; // 1 sekunda
            timer.Start();
        }

        // odliczanie co jedna sekunde, ze sprawdzeniem jaki tryb i nastepnie włączenie świateł dla odpowiedniego komponentu

        private void timerTick(object sender, EventArgs e)
        {
            counter--;
            Console.WriteLine("Q: "+queueNumber+", count: " + counter);

            if (nonCollisionMode == false)
            {
                if (counter == 0)
                {
                    counter = timeForAllStates * 2;
                } else
                {
                    if (queueNumber == 1)
                    {
                        switchingLights();
                    }
                    else if (queueNumber == 2)
                    {
                        switchingLights();
                    }
                }
               
            }
            else if (nonCollisionMode == true)
            {
                if (counter == 0)
                {
                    counter = timeForAllStates * 4;
                }
                else
                {
                    if (queueNumber == 1)
                    {
                        switchingNonCollisionLights();
                    }
                    else if (queueNumber == 2)
                    {
                        switchingNonCollisionLights();
                    }
                    else if (queueNumber == 3)
                    {
                        switchingNonCollisionLights();
                    }
                    else if (queueNumber == 4)
                    {
                        switchingNonCollisionLights();
                    }
                }   
            }
       }

        private void switchingLights()
        {
            if (counter > (timeForAllStates * queueNumber - timeForRedLight))
            {
                chosenDirection = 1; // RED
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime))
            {
                chosenDirection = 2; // RED YELLOW
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight))
            {
                chosenDirection = 3; // GREEN
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 4; // YELLOW
            }
            else if (counter < (timeForAllStates* queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 1; // RED
            }

            Invalidate();
        }

        private void switchingNonCollisionLights()
        {
            if (counter > (timeForAllStates * queueNumber - timeForRedLight))
            {
                chosenDirection = 1; // RED
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime))
            {
                chosenDirection = 2; // RED YELLOW
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight))
            {
                chosenDirection = 6; // GREEN
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 4; // YELLOW
            }
            else if (counter < (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 1; // RED
            }
            Invalidate();
        }
    }
}
