//#define SAVE_USERINFO
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static State state;
	public enum State
	{
		InputName,  // 名前入力
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
		ui.Init((name) => _inputNameFinished(name));
		ui.SetDrops(player.drops);
		// プレイヤー初期化
		player.Init(
			() => _ready(),
			() => _gameOver(),
			(item) => _getItem(item)
			);
		// ユーザ名取得
#if SAVE_USERINFO
		string userName = PlayerPrefs.GetString("name");
#else
		string userName = RankingController.userName;
#endif
		if (string.IsNullOrEmpty(userName))
		{
			state = State.InputName;
			ui.ShowTitle();
			ui.HideDesc();
		}
		else
		{
			ui.HideTitle();
			player.Ready();
			state = State.InitPlayer;
		}
		// ランキング情報取得
		ranking.RefreshScoreList();
	}

	private void _inputNameFinished(string name)
	{
		Debug.Log("name : " + name);
#if SAVE_USERINFO
		PlayerPrefs.SetString("name", name);
#else
		RankingController.userName = name;
#endif
		ui.HideTitle();
		player.Ready();
		state = State.InitPlayer;
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

		// スコア送信
		ranking.SendScore(player.distance);

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

	private void _getItem(GameObject item)
	{
		stage.ResetItem(item);
		player.Recover();
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
