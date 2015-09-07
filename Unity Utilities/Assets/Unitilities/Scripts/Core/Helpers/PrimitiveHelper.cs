// <summary>
/// PrimitiveHelper v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Useful functions for C# primitives.
/// </summary>


namespace Unitilities
{

    public static class PrimitiveHelper
    {

        #region string

        public static string FillNumberWithLeftZeros(int number, int maxDigits)
        {
            string filledNumber = number.ToString();
            int zerosToAdd = maxDigits - filledNumber.Length;
            for (int i = 0; i < zerosToAdd; i++)
            {
                filledNumber = "0" + filledNumber;
            }

            return filledNumber;
        }


        #endregion

    }

}