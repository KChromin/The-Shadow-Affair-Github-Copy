using UnityEngine;

namespace SmugRagGames.Patterns.Singleton
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

            //Make sure that it is persistent//
            //Check if object have parent//
            if (transform.parent != null)
            {
                //Check if parent object is in DontDestroyOnLoad//
                if (transform.parent.gameObject.scene.name != "DontDestroyOnLoad")
                {
                    DontDestroyOnLoad(transform.parent.gameObject);
                }
            } //Without parent, just check the object//
            else if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}