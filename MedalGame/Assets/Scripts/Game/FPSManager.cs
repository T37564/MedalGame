using UnityEngine;

/// <summary>
/// FPSの計算を行うクラス
/// </summary>
public class FPSManager : MonoBehaviour
{
    // FPSの更新間隔
    private readonly float FPS_UPDATE_INTERVAL = 0.1f;

    // FPS表示を見やすくするための平滑化係数
    private readonly float SMOOTHING_COEFFICIENT = 0.1f;


    // FPS更新用タイマー
    private float fpsUpdateTimer = 0.0f;

    // FPS計算に使用するフレーム時間
    private float smoothedDeltaTime = 0.0f;

    /// <summary>
    /// 現在のFPS
    /// </summary>
    public float CurrentFps { get; private set; }

    /// <summary>
    /// FPSの初回計算が完了したか
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// FPSを計算して更新する
    /// </summary>
    private void Update()
    {
        // FPS計算が開始可能になるまで処理しない
        if (!UIManager.Instance.CanCalculateFPS) return;

        // フレーム時間を滑らかにする
        smoothedDeltaTime += (Time.deltaTime - smoothedDeltaTime) * SMOOTHING_COEFFICIENT;

        // FPS更新用タイマー
        fpsUpdateTimer += Time.deltaTime;

        // FPSを0.1秒ごとに更新
        if (fpsUpdateTimer >= FPS_UPDATE_INTERVAL)
        {
            // FPS計算
            CurrentFps = 1.0f / smoothedDeltaTime;

            // タイマーリセット
            fpsUpdateTimer = 0.0f;

            // 初回FPS計算完了を知らせる
            if (!IsInitialized)
            {
                IsInitialized = true;
            }
        }
    }
}
