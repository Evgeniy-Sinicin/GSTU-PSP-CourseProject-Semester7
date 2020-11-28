using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public static readonly string PLAYER = "Player";

    [SerializeField]
    private float _maxHealth = 100f;

    [SyncVar]
    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void Damage(float damage)
    {
        _health -= damage;

        Debug.Log($"{transform.name} health: {_health}");
    }
}
