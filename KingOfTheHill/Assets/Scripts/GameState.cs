using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    int floorNumber;
    int waveNumber;
    Utils.Phase phase;
    Player activePlayer;
    List<Player> players;
    Transform spawnLocation;
    List<Entity> checkpoints;
    bool finishedCombat;
    bool allUnitsDead;
    bool allCheckpointsReached;

    void SimulatePrep()
    {
        // Simulate preparation phase
    }
    void SimulateCombat()
    {
        // Simulate combat between all players
        foreach (Player player in players)
        {
            foreach (Unit unit in player.getUnits())
            {
                if (unit.isActive())
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

        // Move units
        foreach (Player player in players)
        {
            foreach (Unit unit in player.getUnits())
            {
                // Move unit towards target on path
            }
        }

        // Update losing/winning conditions
        // If all checkpoints are destroyed
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
            if (player.getRole() == Utils.Role.Defender)
            {
                allCheckpointsReached = true;
                foreach (Entity checkpoint in checkpoints)
                {
                    if (checkpoint.getHealth() > 0)
                    {
                        allCheckpointsReached = false;
                        break;
                    }
                }
            }
        }


    }

    void resolveCombat() 
    {
        // check what losing/winning conditions has been met

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
        switch (phase) {
            case Utils.Phase.Prep:
                SimulatePrep();
                break;
            case Utils.Phase.Combat:
                SimulateCombat();
                if (finishedCombat)
                {
                    resolveCombat();
                }
                break;
            case Utils.Phase.Reward:
                SimulateReward();
                break;
            case Utils.Phase.Upgrade:
                SimulateUpgrade();
                break;
        }
    }
}
