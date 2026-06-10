using System.Collections;
using UnityEngine;

/// <summary>
/// UIの表示切り替えや画面遷移を管理するクラス
/// </summary>
public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [Header("タイトルのUI管理クラス参照用")]
    [SerializeField] private TitleUIController titleUIController = null;

    [Header("ゲーム中のUI管理クラス参照用")]
    [SerializeField] private GameUIController gameUIController = null;

    [Header("NowLoadingのUI管理クラス参照用")]
    [SerializeField] private NowLoadingUIController nowLoadingUIController = null;

    [Header("ゲームオーバーUI管理クラス参照用")]
    [SerializeField] private GameOverUIController gameOverUIController = null;

    [Header("ギミック壁クラス参照用")]
    [SerializeField] private GuardRotator[] guardRotator = null;

    [Header("ミニカメラを移動させるクラス参照")]
    [SerializeField] private LaunchMiniCameraUIController launchMiniCameraUIController = null;



    /// <summary>
    /// タイトル画面中かどうかを取得するプロパティ
    /// </summary>
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
        // タイトルUI非表示
        titleUIController.ChangeTitleUIState(false);

        // ローディングUI画面表示
        nowLoadingUIController.ChangeNowLoadingUIState(true);

        // 全てのガードの移動終了待ち
        yield return new WaitUntil(IsAllGuardMoveEnd);

        // ローディングUI画面非表示
        nowLoadingUIController.ChangeNowLoadingUIState(false);

        // ゲーム中のUIを表示する
        gameUIController.ChangeGameUIState(true);
        gameUIController.ChangeLaunchCameraUIState(true);

        // UIのレイアウトを更新する
        Canvas.ForceUpdateCanvases();

        // ミニカメラの移動可能範囲を初期化
        launchMiniCameraUIController.InitializeMoveArea();
    }

    /// <summary>
    /// 全てのガードの移動が完了したか判定する
    /// </summary>
    private bool IsAllGuardMoveEnd()
    {
        // すべてのガードの移動終了フラグを見る
        foreach (GuardRotator guard in guardRotator)
        {
            // ガードの移動が未完了時はfalseを返す
            if (!guard.MoveEnd)
            {
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// ゲームオーバーUIを表示する
    /// </summary>
    public void ShowGameOverUI()
    {
        // ゲーム中UIを非表示にする
        gameUIController.ChangeGameUIState(false);
        gameUIController.ChangeLaunchCameraUIState(false);

        // ゲームオーバーUIを表示する
        gameOverUIController.ChangeGameOverUIState(true);
    }
}
