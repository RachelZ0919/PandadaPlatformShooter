using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class skip_story2 : MonoBehaviour
{
    Button SkipButton;

    // Start is called before the first frame update
    void Start()
    {
        SkipButton=GameObject.Find("Skip_Button").GetComponent<Button>();
        SkipButton.onClick.AddListener(skipStory1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void skipStory1()
    {
        switchScene("Level-1-Start");
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
