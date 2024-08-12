using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class written by the animation creator for testing.
/// </summary>
public class SPUM_Prefabs : MonoBehaviour
{
    [SerializeField] protected float _version;
    [SerializeField] protected SPUM_SpriteList _spriteOBj;
    [SerializeField] protected bool EditChk;
    [SerializeField] protected string _code;
    public Animator _anim;
    [SerializeField] protected bool _horse;
    public bool isRideHorse{
        get => _horse;
        set {
            _horse = value;
            UnitTypeChanged?.Invoke();
        }
    }
    [SerializeField] protected string _horseString;

    [SerializeField] protected UnityEvent UnitTypeChanged = new UnityEvent();

    protected AnimationClip[] _animationClips;
    public AnimationClip[] AnimationClips => _animationClips;
    protected Dictionary<string, int> _nameToHashPair = new Dictionary<string, int>();
    protected virtual void InitAnimPair(){
        _nameToHashPair.Clear();
        _animationClips = _anim.runtimeAnimatorController.animationClips;
        foreach (var clip in _animationClips)
        {
            int hash = Animator.StringToHash(clip.name);
            _nameToHashPair.Add(clip.name, hash);
        }
    }
    protected virtual void Awake() {
        InitAnimPair();
    }
    protected virtual void Start() {
        UnitTypeChanged.AddListener(InitAnimPair);
    }

    /// <summary>
    /// Starts animation by name.
    /// </summary>
    /// <param name="name"></param>
    public virtual void PlayAnimation(string name){
        foreach (var animationName in _nameToHashPair)
        {
            if(animationName.Key.ToLower().Contains(name.ToLower()) ){
                _anim.Play(animationName.Value, 0);
                break;
            }
        }
    }
}
