using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace OS
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
           // textBox3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Refresh();
            SolidBrush sb = new SolidBrush(Color.SteelBlue);
            Pen pen = new Pen(Color.WhiteSmoke, 2);
            Graphics g = panel2.CreateGraphics();
            Graphics b = panel2.CreateGraphics();
            Graphics c = panel2.CreateGraphics();
            FontFamily ff = new FontFamily("Century Gothic");
            System.Drawing.Font font = new System.Drawing.Font(ff, 10 , FontStyle.Bold);
            if (textBox5.Text == "ex: 4")
            {
                MessageBox.Show("Please enter number of processes");
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please choose a schedular");
                return;
            }
            int n;
            try
            {
                 n = int.Parse(textBox5.Text);
            }
            catch
            {
                MessageBox.Show("Error");
                return;
            }
            
            int[] proccess = new int[n];
            double[] arrival= new double[n];
            double[] burst = new double[n];
            double[] arrivalmtrtb = new double[n];
            double[] burstmtrtb = new double[n];
            double AT=0;
            double TAT = 0;
            double totaltime = 0;
            if (textBox1.Text == "ex: 1 2 3 4")
                MessageBox.Show("Please enter arrival time");
            if (textBox2.Text == "ex: 1 2 3 4")
                MessageBox.Show("Please enter burst time");
            string fromarival = textBox1.Text;
            int needleCount = fromarival.Length - fromarival.Replace(" ", "").Length + 1;
            if (needleCount != n)
            {
                MessageBox.Show("Please enter arrival time equal to No. of processes");
                return;
            }
            string[] arrivalarray=fromarival.Split(' ');
            string fromburst = textBox2.Text;
            needleCount = fromburst.Length - fromburst.Replace(" ", "").Length + 1;
            if (needleCount != n)
            {
                MessageBox.Show("Please enter burst time equal to No. of processes");
                return;
            }
            string[] burstarray = fromburst.Split(' ');

            try
            {
                for (int i = 0; i < n; i++)
                {
                    arrival[i] = double.Parse(arrivalarray[i]);
                    burst[i] = double.Parse(burstarray[i]);
                    proccess[i] = i + 1;
                    if (arrival[i] > totaltime)
                        totaltime += (arrival[i] - totaltime);
                    totaltime += burst[i];
                    // AT += arrival[i];//de 7sba msh s7
                    // TAT += burst[i];//de 7sba msh s7
                }
            }
            catch
            {
                MessageBox.Show("Error");
                return;
            }
                
            double temp = 0;
            int temp2;
            double currenttime = 0;
            try
            {
                if ((comboBox1.Text != "Priority - Non-preemptive") && (comboBox1.Text != "Priority - Preemptive"))
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = i + 1; j < n; j++)
                        {
                            if (arrival[j] < arrival[i])
                            {
                                temp = arrival[i];
                                arrival[i] = arrival[j];
                                arrival[j] = temp;
                                temp = burst[i];
                                burst[i] = burst[j];
                                burst[j] = temp;
                                temp2 = proccess[i];
                                proccess[i] = proccess[j];
                                proccess[j] = temp2;
                            }
                        }
                    }
                }

                if (comboBox1.Text == "FCFS")
                {
                    currenttime = 0;
                    TAT = burst[0] - arrival[0];
                    int width, xaxis = 50;
                    string number, num;
                    num = currenttime.ToString();
                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        if (arrival[i] > currenttime)
                        {
                            //AT += (arrival[i] - currenttime);
                            //totaltime += (arrival[i] - currenttime);
                            width = (int)(((arrival[i] - currenttime) * 610) / totaltime);
                            g.FillRectangle(sb, xaxis, 40, width, 50);
                            b.DrawRectangle(pen, xaxis, 40, width, 50);
                            c.DrawString("idle", font, sb, new PointF((float)xaxis, 15));
                            xaxis += width;
                            currenttime += (arrival[i] - currenttime);
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                        }
                        width = (int)((burst[i] * 610) / totaltime);
                        //width = (int)burst[i]*20;
                        number = proccess[i].ToString();

                        // xaxis =(int)(( currenttime * 1000)/totaltime);
                        g.FillRectangle(sb, xaxis, 40, width, 50);
                        b.DrawRectangle(pen, xaxis, 40, width, 50);
                        c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                        xaxis += width;

                        if (i == 0)
                        {
                            currenttime += burst[i];
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                            continue;
                        }
                        AT += (currenttime - arrival[i]);
                        currenttime += burst[i];
                        TAT += (currenttime - arrival[i]);
                        num = currenttime.ToString();
                        c.DrawString(num, font, sb, new PointF((float)xaxis, 95));


                    }
                    currenttime = 0;
                }

                else if (comboBox1.Text == "SJF - Non-preemptive")
                {
                    currenttime = arrival[0];
                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        for (int j = i + 1; j < arrivalarray.Length; j++)
                        {
                            if ((burst[j] < burst[i]) && (arrival[j] <= currenttime))
                            {
                                temp = arrival[i];
                                arrival[i] = arrival[j];
                                arrival[j] = temp;
                                temp = burst[i];
                                burst[i] = burst[j];
                                burst[j] = temp;
                                temp2 = proccess[i];
                                proccess[i] = proccess[j];
                                proccess[j] = temp2;

                            }
                        }
                        currenttime += burst[i];
                    }
                    currenttime = 0;
                    TAT = burst[0] - arrival[0];
                    int width, xaxis = 50;
                    string number;
                    string num;
                    num = currenttime.ToString();
                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        if (arrival[i] > currenttime)
                        {
                            // AT += (arrival[i] - currenttime);
                            //totaltime += (arrival[i] - currenttime);
                            width = (int)(((arrival[i] - currenttime) * 610) / totaltime);
                            g.FillRectangle(sb, xaxis, 40, width, 50);
                            b.DrawRectangle(pen, xaxis, 40, width, 50);
                            c.DrawString("idle", font, sb, new PointF((float)xaxis, 15));
                            xaxis += width;
                            currenttime += (arrival[i] - currenttime);
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                        }
                        width = (int)((burst[i] * 610) / totaltime);
                        //width = (int)burst[i]*20;
                        number = proccess[i].ToString();

                        // xaxis =(int)(( currenttime * 1000)/totaltime);
                        g.FillRectangle(sb, xaxis, 40, width, 50);
                        b.DrawRectangle(pen, xaxis, 40, width, 50);
                        c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                        xaxis += width;

                        if (i == 0)
                        {
                            currenttime += burst[i];
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                            continue;
                        }
                        AT += (currenttime - arrival[i]);
                        currenttime += burst[i];
                        TAT += (currenttime - arrival[i]);
                        num = currenttime.ToString();
                        c.DrawString(num, font, sb, new PointF((float)xaxis, 95));


                    }
                    currenttime = 0;
                }
                else if (comboBox1.Text == "Priority - Non-preemptive ")
                {
                    int[] priority = new int[100];
                    int[] prioritymtrtb = new int[100];
                    if (textBox3.Text == "ex: 1 2 3 4")
                        MessageBox.Show("Please enter priority numbers");
                    string frompriority = textBox3.Text;
                    needleCount = frompriority.Length - frompriority.Replace(" ", "").Length + 1;
                    if (needleCount != n)
                        MessageBox.Show("Please enter priority numbers equal to no. of processes");
                    string[] priorityarray = frompriority.Split(' ');
                    try
                    {
                        for (int i = 0; i < n; i++)
                        {
                            priority[i] = int.Parse(priorityarray[i]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("error");
                        return;
                    }

                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        for (int j = i + 1; j < arrivalarray.Length; j++)
                        {
                            if (arrival[j] < arrival[i])
                            {
                                temp2 = priority[i];
                                priority[i] = priority[j];
                                priority[j] = temp2;
                                temp = arrival[i];
                                arrival[i] = arrival[j];
                                arrival[j] = temp;
                                temp = burst[i];
                                burst[i] = burst[j];
                                burst[j] = temp;
                                temp2 = proccess[i];
                                proccess[i] = proccess[j];
                                proccess[j] = temp2;
                            }
                        }
                    }
                    currenttime = arrival[0];
                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        for (int j = i + 1; j < arrivalarray.Length; j++)
                        {
                            if ((priority[j] < priority[i]) && (arrival[j] <= currenttime))
                            {

                                temp2 = priority[i];
                                priority[i] = priority[j];
                                priority[j] = temp2;
                                temp = arrival[i];
                                arrival[i] = arrival[j];
                                arrival[j] = temp;
                                temp = burst[i];
                                burst[i] = burst[j];
                                burst[j] = temp;
                                temp2 = proccess[i];
                                proccess[i] = proccess[j];
                                proccess[j] = temp2;

                            }
                        }
                        currenttime += burst[i];
                    }
                    currenttime = 0;
                    TAT = burst[0] - arrival[0];
                    int width, xaxis = 50;
                    string number, num;
                    num = currenttime.ToString();
                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        if (arrival[i] > currenttime)
                        {
                            //AT += (arrival[i] - currenttime);
                            // totaltime += (arrival[i] - currenttime);
                            width = (int)(((arrival[i] - currenttime) * 610) / totaltime);
                            g.FillRectangle(sb, xaxis, 40, width, 50);
                            b.DrawRectangle(pen, xaxis, 40, width, 50);
                            c.DrawString("idle", font, sb, new PointF((float)xaxis, 15));
                            xaxis += width;
                            currenttime += (arrival[i] - currenttime);
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                        }
                        width = (int)((burst[i] * 610) / totaltime);
                        //width = (int)burst[i]*20;
                        number = proccess[i].ToString();

                        // xaxis =(int)(( currenttime * 1000)/totaltime);
                        g.FillRectangle(sb, xaxis, 40, width, 50);
                        b.DrawRectangle(pen, xaxis, 40, width, 50);
                        c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                        xaxis += width;

                        if (i == 0)
                        {
                            currenttime += burst[i];
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                            continue;
                        }
                        AT += (currenttime - arrival[i]);
                        currenttime += burst[i];
                        TAT += (currenttime - arrival[i]);
                        num = currenttime.ToString();
                        c.DrawString(num, font, sb, new PointF((float)xaxis, 95));


                    }
                    currenttime = 0;

                }
                else if (comboBox1.Text == "Round Robin")
                {
                    if (textBox4.Text == "ex: 4")
                        MessageBox.Show("Please enter time quantum");
                    int q ;
                    try
                    {
                        q = int.Parse(textBox4.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                        return;
                    }
                    int count = 0;
                    int processinQ;
                    Queue reqQueue = new Queue();
                    currenttime = 0;
                    int width, xaxis = 50;
                    string number, num;
                    num = currenttime.ToString();
                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                    try
                    {
                        while ((count < n) || (reqQueue.Count != 0))
                        {

                            if ((count < n) && (arrival[count] > currenttime) && (reqQueue.Count == 0))
                            {
                                //AT += (arrival[count] - currenttime);
                                //totaltime += (arrival[count] - currenttime);
                                width = (int)(((arrival[count] - currenttime) * 610) / totaltime);
                                g.FillRectangle(sb, xaxis, 40, width, 50);
                                b.DrawRectangle(pen, xaxis, 40, width, 50);
                                c.DrawString("idle", font, sb, new PointF((float)xaxis, 15));
                                xaxis += width;
                                currenttime += (arrival[count] - currenttime);
                                num = currenttime.ToString();
                                c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                            }
                            if ((count < n) && (arrival[count] <= currenttime))
                            {

                                AT += (currenttime - arrival[count]);
                                if (q >= burst[count])
                                {
                                    currenttime += burst[count];
                                    //burst[count] -= burst[count];

                                    width = (int)((burst[count] * 610) / totaltime);
                                    //width = (int)burst[i]*20;
                                    number = proccess[count].ToString();

                                    // xaxis =(int)(( currenttime * 1000)/totaltime);
                                    g.FillRectangle(sb, xaxis, 40, width, 50);
                                    b.DrawRectangle(pen, xaxis, 40, width, 50);
                                    c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                                    xaxis += width;
                                    TAT += (currenttime - arrival[count]);

                                }

                                else
                                {
                                    currenttime += q;
                                    reqQueue.Enqueue(count);
                                    burst[count] -= q;
                                    TAT += (currenttime - arrival[count]);
                                    arrival[count] = currenttime;

                                    width = (int)((q * 610) / totaltime);
                                    //width = (int)burst[i]*20;
                                    number = proccess[count].ToString();

                                    // xaxis =(int)(( currenttime * 1000)/totaltime);
                                    g.FillRectangle(sb, xaxis, 40, width, 50);
                                    b.DrawRectangle(pen, xaxis, 40, width, 50);
                                    c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                                    xaxis += width;
                                }
                                
                                num = currenttime.ToString();
                                c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                                count++;
                            }
                            else
                            {
                                processinQ = (int)reqQueue.Dequeue();
                                AT += (currenttime - arrival[processinQ]);
                                if (q >= burst[processinQ])
                                {
                                    currenttime += burst[processinQ];
                                    //burst[processinQ] -= burst[processinQ];

                                    width = (int)((burst[processinQ] * 610) / totaltime);
                                    //width = (int)burst[i]*20;
                                    number = proccess[processinQ].ToString();

                                    // xaxis =(int)(( currenttime * 1000)/totaltime);
                                    g.FillRectangle(sb, xaxis, 40, width, 50);
                                    b.DrawRectangle(pen, xaxis, 40, width, 50);
                                    c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                                    xaxis += width;
                                    TAT += (currenttime - arrival[processinQ]);
                                }
                                else
                                {
                                    currenttime += q;
                                    reqQueue.Enqueue(processinQ);
                                    burst[processinQ] -= q;
                                    TAT += (currenttime - arrival[processinQ]);
                                    arrival[processinQ] = currenttime;

                                    width = (int)((q * 610) / totaltime);
                                    //width = (int)burst[i]*20;
                                    number = proccess[processinQ].ToString();

                                    // xaxis =(int)(( currenttime * 1000)/totaltime);
                                    g.FillRectangle(sb, xaxis, 40, width, 50);
                                    b.DrawRectangle(pen, xaxis, 40, width, 50);
                                    c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                                    xaxis += width;
                                }
                                
                                num = currenttime.ToString();
                                c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                            }


                        }
                    }
                    catch
                    {
                        MessageBox.Show("error please enter q");
                    }

                }
                else if (comboBox1.Text == "SJF - Preemptive")
                {
                    int count = 0;
                    int proccessinst = 0;
                    Stack st = new Stack();
                    // int stpointer1,stpointer2;
                    currenttime = 0;
                    double pasttime;
                    int width, xaxis = 50;
                    string number, num;
                    num = arrival[0].ToString();
                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                    int countx;
                    while ((count < n) || (st.Count != 0))
                    {
                        if ((count < n) && (arrival[count] > currenttime) && (st.Count == 0))
                        {
                            //AT += (arrival[count] - currenttime);
                            //totaltime += (arrival[count] - currenttime);
                            width = (int)(((arrival[count] - currenttime) * 610) / totaltime);
                            g.FillRectangle(sb, xaxis, 40, width, 50);
                            b.DrawRectangle(pen, xaxis, 40, width, 50);
                            c.DrawString("idle", font, sb, new PointF((float)xaxis, 15));
                            xaxis += width;
                            currenttime += (arrival[count] - currenttime);
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                        }
                        if (st.Count > 2)
                        {
                            int r = st.Count;
                            int[] arr = new int[r];
                            for (int i = 0; i < r; i++)
                            {
                                arr[i] = (int)st.Pop();
                            }
                            for (int i = 0; i < r; i++)
                            {
                                for (int j = i + 1; j < r; j++)
                                {
                                    if (burst[arr[j]] < burst[arr[i]])
                                    {
                                        temp2 = arr[i];
                                        arr[i] = arr[j];
                                        arr[j] = temp2;

                                    }
                                }
                            }
                            for (int i = (r - 1); i >= 0; i--)
                            {
                                st.Push(arr[i]);
                            }
                        }
                        for (int i = count; (i < n) && (arrival[i] <= currenttime); i++)
                        {

                            for (int j = i + 1; (j < n) && (arrival[j] <= currenttime); j++)
                            {
                                if (burst[j] < burst[i])
                                {
                                    temp = arrival[i];
                                    arrival[i] = arrival[j];
                                    arrival[j] = temp;
                                    temp = burst[i];
                                    burst[i] = burst[j];
                                    burst[j] = temp;
                                    temp2 = proccess[i];
                                    proccess[i] = proccess[j];
                                    proccess[j] = temp2;
                                }
                            }

                        }
                        if (st.Count != 0)
                        {
                            proccessinst = (int)st.Pop();
                            if ((count == n) || (burst[proccessinst] <= burst[count]))
                            {
                                pasttime = currenttime;
                                //(currenttime < arrival[proccessinst + 1]) && 

                                while (((((count + 1) < n) && (currenttime < arrival[count + 1])) || (count == n)) && ((burst[proccessinst] - (currenttime - pasttime)) > 0)) //rg3i de 
                                {
                                    currenttime++;

                                }
                                burst[proccessinst] -= (currenttime - pasttime);
                                if (pasttime != currenttime)
                                {
                                    AT += (pasttime - arrival[proccessinst]);
                                    width = (int)(((currenttime - pasttime) * 610) / totaltime);
                                    number = proccess[proccessinst].ToString();

                                    // xaxis =(int)(( currenttime * 1000)/totaltime);
                                    g.FillRectangle(sb, xaxis, 40, width, 50);
                                    b.DrawRectangle(pen, xaxis, 40, width, 50);
                                    c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                                    xaxis += width;
                                    num = currenttime.ToString();
                                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                                    TAT += (currenttime - arrival[proccessinst]);
                                    arrival[proccessinst] = currenttime;
                                }
                                if (burst[proccessinst] != 0)
                                {
                                    if (st.Count != 0)
                                    {
                                        temp2 = (int)st.Pop();
                                        if (burst[temp2] <= burst[proccessinst])
                                        {
                                            st.Push(proccessinst);
                                            st.Push(temp2);
                                        }
                                        else
                                        {
                                            st.Push(temp2);
                                            st.Push(proccessinst);
                                        }
                                    }
                                    else
                                        st.Push(proccessinst);
                                }
                            }
                            else
                                st.Push(proccessinst);
                        }
                        if (count < n)
                        {
                            /*if (count < n - 1)
                            {
                           
                            }*/
                            if (st.Count != 0)
                            {
                                proccessinst = (int)st.Pop();
                                st.Push(proccessinst);
                            }
                            pasttime = currenttime;
                            if ((count == (n - 1)) && (((st.Count != 0) && (burst[count] < burst[proccessinst])) || (st.Count == 0)))
                            {
                                while ((burst[count] - (currenttime - pasttime)) > 0)
                                {
                                    currenttime++;

                                }
                            }
                            else if ((burst[count] < burst[proccessinst]) || (st.Count == 0))
                            {
                                countx = count;
                                while (((currenttime < arrival[countx + 1]) || (burst[count] < burst[countx + 1])) && ((burst[count] - (currenttime - pasttime)) > 0)) //hna brdo fe error l curennt time b2a akbr mn kolo
                                {
                                    if ((currenttime == arrival[countx + 1])&&((countx+1) <(n-1)))
                                        countx++;
                                    currenttime++;

                                }
                            }
                            burst[count] -= (currenttime - pasttime);
                            if (pasttime != currenttime)
                            {
                                AT += (pasttime - arrival[count]);
                                width = (int)(((currenttime - pasttime) * 610) / totaltime);
                                number = proccess[count].ToString();

                                // xaxis =(int)(( currenttime * 1000)/totaltime);
                                g.FillRectangle(sb, xaxis, 40, width, 50);
                                b.DrawRectangle(pen, xaxis, 40, width, 50);
                                c.DrawString("p" + number , font, sb, new PointF((float)xaxis, 15));
                                xaxis += width;
                                num = currenttime.ToString();
                                c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                                TAT += (currenttime - arrival[count]);
                                arrival[count] = currenttime;
                            }
                            if (burst[count] != 0)
                            {
                                if (st.Count != 0)
                                {
                                    temp2 = (int)st.Pop();
                                    if (burst[temp2] <= burst[count])
                                    {
                                        st.Push(count);
                                        st.Push(temp2);
                                    }
                                    else
                                    {
                                        st.Push(temp2);
                                        st.Push(count);
                                    }
                                }
                                else
                                    st.Push(count);
                            }
                            count++;
                        }
                    }
                }
                else if (comboBox1.Text == "Priority - Preemptive")
                {
                    int[] priority = new int[n];
                    int[] prioritymtrtb = new int[n];
                    if (textBox3.Text == "ex: 1 2 3 4")
                        MessageBox.Show("Please enter priority numbers");
                    string frompriority = textBox3.Text;
                    needleCount = frompriority.Length - frompriority.Replace(" ", "").Length + 1;
                    if (needleCount != n)
                        MessageBox.Show("Please enter priority numbers equal to no. of processes");
                    string[] priorityarray = frompriority.Split(' ');
                    try
                    {
                        for (int i = 0; i < n; i++)
                        {
                            priority[i] = int.Parse(priorityarray[i]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("error");
                    }

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = i + 1; j < n; j++)
                        {
                            if (arrival[j] < arrival[i])
                            {
                                temp2 = priority[i];
                                priority[i] = priority[j];
                                priority[j] = temp2;
                                temp = arrival[i];
                                arrival[i] = arrival[j];
                                arrival[j] = temp;
                                temp = burst[i];
                                burst[i] = burst[j];
                                burst[j] = temp;
                                temp2 = proccess[i];
                                proccess[i] = proccess[j];
                                proccess[j] = temp2;
                            }
                        }
                    }
                    for (int i = 0; i < arrivalarray.Length; i++)
                    {
                        for (int j = i + 1; j < arrivalarray.Length; j++)
                        {
                            if ((priority[j] < priority[i]) && (arrival[j] == arrival[i]))
                            {

                                temp2 = priority[i];
                                priority[i] = priority[j];
                                priority[j] = temp2;
                                temp = arrival[i];
                                arrival[i] = arrival[j];
                                arrival[j] = temp;
                                temp = burst[i];
                                burst[i] = burst[j];
                                burst[j] = temp;
                                temp2 = proccess[i];
                                proccess[i] = proccess[j];
                                proccess[j] = temp2;

                            }
                        }
                    }
                    int count = 0;
                    int proccessinst = 0;
                    Stack st = new Stack();
                    // int stpointer1,stpointer2;
                    currenttime = 0;
                    double pasttime;
                    int width, xaxis = 50;
                    string number, num;
                    num = arrival[0].ToString();
                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                    int countx;
                    //bool flag = false;
                    while ((count < n) || (st.Count != 0))
                    {
                        if ((count < n) && (arrival[count] > currenttime) && (st.Count == 0))
                        {
                            //AT += (arrival[count] - currenttime);
                            //totaltime += (arrival[count] - currenttime);
                            width = (int)(((arrival[count] - currenttime) * 610) / totaltime);
                            g.FillRectangle(sb, xaxis, 40, width, 50);
                            b.DrawRectangle(pen, xaxis, 40, width, 50);
                            c.DrawString("idle", font, sb, new PointF((float)xaxis, 15));
                            xaxis += width;
                            currenttime += (arrival[count] - currenttime);
                            num = currenttime.ToString();
                            c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                        }
                        if (st.Count > 2)
                        {
                            int r = st.Count;
                            int[] arr = new int[r];
                            for (int i = 0; i < r; i++)
                            {
                                arr[i] = (int)st.Pop();
                            }
                            for (int i = 0; i < r; i++)
                            {
                                for (int j = i + 1; j < r; j++)
                                {
                                    if (priority[arr[j]] < priority[arr[i]])
                                    {
                                        temp2 = arr[i];
                                        arr[i] = arr[j];
                                        arr[j] = temp2;

                                    }
                                }
                            }
                            for (int i = (r - 1); i >= 0; i--)
                            {
                                st.Push(arr[i]);
                            }
                        }
                        for (int i = count; (i < n) && (arrival[i] <= currenttime); i++)
                        {

                            for (int j = i + 1; (j < n) && (arrival[j] <= currenttime); j++)
                            {
                                if (priority[j] < priority[i])
                                {
                                    temp2 = priority[i];
                                    priority[i] = priority[j];
                                    priority[j] = temp2;
                                    temp = arrival[i];
                                    arrival[i] = arrival[j];
                                    arrival[j] = temp;
                                    temp = burst[i];
                                    burst[i] = burst[j];
                                    burst[j] = temp;
                                    temp2 = proccess[i];
                                    proccess[i] = proccess[j];
                                    proccess[j] = temp2;
                                }
                            }

                        }
                        if (st.Count != 0)
                        {
                            proccessinst = (int)st.Pop();
                            if ((count == n) || (priority[proccessinst] <= priority[count]))
                            {
                                pasttime = currenttime;
                                //(currenttime < arrival[proccessinst + 1]) && 

                                while (((((count + 1) < n) && (currenttime < arrival[count + 1])) || (count == n)) && ((burst[proccessinst] - (currenttime - pasttime)) > 0)) //rg3i de 
                                {
                                    currenttime++;

                                }
                                burst[proccessinst] -= (currenttime - pasttime);
                                if (pasttime != currenttime)
                                {
                                    AT += (pasttime - arrival[proccessinst]);
                                    width = (int)(((currenttime - pasttime) * 610) / totaltime);
                                    number = proccess[proccessinst].ToString();

                                    // xaxis =(int)(( currenttime * 1000)/totaltime);
                                    g.FillRectangle(sb, xaxis, 40, width, 50);
                                    b.DrawRectangle(pen, xaxis, 40, width, 50);
                                    c.DrawString("p" + number , font, sb, new PointF((float)xaxis, 15));
                                    xaxis += width;
                                    num = currenttime.ToString();
                                    c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                                    TAT += (currenttime - arrival[proccessinst]);
                                    arrival[proccessinst] = currenttime;
                                }
                                if (burst[proccessinst] != 0)
                                {
                                    
                                    if (st.Count != 0)
                                    {
                                        temp2 = (int)st.Pop();
                                        if (priority[temp2] <= priority[proccessinst])
                                        {
                                            st.Push(proccessinst);
                                            st.Push(temp2);
                                        }
                                        else
                                        {
                                            st.Push(temp2);
                                            st.Push(proccessinst);
                                        }
                                    }
                                    else
                                        st.Push(proccessinst);
                                }
                            }
                            else
                                st.Push(proccessinst);
                        }
                        if (count < n)
                        {
                            /*if (count < n - 1)
                            {
                           
                            }*/
                            if (st.Count != 0)
                            {
                                proccessinst = (int)st.Pop();
                                st.Push(proccessinst);
                            }
                            pasttime = currenttime;
                            if ((count == (n - 1)) && (((st.Count != 0) && (priority[count] < priority[proccessinst])) || (st.Count == 0)))
                            {
                                while ((burst[count] - (currenttime - pasttime)) > 0)
                                {
                                    currenttime++;

                                }
                            }
                            else if ((priority[count] < priority[proccessinst]) || (st.Count == 0))
                            {
                                countx = count;
                                while (((currenttime < arrival[countx + 1]) || (priority[count] < priority[countx + 1])) && ((burst[count] - (currenttime - pasttime)) > 0)) //hna brdo fe error l curennt time b2a akbr mn kolo
                                {
                                    if ((currenttime == arrival[countx + 1])&&((countx+1) <(n-1)))
                                        countx++;
                                    currenttime++;

                                }
                            }
                            burst[count] -= (currenttime - pasttime);
                            if (pasttime != currenttime)
                            {
                                AT += (pasttime - arrival[count]);
                                width = (int)(((currenttime - pasttime) * 610) / totaltime);
                                number = proccess[count].ToString();

                                // xaxis =(int)(( currenttime * 1000)/totaltime);
                                g.FillRectangle(sb, xaxis, 40, width, 50);
                                b.DrawRectangle(pen, xaxis, 40, width, 50);
                                c.DrawString("p" + number, font, sb, new PointF((float)xaxis, 15));
                                xaxis += width;
                                num = currenttime.ToString();
                                c.DrawString(num, font, sb, new PointF((float)xaxis, 95));
                                TAT += (currenttime - arrival[count]);
                                arrival[count] = currenttime;
                            }
                            if (burst[count] != 0)
                            {
                                if (st.Count != 0)
                                {
                                    temp2 = (int)st.Pop();
                                    if (priority[temp2] <= priority[count])
                                    {
                                        st.Push(count);
                                        st.Push(temp2);
                                    }
                                    else
                                    {
                                        st.Push(temp2);
                                        st.Push(count);
                                    }
                                }
                                else
                                    st.Push(count);
                            }
                            count++;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
                return;
            }
            AT /= n;
            TAT /= n;
            label7.Text = AT.ToString();
            label8.Text = TAT.ToString();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            /*SolidBrush sb = new SolidBrush(Color.SteelBlue);
            Graphics g = panel2.CreateGraphics();
            g.FillRectangle(sb, 50, 50, 70, 50);*/
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "ex: 1 2 3 4";
            textBox2.Text = "ex: 1 2 3 4";
            textBox3.Text = "ex: 1 2 3 4";
            textBox4.Text = "ex: 4";
            textBox5.Text = "ex: 4";
            label7.Text = " ";
            label8.Text = " ";
            panel2.Refresh();
            comboBox1.Text = " ";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 32 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only seperated with spaces");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 32 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only seperated with spaces");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 32 )
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only seperated with spaces");
            }
        }
        
    }
}
