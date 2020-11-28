using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public Weapon weapon;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LayerMask _mask;

    private void Start()
    {
        if (_camera == null)
        {
            Debug.LogError("Player Shoot: No camera!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, weapon.Range, _mask))
        {
            if (hit.collider.tag.Equals("Player"))
            {
                CmdShoot(hit.collider.name);
            }
        }
    }

    [Command]
    private void CmdShoot(string name)
    {
        var player = GameManager.GetPlayer(name);

        player.Damage(weapon.Damage);

        Debug.Log($"Player with id {name} was shot.");
    }
}
