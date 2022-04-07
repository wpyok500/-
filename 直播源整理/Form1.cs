using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 直播源整理
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            Array array = (Array)(e.Data.GetData(DataFormats.FileDrop));
            string filepath = array.GetValue(0).ToString();
            string path = filepath.Substring(0, filepath.LastIndexOf("\\"));
            string filenaem = filepath.Substring(filepath.LastIndexOf("\\")+1);

            List<string> list = File.ReadAllLines(filepath).ToList();
            Dictionary<string, string> tvdic = new Dictionary<string, string>();
            foreach (string item in list.ToArray())
            {
                string[] temp = item.Split(',');
                if (temp.Length == 1 || item.Contains("#genre#"))
                {
                    continue;
                }
                if (tvdic.Count == 0)
                {
                    tvdic.Add(temp[0], temp[1]);
                }
                else
                {
                    foreach (KeyValuePair<string, string> item1 in tvdic)
                    {
                        KeyValuePair<string, string> tt = tvdic.FirstOrDefault(x => x.Key.Equals(temp[0]));
                        if (tt.Key == null)
                        {
                            if (item1.Key.Equals(temp[0]))
                            {
                                if (tvdic[temp[0]].Contains(temp[1]))
                                {
                                    continue;
                                }
                                tvdic[temp[0]] = tvdic[temp[0]] + "#" + temp[1];
                                break;
                            }
                            else
                            {

                                tvdic.Add(temp[0], temp[1]);
                                break;
                            }
                        }
                        else
                        {
                            if (tvdic[temp[0]].Contains(temp[1]))
                            {
                                continue;
                            }
                            tvdic[temp[0]] = tvdic[temp[0]] + "#" + temp[1];
                            break;
                        }

                    }
                }



            }
            List<string> list1 = new List<string>();
            foreach (KeyValuePair<string, string> item in tvdic)
            {
                list1.Add(item.Key + "," + item.Value);
            }
            list1.Sort((x, y) => -x.CompareTo(y));
            string time = DateTime.Now.ToString("yyMMddHHmmss");
            //File.WriteAllLines(System.Environment.CurrentDirectory + $"\\sort{time}.txt", list1.ToArray());
            filenaem = filenaem.Replace(".", "_sort.");
            File.WriteAllLines(path + $"\\{filenaem}.txt", list1.ToArray());
            MessageBox.Show("整理完成");
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

    }
}
