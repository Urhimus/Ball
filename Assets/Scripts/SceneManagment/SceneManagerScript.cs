using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagerScript: MonoBehaviour
{
    public static SceneManagerScript instance;
    public static event Action sceneAsyncLoadingBegan;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string index)
    {
        StartCoroutine(LoadAsyncScene(index));
    }

    IEnumerator LoadAsyncScene (string index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        if (asyncLoad != null)
            sceneAsyncLoadingBegan?.Invoke();

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnDisable()
    {
        sceneAsyncLoadingBegan = null;
    }
}
