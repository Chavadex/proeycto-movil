using System;
using System.Collections.Generic;
using System.Text;

namespace chava.entities
{
    public struct LeaderBoardData
    {
        public List<(string name, int score)> Scores;

        public override string ToString()
        {
            string result = "";
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var score in Scores)
            {
                stringBuilder.Append(score.name).Append(",").Append(score.score).Append("-");
            }
            result = stringBuilder.ToString();
            return result;
        }
    }
}
