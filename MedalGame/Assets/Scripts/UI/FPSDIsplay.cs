using TMPro;
using UnityEngine;

/// <summary>
/// FPSを表示するクラス
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    [Header("FPS表示テキスト")]
    [SerializeField] private TMP_Text fpsText = null;

    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;

    /// <summary>
    /// FPS表示
    /// </summary>
    private void Update()
    {
        fpsText.text = $"FPS : {Mathf.RoundToInt(fpsManager.CurrentFps)}";
    }
}
