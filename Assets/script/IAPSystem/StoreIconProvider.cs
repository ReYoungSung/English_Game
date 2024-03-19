using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class StoreIconProvider
{
    // 디폴트 초기화 (Initialize by default)가 사용 됨 
    public static Dictionary<string, Texture2D> Icons
    {
        get;
        private set;
    } = new();
    private static int TargetIconCount;
    public delegate void LoadCompleteAction();
    public static event LoadCompleteAction OnLoadComplete;

    public static void Initialize(ProductCollection Products)
    {
        if (Icons.Count == 0)
        {
            Debug.Log($"아이콘 로딩 중... {Products.all.Length} .");
            TargetIconCount = Products.all.Length;
            foreach (Product product in Products.all)
            {
                Debug.Log($"스토어 아이콘 로딩중... 경로 StoreIcons/{product.definition.id}");
                ResourceRequest operation = Resources.LoadAsync<Texture2D>($"StoreIcons/{product.definition.id}");
                operation.completed += HandleLoadIcon;
            }
        }
        else
        {
            Debug.LogError("StoreIconProvider 초기화 완료");
        }
    }

    public static Texture2D GetIcon(string Id)
    {
        if (Icons.Count == 0)
        {
            Debug.LogError("StoreIconProvider.GetIcon 호출 - 초기화 전" +
                "This is not a supported operation!");
            throw new InvalidOperationException("StoreIconProvider.GetIcon() 호출불가 -" +
                "(StoreIconProivder.Initialize() 호출 이전)");
        }
        else
        {
            Icons.TryGetValue(Id, out Texture2D icon);
            return icon;
        }
    }

    private static void HandleLoadIcon(AsyncOperation Operation)
    {
        ResourceRequest request = Operation as ResourceRequest;
        if (request.asset != null)
        {
            Debug.Log($"에셋 로드 완료 {request.asset.name}");
            Icons.Add(request.asset.name, request.asset as Texture2D);

            if (Icons.Count == TargetIconCount)
            {
                OnLoadComplete?.Invoke();
            }
        }
        else
        {
            // Subtract from total because something failed to load
            TargetIconCount--;
        }
    }
}
