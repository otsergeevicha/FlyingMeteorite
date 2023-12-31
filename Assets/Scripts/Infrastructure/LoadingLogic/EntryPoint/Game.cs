﻿using Infrastructure.GameAI.StateMachine;
using Infrastructure.LoadingLogic.ScreenLoading;

namespace Infrastructure.LoadingLogic.EntryPoint
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(LoadingCurtain loadingCurtain) => 
            StateMachine = new GameStateMachine(new SceneLoader(), loadingCurtain);
    }
}