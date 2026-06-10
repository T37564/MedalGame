using UnityEngine;

/// <summary>
/// ミニカメラを移動させるクラス
/// </summary>
public class LaunchMiniCameraUIController : MonoBehaviour
{
    // カメラのサイズを半分にするときに使用
    private readonly float HALF_DIVISOR = 2.0f;


    [Header("ミニカメラ")]
    [SerializeField] private RectTransform miniCamera = null;

    [Header("移動可能範囲")]
    [SerializeField] private RectTransform moveArea = null;

    [Header("ドラッグできる範囲のフレーム一覧")]
    [SerializeField] private RectTransform[] dragFrames = null;


    // 前回のタッチ位置を保持
    private Vector2 previousPosition = Vector2.zero;

    // ドラッグ可能範囲(最小、最大)
    private Vector2 minMoveRange = Vector2.zero;
    private Vector2 maxMoveRange = Vector2.zero;

    /// <summary>
    /// ドラッグ判定に使用するフレーム一覧
    /// </summary>
    public RectTransform[] MiniCameraDragFrames => dragFrames;

    /// <summary>
    /// ミニカメラの移動可能範囲を計算して初期化する処理
    /// </summary>
    public void InitializeMoveArea()
    {
        // 移動可能範囲の幅と高さを取得
        float areaWidth = moveArea.rect.width;
        float areaHeight = moveArea.rect.height;

        // ミニカメラの幅と高さを取得
        float miniWidth = miniCamera.rect.width;
        float miniHeight = miniCamera.rect.height;

        // ミニカメラの中心が移動可能範囲内に収まるように、
        // 移動可能範囲の端からミニカメラの半分の幅と高さを引いて計算
        minMoveRange = new Vector2(-(areaWidth / HALF_DIVISOR) + (miniWidth / HALF_DIVISOR),
                                    -(areaHeight / HALF_DIVISOR) + (miniHeight / HALF_DIVISOR));
        maxMoveRange = new Vector2((areaWidth / HALF_DIVISOR) - (miniWidth / HALF_DIVISOR),
                                    (areaHeight / HALF_DIVISOR) - (miniHeight / HALF_DIVISOR));
    }

    /// <summary>
    /// ミニカメラを移動させる処理
    /// </summary>
    public void MoveMiniCamera(Vector2 touchPosition)
    {
        // ドラッグ開始位置と現在のタッチ位置の差分を計算
        Vector2 dragDelta = touchPosition - previousPosition;

        // ミニカメラを移動
        miniCamera.anchoredPosition += dragDelta;

        Vector2 position = miniCamera.anchoredPosition;

        // 移動可能範囲内へ制限
        position.x = Mathf.Clamp(position.x, minMoveRange.x, maxMoveRange.x);
        position.y = Mathf.Clamp(position.y, minMoveRange.y, maxMoveRange.y);

        // 制限後の位置を適用
        miniCamera.anchoredPosition = position;

        // 現在位置を保存
        previousPosition = touchPosition;
    }

    /// <summary>
    /// 前回のタッチ位置を更新する処理
    /// </summary>
    public void SetPreviousPosition(Vector2 position)
    {
        previousPosition = position;
    }
}