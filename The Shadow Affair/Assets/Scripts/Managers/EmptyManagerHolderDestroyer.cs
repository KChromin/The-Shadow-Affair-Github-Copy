using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//For destroying Manager Holders, when all children singletons self destroyed//
public class EmptyManagerHolderDestroyer : MonoBehaviour
{
    private void CheckIfNeedToBeDestroyed(Scene scene, LoadSceneMode loadMode)
    {
        StartCoroutine(CheckAfterFrame());
    }

    private IEnumerator CheckAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckIfNeedToBeDestroyed;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckIfNeedToBeDestroyed;
    }
}