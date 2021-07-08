using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.UI.View
{
    public class GameViewResult : IViewResult
    {
        public UnityEvent OnPause;

        public GameViewResult()
        {
            OnPause = new UnityEvent();
        }

        public void Dispose()
        {
            OnPause.RemoveAllListeners();
        }
    }

    public class GameView : AbstractView<GameViewResult>
    {
        [SerializeField] private Button PauseButoon;

        protected override void OnEnable()
        {
            PauseButoon.onClick.AddListener(Result.OnPause.Invoke);
        }

        protected override void OnDisableInternal()
        {
            PauseButoon.onClick.RemoveListener(Result.OnPause.Invoke);
        }
    }
}