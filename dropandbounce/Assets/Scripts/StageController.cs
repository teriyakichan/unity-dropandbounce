using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
	internal const int BLOCK_COUNT = 10;
	internal const float PLACE_OFFSET_X = 15f;
	internal const float MIN_DISTANCE = 10f;

	public Transform cameraTrans;
	public GameObject blockOriginal;

	// 床オブジェクトリスト
	private List<Transform> _blocks = new List<Transform>();
	// 床オブジェクトx座標
	private List<float> _blockXList = new List<float>();

	// angle 30 - 60
	// y -6.5 - -8.5

	// 現在の先頭ブロック座標
	private float _frontXPosition = PLACE_OFFSET_X;

	public void Init()
	{
		// ブロック生成
		_blocks.Add(blockOriginal.transform);
		_blockXList.Add(-999f);
		for (int i = 0; i < BLOCK_COUNT - 1; ++i)
		{
			GameObject obj = Instantiate(blockOriginal);
			obj.transform.parent = transform;
			_blocks.Add(obj.transform);
			_blockXList.Add(-999f);
		}
	}

	// カメラ移動に応じて配置更新
	public void Refresh()
	{
		// 画面外(左側)オブジェクトを右に配置
		for (int i = 0; i < _blockXList.Count; ++i) {
			if (_blockXList[i] < cameraTrans.localPosition.x - PLACE_OFFSET_X)
			{
				_place(i);
			}
		}
	}

	// ブロック配置
	private void _place(int index)
	{
		float x = _frontXPosition + Random.Range(3f, 15f);
		float y = Random.Range(-6.5f, -8.5f);
		_blockXList[index] = x;
		_blocks[index].transform.localPosition = new Vector3(x, y);
		_blocks[index].transform.FindChild("Cube").transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(30f, 60f));
		_frontXPosition = x;
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
