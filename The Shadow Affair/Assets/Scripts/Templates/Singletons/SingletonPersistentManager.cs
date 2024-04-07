using UnityEngine;

namespace SmugRag.Templates.Singletons
{
    public class SingletonPersistentManager<T> : MonoBehaviour where T : Component
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

            gameObject.transform.parent = GetManagerParent();
        }

        private Transform GetManagerParent()
        {
            //Check if game manager holder exists//
            GameObject parentObject = GameObject.FindWithTag("GameManagers");

            //If parent does not exist, then create new object//
            //Give him tag for more optimized finding, and add to Dont destroy on load//
            if (parentObject == null)
            {
                parentObject = CreateAndGetNewParentObject();
            }
            else if (parentObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(parentObject);
            }

            //Return new parent//
            return parentObject.transform;
        }

        private GameObject CreateAndGetNewParentObject()
        {
            GameObject parentObject = new("--Game Managers")
            {
                tag = "GameManagers"
            };

            DontDestroyOnLoad(parentObject);

            return parentObject;
        }
    }
}