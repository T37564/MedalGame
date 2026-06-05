using UnityEngine;

/// <summary>
/// UIをSafeArea内に収めるクラス
/// </summary>
public class SafeArea : MonoBehaviour
{
    // SafeAreaを適用する自身のRectTransform
    private RectTransform safeAreaRect = null;

    /// <summary>
    /// Awake時にSafeAreaを適用する処理
    /// </summary>
    private void Awake()
    {
        safeAreaRect = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    /// <summary>
    /// UIをSafeArea内に収める処理
    /// </summary>
    private void ApplySafeArea()
    {
        // 画面のSafeAreaを取得
        Rect safeArea = Screen.safeArea;

        // SafeAreaの位置とサイズを画面サイズで割って、アンカーの値に変換
        Vector2 minAnchor = safeArea.position;

        // SafeAreaの右上の位置を計算
        Vector2 maxAnchor = safeArea.position + safeArea.size;

        // 0.0～1.0のアンカー座標へ変換
        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;

        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        // RectTransformのアンカーをSafeAreaに合わせて設定
        safeAreaRect.anchorMin = minAnchor;
        safeAreaRect.anchorMax = maxAnchor;

        // オフセットを0に設定して、UIがSafeArea内に収まるようにする
        safeAreaRect.offsetMin = Vector2.zero;
        safeAreaRect.offsetMax = Vector2.zero;
    }
}
