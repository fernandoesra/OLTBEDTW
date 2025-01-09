using SQLite4Unity3d;
using UnityEngine;
// using Unity.VisualScripting.Dependencies.Sqlite;
using SQLiteConnection = SQLite4Unity3d.SQLiteConnection;
using System.IO;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class OLTtDataService
{

	string AbsolutePath;
	private SQLiteConnection _connection;

	public OLTtDataService()
	{
		string DatabaseName = "oltbedtwfinal.db";

#if UNITY_EDITOR
		// var dbPath = Path.Combine(Application.streamingAssetsPath, DatabaseName);
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		// DeleteIfExists(dbPath);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
		// DeleteIfExists(filepath);
        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
		_connection = new SQLiteConnection(dbPath, SQLite4Unity3d.SQLiteOpenFlags.ReadWrite | SQLite4Unity3d.SQLiteOpenFlags.Create);
		// Debug.Log("Final PATH: " + dbPath);
		AbsolutePath = dbPath;
	}
	
	private void DeleteIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            // Debug.Log("Deleted existing database file: " + filePath);
        }
    }
	public string ShowPath()
	{
		return AbsolutePath;
	}

	public SQLiteConnection GetConnection()
	{
		return _connection;
	}

}
