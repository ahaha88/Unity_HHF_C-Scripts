using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任意のアニメーションステートにアタッチするAnimationBehaviourの基底クラス。
/// アニメーションの開始と終了時に外部から登録した処理を実行する。
/// 設計をスッキリさせるためにイベントリスナーを利用した。
/// 派生クラスはこのクラスの継承のみをし、処理を書き込む必要はない。クラス名のみ分けること。
/// </summary>

public class AnimationBehaviour : StateMachineBehaviour
{
    private Action OnAnimEnterListener; // アニメーションが開始したときのイベントリスナー
    private Action OnAnimUpdateListener; // アニメーションがフレームごとに更新するときのイベントリスナー
    private Action OnAnimExitListener;  // アニメーションが終了したときのイベントリスナー

    // アニメーションが開始したとき登録した処理を実行
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnAnimEnterListener != null)
        {
            OnAnimEnterListener.Invoke();
        }
    }

    // アニメーションが更新するとき登録した処理を実行
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnAnimUpdateListener != null)
        {
            OnAnimUpdateListener.Invoke();
        }
    }

    // アニメーションが終了したとき登録した処理を実行
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnAnimExitListener != null)
        {
            OnAnimExitListener.Invoke();
        }
    }

    // 他のクラスからアニメーションが開始したときの処理を登録
    public void SetAnimEnterListener(Action action)
    {
        OnAnimEnterListener += action;
    }

    // 他のクラスからアニメーションが更新するときの処理を登録
    public void SetAnimUpdateListener(Action action)
    {
        OnAnimUpdateListener += action;
    }

    // 他のクラスからアニメーションが終了したときの処理を登録
    public void SetAnimExitrListener(Action action)
    {
        OnAnimExitListener += action; 
    }
}