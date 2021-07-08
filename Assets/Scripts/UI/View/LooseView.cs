using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace DefaultNamespace.UI.View
{
    public class LooseViewResult : IViewResult
    {
        public UnityEvent OnHome { get; }
        public UnityEvent OnRestart { get; }

        public LooseViewResult()
        {
            OnHome = new UnityEvent();
            OnRestart = new UnityEvent();
        }

        public void Dispose()
        {
            OnHome.RemoveAllListeners();
            OnRestart.RemoveAllListeners();
        }
    }


    public class LooseView : AbstractView<LooseViewResult>
    {
        [SerializeField] private Button Home;
        [SerializeField] private Button Restart;

        protected override void OnEnable()
        {
            Home.onClick.AddListener(Result.OnHome.Invoke);
            Restart.onClick.AddListener(Result.OnHome.Invoke);
        }

        protected override void OnDisableInternal()
        {
            Home.onClick.RemoveListener(Result.OnHome.Invoke);
            Restart.onClick.RemoveListener(Result.OnRestart.Invoke);
        }

    }
}