using UnityEngine;

/// <summary>
/// ミニカメラを移動させるクラス
/// </summary>
public class LaunchMiniCameraUIController : MonoBehaviour
{
    [Header("ミニカメラ")]
    [SerializeField] private RectTransform miniCamera = null;

    [Header("ドラッグできる範囲のフレーム一覧")]
    [SerializeField] private RectTransform[] dragFrames = null;

    // 前回のタッチ位置を保持
    private Vector2 previousPosition = Vector2.zero;

    [Header("ドラッグできる範囲")]
    [SerializeField] private float minX = 0; // ドラッグできる最小X座標
    [SerializeField] private float maxX = 0;  // ドラッグできる最大X座標
    [SerializeField] private float minY = 0; // ドラッグできる最小Y座標
    [SerializeField] private float maxY = 0;  // ドラッグできる最大Y座標

    // ドラッグ判定に使用するフレーム一覧
    public RectTransform[] MiniCameraDragFrames => dragFrames;


    /// <summary>
    /// ミニカメラを移動させる処理
    /// </summary>
    public void SlideCamera(Vector2 touchPosition)
    {
        // ドラッグ開始位置と現在のタッチ位置の差分を計算
        Vector2 delta = touchPosition - previousPosition;

        // ミニカメラのルートを移動
        miniCamera.anchoredPosition += delta;

        // ミニカメラの位置をドラッグできる範囲内に制限
        Vector2 position = miniCamera.anchoredPosition;

        // ドラッグできる範囲内に位置を制限
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // 制限された位置をミニカメラに適用
        miniCamera.anchoredPosition = position;

        // 現在のタッチ位置を次のフレームのドラッグ開始位置として保存
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
