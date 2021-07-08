using System;
using UnityEngine;

namespace DefaultNamespace.UI.View
{
    public class AbstractView : MonoBehaviour
    {
        public virtual AbstractView Show()
        {
            gameObject.SetActive(true);
            return this;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }
    }

    
    public class AbstractView<TResult> : AbstractView where TResult : IViewResult, new()
    {
        public TResult Result = new TResult();

        protected override void OnDisable()
        {
            base.OnDisable();
            OnDisableInternal();
            Result.Dispose();
        }

        protected virtual void OnDisableInternal()
        {
        }
    }

    public interface IViewResult : IDisposable
    {
    }
}