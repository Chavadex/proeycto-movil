using chava.entities;
using System.Collections.Generic;

namespace chava.domain
{
    public interface ILocalDataAccess
    {
        bool IsNewUser();
        void SetDefaultConfig();
        void SetHighScore(string name, int score);
        LeaderBoardData GetHighScores();
        void Flush(); //flush 
    }


}
