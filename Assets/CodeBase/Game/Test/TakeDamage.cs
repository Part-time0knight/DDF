using Game.Logic.Player;
using UnityEngine;
using Zenject;

public class TakeDamage : MonoBehaviour
{
    private PlayerHandler _player;

    [Inject]
    private void Construct(PlayerHandler player)
    {
        _player = player;
    }

    public void MakeDamage()
    {
        _player.TakeDamage(1);
    }
}
