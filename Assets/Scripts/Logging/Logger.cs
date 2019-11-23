using System;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static Logger Instance;

    private string path;
    private string file;

    void Awake()
    {
        //if (Instance == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    Instance = this;
        //    DateTime timeNow = DateTime.Now;
        //    string timeStamp = GetFileTimestamp(timeNow);
        //    Instance.path = "Assets/Logging/" + timeStamp + ".txt";
        //    Instance.file = timeStamp + ".txt";
        //    timeStamp = GetTimestamp(timeNow);

        //    using (StreamWriter sw = File.AppendText(Instance.path))
        //    {
        //        sw.WriteLine("Logging Playtest at: " + timeStamp);
        //        sw.WriteLine("---------------------------------");
        //    }
        //}
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}

    }

    private static string GetFileTimestamp(DateTime value)
    {
        return value.ToString(("MMddyyyy_hhmmsstt"));
    }

    public static string GetTimestamp(DateTime value)
    {
        return value.ToString(("MM/dd/yyyy hh:mm:ss tt"));
    }

    public static void WriteToFile(string s)
    {
        string timeStamp = GetTimestamp(DateTime.Now);
        using (StreamWriter sw = File.AppendText(Instance.path))
        {
            sw.WriteLine(timeStamp + ": " + s);
        }
    }
}
