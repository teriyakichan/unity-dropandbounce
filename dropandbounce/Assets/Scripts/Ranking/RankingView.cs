using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class RankingView : MonoBehaviour, IEnhancedScrollerDelegate
{
	private SmallList<RankingData> _dataList;
	public EnhancedScroller scroller;
	public EnhancedScrollerCellView cellViewPrefab;

	void Start()
	{
		scroller.Delegate = this;
		LoadLargeData();
	}

	private void LoadLargeData()
	{
		_dataList = new SmallList<RankingData>();
		for (var i = 0; i < 1000; i++)
			_dataList.Add(new RankingData(){ rank = i + 1, name = "hoge" + i, score = (float)(i * 12.24f) });
		scroller.ReloadData();
	}

	public int GetNumberOfCells(EnhancedScroller scroller)
	{
		return _dataList.Count;
	}

	public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
	{
		return 12f;
	}

	public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		RankingCellView cellView = scroller.GetCellView(cellViewPrefab) as RankingCellView;

		cellView.name = "Cell " + dataIndex.ToString();
		cellView.SetData(_dataList[dataIndex]);
		return cellView;
	}
}