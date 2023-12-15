public class ScoreManager
{
    private int score;

    public int Score => score;

    public ScoreManager()
    {
        score = 0;
    }

    public void IncreaseScore(int points)
    {
        score += points;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
