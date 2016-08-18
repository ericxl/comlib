using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Linq;

public class SpriteAnimator : MonoBehaviour
{
    [System.Serializable]
    public class AnimationTrigger
    {
        public int frame;
        public string name;
    }

    [System.Serializable]
    public class Animation
    {
        public string name;
        public int fps;
        public bool scaled = true;
        public bool loop = true;
        public Sprite[] frames;
        public UnityEvent EndOfAnim;
        public AnimationTrigger[] triggers;
    }

    public SpriteRenderer spriteRenderer;
    public Animation[] animations;

    public bool playing { get; private set; }
    public Animation currentAnimation { get; private set; }
    public int currentFrame { get; private set; }
    public bool loop { get; private set; }

    public string playAnimationOnStart;

#if !GAME_SERVER

    private void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (playAnimationOnStart != "")
            Play(playAnimationOnStart);
    }

    private void OnDisable()
    {
        playing = false;
        currentAnimation = null;
    }

    public void Play(string frameName, bool loop = true, int startFrame = 0)
    {
        var _animation = GetAnimation(frameName);
        if (_animation != null)
        {
            if (_animation != currentAnimation)
            {
                ForcePlay(frameName, _animation.loop, startFrame);
            }
        }
        else
        {
            Debug.LogWarning("could not find animation: " + frameName);
        }
    }

    public void ForcePlay(string frameName, bool shouldLoop = true, int startFrame = 0)
    {
        var _animation = GetAnimation(frameName);
        if (_animation != null)
        {
            this.loop = shouldLoop;
            currentAnimation = _animation;
            playing = true;
            currentFrame = startFrame;
            spriteRenderer.sprite = _animation.frames[currentFrame];
            StopAllCoroutines();
            StartCoroutine(PlayAnimation(currentAnimation));
        }
    }

    public void SlipPlay(string frameName, int wantFrame, params string[] otherNames)
    {
        if (otherNames.Any(t => currentAnimation != null && currentAnimation.name == t))
        {
            Play(frameName, true, currentFrame);
        }
        Play(frameName, true, wantFrame);
    }

    public bool IsPlaying(string animName)
    {
        return (currentAnimation != null && currentAnimation.name == animName);
    }

    public Animation GetAnimation(string animName)
    {
        return animations.FirstOrDefault(_animation => _animation.name == animName);
    }

    private IEnumerator PlayAnimation(Animation _animation)
    {
        var timer = 0f;
        var delay = 1f / (float)_animation.fps;
        while (loop || currentFrame < _animation.frames.Length - 1)
        {

            while (timer < delay)
            {
                if (_animation.scaled)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                }
                yield return 0f;
            }
            while (timer > delay)
            {
                timer -= delay;
                NextFrame(_animation);
            }

            spriteRenderer.sprite = _animation.frames[currentFrame];
        }
        _animation.EndOfAnim.Invoke();
        currentAnimation = null;
    }

    private void NextFrame(Animation _animation)
    {
        currentFrame++;
        if (currentAnimation != null && currentAnimation.triggers.Length > 0)
        {
            foreach (var animationTrigger in currentAnimation.triggers)
            {
                if (animationTrigger.frame == currentFrame)
                {
                    gameObject.SendMessageUpwards(animationTrigger.name);
                }
            }
        }


        if (currentFrame >= _animation.frames.Length)
        {
            if (loop)
                currentFrame = 0;
            else
                currentFrame = _animation.frames.Length - 1;
        }
    }

    public int GetFacing()
    {
        return (int)Mathf.Sign(spriteRenderer.transform.localScale.x);
    }

    public void FlipTo(float dir)
    {
        spriteRenderer.transform.localScale = dir < 0f ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
    }

    public void FlipTo(Vector3 position)
    {
        var diff = position.x - transform.position.x;
        spriteRenderer.transform.localScale = diff < 0f ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
    }

#endif
}