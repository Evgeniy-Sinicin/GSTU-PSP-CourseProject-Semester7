using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    private Camera _mainCamera;

    [SerializeField]
    private List<Behaviour> _disabledComponents;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            _disabledComponents.ForEach(x => x.enabled = false);
            gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
        }
        else
        {
            _mainCamera = Camera.main;
            _mainCamera?.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (_mainCamera != null)
        {
            _mainCamera.gameObject.SetActive(true);
        }

        GameManager.Unregister(transform.name);
    }

    public override void OnStartClient()
    {
        var id = GetComponent<NetworkIdentity>().netId.ToString();
        var player = GetComponent<Player>();

        base.OnStartClient();

        transform.name = $"{Player.PLAYER}{id}";
        GameManager.Register(transform.name, player);
    }
}
