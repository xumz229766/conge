using System.Data;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
namespace System.Toolkit.Helpers
{
    public class CSVHelper
    {

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// ��CSV�ļ������ݶ�ȡ��DataTable��
        /// </summary>
        /// <param name="fileName">CSV�ļ�·��</param>
        /// <returns>���ض�ȡ��CSV���ݵ�DataTable</returns>
        public static DataTable CSV2DataTable(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //��¼ÿ�ζ�ȡ��һ�м�¼
            string strLine = "";
            //��¼ÿ�м�¼�еĸ��ֶ�����
            string[] aryLine = null;
            string[] tableHead = null;
            //��ʾ����
            int columnCount = 0;
            //��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
            bool IsFirst = true;
            //���ж�ȡCSV�е�����
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //������
                    for (int i = 0; i < columnCount; i++)
                    {
                        tableHead[i] = tableHead[i].Replace("\"", "");
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j].Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[2] + " " + "DESC";
            }
            sr.Close();
            fs.Close();
            return dt;
        }

        /// <summary>
        /// ��DataTable������д�뵽CSV�ļ���
        /// </summary>
        /// <param name="dt">�ṩ�������ݵ�DataTable</param>
        /// <param name="fileName">CSV���ļ�·��</param>
        public static bool DataTable2CSV(DataTable dt, string fullPath)
        {
            try
            {
                FileInfo fi = new FileInfo(fullPath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";
                //д��������
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += "\"" + dt.Columns[i].ColumnName.ToString() + "\"";
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
                //д����������
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        str = string.Format("\"{0}\"", str);
                        data += str;
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �޸��ļ����� 
        /// </summary>
        /// <param name="OldPath">�ɵ�·�� ����������·��</param>
        /// <param name="NewPath">�µ�·��</param>
        /// <returns></returns>
        public static bool ChangeFileName(string OldPath, string NewPath)
        {
            bool re = false;
            //OldPath = HttpContext.Current.Server.MapPath(OldPath);�����
            //NewPath = HttpContext.Current.Server.MapPath(NewPath);
            try
            {
                if (File.Exists(OldPath))
                {
                    File.Move(OldPath, NewPath);
                    re = true;
                }
            }
            catch
            {
                re = false;
            }
            return re;
        }

        //ֱ������ҳ���ύ���ݱ�����csv�ļ��� ֱ��д���ļ�
        /// <summary>
        /// д���ļ�
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool SaveCSV(string fullPath, string Data)
        {
            bool re = true;
            try
            {
                FileStream FileStream = new FileStream(fullPath, FileMode.Append,FileAccess.Write,FileShare.ReadWrite);
                //FileStream FileStream = new FileStream(fullPath, FileMode.Append,FileAccess.Write);
                //FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                StreamWriter sw = new StreamWriter(FileStream, System.Text.Encoding.UTF8);
                sw.WriteLine(Data);
                //��ջ�����
                sw.Flush();
                //�ر���
                sw.Close();
                FileStream.Close();
            }
            catch
            {
                re = false;
            }
            return re;
        }

        //����ͺ�,ʱ��,�������,��·��ѹ,����,���Խ�����ϴ����
        public static bool SaveData2CSV(string fullPath, string Data,string Header)
        {
            bool re = true;
            try
            {
                FileInfo fi = new FileInfo(fullPath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }

                if (!File.Exists(fullPath))
                {
                    SaveCSV(fullPath, Header);
                }

                IntPtr vHandle = _lopen(fullPath, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle == HFILE_ERROR)
                {
                    //MessageBox.Show("�����ļ���ռ�ã�");
                    //string str = "";
                    Process[] processes;
                    //Get the list of current active processes.
                    processes = System.Diagnostics.Process.GetProcesses();
                    foreach (var item in processes)
                    {
                        if (item.ProcessName.ToUpper() == "EXCEL")
                        {
                            item.Kill();
                        }
                    }
                    ////Grab some basic information for each process.
                    //Process process;
                    //for (int i = 0; i < processes.Length - 1; i++)
                    //{
                    //    process = processes[i];
                    //    str = str + Convert.ToString(process.Id) + " : " +
                    //    process.ProcessName + "\r\n";
                    //}
                    ////Display the process information to the user
                    //MessageBox.Show(str);
                }
                CloseHandle(vHandle);
                //MessageBox.Show("û�б�ռ�ã�");

                SaveCSV(fullPath, Data);
            }
            catch
            {
                re = false;
            }
            return re;
        }

        public  static bool WriteCSV(string filePath, string fileName, string fileheadLine, string message = null)
        {
            string writeFileName = filePath + "\\" + fileName /*+ "_"*/  + ".csv";       
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            if (!File.Exists(writeFileName))
            {
                StreamWriter sw = new StreamWriter(writeFileName, true, Encoding.GetEncoding("GB2312"));

                sw.WriteLine(fileheadLine);
                sw.Flush();
                sw.Close();
            }
            try
            {
                StreamWriter sw = new StreamWriter(writeFileName, true, Encoding.GetEncoding("GB2312"));
                sw.WriteLine(message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

            }
            return true;
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string vFileName = @"c:\temp\temp.bmp";
        //    if (!File.Exists(vFileName))
        //    {
        //        MessageBox.Show("�ļ��������ڣ���Ͳ�Ҫ����ˣ��");
        //        return;
        //    }
        //    IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
        //    if (vHandle == HFILE_ERROR)
        //    {
        //        MessageBox.Show("�ļ���ռ�ã�");
        //        return;
        //    }
        //    CloseHandle(vHandle);
        //    MessageBox.Show("û�б�ռ�ã�");
        //}

    }
}