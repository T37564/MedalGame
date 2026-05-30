using UnityEngine;

/// <summary>
/// タイトルUIを管理するクラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    [Header("タイトルUI")]
    [SerializeField] private GameObject titleUI = null;

    /// <summary>
    /// タイトル画面かどうか
    /// </summary>
    public bool IsTitle { get; private set; }

    /// <summary>
    /// タイトルUIの初期化
    /// </summary>
    private void Awake()
    {
      ChangeTitleState(true);
    }

    /// <summary>
    /// タイトルUIの表示を切り替える
    /// </summary>
    public void ChangeTitleState(bool isDisplay)
    {
        IsTitle = isDisplay;
        titleUI.SetActive(isDisplay);
    }
}