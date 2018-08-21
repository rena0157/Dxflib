using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class DxfTypeConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public static Type ConvertType(string groupCode)
        {
            var gc = int.Parse(groupCode);

            switch ( gc )
            {
                case int n when (gc >= 0 && gc <= 9):
                    return typeof(string);

                case int n when (gc >= 10 && gc <= 59):
                    return typeof(double);

                case int n when (gc >= 60 && gc <= 79):
                    return typeof(short);

                case int n when (gc >= 60 && gc <= 99):
                    return typeof(int);

                case int n when (gc == 100 || gc == 102 || gc == 105):
                    return typeof(string);

                case int n when (gc >= 110 && gc <= 149):
                    return typeof(double);

                case int n when (gc >= 160 && gc <= 169):
                    return typeof(long);

                case int n when (gc >= 170 && gc <= 179):
                    return typeof(short);

                case int n when (gc >= 210 && gc <= 239):
                    return typeof(double);

                case int n when (gc >= 270 && gc <= 289):
                    return typeof(short);

                case int n when (gc >= 290 && gc <= 299):
                    return typeof(bool);

                case int n when (gc >= 300 && gc <= 369):
                    return typeof(string);

                case int n when (gc >= 370 && gc <= 389):
                    return typeof(short);

                case int n when (gc >= 390 && gc <= 399):
                    return typeof(string);

                case int n when (gc >= 400 && gc <= 409):
                    return typeof(short);

                case int n when (gc >= 410 && gc <= 419):
                    return typeof(string);

                case int n when (gc >= 420 && gc <= 429):
                    return typeof(int);

                case int n when (gc >= 430 && gc <= 439):
                    return typeof(string);

                case int n when (gc >= 440 && gc <= 449):
                    return typeof(int);

                case int n when (gc >= 450 && gc <= 459):
                    return typeof(long);

                case int n when (gc >= 460 && gc <= 469):
                    return typeof(double);

                case int n when (gc >= 470 && gc <= 481):
                    return typeof(string);

                case int n when (gc == 999):
                    return typeof(string);

                case int n when (gc >= 1000 && gc <= 1009):
                    return typeof(string);

                case int n when (gc >= 1010 && gc <= 1059):
                    return typeof(double);
                
                case int n when (gc >= 1060 && gc <= 1070):
                    return typeof(short);

                case int n when (gc == 1071):
                    return typeof(int);

                default:
                    throw new InvalidCastException($"The GroupCode '{groupCode}', was not recognized as a valid group code");
            }
        }
    }
}
