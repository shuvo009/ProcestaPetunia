using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace ProcestaNecessaryFunction
{
    interface InterfaceNecessaryFunction
    {
        bool Isnumber(string numberRecive);
        string FileSizeIs(long fileSize);
        string RateIs(string quantity, string amount);
        string ProfitIs(string totalRate, string amount);
        string AmountIs(string quantity, string rate);
        bool IsEmail(string eMailAddress);
        int PasswordLengthIs(string password, string rePassword);
        bool PasswordIs(string password, string conformPassword);
        Image ResizeImage(Image resizeableImage, int width, int height);
        double ComaSpriter(string textWithComa);
        string SixDigitNumber(string number);
        string MysqlDateFormate(string date);
        string SingleQuotationAdder(string textToAdd);
        string CreditAmountIs(string realAmount, string paymentAmount);
        bool DeleteFile(string filePath);
        bool CreateTextFile(string filePath);
        DataTable ConverDataTableToString(DataTable dataTable);
    }
}
