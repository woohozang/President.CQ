using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
	static GameObject _container;
	static GameObject Container
	{
		get
		{
			return _container;
		}
	}
	static DataController _instance;
	public static DataController Instance
	{
		get
		{
			if (!_instance)
			{
				_container = new GameObject();
				_container.name = "DataController";
				_instance = _container.AddComponent(typeof(DataController)) as DataController;
				DontDestroyOnLoad(_container);
			}
			return _instance;
		}
	}

	public string GameDataFileName = "GameData.json";
	public GameData _gameData;
	public GameData gameData
	{
		get
		{
			if (_gameData == null)
			{
				LoadGameData();
				SaveGameData();
			}
			return _gameData;
		}
	}
	private void Start()
	{
		LoadGameData();
		SaveGameData();
	}

	public void LoadGameData()
	{
		string filePath = Application.persistentDataPath + GameDataFileName;

		if(File.Exists(filePath))
		{
			Debug.Log("game data load");
			string FromJsonData = File.ReadAllText(filePath);
			_gameData = JsonUtility.FromJson<GameData>(FromJsonData);
		}
		else
		{
			Debug.Log("new file");
			_gameData = new GameData();
		}
	}

	public void SaveGameData()
	{
		string ToJsonData = JsonUtility.ToJson(gameData);
		string filepath = Application.persistentDataPath + GameDataFileName;
		File.WriteAllText(filepath, ToJsonData);
		Debug.Log("game data save");
	}

	private void OnApplicationQuit()
	{
		SaveGameData();
	}
}
