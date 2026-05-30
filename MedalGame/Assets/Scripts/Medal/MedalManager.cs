using UnityEngine;

/// <summary>
/// 現在の所持メダル枚数を管理するクラス
/// </summary>
public class MedalManager : MonoBehaviour
{
    // メダルの初期枚数
    private const int INITIAL_MEDAL_COUNT = 100;

    [Header("ゲームUIコントローラー 参照用")]
    [SerializeField] private GameUIController gameUIController = null;

    // 現在のメダルの枚数を保持するプロパティ
    public int CurrentMedalCount { get; private set; } = 0;


    private void Start()
    {
        CurrentMedalCount = INITIAL_MEDAL_COUNT;
    }

    /// <summary>
    /// メダル枚数リセット & UIの更新
    /// </summary>
    private void ResetMedal()
    {
        CurrentMedalCount = 0;
        gameUIController.UpdateMedalCountUI(CurrentMedalCount);
    }


    /// <summary>
    /// メダルを加算 & UIの更新
    /// </summary>
    public void AddMedal()
    {
        CurrentMedalCount++;

        gameUIController.UpdateMedalCountUI(CurrentMedalCount);
    }


    /// <summary>
    /// メダルを減算 & UIの更新
    /// </summary>
    public void RemoveMedal()
    {
        CurrentMedalCount--;

        // メダルの枚数が0未満にならないようにする
        if (CurrentMedalCount < 0)
        {
            CurrentMedalCount = 0;
        }

        gameUIController.UpdateMedalCountUI(CurrentMedalCount);
    }
}
