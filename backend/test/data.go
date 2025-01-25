package main

import (
	"encoding/json"
	"king-of-the-tower/game"
	"king-of-the-tower/ws"
)

var testMessages []ws.WSMessage = []ws.WSMessage{
	{
		MessageType: ws.UnitPlaced,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.UnitPlacedData{
				Unit: game.Knight,
			})
			return data
		}(),
	},
	{
		MessageType: ws.UnitPlaced,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.UnitPlacedData{
				Unit: game.Mage,
			})
			return data
		}(),
	},
	{
		MessageType: ws.TowerPlaced,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.TowerPlacedData{
				Tower: game.KnightTower,
			})
			return data
		}(),
	},
	{
		MessageType: ws.UnitAttack,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.UnitAttackData{
				AttackingUnit: game.Knight,
				Target:        game.Barrier,
			})
			return data
		}(),
	},
	{
		MessageType: ws.UnitAttack,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.UnitAttackData{
				AttackingUnit: game.Mage,
				Target:        game.Barrier,
			})
			return data
		}(),
	},
	{
		MessageType: ws.TowerAttack,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.TowerAttackData{
				AttackingTower: game.KnightTower,
				Target:         game.Knight,
			})
			return data
		}(),
	},
	{
		MessageType: ws.TowerAttack,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.TowerAttackData{
				AttackingTower: game.MageTower,
				Target:         game.Knight,
			})
			return data
		}(),
	},
	{
		MessageType: ws.GameStateSync,
		Data: func() json.RawMessage {
			data, _ := json.Marshal(ws.GameStateSyncData{
				FloorNumber: 1,
				WaveNumber:  1,
				Phase:       1,
				Units: []game.Unit{
					game.Knight,
				},
				Towers: []game.Tower{
					game.KnightTower,
				},
			})
			return data
		}(),
	},
}
