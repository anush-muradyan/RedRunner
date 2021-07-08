using System;
using DefaultNamespace.UI.View;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Win Flag")] [SerializeField] private WinItem winItem;

        [Header("Views")] [SerializeField] private HomeView homeView;
        [SerializeField] private GameView gameView;
        [SerializeField] private PauseView pauseView;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private WinView winView;
        [SerializeField] private LooseView looseView;

        private AbstractView _view;
        private GamePlayHandler gamePlay;
        private AbstractView view
        {
            get => _view;
            set
            {
                if (_view != null)
                {
                    _view.Hide();
                }

                _view = value;
            }
        }

        private void Start()
        {
            gameManager.OnDeath += ShowLooseView;

            ShowHomeView();
        }

        private void Update()
        {
            if (winItem.WinFlag)
            {
                winItem.WinFlag = !winItem.WinFlag;
                ShowWinView();
            }
        }

        private void ShowHomeView()
        {
            view = homeView.Show();
            gamePlay = new GamePlayHandler(gameManager);
            homeView.Result.OnPlay.AddListener(ShowGameView);
        }

        private void ShowGameView()
        {
            view = gameView.Show();
            gamePlay.StartGame();
           
            //gamePlay.RestartGame();
            gameView.Result.OnPause.AddListener(ShowPauseView);
            gamePlay.ResumeGame();
      
        }
        
        private void ShowPauseView()
        {
            view = pauseView.Show();
            gamePlay.PauseGame();
            pauseView.Result.OnResume.AddListener(ShowGameView);
        }

        private void ShowWinView()
        {
            view = winView.Show();
            gamePlay.PauseGame();
            winView.Result.OnHome.AddListener(ShowHomeView);
            //winView.Result.OnNext.AddListener(ShowNextLevel);
        }

        
        [ContextMenu("looseeee")]
        private void ShowLooseView()
        {
            view = looseView.Show();
            looseView.Result.OnHome.AddListener(ShowHomeView);
            looseView.Result.OnRestart.AddListener(RestartView);
        }

        private void RestartView()
        {
            view = gameView.Show();
            gamePlay.RestartGame();
        }
    }
}