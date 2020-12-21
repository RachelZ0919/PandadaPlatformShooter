using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class quit_in_game : MonoBehaviour
{
    Button quit_button;
    // Start is called before the first frame update
    void Start()
    {
        quit_button=this.GetComponent<Button>();
        quit_button.onClick.AddListener(toGameNode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void toGameNode()
    {
        switchScene("GameNode");
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
