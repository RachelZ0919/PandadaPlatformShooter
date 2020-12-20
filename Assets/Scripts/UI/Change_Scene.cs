using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Change_Scene : MonoBehaviour
{
	public float fadeSpeed = 1.0f;      //渐变速度
	public bool sceneStarting = true;   //游戏是否开始(或者其他控制标志)
	private Image rawImage = null;      //RawImage对象
	public int scene_id = 1;

	public void ImageClear()
    {
		rawImage.color = Color.clear;
    }

	private void Awake()
	{
		rawImage = this.GetComponent<Image>();
	}

	//控制rawImage颜色变化
	private void FadeToClear()
	{
		rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
		// rawImage.color=Color.clear;
	}
	private void FadeToBlack()
	{
		rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
		// rawImage.color=Color.clear;
	}

	//开始和结束场景
	private void StartScene()
	{
		FadeToClear();
		if (rawImage.color.a < 0.05f)
		{
			rawImage.color = Color.clear;
			// rawImage.enable=false;
			// sceneStarting=false;
		}
	}
	private void EndScene()
	{
		// rawImage.enable=true;
		FadeToBlack();
		if (rawImage.color.a >= 0.95f)
		{
			SceneManager.LoadScene(scene_id);
		}
	}

	//开始游戏调用
	private void Update()
	{
		if (sceneStarting)
		{
			StartScene();
		}
		else
		{
			EndScene();
		}
	}
}
