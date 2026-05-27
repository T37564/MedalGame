using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// タイトルのUIのアニメーションや表示非表示を管理するクラス
/// </summary>
public class TitleUIController : MonoBehaviour
{
    // フェイド時の透明度
    private const float FADE_ALPHA = 0.1f;
    // フェイドにかかる時間
    private const float FADE_DURATION = 0.8f;


    [Header("タイトルのキャンバス")]
    [SerializeField] private Canvas titleCanvas = null;

    [Header("TAP TO START画像")]
    [SerializeField] private Image titleTextImage = null;

    /// <summary>
    /// 文字をフェードイン、アウトさせるアニメーションを開始する
    /// </summary>
    private void Start()
    {
        // タイトルUIを繰り返しフェードイン、アウトさせる
        titleTextImage.DOFade(FADE_ALPHA, FADE_DURATION).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// タイトルのキャンバスの表示非表示処理
    /// </summary>
    public void SetTitleCanvasActive()
    {
        titleCanvas.gameObject.SetActive(!titleCanvas.gameObject.activeSelf);
    }
}
