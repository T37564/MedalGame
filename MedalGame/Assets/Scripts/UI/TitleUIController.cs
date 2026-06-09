using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// タイトルUIを管理するクラス
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
        // タイトルUIを繰り返しフェードイン、アウトさせる
        titleTextImage.DOFade(FADE_ALPHA, FADE_DURATION).SetLoops(-1, LoopType.Yoyo);
    }


    /// <summary>
    /// タイトルUIのアニメーションを停止する
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
