using System;
using MarioGameSystem.DAL;

namespace MarioGameSystem.BLL
{
    public class GameService
    {
        private readonly ScoreManager scoreManager;

        public GameService(string scoreFilePath)
        {
            scoreManager = new ScoreManager(scoreFilePath);
        }

        public int CalculateCoinsEarned(int score, int remainingTime)
        {
            if (score < 0 || remainingTime < 0) return 0;
            int baseCoins = score / 10;
            int timeBonus = remainingTime / 10;
            return baseCoins + timeBonus;
        }

        public bool CompleteLevel(string playerName, int score, int remainingTime, out int coinsEarned, out string message)
        {
            coinsEarned = 0;
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(playerName) || score < 0)
            {
                message = "Invalid input parameter.";
                return false;
            }

            try
            {
                coinsEarned = CalculateCoinsEarned(score, remainingTime);
                scoreManager.SaveScore(playerName, score);
                message = $"Level Completed! Earned {coinsEarned} coins.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
                return false;
            }
        }
    }
}