using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

// 상품 항목 UI 
public class UIProduct : MonoBehaviour
{
    // 상품 이름
    [SerializeField]
    private TextMeshProUGUI NameText;
    // 상품 설명
    [SerializeField]
    private TextMeshProUGUI DescriptionText;
    // 상품 아이콘 이미지
    [SerializeField]
    private Image Icon;
    // 상품 가격 폰트
    [SerializeField]
    private TextMeshProUGUI PriceText;
    // 구매 버튼
    [SerializeField]
    private Button PurchaseButton;

    public delegate void PurchaseEvent(Product Model, Action OnComplete);
    public event PurchaseEvent OnPurchase;

    private Product Model;

    // Unity Purchasing 에서 상품 정보를 받아 UI 를 초기화
    public void Setup(Product Product)
    {
        Model = Product;
        NameText.SetText(Product.metadata.localizedTitle);
        DescriptionText.SetText(Product.metadata.localizedDescription);
        PriceText.SetText($"{Product.metadata.localizedPriceString} " +
            $"{Product.metadata.isoCurrencyCode}");
        Texture2D texture = StoreIconProvider.GetIcon(Product.definition.id);
        if (texture != null)
        {
            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.one / 2f
            );

            Icon.sprite = sprite;
        }
        else
        {
            Debug.LogError($"No Sprite found for {Product.definition.id}!");
        }
    }

    // 구매 버튼이 눌렸을때
    public void Purchase()
    {
        PurchaseButton.enabled = false;
        OnPurchase?.Invoke(Model, HandlePurchaseComplete);
    }

    // 구매 프로세스가 종료되었을때
    private void HandlePurchaseComplete()
    {
        PurchaseButton.enabled = true;
    }
}
