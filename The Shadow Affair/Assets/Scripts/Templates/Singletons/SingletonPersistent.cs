using UnityEngine;

namespace SmugRag.Templates.Singletons
{
    public class SingletonPersistent<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}