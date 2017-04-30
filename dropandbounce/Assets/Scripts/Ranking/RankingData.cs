public class RankingData
{
	public int rank;
	public float score;
	public string name;

	public string fixedScore
	{
		get
		{
			return string.Format("{0:0.00}", score);
		}
	}
}
