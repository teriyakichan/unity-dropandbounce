using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI表示制御
/// </summary>
public class UIController : MonoBehaviour
{
	public GameObject descLabel;
	public UnityEngine.UI.Text scoreLabel;
	public GameObject rankingList;

	public List<UnityEngine.UI.Image> dropImages;

	public void Init()
	{

	}

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

	// Update is called once per frame
	void Update()
	{

	}
}
