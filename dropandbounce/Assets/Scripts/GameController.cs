using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static State state;
	public enum State
	{
		InitPlayer, // プレイヤーを初期座標に配置
		Ready,      // プレイ可能
		Playing,    // プレイ中
		GameOver,   // ゲームオーバー
		Score,   // ゲームオーバー
	}

	public PlayerController player;

	void Start()
	{
		_init();
	}
	/// <summary>
	/// initialize
	/// </summary>
	private void _init()
	{
		Application.targetFrameRate = 60;
		player.Init(() => _ready(), () => _gameOver());
		player.Ready();
		state = State.InitPlayer;
	}

	private void _ready()
	{
		state = State.Ready;
	}

	private void _startGame()
	{
		state = State.Playing;
		player.Drop();
	}

	/// <summary>
	/// gameover
	/// </summary>
	private void _gameOver()
	{
		Debug.Log("gameover");
		state = State.GameOver;
		player.Lock();
	}

	void Update()
	{
		switch (state)
		{
			case State.Ready:
				if (Input.GetKeyDown("space"))
				{
					_startGame();
				}
				break;
			case State.Playing:
				if (Input.GetKeyDown("s"))
				{
					// debug
				}
				if (Input.GetKeyDown("space"))
				{
					player.Drop();
				}
				break;
		}
	}
}
