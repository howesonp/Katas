﻿using System;

namespace CodeKatas.TicTacToe
{
    public static class GameStateExtensions
    {
        public static void CheckIfIsGameOver(this GameState currentGameState)
        {
            if (currentGameState == GameState.IsDraw || currentGameState == GameState.PlayerXWin || currentGameState == GameState.PlayerOWin)
            {
                throw new Exception("Game over");
            }
        }
    }
}
