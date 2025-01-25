using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    int floorNumber;
    int waveNumber;
    Utils.Phase phase;
    Player activePlayer;
    List<Player> players;
}
