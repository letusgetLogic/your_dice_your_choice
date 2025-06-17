using System;
using System.Collections.Generic;

namespace Assets.Scripts.Action
{
    public static class EnumConverter
    {
        /// <summary>
        /// Creates an enum of type MovementType.MovementKey from a list of enums.
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        public static MovementType.MovementKey CreateEnumFrom(List<object> enums)
        {
            string enumText = "";

            for (int i = 0; i < enums.Count; i++)
            {
                string text = enums[i].ToString();
                enumText += text;

                if (i < enums.Count - 1)
                    enumText += "_";
            }

            return (MovementType.MovementKey)Enum.Parse(typeof(MovementType.MovementKey), enumText, true);
        }
    }
}
