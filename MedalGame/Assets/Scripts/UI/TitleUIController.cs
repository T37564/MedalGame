using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// タイトルUIを管理するクラス
/// </summary>
public class TitleUIController : MonoBehaviour
{
    // フェード時の目標透明度
    private readonly float FADE_TARGET_ALPHA = 0.1f;
    // フェードにかかる時間
    private readonly float FADE_DURATION = 0.8f;


    [Header("タイトルCanvas")]
    [SerializeField] private Canvas titleCanvas = null;

    [Header("TAP TO STARTの画像")]
    [SerializeField] private Image titleTextImage = null;

    /// <summary>
    /// タイトル画面かどうか
    /// </summary>
    public bool IsTitle { get; private set; }

    /// <summary>
    /// タイトルUIの初期化
    /// </summary>
    private void Awake()
    {
        ChangeTitleUIState(true);
    }


    /// <summary>
    ///  「TAP TO START」画像のフェードアニメーションを開始する
    /// </summary>
    private void Start()
    {
        // 「TAP TO START」画像を繰り返しフェードイン・フェードアウトさせる
        titleTextImage.DOFade(FADE_TARGET_ALPHA, FADE_DURATION).SetLoops(-1, LoopType.Yoyo);
    }


    /// <summary>
    /// 「TAP TO START」画像のフェードアニメーションを停止する
    /// </summary>
    private void OnDisable()
    {
        titleTextImage.DOKill();
    }


    /// <summary>
    /// タイトルUIの表示を切り替える
    /// </summary>
    public void ChangeTitleUIState(bool isDisplay)
    {
        // タイトル画面の状態を更新
        IsTitle = isDisplay;

        // タイトルUIの表示状態を切り替える
        titleCanvas.gameObject.SetActive(isDisplay);
    }
}
