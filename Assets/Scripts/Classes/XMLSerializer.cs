using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Xml;

static class XMLSerializer
{
    public static bool Save<T>(T objGraph, string filename) where T : class
    {
        XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
        using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            try
            {
                xmlFormat.Serialize(fStream, objGraph);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static T Load<T>(string filename) where T : class
    {
        XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
        using (Stream fStream = File.OpenRead(filename))
        {
            try
            {
                return (T)xmlFormat.Deserialize(fStream);
            }
            catch
            {
                return null;
            }
        }
    }

    public static T LoadFromResources<T>(string filename) where T : class
    {
        TextAsset textAsset = Resources.Load<TextAsset>(filename);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);

        XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
        using (XmlReader xmlReader = new XmlNodeReader(xmlDocument))
        {
            try
            {
                return (T)xmlFormat.Deserialize(xmlReader);
            }
            catch
            {
                return null;
            }
        }
    }

}
