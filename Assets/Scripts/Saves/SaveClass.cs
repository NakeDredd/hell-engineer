using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveClass
{
    public static void Save<T> (string name, T serializableObj)
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream fs = new FileStream("Saves/" + name + ".dat", FileMode.OpenOrCreate))
        {
            bf.Serialize(fs, serializableObj);
        }
    }
    public static T GetSave<T>(string name)
    {
        if (!File.Exists("Saves/" + name + ".dat"))
        {
            return default(T);
        }

        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream fs = new FileStream("Saves/" + name + ".dat", FileMode.OpenOrCreate))
        {
            return (T)bf.Deserialize(fs);
        }
    }
}

public class SerializableObject
{
}
