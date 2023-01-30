using System;

namespace _Project.Scripts.Managers
{
    public static class EventManager
    {
        public static Action OnPlayerDeath;
        public static Action<int> OnAsteroidDestroyed;
        public static Action<int> OnAsteroidSplit;
    }
}