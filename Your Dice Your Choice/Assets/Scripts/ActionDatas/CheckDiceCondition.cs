using Assets.Scripts.DicePrefab;

namespace Assets.Scripts.ActionDatas
{
    public static class CheckDiceCondition
    {
        /// <summary>
        /// Is the dice number acceptabled?
        /// </summary>
        /// <param name="allowedNumber"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumberValid(AllowedDiceNumber allowedNumber, int number)
        {
            // This for-loop checks the first enum indexes, which starts by 1.
            for (int i = 1; i <= Dice.MaxNumber; i++)
            {
                if (number == (int)allowedNumber)
                    return true;
            }

            switch (allowedNumber)
            {
                case AllowedDiceNumber.None:
                    return false;
                case AllowedDiceNumber.D1_6:
                    return true;
                case AllowedDiceNumber.D1_3:
                    if (number <= 3)
                        return true;
                    else return false;
                case AllowedDiceNumber.D4_6:
                    if (number >= 4)
                        return true;
                    else return false;
            }

            return false;
        }
    }
}
