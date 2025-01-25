# General Unity Architecture
The following tasks will need to be completed:
* Towers
* Attackers
* Abilities
* Upgrades
* Pathfinding
* UI
* Game Logic/Player State

### Towers
* Name
* Current Location
* Damage
* Attack Speed
* Range
* Active (boolean)
* Damage Type (ENUM)
* Target Priority (ENUM)
* Upgrades
  * Array of Upgrade objects
* Game State
  * GetBonuses()

### Units
* Name
* Location
* Damage
* Attack Speed
* Range
* Movement Speed
* Active
* Damage Type (ENUM)
* Upgrades
* Array of Upgrade objects
* Game State
  * GetBonuses()

### UI Manager
* Has a reference to current clicked object
  * DoUpgrade(int)
  * Destroy()
* Game State

### Game State
* Floor Number
* Wave Number
* Phase (ENUM)
  * Prep
  * Combat
  * Reward
  * Upgrade
* Active Player
* Players (array)

### Player
* Economy
* Upgrade Manager
* Units
* Towers

### Upgrade Manager
* Retrieves info from json upon creation
* ParentObject (ENUM)
* Upgrade GetUpgradeInfo(ParentObject(ENUM value), int path (0 or 1), int upgradeNumber(0-3))
  * searches the list of structs based on ParentObject, path, and upgradeNumber

### Upgrade (struct)
* Damage
* Attack Speed
* Range
* Name
* ParentObject
* int path
* int upgradeNumber
* Cost
* Active
* Unlocked

### Messages to/from the server
* Unit placed
    - Which player placed it? - can be inferred
    - What type of unit?
    - Where was it placed?
* Tower placed
    - Which player placed it? - can be inferred
    - What type of unit?
    - Where was it placed?
* Unit attack
    - Which unit?
    - What type of unit?
    - Which entity was it aiming at?
* Tower attack
    - Which tower?
    - What type of tower?
    - Which entity was it aiming at?
* Game State Sync
    - Alternate which player initiates
    - Contains all the data in a GameState object
* Abilities
    - TBD. abilities not implemented yet
* Threshold crossed
    - is this necessary?
* Unit death
    - Which unit?
* Barrier broken
    - Which barrier?
