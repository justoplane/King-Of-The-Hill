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

