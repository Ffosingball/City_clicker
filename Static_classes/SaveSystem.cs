using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(int saveNum, GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create path to the file
        //Application.persistentDataPath is useful to create files for crossplatforms games
        //After add name and extension of the binary file
        string path = Application.persistentDataPath + "/game"+saveNum+".vvg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static GameData LoadGame(int saveNum)
    {
        string path = Application.persistentDataPath + "/game"+saveNum+".vvg";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("File does not exist "+path);
            return null;
        }
    }


    public static void SaveNames(string[] names)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create path to the file
        //Application.persistentDataPath is useful to create files for crossplatforms games
        //After add name and extension of the binary file
        string path = Application.persistentDataPath + "/gameNames.vvg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, names);
        stream.Close();
    }


    public static string[] LoadNames()
    {
        string path = Application.persistentDataPath + "/gameNames.vvg";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            string[] names = formatter.Deserialize(stream) as string[];
            stream.Close();
            return names;
        }
        else
        {
            Debug.Log("File does not exist "+path);
            return null;
        }
    }


    public static void DeleteSave(int saveNum)
    {
        string path = Application.persistentDataPath + "/game"+saveNum+".vvg";
        // Delete the file
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File deleted successfully.");
        }
        else
            Debug.Log("File does not exist.");
    }



    public static void SaveSettings()
    {
        SettingsInfoKeeper info = SettingsInfo.getAllInfo();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.vvg";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, info);
        stream.Close();
    }


    public static SettingsInfoKeeper LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.vvg";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            SettingsInfoKeeper info = formatter.Deserialize(stream) as SettingsInfoKeeper;
            stream.Close();
            return info;
        }
        else
        {
            Debug.Log("File does not exist "+path);
            return null;
        }
    }
}
