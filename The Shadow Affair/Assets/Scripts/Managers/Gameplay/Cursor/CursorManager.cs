using UnityEngine;
using SmugRagGames.Patterns.Singleton;

public class CursorManager : SingletonPersistent<CursorManager>
{
    public static void SetForUI()
    {
        SetVisibility(true);
        SetLock(CursorLockMode.None);
    }

    public static void SetForGameplay()
    {
        SetVisibility(false);
        SetLock(CursorLockMode.Locked);
    }

    #region Private methodes

    private static void SetVisibility(bool newVisibility)
    {
        Cursor.visible = newVisibility;
    }

    private static void SetLock(CursorLockMode newLockMode)
    {
        Cursor.lockState = newLockMode;
    }

    #endregion Private methodes
}