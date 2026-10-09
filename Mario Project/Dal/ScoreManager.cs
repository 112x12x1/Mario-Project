using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarioGameSystem.DAL
{
    public class ScoreRecord
    {
        public string PlayerName { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime PlayDate { get; set; }
    }

    public class ScoreManager
    {
        private readonly string filePath;

        public ScoreManager(string path)
        {
            filePath = path;
        }

        public void SaveScore(string name, int score)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) throw new Exception("Invalid player name.");
                if (score < 0) throw new Exception("Invalid score.");

                string line = $"{name.Trim()},{score},{DateTime.Now:yyyy-MM-dd HH:mm:ss}" + Environment.NewLine;
                File.AppendAllText(filePath, line);
            }
            catch (Exception ex)
            {
                throw new Exception("Save score failed: " + ex.Message);
            }
        }

        public List<ScoreRecord> GetTopScores(int top)
        {
            var list = new List<ScoreRecord>();
            try
            {
                if (!File.Exists(filePath)) return list;

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split(',');
                    if (parts.Length >= 3 && int.TryParse(parts[1], out int sc))
                    {
                        list.Add(new ScoreRecord
                        {
                            PlayerName = parts[0],
                            Score = sc,
                            PlayDate = DateTime.Parse(parts[2])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Read scores failed: " + ex.Message);
            }

            return list.OrderByDescending(x => x.Score).Take(top).ToList();
        }
    }
}