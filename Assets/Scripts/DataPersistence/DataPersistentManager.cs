using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistentManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "data.json";

    GameData gameData;
    private List<IDataPersistence> dataPersistanceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistentManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();

    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
            NewGame();

        foreach (IDataPersistence dataPersistenceObj in dataPersistanceObjects)
            dataPersistenceObj.LoadData(gameData);
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistanceObjects)
            dataPersistenceObj.SaveData(ref gameData);

        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistanceObjects);
    }

    [ContextMenu("Delete All Data")]
    public void DeleteAllData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataHandler.DeleteAllData();
    }

    private void OnApplicationQuit()
    {
       // SaveGame();
    }
}
