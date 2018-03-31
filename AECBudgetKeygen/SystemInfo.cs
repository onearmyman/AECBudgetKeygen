using System;
using System.Management;
using System.Windows.Forms;

namespace AECBudgetKeygen
{
    public class SystemInfo
    {
        public static bool UseProcessorID;
        public static bool UseBaseBoardProduct;
        public static bool UseBaseBoardManufacturer;
        public static bool UseDiskDriveSignature;
        public static bool UseVideoControllerCaption;
        public static bool UsePhysicalMediaSerialNumber;
        public static bool UseBiosVersion;
        public static bool UseBiosManufacturer;
        public static bool UseWindowsSerialNumber;
        public static bool CheckVesrsion;
        public static bool CheckVesrsionMB;
        public static string strProcessorID = "";
        public static string strMBId = "";

        public static string GetSystemInfo(string SoftwareName)
        {
            if (CheckVesrsion) SoftwareName += RunQuerys();
            if (CheckVesrsionMB) SoftwareName += RunQuerysMB();
            if (UseBiosVersion) SoftwareName += RunQuery("BIOS", "Version");
            if (UseBaseBoardManufacturer) SoftwareName += RunQuery("BaseBoard", "Manufacturer");
            if (UseDiskDriveSignature) SoftwareName += RunQuery("DiskDrive", "Signature");
            if (UseVideoControllerCaption) SoftwareName += RunQuery("VideoController", "Caption");
            if (UsePhysicalMediaSerialNumber) SoftwareName += RunQuery("PhysicalMedia", "SerialNumber");
            if (UseWindowsSerialNumber) SoftwareName += RunQuery("OperatingSystem", "SerialNumber");
            if (UseProcessorID) SoftwareName += RunQuery("Processor", "ProcessorId");
            if (UseBaseBoardProduct) SoftwareName += RunQuery("BaseBoard", "Product");
            SoftwareName = RemoveUseLess(SoftwareName);
            string result;
            if (SoftwareName.Length < 30)
                result = GetSystemInfo(SoftwareName);
            else
                result = SoftwareName.Substring(0, 30).ToUpper();
            return result;
        }

        private static string RemoveUseLess(string st)
        {
            for (var i = st.Length - 1; i >= 0; i--)
            {
                var c = char.ToUpper(st[i]);
                if ((c < 'A' || c > 'Z') && (c < '0' || c > '9')) st = st.Remove(i, 1);
            }

            return st;
        }

        private static string RunQuerys()
        {
            var managementObjectSearcher =
                new ManagementObjectSearcher("\\root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (var managementBaseObject in managementObjectSearcher.Get())
            {
                var managementObject = (ManagementObject) managementBaseObject;
                try
                {
                    return strProcessorID = Convert.ToString(managementObject["ProcessorId"]);
                }
                catch
                {
                }
            }

            return "";
        }


        private static string RunQuerysMB()
        {
            var managementObjectSearcher =
                new ManagementObjectSearcher("\\root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (var managementBaseObject in managementObjectSearcher.Get())
            {
                var managementObject = (ManagementObject) managementBaseObject;
                try
                {
                    return strMBId = managementObject.Properties["SerialNumber"].Value.ToString();
                }
                catch
                {
                    
                }
            }

            return "";
        }


        private static string RunQuery(string TableName, string MethodName)
        {
            var managementObjectSearcher =
                new ManagementObjectSearcher("\\root\\CIMV2", "Select * from Win32_" + TableName);
            foreach (var managementBaseObject in managementObjectSearcher.Get())
            {
                var managementObject = (ManagementObject) managementBaseObject;
                try
                {
                    return managementObject[MethodName].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return "";
        }
    }
}