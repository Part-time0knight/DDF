using System;
using System.Linq;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class UnitAnimationWrapper : SPUM_Prefabs
    {

        private Action _clipCallback;

        public override void PlayAnimation(string name)
        {
            base.PlayAnimation(name);
        }

        public void PlayAnimation(string name, Action callback)
        {
            const string invokeCallback = nameof(InvokeCallback);
            const int animationLayer = 0;

            foreach (var animationName in _nameToHashPair)
            {
                if (animationName.Key.ToLower().Contains(name.ToLower()))
                {
                    var clip = _animationClips.FirstOrDefault(item => item.name == animationName.Key);
                    
                    if (!clip)
                        return;

                    _clipCallback = callback;

                    if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == clip.name)
                    {
                        InvokeCallback();
                        return;
                    }

                    if (!clip.events.Any(c => c.functionName == invokeCallback))
                    {
                        AnimationEvent animationCallback = new();
                        animationCallback.time = clip.length;
                        animationCallback.functionName = invokeCallback;
                        clip.AddEvent(animationCallback);
                    }

                    _anim.Play(animationName.Value, animationLayer);
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