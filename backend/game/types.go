package game

type GameState struct {
	FloorNumber  uint32 `json:"floorNumber"`
	WaveNumber   uint32 `json:"waveNumber"`
	Phase        uint32 `json:"phase"`
	ActivePlayer uint8  `json:"activePlayer"` // unused on the server
}

type Player struct {
	Economy uint32  `json:"economy"`
	Units   []Unit  `json:"units"`
	Towers  []Tower `json:"towers"`
	// Abilities []Ability `json:"abilities"`
}

type Entity struct {
	Name        string     `json:"name"`
	Location    Coordinate `json:"location"`
	Damage      uint32     `json:"damage"`
	AttackSpeed float32    `json:"attackSpeed"`
	Range       float32    `json:"range"`
	DamageType  string     `json:"damageType"`
	Active      bool       `json:"active"`
	Upgrades    []Upgrade  `json:"upgrades"`
}

type Unit struct {
	Entity
	MovementSpeed float32 `json:"movementSpeed"`
}

type Tower struct {
	Entity
}

type Upgrade struct {
	Damage        uint32  `json:"damage"`
	AttackSpeed   float32 `json:"attackSpeed"`
	Range         float32 `json:"range"`
	MovementSpeed float32 `json:"movementSpeed"`
	Name          string  `json:"name"`
	ParentObject  string  `json:"parentObject"`
	PathNumber    uint8   `json:"pathNumber"`
	UpgradeNumber uint8   `json:"upgradeNumber"`
	Cost          uint32  `json:"cost"`
	Active        bool    `json:"active"`
	Unlocked      bool    `json:"unlocked"`
}

type Coordinate struct {
	X float32 `json:"x"`
	Y float32 `json:"y"`
}
