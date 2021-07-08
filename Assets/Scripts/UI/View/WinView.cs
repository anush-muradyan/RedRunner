using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.UI.View
{
    public class WinViewResult : IViewResult
    {
        public UnityEvent OnHome { get; }
        public UnityEvent OnNext { get; }

        public WinViewResult()
        {
            OnHome = new UnityEvent();
            OnNext = new UnityEvent();
        }

        public void Dispose()
        {
            OnHome.RemoveAllListeners();
            OnNext.RemoveAllListeners();
        }
    }


    public class WinView : AbstractView<WinViewResult>
    {
        [SerializeField] private Button Home;
        [SerializeField] private Button Next;

        protected override void OnEnable()
        {
            Home.onClick.AddListener(Result.OnHome.Invoke);
            Next.onClick.AddListener(Result.OnHome.Invoke);
        }

        protected override void OnDisableInternal()
        {
            Home.onClick.RemoveListener(Result.OnHome.Invoke);
            Next.onClick.RemoveListener(Result.OnNext.Invoke);
        }
    }
}