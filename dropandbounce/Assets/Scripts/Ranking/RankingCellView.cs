using UnityEngine;
using EnhancedUI.EnhancedScroller;

public class RankingCellView : EnhancedScrollerCellView
{
	public UnityEngine.UI.Text rankLabel;
	public UnityEngine.UI.Text nameLabel;
	public UnityEngine.UI.Text scoreLabel;

	public void SetData(RankingData data)
	{
		rankLabel.text = data.rank.ToString();
		nameLabel.text = data.name;
		scoreLabel.text = data.fixedScore;
	}
}