using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

// ��ǰ �׸� UI 
public class UIProduct : MonoBehaviour
{
    // ��ǰ �̸�
    [SerializeField]
    private TextMeshProUGUI NameText;
    // ��ǰ ����
    [SerializeField]
    private TextMeshProUGUI DescriptionText;
    // ��ǰ ������ �̹���
    [SerializeField]
    private Image Icon;
    // ��ǰ ���� ��Ʈ
    [SerializeField]
    private TextMeshProUGUI PriceText;
    // ���� ��ư
    [SerializeField]
    private Button PurchaseButton;

    public delegate void PurchaseEvent(Product Model, Action OnComplete);
    public event PurchaseEvent OnPurchase;

    private Product Model;

    // Unity Purchasing ���� ��ǰ ������ �޾� UI �� �ʱ�ȭ
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

    // ���� ��ư�� ��������
    public void Purchase()
    {
        PurchaseButton.enabled = false;
        OnPurchase?.Invoke(Model, HandlePurchaseComplete);
    }

    // ���� ���μ����� ����Ǿ�����
    private void HandlePurchaseComplete()
    {
        PurchaseButton.enabled = true;
    }
}
