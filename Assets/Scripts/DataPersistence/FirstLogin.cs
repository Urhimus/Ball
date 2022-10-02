using UnityEngine;

public class FirstLogin : MonoBehaviour
{
    private void Start()
    {
        DataPersistentManager.instance.SaveGame();
    }
}
