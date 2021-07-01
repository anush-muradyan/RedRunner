using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private uint minCount = 3;
        [SerializeField] private uint maxCount = 8;
        [SerializeField] private Transform lifePrefab;
        

        private void Awake()
        {
            var count = Mathf.FloorToInt(Random.Range(minCount, maxCount));
            var indexes = Enumerable
                .Range(0, transform.childCount)
                .OrderBy(i => Guid.NewGuid())
                .Take(count);
            
            foreach (var index in indexes)
            {
                Instantiate(lifePrefab, transform.GetChild(index));
            }
        }
    }
}