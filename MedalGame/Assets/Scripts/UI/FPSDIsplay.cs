using TMPro;
using UnityEngine;

/// <summary>
/// FPSを計算して表示するクラス
/// </summary>
public class FPSDIsplay : MonoBehaviour
{
    [Header("FPS表示テキスト")]
    [SerializeField] private TMP_Text fpsText = null;

    // 更新間隔
    private float timer = 0.0f;

    // FPS計算用
    private float deltaTime = 0.0f;

    /// <summary>
    /// FPSの計算と表示
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
            float fps = 1.0f / deltaTime;

            fpsText.text = $"FPS : {Mathf.Ceil(fps)}";

            timer = 0.0f;
        }
    }
}
