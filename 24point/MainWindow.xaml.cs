using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _24point
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        struct history
        {
            public string val;
            public string fm;

        };
        public string Fm = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CaclClick(object sender, RoutedEventArgs e)
        {
            int d = Convert.ToInt32(Des.Text.Trim());
            string[] sarr = Sour.Text.Trim().Split(',');
            int[] sur = new int[sarr.Length];
            int index = 0;
            foreach (string s in sarr)
            {
                sur[index] = Convert.ToInt32(s);
                if (sur[index] <= 0) return; //input should >0
                index++;
            }
            history his = new history();
            his.val = d.ToString() + "/1";
            his.fm = d.ToString();
            if (Cacl(his, sur))
            // if (Cacltozero(his, sur)) //another way to get result;
            {
                result.Text = Fm;
            }
            else
            {
                result.Text = "false";
            }
        }
        private bool Cacl(history des, int[] sur)
        {
            bool bfind = false;
            history[] newdes = new history[6];
            int iLen = sur.Length;
            if (iLen > 1)
            {
                int index = 0;
                int[] newsur = new int[iLen - 1];
                foreach (int m in sur)
                {
                    Get_6_val(ref newdes, des, m);
                    GetNewSur(ref newsur, sur, index); //get sub array except index element
                    foreach (history h in newdes)
                    {
                        //except  a1=0
                        string[] a = des.val.Split('/');
                        if (Convert.ToInt64(a[1]) == 0) continue;
                        bfind = Cacl(h, newsur);
                        if (bfind) break;
                    }
                    if (bfind) break;
                    index++;
                }
                return bfind;
            }
            else //check if equal
            {
                string[] a = des.val.Split('/');
                long a1 = Convert.ToInt64(a[0]);
                long a2 = Convert.ToInt64(a[1]);
                if (a1 == a2 * sur[0])
                {
                    Fm = des.fm + "=" + sur[0].ToString();
                    return true;
                }
                else
                { return false; }
            }
        }
        //another method //just as make an arrary caculate to 0 is true and last step is sub
        private bool Cacltozero(history des, int[] sur)
        {
            bool bfind = false;
            history[] newdes = new history[6];
            int iLen = sur.Length;
            if (iLen == 0)
            {
                string[] a = des.val.Split('/');
                long a1 = Convert.ToInt64(a[0]);
                if (a1 == 0 && isnotMultilast(des.fm))  //
                {
                    Fm = des.fm + "=0";
                    return true;
                }
                else
                { return false; }

            }
            else
            {
                int index = 0;
                int[] newsur = new int[iLen - 1];
                foreach (int m in sur)
                {
                    Get_6_val(ref newdes, des, m);
                    GetNewSur(ref newsur, sur, index); //get sub array except index element
                    foreach (history h in newdes)
                    {
                        //except  a1=0
                        string[] a = des.val.Split('/');
                        if (Convert.ToInt64(a[1]) == 0) continue;
                        bfind = Cacltozero(h, newsur);
                        if (bfind) break;
                    }
                    if (bfind) break;
                    index++;
                }
                return bfind;
            }

        }
        private void Get_6_val(ref history[] v, history des, int m)
        {
            long a1, a2, b1, b2;
            string[] a = des.val.Split('/');
            a1 = Convert.ToInt64(a[0]);
            a2 = Convert.ToInt64(a[1]);
            //+
            b1 = a1 + a2 * m;
            b2 = a2;
            v[0].val = b1.ToString() + "/" + b2.ToString();
            v[0].fm = "(" + des.fm + "+" + m.ToString() + ")";
            //val -m
            b1 = a1 - a2 * m;
            b2 = a2;
            v[1].val = b1.ToString() + "/" + b2.ToString();
            v[1].fm = "(" + des.fm + "-" + m.ToString() + ")";
            //m-val 
            b1 = a2 * m - a1;
            b2 = a2;
            v[2].val = b1.ToString() + "/" + b2.ToString();
            v[2].fm = "(" + m.ToString() + "-" + des.fm + ")";
            // *
            b1 = m * a1;
            b2 = a2;
            v[3].val = b1.ToString() + "/" + b2.ToString();
            v[3].fm = "(" + des.fm + "*" + m.ToString() + ")";
            //val /m
            b1 = a1;
            b2 = a2 * m;
            v[4].val = b1.ToString() + "/" + b2.ToString();
            v[4].fm = "(" + des.fm + "/" + m.ToString() + ")";
            // m/val
            b1 = a2 * m;
            b2 = a1;
            v[5].val = b1.ToString() + "/" + b2.ToString();
            v[5].fm = "(" + m.ToString() + "/" + des.fm + ")";

        }
        //get sub array except index element
        private void GetNewSur(ref int[] newsur, int[] sur, int index)
        {
            int newindex = 0;
            int itmp = 0;
            foreach (int m in sur)
            {
                if (newindex == index)
                {
                    itmp = 1;
                }
                else
                {
                    newsur[newindex - itmp] = m;
                }
                newindex++;
            }
        }
        private bool isnotMultilast(string s)
        {
            int i = 1;
            bool bfind = false;
            string s1 = "";
            while (!bfind)
            {
                s1 = s.Substring(s.Length - i, 1);
                if ((s1 == "+") || (s1 == "-") || (s1 == "*") || (s1 == "/")) bfind = true;
                i++;
            }
            if ((s1 == "+") || (s1 == "-")) return true; else return false;
        }
    }
}
