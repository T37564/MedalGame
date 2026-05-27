using UnityEngine;

/// <summary>
/// スマートフォンタップ判定
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MedalLauncher medalLauncher = null;

    private void Update()
    {
        // タッチされていないなら処理しない
        if (Input.touchCount <= 0) return;

        // 最初のタッチ情報を取得
        Touch touch = Input.GetTouch(0);

        // タッチ開始
        if (touch.phase == TouchPhase.Began)
        {
            // Rayを飛ばす処理
            Debug.Log("タッチ開始");
        }

        // タッチ中
        if (touch.phase == TouchPhase.Moved)
        {
            // Rayの移動
            Debug.Log("タッチ移動中");
        }

        // 指を離した瞬間
        if (touch.phase == TouchPhase.Ended)
        {
            // 座標取得

            //　取得した座標をもとにメダルを発射する処理
            //medalLauncher.LaunchMedal();
        }
    }
}
