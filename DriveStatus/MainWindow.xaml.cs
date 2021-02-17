using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
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

namespace DriveStatus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class HardDrive
    {
        public string Model { get; set; }
        public string InterfaceType { get; set; }
        public string Caption { get; set; }
        public string SerialNo { get; set; }
        
    }
    public class DriveDataInfo
    {
        public string Name { get; set; }
        public string Available_space { get; set; }
        public string Total_Size { get; set; }
        public string DriveFormat { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            #region DRIVE INFO
            DriveInfo[] drives = DriveInfo.GetDrives();
            var hdInfo = new List<DriveDataInfo>();
            foreach (DriveInfo drive in drives)
            {
                //There are more attributes you can use.
                //Check the MSDN link for a complete example.
                Console.WriteLine(drive.Name);
                if (drive.IsReady)
                {
                    //Console.WriteLine("Total Size : " + drive.TotalSize);
                    //Console.WriteLine("Available Space : " + drive.AvailableFreeSpace);
                    //Console.WriteLine("File System : " + drive.DriveFormat);
                    var tempinfo = new DriveDataInfo();
                    tempinfo.Available_space = "Available Space : " + drive.AvailableFreeSpace;
                    tempinfo.Name = drive.Name;
                    tempinfo.Total_Size = "Total Size : " + drive.TotalSize;
                    tempinfo.DriveFormat = "File System : " + drive.DriveFormat;
                    hdInfo.Add(tempinfo);
                }
            }
            ListBoxDrive.ItemsSource = hdInfo;
            #endregion


            #region Drive Manufacturer
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            var hdCollection = new List<HardDrive>();
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();

                hd.Model = "Model : "+wmi_HD["Model"].ToString();
                hd.InterfaceType = "IntefaceType : "+wmi_HD["InterfaceType"].ToString();
                hd.Caption = "Caption : "+wmi_HD["Caption"].ToString();
                hd.SerialNo = "Serial Number : "+wmi_HD.GetPropertyValue("SerialNumber").ToString();//get the serailNumber of diskdrive
                hdCollection.Add(hd);
            }
            ListBoxDrivemanufacturer.ItemsSource = hdCollection;
            Console.WriteLine("HD COLLECTION "+JsonConvert.SerializeObject(hdCollection));
            #endregion


        }
    }
}
