using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ReName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SourcetPath = textBox1.Text.Trim();
            //string[] FileName = Directory.GetFiles(SourcetPath, "*.rar");
            string[] FileName = Directory.GetFiles(SourcetPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".rar") || s.EndsWith(".zip")).ToArray();
            string DirectoryName = SourcetPath.Substring(SourcetPath.LastIndexOf("\\"));

            Log(SourcetPath, SourcetPath + "/" + DirectoryName + ".txt", FileName);
            MessageBox.Show("OK");
        }

        protected void Log(string SourcetPath, string LogFilePath, string[] FileName)
        {
            //在将文本写入文件前，处理文本行
            //StreamWriter一个参数默认覆盖
            //StreamWriter第二个参数为false覆盖现有文件，为true则把文本追加到文件末尾
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogFilePath, true))
            {
                for (int i = 0; i < FileName.Length; i++)
                {
                    string NewName = "a" + (i + 1).ToString().PadLeft(3, '0');
                    //sw.Write(line);//直接追加文件末尾，不换行
                    sw.WriteLine(NewName + "    " + FileName[i].Substring(FileName[i].LastIndexOf("\\") + 1));// 直接追加文件末尾，换行   

                    //重命名
                    ReName(FileName[i], SourcetPath + "/" + NewName + ".rar");
                }
                sw.Flush();
                sw.Close();
            }
        }

        protected void ReName(string FilePath, string TargetPath)
        {
            if (!File.Exists(TargetPath))
            {
                File.Move(FilePath, TargetPath);

                //FileInfo file = new FileInfo(FilePath);
                //file.MoveTo(TargetPath);
            }
        }

    }
}
