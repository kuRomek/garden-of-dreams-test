using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MapSerializer
{
    private const string FileName = "CurrentMap";
    private static string MapsPath = Application.persistentDataPath + "/Maps/";

    public static void SaveMap(GridData gridData)
    {
        if (Directory.Exists(MapsPath) == false)
            Directory.CreateDirectory(MapsPath);

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file;
        string filePath = MapsPath + FileName + ".dat";

        if (Directory.Exists(filePath))
            file = File.Open(MapsPath + FileName + ".dat", FileMode.Create);
        else
            file = File.Create(MapsPath + FileName + ".dat");

        bf.Serialize(file, gridData);
        file.Close();
    }

    public static GridData LoadMap()
    {
        if (File.Exists(MapsPath + FileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(MapsPath + FileName + ".dat", FileMode.Open);
            file.Position = 0;
            GridData data = bf.Deserialize(file) as GridData;
            file.Close();
            Debug.Log("Game data loaded!");
            return data;
        }
        else
        {
            return null;
        }
    }
}