using Game.Logic.Handlers;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player.Animation
{

    public class UnitAnimationWrapper : SPUM_Prefabs, IPauseble, IDisposable
    {

        private Action _clipCallback;

        private IPauseHandler _pauseHandler;

        public float CurrentClipSpeed => _anim.speed;

        [Inject]
        private void Construct(IPauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
            _pauseHandler.SubscribeElement(this);
        }

        public void Dispose()
        {
            _pauseHandler.UnsubscribeElement(this);
        }

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

        private void AnimationSpeed(float speed)
        {
            _anim.speed = speed;
        }

        public void InvokeCallback()
        {
            _clipCallback?.Invoke();
            _clipCallback = null;
        }

        public void OnPause(bool active)
        {
            if (active)
                AnimationSpeed(0f);
            else
                AnimationSpeed(1f);
        }

    }
}