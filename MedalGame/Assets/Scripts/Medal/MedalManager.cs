using UnityEngine;

/// <summary>
/// 現在の所持メダル枚数を管理するクラス
/// </summary>
public class MedalManager : MonoBehaviour
{
    [Header("ゲームUIコントローラー 参照用")]
    [SerializeField] private GameUIController gameUIController = null;

    // 現在のメダルの枚数を保持するプロパティ
    public int GetMedalCount { get; private set; } = 0;

    /// <summary>
    /// メダルを加算 & UIの更新
    /// </summary>
    public void AddMedal()
    {
        GetMedalCount++;

        gameUIController.UpdateMedalCountUI(GetMedalCount);
    }
}
