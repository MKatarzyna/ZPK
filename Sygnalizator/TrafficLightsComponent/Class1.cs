﻿using System;
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
        private int counterGreen;
        private int counterRed2, counterRed3, counterRed4;

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

        int height1of4;
        int height2of4;
        int height3of4;

        int restOfCycle4 = 0;
        int restOfCycle3 = 0;
        int restOfCycle2 = 0;

        FontFamily fontFamily;
        Font font;
        SolidBrush solidBrushGreenFont;
        SolidBrush solidBrushRedFont;

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
        SolidBrush backgroundOfCounter;

        Pen penLineBrightGreen = new Pen(Color.FromArgb(255, 0, 255, 0), 6); // brightGreen
        Pen penLineBrightYellow = new Pen(Color.FromArgb(255, 255, 255, 0), 6); // brightYellow
        Pen penLineBrightRed = new Pen(Color.FromArgb(255, 255, 0, 0), 6); // brightRed
        Pen penLineBlack = new Pen(Color.Black, 6); // Color Black

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

                Invalidate();
            }
        }

        [Category("Layout")]
        public bool IsArrowLightening
        {
            get
            {
                return isArrowLightening;
            }
            set
            {
                isArrowLightening = value;

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
            isArrowLightening = false;
            nonCollisionMode = false;
            queueNumber = 1;
            timeForRedLight = 3;
            timeForGreenLight = 5;

            CalculateAllTime();
            restartGreenRedCounters();

            autoTime = 1; // czas na żółte światła
            sizeOfComponent = 5;
            Size = new Size(sizeOfComponent * 20, sizeOfComponent * 40); // ustawianie rozmiaru  
            height1of4 = (int)(Size.Height / 4); // 1/4 komponentu przeznaczona na licznik
            height2of4 = (int)(Size.Height * 2 / 4);
            height3of4 = (int)(Size.Height * 3 / 4);
            chosenDirection = 0;
        }

        private void restartGreenRedCounters()
        {
            counterGreen = timeForGreenLight;
            
            counterRed2 = timeForRedLight + 2 * timeForAllStates;
            counterRed3 = timeForRedLight + 1 * timeForAllStates;
            counterRed4 = timeForRedLight;
        }

        // namalowanie niezapalonych świateł
        private void defaultStateOfLights(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerRedBrush, freeSpaceBar, freeSpaceBar + height1of4, widthOfCircle, heightOfCircle);
            g.FillEllipse(darkerYellowBrush, freeSpaceBar, height2of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
            g.FillEllipse(darkerGreenBrush, freeSpaceBar, height3of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo czerwonego swiatla
        private void turnOnRedLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterRedBrush, freeSpaceBar, freeSpaceBar + height1of4, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo czerwonego swiatla bezkolizyjnego ze strzalkami
        private void turnOnRedLightNonCollision(PaintEventArgs e, Graphics g)
        {
            penLineBlack.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka
            penLineBrightRed.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            // wygaszona zielona strzałka
            g.DrawLine(penLineBlack, freeSpaceBar, height3of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // wygaszona strzalka zolta
            g.DrawLine(penLineBlack, freeSpaceBar, height2of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // zapalona strzalka czerowna
            g.DrawLine(penLineBrightRed, freeSpaceBar, freeSpaceBar + heightOfCircle / 2 + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle / 2 + height1of4); // strzałka w lewo
            g.DrawLine(penLineBrightRed, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle + height1of4);    // strzałka prosto
        }

        // namalowanie wlączongo żółteg, czerwonego swiatla
        private void turnOnRedYellowLights(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterRedBrush, freeSpaceBar, freeSpaceBar + height1of4, widthOfCircle, heightOfCircle);
            g.FillEllipse(brighterYellowBrush, freeSpaceBar, height2of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo żółteg, czerwonego swiatla bezkolizyjnego ze strzalkami
        private void turnOnRedYellowLightsNonCollision(PaintEventArgs e, Graphics g)
        {
            penLineBlack.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka
            penLineBrightRed.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            penLineBrightYellow.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            // wygaszona zielona strzałka
            g.DrawLine(penLineBlack, freeSpaceBar, height3of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // wygaszona strzalka zolta
            g.DrawLine(penLineBrightYellow, freeSpaceBar, height2of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBrightYellow, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // zapalona strzalka czerowna
            g.DrawLine(penLineBrightRed, freeSpaceBar, freeSpaceBar + heightOfCircle / 2 + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle / 2 + height1of4); // strzałka w lewo
            g.DrawLine(penLineBrightRed, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle + height1of4);    // strzałka prosto
        }

        // namalowanie wlączongo zielonego swiatla
        private void turnOnGreenLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterGreenBrush, freeSpaceBar, height3of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo zoltego swiatla
        private void turnOnYellowLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(brighterYellowBrush, freeSpaceBar, height2of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
        }

        // namalowanie wlączongo zoltego swiatla bezkolizyjnego ze strzalkami
        private void turnOnYellowLightNonCollision(PaintEventArgs e, Graphics g)
        {
            penLineBlack.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka
            penLineBrightYellow.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            // wygaszona zielona strzałka
            g.DrawLine(penLineBlack, freeSpaceBar, height3of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // wygaszona strzalka zolta
            g.DrawLine(penLineBrightYellow, freeSpaceBar, height2of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBrightYellow, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // zapalona strzalka czerowna
            g.DrawLine(penLineBlack, freeSpaceBar, freeSpaceBar + heightOfCircle / 2 + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle / 2 + height1of4); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle + height1of4);    // strzałka prosto
        }

        // namalowanie włączonego strzalki i swiatla kierunkowego w prawo
        private void turnOnConditionalRightLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerGreenBrush, freeSpaceBar + Size.Width / 2, height3of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
            penLineBrightGreen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka
            g.DrawLine(penLineBrightGreen, freeSpaceBar + widthOfCircle + (int)(Size.Width / 2), height3of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + (int)(Size.Width / 2), height3of4 + freeSpaceBar + heightOfCircle / 2); // strzałka warunkowa w prawo
        }

        // namalowanie wylaczonego swiatla kierunkowego w prawo
        private void turnOffConditionalRightLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerGreenBrush, freeSpaceBar + Size.Width / 2, height3of4 + freeSpaceBar, widthOfCircle, heightOfCircle);

        }

        // namalowanie wlączonego swiatla bezkolizyjnego w lewo
        private void turnOnNonCollisionLeftLight(PaintEventArgs e, Graphics g)
        {
            g.FillEllipse(darkerGreenBrush, freeSpaceBar, height3of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
            penLineBrightGreen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka
            penLineBlack.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka

            // zapalone zielone strzałki
            g.DrawLine(penLineBrightGreen, freeSpaceBar, height3of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBrightGreen, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // wygaszona strzalka zolta
            g.DrawLine(penLineBlack, freeSpaceBar, height2of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
            // wygaszona strzalka czerowna
            g.DrawLine(penLineBlack, freeSpaceBar, freeSpaceBar + heightOfCircle / 2 + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle / 2 + height1of4); // strzałka w lewo
            g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle + height1of4);    // strzałka prosto
        }

        // cykliczne odmalowanie swiatel i namalowanie pierwszy raz
        protected override void OnPaint(PaintEventArgs e)
        {
            Width = sizeOfComponent * 20;
            Height = sizeOfComponent * 40;
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
            backgroundOfCounter = new SolidBrush(Color.FromArgb(255, 90, 90, 90));

            // glowny czarny prostokat komponentu
            g.FillRectangle(backgroundOfComponent, 0, 0, (int)(Size.Width / 2), (int)Size.Height);
            g.FillRectangle(backgroundOfCounter, freeSpaceBar, freeSpaceBar, (int)(Size.Width / 2 - freeSpaceBar * 2), (int)(height1of4 - freeSpaceBar * 2));

            defaultStateOfLights(e, g);

            if (isArrowLightening == true)
            {
                // wyrysowanie bocznego kwadratu komponentu do strzałki warunkowej
                g.FillRectangle(backgroundOfComponent, (int)(Size.Width / 2), height3of4, (int)(Size.Width / 2), height1of4);
                g.FillEllipse(darkerGreenBrush, freeSpaceBar + Size.Width / 2, height3of4 + freeSpaceBar, widthOfCircle, heightOfCircle);
            }

            if (nonCollisionMode == true)
            {
                penLineBlack.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // jaki rodzaj linii -> strzałka

                // zgaszone zielone strzałki
                g.DrawLine(penLineBlack, freeSpaceBar, height3of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
                g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height3of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
                // zgaszone zolte strzałki                                                                                                                                                               // wygaszona strzalka zolta
                g.DrawLine(penLineBlack, freeSpaceBar, height2of4 + freeSpaceBar + heightOfCircle / 2, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle / 2); // strzałka w lewo
                g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar, freeSpaceBar + widthOfCircle / 2, height2of4 + freeSpaceBar + heightOfCircle);    // strzałka prosto
                // zgaszone czerwone strzałki                                                                                                                                                                        // wygaszona strzalka czerowna
                g.DrawLine(penLineBlack, freeSpaceBar, freeSpaceBar + heightOfCircle / 2 + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle / 2 + height1of4); // strzałka w lewo
                g.DrawLine(penLineBlack, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + height1of4, freeSpaceBar + widthOfCircle / 2, freeSpaceBar + heightOfCircle + height1of4);    // strzałka prosto

            }

            

            //e.Graphics.DrawString("00", this.Font, Brushes.Black, 0, 0);
            fontFamily = new FontFamily("Times New Roman");
            font = new Font(fontFamily, 32, FontStyle.Regular, GraphicsUnit.Pixel);
            solidBrushGreenFont = new SolidBrush(Color.FromArgb(255, 0, 255, 0));
            solidBrushRedFont = new SolidBrush(Color.FromArgb(255, 255, 0, 0));


            if (chosenDirection == 0)
            {
                defaultStateOfLights(e, g);
            }
            else if (chosenDirection == 1)
            {
                turnOnRedLight(e, g);
                int temp = 0;
                if (queueNumber == 1) temp = counter - (timeForAllStates * queueNumber - timeForRedLight);
                if (queueNumber == 2) temp = counter - (timeForAllStates * queueNumber - timeForRedLight) + restOfCycle2;
                e.Graphics.DrawString(temp.ToString(), font, solidBrushRedFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 2)
            {
                turnOnRedYellowLights(e, g);
                e.Graphics.DrawString(" ", font, solidBrushGreenFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 3)
            {
                turnOnGreenLight(e, g);
                e.Graphics.DrawString(counterGreen.ToString(), font, solidBrushGreenFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 4)
            {
                turnOnYellowLight(e, g);
                e.Graphics.DrawString(" ", font, solidBrushGreenFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 5)
            {
                turnOnConditionalRightLight(e, g); // work, move to standard red with IF condition for boolean flag set by counter
            }
            else if (chosenDirection == 60)
            {
                turnOnNonCollisionLeftLight(e, g);
                e.Graphics.DrawString(counterGreen.ToString(), font, solidBrushGreenFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 10)
            {
                turnOnRedLightNonCollision(e, g);
                int temp = 0;
                if (queueNumber == 1) temp = counter - (timeForAllStates * queueNumber - timeForRedLight);
                if (queueNumber == 2) temp = counter - (timeForAllStates * queueNumber - timeForRedLight) + restOfCycle2;
                if (queueNumber == 3) temp = counter - (timeForAllStates * queueNumber - timeForRedLight) + restOfCycle3;
                if (queueNumber == 4) temp = counter - (timeForAllStates * queueNumber - timeForRedLight) + restOfCycle4;
                e.Graphics.DrawString(temp.ToString(), font, solidBrushRedFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 20)
            {
                turnOnRedYellowLightsNonCollision(e, g);
                e.Graphics.DrawString(" ", font, solidBrushGreenFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
            else if (chosenDirection == 40)
            {
                turnOnYellowLightNonCollision(e, g);
                e.Graphics.DrawString(" ", font, solidBrushGreenFont, new PointF(0 + freeSpaceBar, 0 + freeSpaceBar));
            }
        }

        private void CalculateAllTime()
        {
            timeForAllStates = timeForRedLight + timeForGreenLight + autoTime + autoTime;
        }

        //liczniki ustalenia czasu (dlugosci cyklu) dla trybu standard i non-collision
        public void  StartCounter()
        {
            CalculateAllTime();

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

        //zatrzymanie sygnalizacji swietlnej poprzez zatrzymanie timera
        public void StopCounter()
        {
            timer.Stop();
            restOfCycle4 = 0;
            restOfCycle3 = 0;
            restOfCycle2 = 0;
        }

        // odliczanie co jedna sekunde, ze sprawdzeniem jaki tryb i nastepnie włączenie świateł dla odpowiedniego komponentu

        private void timerTick(object sender, EventArgs e)
        {
            counter--;
            //Console.WriteLine("Q: "+queueNumber+", count: " + counter);

            if (nonCollisionMode == false)
            {
                if (counter == 0)
                {
                    counter = timeForAllStates * 2;
                    restOfCycle2 = 0;
                } else
                {
                    if (counter == timeForAllStates * 2 - timeForAllStates * 2 / 2)
                    {
                        // do 2 dodaj pelne 2 cykle
                        restOfCycle2 = timeForAllStates * 2;
                    }
                    

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
                    restOfCycle4 = 0;
                    restOfCycle3 = 0;
                    restOfCycle2 = 0;
                }
                else
                {
                    if (counter == timeForAllStates * 4 - timeForAllStates * 4 / 4)
                    {
                        // do 4 dodaj pelne 4 cykle
                       restOfCycle4 = timeForAllStates * 4;
                    } else if (counter == timeForAllStates * 4 / 2)
                    {
                        // do 3 dodaj 4 cykle
                        restOfCycle3 = timeForAllStates * 4;
                    } else if (counter == timeForAllStates * 4 / 4)
                    {
                        // do 2 dodaj 4 cykle
                        restOfCycle2 = timeForAllStates * 4;
                    }

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
                Console.WriteLine("RED: " + counter);
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime))
            {
                chosenDirection = 2; // RED YELLOW
                counterGreen = timeForGreenLight;
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight))
            {
                chosenDirection = 3; // GREEN
                Console.WriteLine("GREEN: " + counter);
                decreaseGreenCounter();
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 4; // YELLOW
                counterGreen = timeForGreenLight;
            }
            else if (counter < (timeForAllStates* queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 1; // RED
                Console.WriteLine("RED: " + counter);
            }

            Invalidate();
        }

        private void switchingNonCollisionLights()
        {
            if (counter > (timeForAllStates * queueNumber - timeForRedLight))
            {
                chosenDirection = 10; // RED
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime))
            {
                chosenDirection = 20; // RED YELLOW
                counterGreen = timeForGreenLight;
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight))
            {
                chosenDirection = 60; // GREEN
                decreaseGreenCounter();
            }
            else if (counter > (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 40; // YELLOW
                counterGreen = timeForGreenLight;
            }
            else if (counter < (timeForAllStates * queueNumber - timeForRedLight - autoTime - timeForGreenLight - autoTime))
            {
                chosenDirection = 10; // RED
            }
            Invalidate();
        }

        private void decreaseGreenCounter()
        {
            counterGreen--;
        } 
    }
}
