using System.Collections;
using UnityEngine;

/// <summary>
/// ゲーム中のUI遷移を管理するクラス
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("タイトルのUI管理クラス参照用")]
    [SerializeField] private TitleUIController titleUIController = null;

    [Header("ゲーム中のUI管理クラス参照用")]
    [SerializeField] private GameUIController gameUIController = null;

    [Header("NowLoadingのUI管理クラス参照用")]
    [SerializeField] private NowLoadingUIController nowLoadingUIController = null;

    [Header("ギミック壁クラス参照用")]
    [SerializeField] private GuardRotator[] guardRotator = null;


    // タイトル画面中かどうかを取得するプロパティ
    public bool IsTitle => titleUIController.IsTitle;


    /// <summary>
    /// タイトル画面からゲーム画面への遷移を開始する
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    /// <summary>
    /// タイトル画面からゲーム画面へ遷移するコルーチン
    /// </summary>
    private IEnumerator StartGameRoutine()
    {
        titleUIController.ChangeTitleUIState(false);

        nowLoadingUIController.ChangeNowLoadingUIState(true);

        // 全てのガードの移動終了待ち
        yield return new WaitUntil(IsAllGuardMoveEnd);

        nowLoadingUIController.ChangeNowLoadingUIState(false);

        gameUIController.ChangeGameUIState(true);

        gameUIController.ChangeLaunchCameraUIState(true);
    }

    /// <summary>
    /// 全てのガードの移動が完了したか判定する
    /// </summary>
    private bool IsAllGuardMoveEnd()
    {
        foreach (GuardRotator guard in guardRotator)
        {
            if (!guard.MoveEnd)
            {
                return false;
            }
        }

        return true;
    }
}
