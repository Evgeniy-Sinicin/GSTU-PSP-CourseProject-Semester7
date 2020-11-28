using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Dictionary<string, Player> _players = new Dictionary<string, Player>();

    public static void Register(string name, Player player)
    {
        _players.Add(name, player);
    }

    public static void Unregister(string name)
    {
        _players.Remove(name);
    }

    public static Player GetPlayer(string name)
    {
        return _players[name];
    }
}
