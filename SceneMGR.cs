using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMGR : MonoBehaviour
{

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void loadStartScene(int index)
    {
        StartCoroutine(loadFirstScene(index));
    }

    private IEnumerator loadFirstScene(int index)
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public static void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
