using UnityEngine;

public class LicenseUnlockManager : MonoBehaviour
{
    private bool licenseUnlocked = false;
    [SerializeField]
    private GameObject chapterButtonManagerHolder = null;
    private ChapterButtonManager chapterButtonManager = null;

    private void Awake()
    {
        chapterButtonManager = chapterButtonManagerHolder.GetComponent<ChapterButtonManager>();
        //foreach (Button button in chapterButtonManager.buttons)
        //{
        //    Transform lockButtonTransform = button.transform.GetChild(3);
        //    CodelessIAPButton IAPButton = lockButtonTransform.GetComponent<CodelessIAPButton>();
        //    IAPButton.onPurchaseComplete
        //}
    }

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
