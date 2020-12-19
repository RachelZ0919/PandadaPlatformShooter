using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Quit按钮监听事件
/// </summary>

public class DeadWindowButton : MonoBehaviour
{
    private string SceneName;
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        if (btn.name == "PlayAgainButton")
        {
            SceneName = "Scene2";
        }
        else if (btn.name == "QuitButton")
        {
            SceneName = "MainScene";
        }
        btn.onClick.AddListener(button_funtion);
    }

    public void button_funtion()
    {
        Debug.LogWarning("ChangeScene!");
        if (SceneName==null)
        {
            Debug.LogWarning("死亡转场加载失败");
        }
        switchScene(SceneName);
    }
    public void switchScene(string sceneName)
    {
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForEndOfFrame();
        op.allowSceneActivation = true;
    }
}
