using System;
using System.IO;
using System.Windows;
using NetInfo.Wpf.AvalonDock.Layout.Serialization;

namespace NIAvalonDockDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(PanelManager);
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            if (File.Exists(path))
            {
                using (var stream = new StreamReader(path))
                {
                    serializer.Deserialize(stream);
                }
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(PanelManager);
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream);
            }
            MessageBox.Show("End");
        }

        private void BtnRestore_OnClick(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(PanelManager);
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            using (var stream = new StreamReader(path))
            {
                serializer.Deserialize(stream);
            }
        }
    }
}
