using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;
using System.IO;

namespace Security
{
    public class UsbChecker
    {
        public UsbChecker()
        {

        }

        public bool Check()
        {

            bool keyExist = false;
            string diskName = string.Empty;
            //Получение списка накопителей подключенных через интерфейс USB
            foreach (System.Management.ManagementObject drive in
                      new System.Management.ManagementObjectSearcher(
                       "select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                //Получаем букву накопителя
                foreach (ManagementObject partition in
                new System.Management.ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"]
                      + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (ManagementObject disk in
                 new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                          + partition["DeviceID"]
                          + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        //Получение буквы устройства
                         // diskName = disk["Name"].ToString().Trim();
                        //           decimal dSize = Math.Round((Convert.ToDecimal(
                        //new System.Management.ManagementObject("Win32_LogicalDisk.DeviceID='"
                        //        + diskName + "'")["Size"]) / 1073741824), 2);
                        var root = Directory.GetCurrentDirectory().Substring(0,2);
                        DiskInfo MyUSB = new DiskInfo(root, drive["Model"].ToString().Trim(), 0, drive["PNPDeviceID"].ToString().Trim());
                        if (File.Exists(root + "\\Licence.key"))
                            keyExist = MyUSB.CheckKey(root + "\\\\Licence.key") ? true : false;
                    }
                }
            }
            return keyExist;
        }
    }
}