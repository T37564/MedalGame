using UnityEngine;

/// <summary>
/// ミニカメラを移動させるクラス
/// </summary>
public class LaunchMiniCameraUIController : MonoBehaviour
{
    [Header("ミニカメラ")]
    [SerializeField] private RectTransform miniCamera = null;

    [Header("移動可能範囲")]
    [SerializeField] private RectTransform moveArea = null;

    [Header("ドラッグできる範囲のフレーム一覧")]
    [SerializeField] private RectTransform[] dragFrames = null;

    // 前回のタッチ位置を保持
    private Vector2 previousPosition = Vector2.zero;

    // ドラッグ判定に使用するフレーム一覧
    public RectTransform[] MiniCameraDragFrames => dragFrames;

    // ドラッグ可能範囲
    private float minX = 0.0f;
    private float maxX = 0.0f;
    private float minY = 0.0f;
    private float maxY = 0.0f;

    private void Start()
    {
        // 移動可能範囲の幅と高さを取得
        float areaWidth = moveArea.rect.width;
        float areaHeight = moveArea.rect.height;

        // ミニカメラの幅と高さを取得
        float miniWidth = miniCamera.rect.width;
        float miniHeight = miniCamera.rect.height;


        // ミニカメラが移動可能範囲からはみ出さないよう制御値を計算
        minX = -(areaWidth / 2f) + (miniWidth / 2f);
        maxX = (areaWidth / 2f) - (miniWidth / 2f);

        minY = -(areaHeight / 2f) + (miniHeight / 2f);
        maxY = (areaHeight / 2f) - (miniHeight / 2f);
    }

    /// <summary>
    /// ミニカメラを移動させる処理
    /// </summary>
    public void SlideCamera(Vector2 touchPosition)
    {
        // ドラッグ開始位置と現在のタッチ位置の差分を計算
        Vector2 delta = touchPosition - previousPosition;

        // ミニカメラを移動
        miniCamera.anchoredPosition += delta;

        // ミニカメラ位置を取得
        Vector2 position = miniCamera.anchoredPosition;

        // 移動可能範囲内へ制限
        position.x = Mathf.Clamp(position.x, minX, maxX);

        position.y = Mathf.Clamp(position.y, minY, maxY);

        // 制限後の位置を適用
        miniCamera.anchoredPosition = position;

        // 現在位置を保存
        previousPosition = touchPosition;
    }

    /// <summary>
    /// 前回のタッチ位置を更新する処理
    /// </summary>
    public void UpdatePreviousPosition(Vector2 position)
    {
        previousPosition = position;
    }
}