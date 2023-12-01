using System.IO;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Service for file management and directory
    /// </summary>
    public class FileService
    {
        //Save information to a file. If the file doesn't exist, it will be created
        public void SaveToFile(string[] value, string fileName)
        {
            string destination = Application.persistentDataPath + "/" + fileName;
            Debug.Log(destination);

            if (File.Exists(fileName) == false)
            {
                File.WriteAllText(destination, "");
            }
            
            File.AppendAllLines(destination, value);
        }
        
        //Check if the file exists
        public bool IfFileExists(string fileName) => File.Exists(fileName);
       
        //Retrieve data from the file
        public string GetFileData(string fileName)
        {
            string destination = Application.persistentDataPath + "/" + fileName;
            return File.ReadAllText(destination);
        }
    }
}