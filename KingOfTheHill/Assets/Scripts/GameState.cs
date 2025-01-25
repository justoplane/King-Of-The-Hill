using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public Path[] paths;
    int floorNumber;
    int waveNumber;
    Utils.Phase phase;
    Player activePlayer;
    List<Player> players;
    List<Entity> checkpoints;
    bool finishedCombat;
    bool allUnitsDead;
    bool allCheckpointsReached;
    [SerializeField]
    GameObject prefab;

    private void Start()
    {
        floorNumber = 0;
        waveNumber = 0;
        phase = Utils.Phase.Prep;
        players = new List<Player>
        {
            // new Player(Utils.Role.Attacker),
            // new Player(Utils.Role.Defender),
        };
        // activePlayer = players[0];

    }

    void SimulatePrep()
    {
        // Simulate preparation phase
        // Janky manual spawning...
        for (int i = 0; i < 5; i++)
        {
            players[0].addUnit(Utils.ParentObject.Knight, paths[0]);
        }
        // Spawn troops
        for (int i = 0; i < players.Count; i++)
        {
            players[1].addUnit(Utils.ParentObject.Knight, paths[1]);
        }
    }
    void SimulateCombat()
    {
        // Simulate combat between all players
        foreach (Player player in players)
        {
            foreach (Unit unit in player.getUnits())
            {
                if (unit.isActive() && unit.canAttack())
                {
                    // Play attack animation
                    
                    // Deal damage to target
                    unit.getTarget().takeDamage(unit.getDamage());
                }
            }
            foreach (Tower tower in player.getTowers())
            {
                if (tower.isActive())
                {
                    // Play attack animation

                    // Deal damage to target
                    tower.getTarget().takeDamage(tower.getDamage());
                }
            }
        }

        // Move units and check if last checkpoint is reached
        foreach (Player player in players)
        {
            foreach (Unit unit in player.getUnits())
            {
                // Move unit towards target on path
                if (unit.Move())
                {
                    allCheckpointsReached = true;
                    break;
                }
            }
        }

        // If all units are dead
        foreach (Player player in players)
        {
            if (player.getRole() == Utils.Role.Attacker)
            {
                allUnitsDead = true;
                foreach (Unit unit in player.getUnits())
                {
                    if (unit.getHealth() > 0)
                    {
                        allUnitsDead = false;
                        break;
                    }
                }
            }
        }
        // Set flags for finished combat
        if (allUnitsDead || allCheckpointsReached)
        {
            finishedCombat = true;
        }
    }

    void resolveCombat() 
    {
        // check what losing/winning conditions has been met
        if (allUnitsDead)
        {
            // Defenders win
            WinWave(Utils.Role.Defender);
        }
        else if (allCheckpointsReached)
        {
            // Attackers win
            WinWave(Utils.Role.Attacker);
        }
    }

    void SimulateReward()
    {
       // Simulate reward phase
    }

    void SimulateUpgrade()
    {
        // Simulate upgrade phase
    }

    void WinWave(Utils.Role role)
    {
        // Update consistent score and wave info and stuff
    }

    private void Update()
    {
        // switch (phase) {
        //     case Utils.Phase.Prep:
        //         SimulatePrep();
        //         break;
        //     case Utils.Phase.Combat:
        //         SimulateCombat();
        //         if (finishedCombat)
        //         {
        //             resolveCombat();
        //         }
        //         break;
        //     case Utils.Phase.Reward:
        //         SimulateReward();
        //         break;
        //     case Utils.Phase.Upgrade:
        //         SimulateUpgrade();
        //         break;
        // }
    }
}
