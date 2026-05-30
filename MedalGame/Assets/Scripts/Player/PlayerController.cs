using UnityEngine;

/// <summary>
/// スマートフォンタップ判定
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("タイトルのUI管理クラス参照用")]
    [SerializeField] private TitleManager titleManager = null;

    [Header("ゲーム中のUI管理クラス参照用")]
    [SerializeField] private GameUIController gameUIController = null;

    [Header("メダル発射クラス参照用")]
    [SerializeField] private MedalLauncher medalLauncher = null;

    [Header("Rayを飛ばすクラス参照用")]
    [SerializeField] private RayController rayController = null;

    [Header("ミニカメラを移動させるクラス参照")]
    [SerializeField] private LaunchMiniCameraUIController launchMiniCameraUIController = null;

    [Header("Rayを飛ばすときに使用するカメラ")]
    [SerializeField] private Camera launchCamera = null;

    [Header("Rayを飛ばすときに使用するカメラUI")]
    [SerializeField] private RectTransform miniCameraUI = null;


    // タッチ入力を検知するinputSystemクラス
    private PlayerInputActions inputActions = null;

    // タップ位置が有効かどうか
    private bool isAimValid = false;

    // ドラッグ中かどうか
    private bool isDragging = false;

    /// <summary>
    /// 新しくInputSystemクラスを生成
    /// </summary>
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }


    /// <summary>
    /// 入力監視開始
    /// </summary>
    private void OnEnable()
    {
        inputActions.Enable();
    }


    /// <summary>
    /// 入力監視停止
    /// </summary>
    private void OnDisable()
    {
        inputActions.Disable();
    }


    /// <summary>
    /// プレイヤーがタップしたときの処理
    /// </summary>
    private void Update()
    {
        // タップした瞬間の処理
        if (inputActions.Player.Press.WasPressedThisFrame())
        {
            // タップ位置を取得
            Vector2 touchPosition = inputActions.Player.Position.ReadValue<Vector2>();

            // ドラッグ開始の判定
            foreach (RectTransform frame in launchMiniCameraUIController.MiniCameraDragFrames)
            {
                // タップ位置がminiCameraUIの範囲内かどうかを確認
                if (RectTransformUtility.RectangleContainsScreenPoint(frame, touchPosition))
                {
                    // ドラッグ開始
                    isDragging = true;

                    // ドラッグ開始位置を保存
                    launchMiniCameraUIController.UpdatePreviousPosition(touchPosition);
                    return;
                }
            }

            // タイトル画面中でなければ処理しない
            if (!titleManager.IsTitle) return;

            // タイトルUIを非表示にする
            titleManager.ChangeTitleState(false);

            // ゲーム中のUIを表示する
            gameUIController.ChangeGameUIState(true);
            gameUIController.ChangeLaunchCameraUIState(true);

        }

        if (titleManager.IsTitle) return;

        // タップしている間の処理
        if (inputActions.Player.Press.IsPressed())
        {
            // タップ位置を取得
            Vector2 touchPosition = inputActions.Player.Position.ReadValue<Vector2>();

            // ドラッグ中はミニカメラを移動
            if (isDragging)
            {
                launchMiniCameraUIController.SlideCamera(touchPosition);

                return;
            }


            // タップ位置を保持する変数
            Vector2 localPoint = Vector2.zero;

            // タップ位置をminiCameraUIのローカル座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(miniCameraUI, touchPosition, null, out localPoint);

            // miniCameraUIのRectを取得
            Rect rect = miniCameraUI.rect;

            // タップ位置を0.0f～1.0fの範囲に変換
            localPoint.x = (localPoint.x - rect.x) / rect.width;
            localPoint.y = (localPoint.y - rect.y) / rect.height;

            // タップ位置が有効かどうかを確認
            if (0.0f <= localPoint.x && localPoint.x <= 1.0f &&
                0.0f <= localPoint.y && localPoint.y <= 1.0f)
            {
                // Rayを飛ばすメソッド実行
                rayController.RayLaunch(launchCamera, localPoint);

                // タップ位置が有効
                isAimValid = true;

            }
            // タップ位置が無効
            else
            {
                isAimValid = false;
            }
        }

        // タップをやめた瞬間の処理
        if (inputActions.Player.Press.WasReleasedThisFrame())
        {
            // ドラッグ終了
            isDragging = false;

            // タップ位置が有効かどうかを確認
            if (!isAimValid) return;
            // Rayが当たった座標がnullでないか確認
            if (!rayController.isHit) return;

            // 特定の座標にコインを飛ばす
            medalLauncher.LaunchMedal(rayController.GetHitPoint());

        }
    }
}
