using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class RankingView : MonoBehaviour, IEnhancedScrollerDelegate
{
	private SmallList<RankingData> _dataList = new SmallList<RankingData>();
	public EnhancedScroller scroller;
	public EnhancedScrollerCellView cellViewPrefab;

	void Start()
	{
		scroller.Delegate = this;
	}

	public void LoadData(SmallList<RankingData> dataList)
	{
		_dataList = dataList;
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