using System;

namespace DefaultNamespace.IGameStates
{
    public interface IDeath
    {
        event Action OnDeath;
    }
}