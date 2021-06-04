using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadSystem
{

    public static void SaveInventory(InventoryManager inventoryManager)
    {

        string path = Application.persistentDataPath + "/inv.dd";
        Inventory inventory = new Inventory(inventoryManager);
        string jsonString = JsonUtility.ToJson(inventory);

        BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create));

        writer.Write(jsonString);
        writer.Close();
    }

    public static Inventory LoadInventory()
    {
        string path = Application.persistentDataPath + "/inv.dd";

        if (File.Exists(path))
        {
            BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));

            Inventory inventory = JsonUtility.FromJson<Inventory>(reader.ReadString());
            reader.Close();

            return inventory;
        }
        else
        {
            //Debug.Log("No save file found");
            return new Inventory(InventoryManager.current);
        }
    }


    /* DONT USE BINARY FORMATTER
    public static void SaveInventory2(InventoryManager inventoryManager)
    {

        string path = Application.persistentDataPath + "/inventory.dd";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        Inventory inventory = new Inventory(inventoryManager);

        formatter.Serialize(stream, inventory);
        stream.Close();

    }

    public static Inventory LoadInventory2()
    {
        string path = Application.persistentDataPath + "/inventory.dd";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Inventory inventory = formatter.Deserialize(stream) as Inventory;
            stream.Close();

            return inventory;
        }
        else
        {
            //Debug.Log("No save file found");
            return null;
        }
    }
    */




}

