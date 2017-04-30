using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
	internal const float PLACE_OFFSET_X = 15f;
	internal const float ITEM_PLACE_OFFSET_X = 45f;

	internal const int BLOCK_COUNT = 10;
	internal const float MIN_DISTANCE = 10f;

	public Transform cameraTrans;
	public GameObject blockOriginal;
	public GameObject itemOriginal;

	// ブロック数 = アイテム数

	// 床オブジェクトリスト
	private List<Transform> _blocks = new List<Transform>();
	// 床オブジェクトx座標
	private List<float> _blockXList = new List<float>();

	// アイテムオブジェクトリスト
	private List<Transform> _items = new List<Transform>();
	// アイテムオブジェクトx座標
	private List<float> _itemXList = new List<float>();

	// angle 30 - 60
	// y -6.5 - -8.5

	// 現在の先頭ブロック座標
	private float _frontXPosition = PLACE_OFFSET_X;
	private float _itemFrontXPosition = ITEM_PLACE_OFFSET_X;

	public void Init()
	{
		// ブロック&アイテム生成
		_blocks.Add(blockOriginal.transform);
		_blockXList.Add(-999f);
		_items.Add(itemOriginal.transform);
		_itemXList.Add(-999f);
		GameObject obj;
		for (int i = 1; i < BLOCK_COUNT; ++i)
		{
			// block
			obj = Instantiate(blockOriginal);
			obj.transform.parent = transform;
			_blocks.Add(obj.transform);
			_blockXList.Add(-999f);
			// item
			obj = Instantiate(itemOriginal);
			obj.transform.parent = transform;
			obj.GetComponent<DropItemController>().index = i;
			_items.Add(obj.transform);
			_itemXList.Add(-999f);
		}
	}

	// カメラ移動に応じて配置更新
	public void Refresh()
	{
		// 画面外(左側)オブジェクトを右に配置
		for (int i = 0; i < BLOCK_COUNT; ++i) {
			if (_blockXList[i] < cameraTrans.localPosition.x - PLACE_OFFSET_X)
				_placeBlock(i);
			if (_itemXList[i] < cameraTrans.localPosition.x - PLACE_OFFSET_X)
				_placeItem(i);
		}
	}

	// ブロック配置
	private void _placeBlock(int index)
	{
		float x = _frontXPosition + Random.Range(3f, PLACE_OFFSET_X);
		float y = Random.Range(-6.5f, -8.5f);
		_blockXList[index] = x;
		_blocks[index].transform.localPosition = new Vector3(x, y);
		_blocks[index].transform.FindChild("Cube").transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(30f, 60f));
		_frontXPosition = x;
	}

	// アイテム配置
	private void _placeItem(int index)
	{
		float x = _itemFrontXPosition + Random.Range(15f, ITEM_PLACE_OFFSET_X);
		float y = Random.Range(-1f, 2f);
		_itemXList[index] = x;
		_items[index].transform.localPosition = new Vector3(x, y);
		_itemFrontXPosition = x;
	}

	public void ResetItem(GameObject item)
	{
		Debug.Log("reset " + item);
		int index = item.GetComponent<DropItemController>().index;
		item.transform.localPosition = new Vector2(0, -999);
		_itemXList[index] = -999f;
	}

	// Update is called once per frame
	void Update()
	{
		switch (GameController.state)
		{
			case GameController.State.InitPlayer:
			case GameController.State.Ready:
			case GameController.State.Playing:
				Refresh();
				break;
		}
	}
}
