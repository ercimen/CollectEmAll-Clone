using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomEvents 
{
    /// <summary>
    /// Add custom events here.
    /// <example> <code> public const string SomeEvent = nameof(SomeEvent); </code> </example>
    /// </summary>

    #region ObjectPool
    public const string onGetCube = nameof(onGetCube);
    public const string onReleaseCube = nameof(onReleaseCube);
    #endregion

    #region Cube
    public const string onGetCubeRigidbody = nameof(onGetCubeRigidbody);
    #endregion

    #region GameManager
    public const string onNextLevel = nameof(onNextLevel);
    public const string onLevelStart = nameof(onLevelStart);
    public const string onRestartLevel = nameof(onRestartLevel);
    public const string onLevelFailed = nameof(onLevelFailed);
    #endregion

    #region LevelManager
    public const string onReadyNewLevel = nameof(onReadyNewLevel);
    #endregion



}
