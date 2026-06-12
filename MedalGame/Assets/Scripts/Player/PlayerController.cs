using UnityEngine;

/// <summary>
/// タップ入力による操作を管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("メダル発射クラス参照用")]
    [SerializeField] private MedalLauncher medalLauncher = null;

    [Header("Rayを飛ばすクラス参照用")]
    [SerializeField] private RayController rayController = null;

    [Header("Rayを飛ばすときに使用するカメラ")]
    [SerializeField] private Camera launchCamera = null;

    [Header("Rayを飛ばすときに使用するカメラUI")]
    [SerializeField] private RectTransform miniCameraUI = null;


    // Input Systemの入力情報
    private PlayerInputActions inputActions = null;

    // エイム位置が有効かどうか
    private bool isAimValid = false;

    // ドラッグ中かどうか
    private bool isDragging = false;

    /// <summary>
    /// PlayerInputActionsを生成する
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
        // タップ開始時の処理
        HandlePressStart();

        // タイトル画面中はゲーム操作を行わない
        if (UIManager.Instance.IsTitle) return;

        // タップ中の処理
        HandlePressing();

        // タップをやめた時の処理
        HandlePressRelease();
    }

    /// <summary>
    /// タップを開始したときの処理
    /// </summary>
    private void HandlePressStart()
    {
        // タップした瞬間のみ処理
        if (!inputActions.Player.Press.WasPressedThisFrame()) return;

        // タップ位置を取得
        Vector2 touchPosition = inputActions.Player.Position.ReadValue<Vector2>();

        // ドラッグ開始判定
        if (TryStartDrag(touchPosition))
        {
            return;
        }

        // タイトル画面中でなければ処理しない
        if (!UIManager.Instance.IsTitle) return;

        // ゲーム開始のUI遷移を開始する
        UIManager.Instance.StartGame();
    }

    /// <summary>
    /// ドラッグ開始判定を行う
    /// </summary>
    private bool TryStartDrag(Vector2 touchPosition)
    {
        // ドラッグ可能エリアを確認
        foreach (RectTransform frame in UIManager.Instance.LaunchMiniCameraUIController.MiniCameraDragFrames)
        {
            // タップ位置がドラッグ可能エリア内か確認
            if (RectTransformUtility.RectangleContainsScreenPoint(frame, touchPosition))
            {
                // ドラッグ開始
                isDragging = true;

                // ドラッグ開始位置を保存
                UIManager.Instance.LaunchMiniCameraUIController.SetPreviousPosition(touchPosition);

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// タップ中の処理を行う
    /// </summary>
    private void HandlePressing()
    {
        // タップ中でなければ処理しない
        if (!inputActions.Player.Press.IsPressed()) return;

        // 現在のタップ位置を取得
        Vector2 touchPosition = inputActions.Player.Position.ReadValue<Vector2>();

        // ドラッグ中の処理
        if (isDragging)
        {
            UIManager.Instance.LaunchMiniCameraUIController.MoveMiniCamera(touchPosition);

            return;
        }

        UpdateAimPoint(touchPosition);
    }

    /// <summary>
    /// エイムの位置を更新する処理
    /// </summary>
    private void UpdateAimPoint(Vector2 touchPosition)
    {
        Vector2 viewportPoint = Vector2.zero;

        if (TryConvertToViewportPoint(touchPosition, out viewportPoint))
        {
            // タップ位置へRayを飛ばす
            rayController.LaunchRay(launchCamera, viewportPoint);

            // エイム位置が有効
            isAimValid = true;

            return;
        }

        // エイム位置が無効
        isAimValid = false;
    }

    /// <summary>
    /// タップ位置をViewport座標へ変換し、有効範囲内か判定する
    /// </summary>
    private bool TryConvertToViewportPoint(Vector2 touchPosition, out Vector2 viewportPoint)
    {
        // タップ位置をminiCameraUIのローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(miniCameraUI, touchPosition, null, out viewportPoint);

        // miniCameraUIのRectを取得
        Rect rect = miniCameraUI.rect;

        // タップ位置を0.0f～1.0fの範囲に変換
        viewportPoint.x = (viewportPoint.x - rect.x) / rect.width;
        viewportPoint.y = (viewportPoint.y - rect.y) / rect.height;

        // Viewport座標が有効範囲内か確認
        bool isInsideViewportArea = 0.0f <= viewportPoint.x && viewportPoint.x <= 1.0f &&
                                    0.0f <= viewportPoint.y && viewportPoint.y <= 1.0f;

        return isInsideViewportArea;
    }

    /// <summary>
    /// タップをやめた瞬間の処理
    /// </summary>
    private void HandlePressRelease()
    {
        // タップを離していなければ処理しない
        if (!inputActions.Player.Press.WasReleasedThisFrame()) return;

        // ドラッグ終了
        isDragging = false;

        // エイム位置が無効なら処理しない
        if (!isAimValid) return;
        // Rayがヒットしていなければ処理しない
        if (!rayController.IsHit) return;

        // Rayのヒット位置へメダルを発射する
        medalLauncher.LaunchMedal(rayController.GetHitPoint());
    }
}
