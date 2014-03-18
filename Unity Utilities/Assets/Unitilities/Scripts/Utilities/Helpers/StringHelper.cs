using UnityEngine;

public class StringHelper
{

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

}
