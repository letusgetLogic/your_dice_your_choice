public static class TurnManager
{
    public static TurnState[] Parties { get; private set; }
    public static TurnState Turn { get; private set; }
    public enum TurnState
    {
        None,
        Player1,
        Player2,
        Player3
    }

    /// <summary>
    /// Set parties.
    /// </summary>
    /// <param name="turn"></param>
    public static void Set(TurnState[] parties)
    {
        Parties = parties;
    }

    /// <summary>
    /// Set turn.
    /// </summary>
    /// <param name="turn"></param>
    public static void SetTurn()
    {
        for (int i = 0; i < Parties.Length; i++)
        {
            if (Turn == Parties[i])
            {
                if (i == Parties.Length - 1) // if i is the last one, set turn to the first player.
                {
                    Turn = Parties[0];
                    return;
                }

                Turn = Parties[i + 1];
            }
        }

        //switch (Turn)
        //{
        //    case TurnState.None:
        //        throw new System.Exception("Turn = " + Turn);

        //    case TurnState.Player1:
        //        Turn = TurnState.Player2;
        //        break;

        //    case TurnState.Player2:
        //        Turn = TurnState.Player3;
        //        break;

        //    case TurnState.Player3:
        //        Turn = TurnState.Player1;
        //        break;

        //}
    }
}
