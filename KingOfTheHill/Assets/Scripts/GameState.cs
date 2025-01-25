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
    public Spawner spawner;
    public UIManager uiManager;

    private void Start()
    {
        floorNumber = 0;
        waveNumber = 0;
        phase = Utils.Phase.Prep;
        players = new List<Player>
        {
            new Player(Utils.Role.Attacker),
            new Player(Utils.Role.Defender),
        };
        activePlayer = players[0];

    }

    void SimulatePrep()
    {
        // Simulate preparation phase
        // Get the spawn that was clicked, if any and add a unit there.
        // TODO: Implement more paths to correspond to spawns
        GameObject tempSpawn = uiManager.GetSpawnClicked();
        // Debug.Log("passing spawn code");
        if (tempSpawn != null)
        {
            Debug.Log("shoulda spawned");
            players[0].addUnit(spawner.GetPrefabInstance(Utils.ParentObject.Knight, players[0].getRole(), paths[0]));
        }

        if (uiManager.GetReadyClicked()) 
        {
            phase = Utils.Phase.Combat;
        }
    }
    void SimulateCombat()
    {
        // Simulate combat between all players
        foreach (Player player in players)
        {
            foreach (GameObject unit in player.getUnits())
            {
                if (unit.GetComponent<Unit>().IsActive() && unit.GetComponent<Unit>().CanAttack())
                {
                    // Play attack animation

                    // Deal damage to target
                    // TODO Add null check for target
                    //unit.GetComponent<Unit>().getTarget().takeDamage(unit.GetComponent<Unit>().getDamage());
                }
            }
            foreach (GameObject tower in player.getTowers())
            {
                if (tower.GetComponent<Tower>().IsActive())
                {
                    // Play attack animation

                    // Deal damage to target
                    tower.GetComponent<Tower>().GetTarget().TakeDamage(tower.GetComponent<Tower>().GetDamage());
                }
            }
        }

        // Move units and check if last checkpoint is reached
        foreach (Player player in players)
        {
            foreach (GameObject unit in player.getUnits())
            {
                // Move unit towards target on path
                if (unit.GetComponent<Unit>().Move())
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
                foreach (GameObject unit in player.getUnits())
                {
                    if (unit.GetComponent<Unit>().GetHealth() > 0)
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
        // Debug.Log("Update");
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
