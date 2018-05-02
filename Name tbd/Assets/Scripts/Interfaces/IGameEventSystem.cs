using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGameEventSystem : IEventSystemHandler {

    void LevelOver();
    void GameOver();
}
