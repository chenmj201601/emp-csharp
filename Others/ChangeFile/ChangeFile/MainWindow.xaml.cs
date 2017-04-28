using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace ChangeFile
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private int mCount1;
        private int mCount2;
        private int mCount3;
        private int mCount4;
        private string mRootPath;
        private int mBuildNo;
        private bool mIsWithDate;
        private BackgroundWorker mWorker;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            BtnTest.Click += BtnTest_Click;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mRootPath = @"D:\GitRoot\EMP\emp-csharp";
            mBuildNo = 1;
            TxtRootPath.Text = mRootPath;
            TxtBuildNo.Text = mBuildNo.ToString();
        }

        void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            string path = TxtRootPath.Text;
            if (string.IsNullOrEmpty(path))
            {
                AppendMessage(string.Format("Path is empty."));
                return;
            }
            mRootPath = path;
            int buildNo;
            if (!int.TryParse(TxtBuildNo.Text, out buildNo))
            {
                AppendMessage(string.Format("BuildNo invalid."));
                return;
            }
            mBuildNo = buildNo;
            mIsWithDate = CbWithDate.IsChecked == true;
            mCount1 = 0;        //替换文件版本的个数
            mCount2 = 0;        //替换产品版本的个数
            mCount3 = 0;        //扫描文件的个数
            mCount4 = 0;        //跳过替换的个数
            mWorker = new BackgroundWorker();
            mWorker.DoWork += (s, de) =>
            {
                try
                {
                    EnumDir(path);
                    AppendMessage(string.Format("End"));
                }
                catch (Exception ex)
                {
                    AppendMessage(string.Format("Fail.\t{0}", ex.Message));
                }
            };
            mWorker.RunWorkerCompleted += (s, re) =>
            {
                mWorker.Dispose();

                AppendMessage(string.Format("Count1:{0} Count2:{1} Count3:{2} Count4:{3}", mCount1, mCount2, mCount3,
                    mCount4));
            };
            mWorker.RunWorkerAsync();
        }

        private void EnumDir(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                var sub = dirs[i];
                if (sub.Name.StartsWith("emp")) { continue; }
                EnumDir(sub.FullName);
            }
            var files = dirInfo.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];

                //替换修改项目版本号
                //if (file.Extension.ToLower() == ".cs")
                //{
                //    ReplaceText(file.FullName);
                //}

                //修改文件头
                if (file.Extension.ToLower() == ".cs")
                {
                    ModifyFileHead(file.FullName);
                }

                ////删除BuildInfo文件
                //if (file.FullName.EndsWith("BuildInfo.txt"))
                //{
                //    File.Delete(file.FullName);
                //    mCount1++;
                //}

                SetState();
            }
        }

        private void ReplaceText(string fileName)
        {
            if (fileName.EndsWith("AssemblyInfo.cs"))
            {
                AppendMessage(string.Format("Replacing...\t{0}", fileName));
                mCount3++;
                string strReplacing = "\\[assembly: AssemblyVersion\\(\"" +         //匹配前导固定文本及括号引号
                                      "(\\d{1,3}\\.){3}" +                          //匹配版本号中的一位，重复3次
                                      "\\d{1,3}" +                                  //匹配版本号的最后一位（Build号）
                                      "\"\\)\\]";                                   //匹配结尾的引号括号和方括号

                string strReplacing2 = "\\[assembly: AssemblyFileVersion\\(\"" +    //匹配前导固定文本及括号引号
                                       "(\\d{1,3}\\.){3}" +                         //匹配版本号中的一位，重复3次
                                       "\\d{1,3}" +                                 //匹配版本号的最后一位（Build号）
                                       "(\\(\\d{0,8}\\))?" +                        //匹配编译日期，如 (20150928)
                                       "\"\\)\\]";                                  //匹配结尾的引号括号和方括号

                string strReplacing3 = "\\[assembly: AssemblyCopyright\\(\"" +      //匹配前导固定文本及括号引号
                                       ".*" +                                       //匹配版权信息
                                       "\"\\)\\]";                                  //匹配结尾的引号括号和方括号


                string strReplaced = string.Format("[assembly: AssemblyVersion(\"2.1.1.{0}\")]",
                    mBuildNo);

                string strReplaced2;
                if (mIsWithDate)
                {
                    strReplaced2 = string.Format("[assembly: AssemblyFileVersion(\"2.01.001.{0}({1})\")]",
                        mBuildNo.ToString("000"),
                        DateTime.Now.ToString("yyyyMMdd"));
                }
                else
                {
                    strReplaced2 = string.Format("[assembly: AssemblyFileVersion(\"2.01.001.{0}\")]",
                        mBuildNo.ToString("000"));
                }

                string strReplaced3 = string.Format("[assembly: AssemblyCopyright(\"Copyright © 2017 NetInfo Technologies Ltd.\")]");

                Regex regex = new Regex(strReplacing);
                Regex regex2 = new Regex(strReplacing2);
                Regex regex3 = new Regex(strReplacing3);
                string[] listContent = File.ReadAllLines(fileName);
                int lineCount = Math.Min(100, listContent.Length);
                bool isSave = false;
                for (int i = 0; i < lineCount; i++)
                {
                    string str = listContent[i];
                    if (str == strReplaced
                        || str == strReplaced2
                        || str == strReplaced3)
                    {
                        //如果与指定的完全相同，不用替换
                        mCount4++;
                        continue;
                    }
                    if (regex.IsMatch(str))
                    {
                        str = regex.Replace(str, strReplaced);
                        isSave = true;
                        mCount1++;
                    }
                    if (regex2.IsMatch(str))
                    {
                        str = regex2.Replace(str, strReplaced2);
                        isSave = true;
                        mCount2++;
                    }
                    if (regex3.IsMatch(str))
                    {
                        str = regex3.Replace(str, strReplaced3);
                        isSave = true;
                        mCount2++;
                    }
                    listContent[i] = str;
                }
                if (isSave)
                {
                    File.WriteAllLines(fileName, listContent);
                }
                SetState();
            }
        }

        private void ModifyFileHead(string fileName)
        {
            mCount3++;
            string strReplacing = string.Format("\\w*Copyright © 2014\\-2015 VoiceCyber Technology \\(SH\\) Ltd\\.\\w*");
            string strReplacing2 = string.Format("\\w*Organization:             VoiceCyber\\w*");
            string strReplacing3 = string.Format("\\w*Namespace:                VoiceCyber\\.\\w*");
            string strReplacing4 = string.Format("\\w*http://www\\.voicecyber\\.com\\w*");
            string strReplaced = string.Format("Copyright © 2017 NetInfo Technologies Ltd.");
            string strReplaced2 = string.Format("Organization:             NetInfo");
            string strReplaced3 = string.Format("Namespace:                NetInfo");
            string strReplaced4 = string.Format("http://www.netinfo.com");
            string[] listContent = File.ReadAllLines(fileName);
            int lineCount = Math.Min(100, listContent.Length);
            bool isSave = false;
            for (int i = 0; i < lineCount; i++)
            {
                //扫描前100行内容
                if (i < 100)
                {
                    string str = listContent[i];
                    Regex regex = new Regex(strReplacing);
                    Regex regex2 = new Regex(strReplacing2);
                    Regex regex3 = new Regex(strReplacing3);
                    Regex regex4 = new Regex(strReplacing4);
                    if (regex.IsMatch(str))
                    {
                        str = regex.Replace(str, strReplaced);
                        isSave = true;
                        mCount1++;
                    }
                    else if (regex2.IsMatch(str))
                    {
                        str = regex2.Replace(str, strReplaced2);
                        isSave = true;
                        mCount1++;
                    }
                    else if (regex3.IsMatch(str))
                    {
                        str = regex3.Replace(str, strReplaced3);
                        isSave = true;
                        mCount1++;
                    }
                    else if (regex4.IsMatch(str))
                    {
                        str = regex4.Replace(str, strReplaced4);
                        isSave = true;
                        mCount1++;
                    }
                    else
                    {
                        mCount4++;
                    }
                    listContent[i] = str;
                }
            }
            if (isSave)
            {
                File.WriteAllLines(fileName, listContent);
            }
            SetState();
        }

        private void SetState()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                TxtCount1.Text = mCount1.ToString();
                TxtCount2.Text = mCount2.ToString();
                TxtCount3.Text = mCount3.ToString();
                TxtCount4.Text = mCount4.ToString();
            }));
        }

        private void AppendMessage(string msg)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                TxtMsg.AppendText(string.Format("{0}\t{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), msg));
                TxtMsg.ScrollToEnd();
            }));
        }
    }
}
