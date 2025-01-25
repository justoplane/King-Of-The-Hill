package ws

import (
	"encoding/json"
	"king-of-the-tower/game"
)

type MessageType string

const (
	UnitPlaced       MessageType = "unitPlaced"
	UnitAttack       MessageType = "unitAttack"
	UnitDeath        MessageType = "unitDeath"
	TowerPlaced      MessageType = "towerPlaced"
	TowerAttack      MessageType = "towerAttack"
	BarrierBroken    MessageType = "barrierBroken"
	GameStateSync    MessageType = "gameStateSync"
	ThresholdCrossed MessageType = "thresholdCrossed"
)

type WSMessage struct {
	MessageType MessageType     `json:"messageType"`
	Data        json.RawMessage `json:"data"`
}

type UnitPlacedData struct {
	Unit game.Unit `json:"unit"`
}

type UnitAttackData struct {
	AttackingUnit game.Unit  `json:"attackingUnit"`
	Target        game.Tower `json:"target"`
}

type UnitDeathData struct {
	Unit game.Unit `json:"unit"`
}

type TowerPlacedData struct {
	Tower game.Tower `json:"tower"`
}

type TowerAttackData struct {
	AttackingTower game.Tower `json:"attackingTower"`
	Target         game.Unit  `json:"target"`
}

type BarrierBrokenData struct {
	Barrier game.Tower `json:"barrier"`
}

type GameStateSyncData struct {
	FloorNumber uint32       `json:"floorNumber"`
	WaveNumber  uint32       `json:"waveNumber"`
	Phase       uint32       `json:"phase"`
	Towers      []game.Tower `json:"towers"`
	Units       []game.Unit  `json:"units"`
}

type ThresholdCrossedData struct {
	Threshold uint32 `json:"threshold"`
}

func (wsMessage WSMessage) UnmarshalData(data interface{}) error {
	return json.Unmarshal(wsMessage.Data, data)
}
