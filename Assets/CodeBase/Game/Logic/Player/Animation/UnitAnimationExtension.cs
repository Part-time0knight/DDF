using System;
using System.Linq;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class UnitAnimationExtension : SPUM_Prefabs
    {
        private Action _clipCallback;

        public override void PlayAnimation(string name)
        {
            base.PlayAnimation(name);
        }

        public void PlayAnimation(string name, Action callback)
        {
            foreach (var animationName in _nameToHashPair)
            {
                if (animationName.Key.ToLower().Contains(name.ToLower()))
                {
                    var clip = _animationClips.FirstOrDefault(item => item.name == animationName.Key);
                    _clipCallback = callback;

                    if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == clip.name)
                    {
                        InvokeCallback();
                        return;
                    }

                    if (!clip.events.Any(c => c.functionName == "InvokeCallback"))
                    {
                        AnimationEvent animationCallback = new();
                        animationCallback.time = clip.length;
                        animationCallback.functionName = "InvokeCallback";
                        clip.AddEvent(animationCallback);
                    }

                    _anim.Play(animationName.Value, 0);
                    return;
                }
            }
        }

        public void InvokeCallback()
        {
            _clipCallback?.Invoke();
            _clipCallback = null;
        }
    }
}