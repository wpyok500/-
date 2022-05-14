using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 直播源分类
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "直播源整理分类";
            Console.SetWindowSize(50, 10);
            Console.SetBufferSize(50, 10);

            if (args.Length == 0)
            {
                return;
            }
            Console.WriteLine("整理中。。。");
            string[] tvlines = File.ReadAllLines(args[0]);
            //string[] tvlines = File.ReadAllLines(System.Environment.CurrentDirectory + $"\\tv.txt");
            
            if (tvlines.Length<0)
            {
                return;
            }
            int num = 1;
            while (Isint(ref num))
            {
                break;
            }
            if (num==0)
            {
                return;
            }
            int tempnum = 1;
            string tempfilename = string.Empty;
            foreach (string item in tvlines)
            {
                if (item.Contains("#genre#"))
                {
                    string[] temptitle = item.Split(',');
                    tempnum = num;
                    tempfilename = $"\\{tempnum}#{temptitle[0]}.txt";
                    using (FileStream fs = new FileStream(System.Environment.CurrentDirectory + tempfilename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        fs.Close();
                    }
                    num++;
                }
                else if (string.IsNullOrEmpty(item) || !item.Contains("#__#"))
                {
                    using (FileStream fs = new FileStream(System.Environment.CurrentDirectory + tempfilename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            fs.Position = fs.Length;
                            sw.WriteLine(item);
                            sw.Close();
                            fs.Close();
                        }
                    }
                }

            }
            Console.WriteLine("整理完成");
            Console.ReadLine();
        }
        static bool Isint(ref int num)
        {
            try
            {
                Console.Write("请输入非0的正整数起始号：");
                num = int.Parse(Console.ReadLine());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
