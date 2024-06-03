using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PurchaseWindowManager : MonoBehaviour
{
    Vector2 popUpScale = new Vector2(1.0f, 1.0f);

    private static PurchaseWindowManager instance;

    public static PurchaseWindowManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("PurchaseWindow").GetComponent<PurchaseWindowManager>();
                instance.transform.localScale = Vector2.zero;
            }
            return instance;
        }
    }

    private void Awake()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(CloseDown);

        GameObject button = transform.GetChild(1).gameObject;
        button.transform.gameObject.GetComponent<CodelessIAPButton>().
            onPurchaseComplete.AddListener(
                LicenseUnlockManager.Instance.OnUnlockChapterAction
            );
        button.transform.gameObject.GetComponent<CodelessIAPButton>().
            onPurchaseComplete.AddListener(
                CloseDown
            );
    }

    public void PopUp()
    {
        LeanTween.scale(this.gameObject, popUpScale, 0.5f).setEaseOutQuint();
    }

    public void CloseDown()
    {
        LeanTween.scale(this.gameObject, Vector2.zero, 0.5f).setEaseInBack();
    }

    public void CloseDown(UnityEngine.Purchasing.Product product)
    {
        LeanTween.scale(this.gameObject, Vector2.zero, 0.5f).setEaseInBack();
    }
}
