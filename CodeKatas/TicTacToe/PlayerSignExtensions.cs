using System;

namespace CodeKatas.TicTacToe
{
    public static class PlayerSignExtensions
    {
        public static void ValidateCorrectPlayer(this PlayerSign currentPlayer, PlayerSign previousPlayer)
        {
            if (currentPlayer == previousPlayer)
            {
                throw new Exception($"{currentPlayer} took the previous turn");
            }
        }
    }
}
