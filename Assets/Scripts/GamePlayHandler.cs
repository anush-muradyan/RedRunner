using DefaultNamespace.UI.View;
using UnityEngine;

namespace DefaultNamespace
{
    public class GamePlayHandler
    {
        private GameManager gameManager;

        public GamePlayHandler(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void StartGame()
        {
            gameManager.StartGame();
            
        }
        
        public void PauseGame()
        {
            gameManager.PauseGame();
        }
        
        public void ResumeGame()
        {
            gameManager.ResumeGame();
        }

        public void RestartGame()
        {
            gameManager.RestartGame();
        }
    }
}