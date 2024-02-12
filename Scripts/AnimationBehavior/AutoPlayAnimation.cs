using UnityEngine;

public class AutoPlayAnimations : MonoBehaviour
{
    public AnimationClip[] animations;
    private int currentAnimationIndex = 0;
    Transform initialTransform;

    private void Start()
    {
        PlayNextAnimation();
    }

    private void Update()
    {
        // アニメーションが終了したら次のアニメーションを再生
        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animations[currentAnimationIndex].name) &&
            GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0)
        {
            PlayNextAnimation();
        }
    }

    private void PlayNextAnimation()
    {
        GetComponent<Animator>().Play(animations[currentAnimationIndex].name);
        currentAnimationIndex = (currentAnimationIndex + 1) % animations.Length;
    }
}
