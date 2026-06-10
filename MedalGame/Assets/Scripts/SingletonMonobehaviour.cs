// -----------------------------------------------------------------------------------
// MonoBehaviour を継承したシングルトン基底クラス
// <typeparam name="T">シングルトン化したい MonoBehaviour クラス</typeparam>
// SingletonMonoBehaviour.cs
// Create.by YaegashiNaoki
//-----------------------------------------------------------------------------------
using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // この型の唯一のインスタンスを保持する静的フィールド
    private static T instance;

    /// <summary>
    /// シングルトンインスタンスへのグローバルアクセサ
    /// </summary>
    public static T Instance
    {
        get
        {
            // まだインスタンスが存在しない場合は探す
            if (instance == null)
            {
                Type t = typeof(T);

                // シーン内に存在するオブジェクトを検索
                instance = (T)FindAnyObjectByType(t);

                // 見つからなかった場合はエラーログを出す
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            // インスタンスを返す
            return instance;
        }
    }


    /// <summary>
    /// オブジェクト生成時に呼ばれる処理
    /// </summary>
    virtual protected void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }


    /// <summary>
    /// インスタンスの重複をチェックし、必要なら破棄する。
    /// </summary>
    /// <returns>
    /// このインスタンスが有効な場合は true、重複して破棄された場合は false。
    /// </returns>
    protected bool CheckInstance()
    {
        // まだインスタンスが存在しない場合、このオブジェクトを登録
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        else if (Instance == this)// すでに登録されているインスタンスが自分自身なら OK
        {
            return true;
        }

        // それ以外の場合は重複しているのでこのコンポーネントを破棄する
        Destroy(this);

        // 無効であることを返す
        return false;
    }
}