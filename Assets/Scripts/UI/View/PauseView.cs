using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.UI.View
{
    public class PauseViewResult : IViewResult
    {
        public UnityEvent OnResume { get; }

        public PauseViewResult()
        {
            OnResume = new UnityEvent();
        }
        
        public void Dispose()
        {
            OnResume.RemoveAllListeners();
        }
        
    }
    
    
    public class PauseView:AbstractView<PauseViewResult>
    {
        [SerializeField] private Button resume;

        protected override void OnEnable()
        {
            resume.onClick.AddListener(Result.OnResume.Invoke);
        }

        protected override void OnDisableInternal()
        {
            resume.onClick.RemoveListener(Result.OnResume.Invoke);
        }
    }
}