using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_game : MonoBehaviour
{

    Button StartButton;
    // Start is called before the first frame update
    void Start()
    {
         GameObject.Find("Start_Button").GetComponent<Button>().onClick.AddListener(startthegame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startthegame()
    {
        switchScene("Animation2");
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
