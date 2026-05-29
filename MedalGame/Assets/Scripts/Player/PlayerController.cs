using UnityEngine;

/// <summary>
/// スマートフォンタップ判定
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("メダル発射クラス参照用")]
    [SerializeField] private MedalLauncher medalLauncher = null;

    [Header("Rayを飛ばすクラス参照用")]
    [SerializeField] private RayController rayController = null;

    [Header("Rayを飛ばすときに使用するカメラ")]
    [SerializeField] private Camera launchCamera = null;

    // タッチ入力を検知するinputSystemクラス
    private PlayerInputActions inputActions = null;

    // タップ位置が有効かどうか
    private bool isAimValid = false;

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
            // 落下UIの表示
        }

        if (inputActions.Player.Press.IsPressed())
        {
            // タップ位置を取得
            Vector2 touchPosition = inputActions.Player.Position.ReadValue<Vector2>();

            // タップ位置をスクリーン座標からビューポート座標に変換
            Vector3 viewportPoint = launchCamera.ScreenToViewportPoint(touchPosition);

            // タップ位置が有効かどうかを確認
            if (0.0f <= viewportPoint.x && viewportPoint.x <= 1.0f &&
                0.0f <= viewportPoint.y && viewportPoint.y <= 1.0f)
            {
                isAimValid = true;

                // Rayを飛ばすメソッド実行
                rayController.RayLaunch(touchPosition, launchCamera);
            }
            else isAimValid = false;
        }

        if (inputActions.Player.Press.WasReleasedThisFrame())
        {
            // タップ位置が有効かどうかを確認
            if (!isAimValid) return;
            // Rayが当たった座標がnullでないか確認
            if (!rayController.isHit) return;

            // 特定の座標にコインを飛ばす
            medalLauncher.LaunchMedal(rayController.GetHitPoint());
        }
    }
}
