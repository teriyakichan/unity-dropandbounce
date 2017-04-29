using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Transform cameraTrans;
	public GameObject marker;
	private Rigidbody2D rigid;

	// callback
	private Action _readyCallback; // 準備できた
	private Action _deadCallback;  // しんだ

	private bool _markerVisible = false;

	/// <summary>
	/// initialize
	/// </summary>
	/// <param name="deadCallback"></param>
	public void Init(Action readyCallback, Action deadCallback)
	{
		rigid = GetComponent<Rigidbody2D>();
		_deadCallback = deadCallback;
		_readyCallback = readyCallback;
		marker.SetActive(false);
		Lock();
	}
	public void Lock()
	{
		rigid.gravityScale = 0f;
		rigid.velocity = Vector2.zero;
	}
	public void Unlock()
	{
		rigid.gravityScale = 1f;
	}

	/// <summary>
	/// check position
	/// </summary>
	private void _checkAlive()
	{
		if (cameraTrans.localPosition.x - 10 > transform.localPosition.x)
		{
			_deadCallback.Invoke();
			rigid.velocity = Vector2.zero;
		}
		if (transform.localPosition.y <= -6f)
			_deadCallback.Invoke();
	}

	/// <summary>
	/// ゲーム開始準備
	/// </summary>
	public void Ready()
	{
		transform.localPosition = new Vector2(cameraTrans.localPosition.x - 10f, 3f);
	}

	/// <summary>
	/// stop & drop
	/// </summary>
	public void Drop()
	{
		rigid.gravityScale = 1f;
		rigid.velocity = Vector2.zero;
		rigid.AddForce(new Vector2(0, -1500), ForceMode2D.Force);
	}

	public void FollowPlayer()
	{
		cameraTrans.localPosition = new Vector2(transform.localPosition.x + 5, 0);
	}

	// マーカー表示/非表示
	private void _showMarker()
	{
		if (_markerVisible) return;
		_markerVisible = !_markerVisible;
		marker.SetActive(true);
		marker.transform.localPosition = new Vector2(transform.localPosition.x, 4.5f);
	}
	private void _hideMarker()
	{
		if (!_markerVisible) return;
		_markerVisible = !_markerVisible;
		marker.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		switch (GameController.state)
		{
			case GameController.State.InitPlayer:
				transform.localPosition += new Vector3(0.5f, 0f);
				if (transform.localPosition.x >= cameraTrans.localPosition.x -5f) {
					_readyCallback.Invoke();
				}
				break;
			case GameController.State.Ready:
				transform.localPosition += new Vector3(0.5f, 0f);
				FollowPlayer();
				break;
			case GameController.State.Playing:
				marker.transform.localPosition = new Vector2(transform.localPosition.x, 4.5f);
				// カメラ追従
				if (rigid.velocity.x > 0)
					FollowPlayer();
				// 範囲外マーカー
				if (transform.localPosition.y > 5.5f)
				{
					_showMarker();
				}
				else
				{
					_hideMarker();
				}
				// 死亡判定
				_checkAlive();
				break;
		}
	}
}
