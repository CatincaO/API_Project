﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace miniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase
    {
        public static int countDigits( int n)
        {
            if (n >= 0)
            {
                if (n < 10) return 1;
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                if (n < 100000) return 5;
                if (n < 1000000) return 6;
                if (n < 10000000) return 7;
                if (n < 100000000) return 8;
                if (n < 1000000000) return 9;
                return 10;
            }
            else
            {
                if (n > -10) return 2;
                if (n > -100) return 3;
                if (n > -1000) return 4;
                if (n > -10000) return 5;
                if (n > -100000) return 6;
                if (n > -1000000) return 7;
                if (n > -10000000) return 8;
                if (n > -100000000) return 9;
                if (n > -1000000000) return 10;
                return 11;
            }
        }
        public static String formatInt(float value, int digits)
        {
            if ( (int) value == 0)
            {
                return "0";
            }

            int numberDigits = countDigits( (int)value);

            if (numberDigits <= digits)
            {
                return ((int)value).ToString();
            }

            return new String('#', digits);

        }

        public static String formatFloat(double value, int desiredDecimals)
        {
            if (desiredDecimals == 0)
            {
                return ((int)value).ToString();
            }

            var precision = 0;
            while (value * (double)Math.Pow(10, precision) != Math.Round(value * (double)Math.Pow(10, precision))) precision++;
            if (desiredDecimals <= precision)
            {
                int p = (int)Math.Pow(10, desiredDecimals);
                return (Math.Floor(value * p) / p).ToString();  
            }
            precision++;
            return value.ToString("F" + precision);



        }
    }
}
