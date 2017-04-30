//#define SAVE_USERINFO
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI;
using MiniJSON;

public class RankingController : MonoBehaviour
{
	internal readonly string SERVER_URL = "";
	internal readonly string API_GETRANKING = "get_ranking.php";
	internal readonly string API_ADDSCORE   = "add_score.php";

	public RankingView rankingView;

	public static string userCode = null;
	public static string userName = null;

	// スコア送信
	public void SendScore(float score)
	{
		StartCoroutine(_sendScore(score));
	}
	private IEnumerator _sendScore(float score)
	{
#if SAVE_USERINFO
		string code = PlayerPrefs.GetString("user_code", null);
#else
		string code = userCode;
#endif
		if (string.IsNullOrEmpty(userCode)) code = _generateUserCode();

		WWWForm form = new WWWForm();
		form.AddField("user_code", code);
#if SAVE_USERINFO
		form.AddField("name", PlayerPrefs.GetString("name"));
#else
		form.AddField("name", userName);
#endif
		form.AddField("score", score.ToString());
		using (WWW www = new WWW(SERVER_URL + API_ADDSCORE, form))
		{
			yield return www;
			yield return new WaitWhile(() => !www.isDone);
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.Log(www.error);
			}
			else
			{
				string res = www.text;
				if (!string.IsNullOrEmpty(res))
					rankingView.LoadData(_parseRankingJson(res));
			}
		}
	}

	// スコアリスト更新
	public void RefreshScoreList()
	{
		StartCoroutine(_refreshRanking());
	}
	private IEnumerator _refreshRanking()
	{
		// ランキングをリクエスト
		string res = null;
		using (WWW www = new WWW(SERVER_URL + API_GETRANKING))
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
				Debug.Log(www.error);
			else
				res = www.text;
		}
		if (res == null) yield break;

		rankingView.LoadData(_parseRankingJson(res));
	}

	private SmallList<RankingData> _parseRankingJson(string jsonString)
	{
		// parse json
		var rankingData = new SmallList<RankingData>();
		var json = Json.Deserialize(jsonString) as List<object>;

		string score;
		int rank;
		int lastRank = 1;
		string lastScore = "";
		for (int i = 0; i < json.Count; ++i)
		{
			var row = json[i] as Dictionary<string, object>;
			// 順位計算
			score = row["score"].ToString();
			if (score == lastScore)
			{
				rank = lastRank;
			}
			else
			{
				rank = i + 1;
				lastRank = i + 1;
			}
			lastScore = score;
			// リストに格納
			rankingData.Add(new RankingData() {
				rank = rank,
				name = row["name"].ToString(),
				score = float.Parse(score),
			});
		}
		return rankingData;
	}

	// なんちゃってユニークキー
	private string _generateUserCode()
	{
		string code = UnityEngine.Random.Range(0, 10000).ToString();
		code += DateTime.Now.ToString("yyyyMMddHHmmssfff");
		code += UnityEngine.Random.Range(0, 10000).ToString();
		code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
#if false
		PlayerPrefs.SetString("user_code", code);
#else
		userCode = code;
#endif
		return code;
	}
}
