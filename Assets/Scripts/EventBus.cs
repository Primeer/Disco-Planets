using System;
using Service;
using Service.Features;
using UnityEngine;
using View;

public class EventBus
{
    public static Action<Vector2> PointerPressed;
    public static Action PointerReleased;
        
    public static Action<BallView, int> BallCreated;
    public static Action<BallView> BallThrown;
    public static Action<int, Vector2> BallsMerged;
    public static Action<BallView> BallDestroyed;
        
    public static Action MainBallMerged;
    public static Action<int> LevelChanged;
    
    public static Action<FeatureEnum> FeatureOpened;
    
    public static Action GamePaused;
    public static Action GameResumed;
}