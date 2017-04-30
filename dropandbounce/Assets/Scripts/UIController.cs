using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI表示制御
/// </summary>
public class UIController : MonoBehaviour
{
	public GameObject title;
	public GameObject descLabel;
	public UnityEngine.UI.Text scoreLabel;
	public GameObject rankingList;
	// 名前入力
	public UnityEngine.UI.InputField nameInputBox;
	public UnityEngine.UI.Button nameSubmitButton;

	public List<UnityEngine.UI.Image> dropImages;

	private Action<string> _inputNameCallback;

	public void Init(Action<string> inputNameCallback)
	{
		_inputNameCallback = inputNameCallback;
		nameSubmitButton.onClick.AddListener(_inputName);
	}

	private void _inputName()
	{
		_inputNameCallback(nameInputBox.text);
	}

	// タイトル表示切替
	public void ShowTitle()
	{
		title.SetActive(true);
	}
	public void HideTitle()
	{
		title.SetActive(false);
	}

	// ランキング表示切替
	public void ShowRanking()
	{
		rankingList.SetActive(true);
	}
	public void HideRanking()
	{
		rankingList.SetActive(false);
	}

	// Space to Dropの表示切替
	public void ShowDesc()
	{
		descLabel.SetActive(true);
	}
	public void HideDesc()
	{
		descLabel.SetActive(false);
	}

	public void SetScore(float score)
	{
		scoreLabel.text = string.Format("{0:0.00}", score);
	}

	public void SetDrops(int drops)
	{
		for (int i = 0; i < dropImages.Count; ++i)
		{
			if (i < drops)
				dropImages[i].color = new Color(1f, 1f, 1f, 1f);
			else
				dropImages[i].color = new Color(0f, 0f, 0f, 0.25f);
		}
	}
}
