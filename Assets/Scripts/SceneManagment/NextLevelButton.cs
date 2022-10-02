using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void LoadScene()
    {
        string path = SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

        SceneManagerScript.instance.LoadScene(sceneName);
    }
}
