using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.IGameStates;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        private Dictionary<Type, List<IGameState>> gameStates;
        public event Action OnDeath;
        private void Awake()
        {
            Prepare();
            
        }

        private void Prepare()
        {
            gameStates = new Dictionary<Type, List<IGameState>>();
            AddType<IStartGame>();
            AddType<IDeath>();
            AddType<IGamePause>();
            AddType<IGameResume>();
            AddType<IGameRestart>();
        }

        private void AddType<T>() where T : class
        {
            gameStates.Add(typeof(T), Utils.GetInterfaces<T, IGameState>());
        }

        private List<T> ResolveType<T>() where T : class
        {
            var type = typeof(T);
            if (!gameStates.ContainsKey(type))
            {
                Debug.LogError("CAN NOT FIND THAT TYPE!!!: " + type);
                return null;
            }

            return gameStates[type].Select(state => state as T).ToList();
        }

        public void StartGame()
        {
            sub();
            ResolveType<IStartGame>().ForEach(start => start.StartGame());
        }

        public void sub()
        {
            ResolveType<IDeath>().ForEach(death => death.OnDeath += OnDeath);
        }
        public void PauseGame()
    { 
            ResolveType<IGamePause>().ForEach(start => start.PauseGame());
        }

        public void ResumeGame()
        {
            ResolveType<IGameResume>().ForEach(start => start.ResumeGame());
        }

        public void RestartGame()
        {
            ResolveType<IGameRestart>().ForEach(restart => restart.RestartGame());
        }
    }
}