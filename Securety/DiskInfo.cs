﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class DiskInfo
    {
        string _Letter;
        string _Model;
        decimal _DiskSize;
        string _SerialNumber;
        /// <summary>
        /// Кодирование строки
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>закодированная информация</returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// Декодирование Base64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns>раскодированная информация</returns>
        private string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// Разбавление мусором данных
        /// </summary>
        /// <param name="Hash">Строка для разбавления</param>
        /// <returns></returns>
        private string garbageAdditor(string Hash)
        {
            Random rnd = new Random();
            for (int i = 0; i < Hash.Length; i += 2)
                Hash = Hash.Insert(i, Convert.ToChar(97 + rnd.Next(25)).ToString());
            return Hash;
        }
        /// <summary>
        /// Сбор мусора из строки
        /// </summary>
        /// <param name="garbagedHash">Строка с мусором</param>
        /// <returns></returns>
        private string garbageCollector(string garbagedHash)
        {
            int hashLength = garbagedHash.Length;
            for (int i = hashLength - 2; i >= 0; i -= 2)
                garbagedHash = garbagedHash.Remove(i, 1);
            return garbagedHash;
        }
        public DiskInfo(string Letter, string Model, decimal DiskSize, string DeviceID)
        {
            _Letter = Letter;
            _Model = Model;
            _DiskSize = DiskSize;
            _SerialNumber = parseSerialFromDeviceID(DeviceID);
        }
        /// <summary>
        /// Создание файла, ключа-лицензии на флеш накопителе
        /// </summary>
        /// <param name="Properties">Параметры</param>
        /// <returns></returns>
        public bool CreateKey(string Owner)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_Letter + "\\Licence.key", FileMode.Create)))
            {
                writer.Write(Base64Encode(Base64Encode(garbageAdditor(_SerialNumber))));
                writer.Write(Base64Encode(Base64Encode(garbageAdditor(_Model))));
                writer.Write(Base64Encode(Base64Encode(garbageAdditor(Owner))));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Проверка целостности ключа
        /// </summary>
        /// <param name="Path">Путь к ключу</param>
        /// <returns>Список параметров или информацию, почему ключ не действителен</returns>
        public string CheckKey(string Path)
        {
            if (File.Exists(Path))
            {
                try
                {
                    using (BinaryReader reader = new BinaryReader(File.OpenRead(Path)))
                    {
                        //List<string> Params = new List<string>();
                        if (garbageCollector(Base64Decode(Base64Decode(reader.ReadString()))) == _SerialNumber &&
                            garbageCollector(Base64Decode(Base64Decode(reader.ReadString()))) == _Model)
                            return garbageCollector(Base64Decode(Base64Decode(reader.ReadString())));
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return "";
        }
        public string Letter
        {
            get { return _Letter; }
        }
        private string parseSerialFromDeviceID(string deviceId)
        {
            string[] splitDeviceId = deviceId.Split('\\');
            string[] serialArray;
            string serial;
            int arrayLen = splitDeviceId.Length - 1;

            serialArray = splitDeviceId[arrayLen].Split('&');
            serial = serialArray[0];

            return serial;
        }
    }
}
