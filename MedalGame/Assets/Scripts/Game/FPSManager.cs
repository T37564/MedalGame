using UnityEngine;

/// <summary>
/// FPSの計算を行うクラス
/// </summary>
public class FPSManager : MonoBehaviour
{
    // 更新間隔
    private float timer = 0.0f;

    // FPS計算用
    private float deltaTime = 0.0f;

    /// <summary>
    /// 現在のFPS
    /// </summary>
    public float CurrentFps { get; private set; }

    /// <summary>
    /// FPSを計算して更新する
    /// </summary>
    private void Update()
    {
        // フレーム時間を滑らかにする
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        timer += Time.deltaTime;

        // 0.5秒ごと更新
        if (timer >= 0.1f)
        {
            // FPS計算
            CurrentFps = 1.0f / deltaTime;

            timer = 0.0f;
        }
    }
}
