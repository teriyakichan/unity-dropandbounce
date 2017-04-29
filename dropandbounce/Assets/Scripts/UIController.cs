using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public GameObject descLabel;
	public UnityEngine.UI.Text scoreLabel;

	public void Init()
	{

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

	// Update is called once per frame
	void Update()
	{

	}
}
