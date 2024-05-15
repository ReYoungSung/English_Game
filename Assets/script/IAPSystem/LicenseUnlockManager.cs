using UnityEngine;

public class LicenseUnlockManager : MonoBehaviour
{
    private bool licenseUnlocked = false;
    public bool LicensesUnlocked
    {
        get { return licenseUnlocked;}
        set { licenseUnlocked = value; }
    }

    public void OnUnlockChapter()
    {
        licenseUnlocked = true;
    }

    public void OnUnlockFailed()
    {
        Debug.Log("Chapter unlock failed");
    }
}
