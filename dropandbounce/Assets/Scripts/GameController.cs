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
	}

	public PlayerController player;
	public StageController stage;
	public UIController ui;
	public RankingController ranking;

	void Start()
	{
		_init();
	}
	/// <summary>
	/// initialize
	/// </summary>
	private void _init()
	{
		stage.Init();
		// プレイヤー初期化
		player.Init(() => _ready(), () => _gameOver());
		player.Ready();
		state = State.InitPlayer;
		ui.SetDrops(player.drops);
		// ランキング情報取得
		ranking.RefreshScoreList();
	}

	private void _ready()
	{
		ui.ShowDesc();
		ui.ShowRanking();
		state = State.Ready;
	}

	private void _startGame()
	{
		ui.HideDesc();
		ui.HideRanking();
		state = State.Playing;
		player.Drop();
		ui.SetDrops(player.drops);
	}

	/// <summary>
	/// gameover
	/// </summary>
	private void _gameOver()
	{
		state = State.GameOver;
		player.Lock();

		// TODO
		ranking.SendScore(player.distance);

		// ランキング情報更新
		ranking.RefreshScoreList();
		StartCoroutine(_restart());
	}
	private IEnumerator _restart()
	{
		yield return new WaitForSeconds(1f);
		// restart
		player.Ready();
		state = State.InitPlayer;
		ui.SetDrops(player.drops);
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
				ui.SetScore(player.distance);
				if (Input.GetKeyDown("s"))
				{
					// debug
				}
				if (Input.GetKeyDown("space"))
				{
					player.Drop();
					ui.SetDrops(player.drops);
				}
				break;
		}
	}
}
