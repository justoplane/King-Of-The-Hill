package game

type EntityType string

const (
	KnightUnit      EntityType = "knight"
	MageUnit        EntityType = "mage"
	KnightTowerType EntityType = "knightTower"
	MageTowerType   EntityType = "mageTower"
	BarrierType     EntityType = "barrier"
)

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
	Type        EntityType `json:"type"`
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

var Knight Unit = Unit{
	Entity: Entity{
		Name:        "Knight",
		Type:        KnightUnit,
		Location:    Coordinate{X: 0, Y: 0, Rotation: 0},
		Damage:      10,
		AttackSpeed: 1.0,
		Range:       1.0,
		DamageType:  "physical",
		Active:      true,
		Upgrades:    nil,
	},
	MovementSpeed: 1.0,
}

var Mage Unit = Unit{
	Entity: Entity{
		Name:        "Mage",
		Type:        MageUnit,
		Location:    Coordinate{X: 0, Y: 0, Rotation: 0},
		Damage:      10,
		AttackSpeed: 1.0,
		Range:       1.0,
		DamageType:  "magical",
		Active:      true,
		Upgrades:    nil,
	},
	MovementSpeed: 1.0,
}

var KnightTower Tower = Tower{
	Entity: Entity{
		Name:        "KnightTower",
		Type:        KnightTowerType,
		Location:    Coordinate{X: 0, Y: 0, Rotation: 0},
		Damage:      10,
		AttackSpeed: 1.0,
		Range:       1.0,
		DamageType:  "physical",
		Active:      true,
		Upgrades:    nil,
	},
	TargetPriority: First,
}

var MageTower Tower = Tower{
	Entity: Entity{
		Name:        "MageTower",
		Type:        MageTowerType,
		Location:    Coordinate{X: 0, Y: 0, Rotation: 0},
		Damage:      10,
		AttackSpeed: 1.0,
		Range:       1.0,
		DamageType:  "magical",
		Active:      true,
		Upgrades:    nil,
	},
	TargetPriority: First,
}

var Barrier Tower = Tower{
	Entity: Entity{
		Name:        "Barrier",
		Type:        BarrierType,
		Location:    Coordinate{X: 0, Y: 0, Rotation: 0},
		Damage:      0,
		AttackSpeed: 0.0,
		Range:       0.0,
		DamageType:  "physical",
		Active:      true,
		Upgrades:    nil,
	},
	TargetPriority: First,
}

type Tower struct {
	Entity
	TargetPriority TargetPriority `json:"targetPriority"`
}

type TargetPriority uint8

const (
	First  TargetPriority = 0
	Second TargetPriority = 1
	Third  TargetPriority = 2
	Last   TargetPriority = 3
)

type Upgrade struct {
	Damage        uint32     `json:"damage"`
	AttackSpeed   float32    `json:"attackSpeed"`
	Range         float32    `json:"range"`
	MovementSpeed float32    `json:"movementSpeed"`
	Name          string     `json:"name"`
	ParentType    EntityType `json:"parentType"`
	PathNumber    uint8      `json:"pathNumber"`
	UpgradeNumber uint8      `json:"upgradeNumber"`
	Cost          uint32     `json:"cost"`
	Active        bool       `json:"active"`
	Unlocked      bool       `json:"unlocked"`
}

type Coordinate struct {
	X        float32 `json:"x"`
	Y        float32 `json:"y"`
	Rotation float32 `json:"rotation"` // in degrees
}
