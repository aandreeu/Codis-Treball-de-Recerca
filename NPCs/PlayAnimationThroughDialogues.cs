using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayAnimationThroughDialogues : MonoBehaviour
{
    public AnimatorAnimation[] animations;
    [SerializeField] Dialogues dialogue;
    [SerializeField] int dialogueLineToPlay;

    private void Update()
    {
        if (dialogue.lineIndex == dialogueLineToPlay)
        {
            foreach(AnimatorAnimation anim in animations)
            {
                anim.animator.Play(anim.animationName);
            }
            this.enabled = false;
        }
    }

}
[System.Serializable]
public class AnimatorAnimation{
    public Animator animator;
    public string animationName;

}
