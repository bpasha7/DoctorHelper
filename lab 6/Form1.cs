using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using System.IO;
using Security;

namespace lab_6
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Список флеш накопителей
        /// </summary>
        List<DiskInfo> DI;
        /// <summary>
        /// Данные длЯ комбобокса 
        /// </summary>
        Dictionary<string, string> comboSource;
        delegate void DelegateDiskBox();
        /// <summary>
        /// Обновление списка доступных флеш накопителей
        /// </summary>
        void UpdateDiskBox()
        {
            if (DiskBox.InvokeRequired)
            {
                DelegateDiskBox d = new DelegateDiskBox(UpdateDiskBox);
                DiskBox.Invoke(d, new object[] { });
            }
            else
            {
                DI = new List<DiskInfo>();
                DiskBox.DisplayMember = "Value";
                DiskBox.ValueMember = "Key";
                comboSource = new Dictionary<string, string>();
                ReadUSBFlashDrivers(comboSource);
                if (comboSource.Count < 1)
                    DiskBox.DataSource = new BindingSource(null, null);
                DiskBox.DataSource = new BindingSource(comboSource, null);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработка событий по подкючению-изъятию флеш накопителей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Событие</param>
        private void DeviceUpToDate(object sender, EventArrivedEventArgs e)
        {
            UpdateDiskBox();
        }
        /// <summary>
        /// Передача параметров для генерации ключа-лицензии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (DI.Count != 0 && OwnerBox.Text != "")
            {
                if (DI[DiskBox.SelectedIndex].CreateKey(OwnerBox.Text.Trim()))
                    MessageBox.Show("Ключ к программе создан!", "Key Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("The key was not created!", "Key Generator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Чтение параметров флеш накопителей и
        /// занесение в список доступных
        /// </summary>
        /// <param name="comboSource"></param>
        private void ReadUSBFlashDrivers(Dictionary<string, string> comboSource)
        {
            string diskName = string.Empty;
            //Получение списка накопителей подключенных через интерфейс USB
            foreach (System.Management.ManagementObject drive in
                      new System.Management.ManagementObjectSearcher(
                       "select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                //Получаем букву накопителя
                foreach (System.Management.ManagementObject partition in
                new System.Management.ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"]
                      + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (System.Management.ManagementObject disk in
                 new System.Management.ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                          + partition["DeviceID"]
                          + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        //Получение буквы устройства
                        diskName = disk["Name"].ToString().Trim();
                    }
                }
                decimal dSize = Math.Round((Convert.ToDecimal(
              new System.Management.ManagementObject("Win32_LogicalDisk.DeviceID='"
                      + diskName + "'")["Size"]) / 1073741824), 2);
                comboSource.Add(DI.Count.ToString(), diskName + drive["Model"].ToString().Trim() + " (" + dSize.ToString() + ") GB");
                DI.Add(new DiskInfo("", drive["Model"].ToString().Trim(), dSize, drive["PNPDeviceID"].ToString().Trim()));
            }

        }
        /// <summary>
        /// Отслеживаем подключение накопителей
        /// </summary>
        void USBInsert()
        {
            ManagementEventWatcher watcherRemove = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            watcherRemove.EventArrived += new EventArrivedEventHandler(DeviceUpToDate);
            watcherRemove.Query = query;
            watcherRemove.Start();
            watcherRemove.WaitForNextEvent();
        }
        /// <summary>
        /// Отслеживаем изъятие накопителей
        /// </summary>
        void USBRemove()
        {
            ManagementEventWatcher watcherInsert = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            watcherInsert.EventArrived += new EventArrivedEventHandler(DeviceUpToDate);
            watcherInsert.Query = query;
            watcherInsert.Start();
            watcherInsert.WaitForNextEvent();
        }
        /// <summary>
        /// Инициализация формы,
        /// запуск потоков проверки флеш накопителей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DI = new List<DiskInfo>();
            DiskBox.DisplayMember = "Value";
            DiskBox.ValueMember = "Key";
            comboSource = new Dictionary<string, string>();
            ReadUSBFlashDrivers(comboSource);
            DiskBox.DataSource = new BindingSource(comboSource, null);
            System.Threading.Thread watcherInsert = new System.Threading.Thread(USBInsert);
            watcherInsert.Start();
            System.Threading.Thread watcherRemove = new System.Threading.Thread(USBRemove);
            watcherRemove.Start();
        }
    }
}
