﻿public static class Constants
{
    //scenes
    public const string InitialScene = "Initial";
    public const string MainScene = "Main";
    
    //resourcesPath
    public const string PlayerPath = "Player/Hero";
    public const string WindowRootPath = "Canvases/WindowRoot";
    public const string ObstaclePath = "Obstacles/Obstacle";
    public const string ObstaclesModulePath = "Obstacles/ObstaclesModule";
    public const string CameraPath = "Camera/CameraTracker";
    public const string PoolPath = "Pools/Pool";
    public const string SoundOperatorPath = "SoundFX/SoundOperator";
    
    //hero
    public const float SpeedHero = 5f;
    public const float TapForce = 200f;

    //RotationHero
    public const float RotationSpeed = 1f;
    public const float MaxRotationZ = 35f;
    public const float MinRotationZ = -65f;
    
    //Obstacle
    public const float OffSetXSpawn = 12f;
    public const float OffSetZSpawn = -5f;
    public const float MinRandomPositionY = 4.5f;
    public const float MaxRandomPositionY = -3.5f;
    public const int CountSpawnObstacle = 8;
    public const float SpawnInterval = 1.5f;
    
    //calculation formula
    public const int MultiplierValueLevel = 15;
    
    //saveLoad
    public const string Progress = "Progress";
    
    //Leaderboard
    public const string Leaderboard = "Leaderboard";
    public const string Anonymous = "Anonymous";
    public const int TopPlayersCount = 15;
    public const int CompletingPlayersCount = 1;
    
    //curtain
    public const int RateCurtain = 3;
    public const float RateAlfaCurtain = .03f;
}