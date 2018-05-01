using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITestEventSystem : IEventSystemHandler
{
    void StartTest();
    void EndTest();
}

