//Title :           Necessary Functions.
//Version :         1.0.0.3
//Copyright :       Copyright (c) 2010
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All Necessary Function here.  Some Function Add Here need Update Albatross
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.Windows.Controls;
using System.IO;
using System.Data;
namespace ProcestaNecessaryFunction
{
    public class NecessaryFunction : InterfaceNecessaryFunction
    {
        //CheckNumber
        public bool Isnumber(string numberRecive)
        {
            string realNumber = numberRecive.Trim();
            double number;
            return double.TryParse(realNumber, out number);
        }
        //File Size checker
        public string FileSizeIs(long fileSize)
        {
            if (fileSize >= 1073741824)
            {
                return (fileSize / 1073741824).ToString("0.00GB");
            }
            else if (fileSize >= 1048576)
            {
                return (fileSize / 1048576).ToString("0.00MB");
            }
            else if (fileSize >= 1024)
            {
                return (fileSize / 1024).ToString("0.00KB");
            }
            else
            {
                return fileSize.ToString("0.00 Byte");
            }
        }
        //Rate Calculator
        public string RateIs(string quantity, string amount)
        {
            double rate=(double)Convert.ToSingle(amount) / (double)Convert.ToSingle(quantity);
            return rate.ToString(".00");
        }
        //Profit Calculator
        public string ProfitIs(string totalRate, string amount)
        {
            return Convert.ToString((float)Convert.ToSingle(amount) - (float)Convert.ToSingle(totalRate));
        }
        //Amount Calculator
        public string AmountIs(string quantity ,string rate)
        {
            return Convert.ToString((double)Convert.ToSingle(rate)*(double)Convert.ToSingle(quantity));
        }
        //E-mail checker
        public bool IsEmail(string eMailAddress)
        {
            int position=eMailAddress.IndexOf("@");
            if (position != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Password Length Checker
        public int PasswordLengthIs(string password, string rePassword)
        {
            if(password.Equals(rePassword))
            {
                if (password.Length >= 5 && rePassword.Length >= 5)
                {
                    if (rePassword.Length >= 5 && rePassword.Length <= 7)
                    {
                        return 1;
                    }
                    else if (rePassword.Length >= 8 && rePassword.Length <= 12)
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        //Password checker
        public bool PasswordIs(string password, string conformPassword)
        {
            if (password!=string.Empty && conformPassword!=string.Empty && password.Equals(conformPassword) && password.Length.Equals(conformPassword.Length))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Resize image
        public Image ResizeImage(Image resizeableImage, int width, int height)
        {
            int sourceWidth = resizeableImage.Width;
            int sourceHight = resizeableImage.Height;
            int sourceX, sourceY, destX, destY;
            sourceHight = sourceWidth = destX = destY= sourceX = sourceY = 0;

            float nPercent, nPercentW, nPercentH;
            nPercent = nPercentH = nPercentW = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((height - (sourceHight * nPercent)) / 2);
            }
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHight * nPercent);
            Bitmap bmImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmImage.SetResolution(resizeableImage.HorizontalResolution, resizeableImage.VerticalResolution);
            Graphics grImage = Graphics.FromImage(bmImage);
            grImage.Clear(Color.White);
            grImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grImage.DrawImage(resizeableImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHight), GraphicsUnit.Pixel);
            grImage.Dispose();
            return bmImage;

        }
        //Coma Sprit from String
        public double ComaSpriter(string textWithComa)
        {
            if (!textWithComa.Equals(string.Empty))
            {
                StringBuilder textWithoutComa = new StringBuilder();
                String[] spritText = textWithComa.Split(',');
                foreach (string mainSprittext in spritText)
                {
                    textWithoutComa.Append(mainSprittext);
                }
                return Convert.ToDouble(textWithoutComa.ToString());
            }
            else
            {
                return 0;
            }
        }
        //Six Digit Number 
        public string SixDigitNumber(string number)
        {
            Int32 sixDigiteNumber = Convert.ToInt16(number);
            return sixDigiteNumber.ToString("000000");
        }
        //Mysql Date Format 
        public string MysqlDateFormate(string date)
        {
            StringBuilder dateBuilder = new StringBuilder();
            String[] subString = date.Split('/', ' ');
            dateBuilder.Append(subString[2]);
            dateBuilder.Append("-");
            dateBuilder.Append(Convert.ToInt16(subString[0]).ToString("00"));
            dateBuilder.Append("-");
            dateBuilder.Append(Convert.ToInt16(subString[1]).ToString("00"));
            return dateBuilder.ToString();
        }
        //Single Quotation Adder 
        public string SingleQuotationAdder(string textToAdd)
        {
            StringBuilder tempStringBulder = new StringBuilder();
            tempStringBulder.Append("'");
            tempStringBulder.Append(textToAdd);
            tempStringBulder.Append("'");
            return tempStringBulder.ToString();
        }
        //Credit Amount Is
        public string CreditAmountIs(string realAmount, string paymentAmount)
        {
            return (Convert.ToDouble(realAmount) - Convert.ToDouble(paymentAmount)).ToString();
        }
        //Delete a File
        public bool DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //Create a Text file
        public bool CreateTextFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.CreateText(filePath).Close();
                    return true;
                }
                else
                {
                    DeleteFile(filePath);
                    CreateTextFile(filePath);
                    return true;
                }
            }
            catch 
            {

                return false;
            }
        }
        //Convert DataTbale All column in String Column
        public DataTable ConverDataTableToString(DataTable dataTable) 
        {
            DataTable tempDataTable = new DataTable();
            foreach (DataColumn columnName in dataTable.Columns)
            {
                DataColumn tempColumn = new DataColumn();
                tempColumn.ColumnName = columnName.ColumnName;
                tempColumn.DataType = System.Type.GetType("System.String");
                tempColumn.AutoIncrement = false;
                tempColumn.Caption = columnName.Caption;
                tempColumn.ReadOnly = false;
                tempColumn.Unique = false;
                tempDataTable.Columns.Add(tempColumn);
                tempColumn.Dispose();
            }
            foreach (DataRow readRow in dataTable.Rows)
            {
                DataRow tempdataRow = tempDataTable.NewRow();
                foreach (DataColumn readColumn in dataTable.Columns)
                {
                    tempdataRow[readColumn.ColumnName] = readRow[readColumn.ColumnName].ToString();
                }
                tempDataTable.Rows.Add(tempdataRow);
            }
            return tempDataTable;
        }
        
    }
}
