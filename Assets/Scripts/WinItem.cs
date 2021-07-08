using System;
using DefaultNamespace.UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinItem:MonoBehaviour
    {
        public bool WinFlag;
        [SerializeField] private LayerMask mask;
        private void OnTriggerEnter2D(Collider2D other)
        {
            WinFlag = true;
        }
    }
}