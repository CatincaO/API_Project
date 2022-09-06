using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using System.Text.Json;

namespace miniAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class IntClass
    {
        public string value { get; set; }

        public int digits { get; set; }
    }

    public class FloatClass
    {
        public string value { get; set; }

        public int decimals { get; set; }
    }

    public class FormatController : ControllerBase
    {
        public static int countDigits(int n)
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

        [EnableCors("Policy1")]
        [HttpPost("/FormatInteger")]

        public String formatIntRequestHandler([FromBody] JsonElement request)
        {
            var json = request.GetRawText();
            try
            {
                IntClass variableInt = JsonConvert.DeserializeObject<IntClass>(json);
                var res = formatInt(variableInt);
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [EnableCors("Policy1")]
        [HttpPost("/FormatFloat")]

        public String formatFloatRequestHandler([FromBody] JsonElement request)
        {
            var json = request.GetRawText();
            try
            {
                FloatClass variableFloat = JsonConvert.DeserializeObject<FloatClass>(json);
                var res = formatFloat(variableFloat);
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public IntClass formatInt(IntClass input)
        {
            double temp = double.Parse(input.value, CultureInfo.InvariantCulture.NumberFormat);
            if ((int)temp == 0 || input.digits == 0)
            {
                input.value = "0";
                return input;
            }

            int numberDigits = countDigits((int)temp);


            if (numberDigits <= input.digits)
            {
                input.value = ((int)temp).ToString();
                return input;
            }

            input.value = new String('#', input.digits);
            return input;
        }

        public FloatClass formatFloat(FloatClass variableFloat)
        {
            double temp = double.Parse(variableFloat.value, CultureInfo.InvariantCulture.NumberFormat);
            if (variableFloat.decimals == 0)
            {
                variableFloat.value = ((int)temp).ToString();
                return variableFloat;
            }

            var precision = 0;
            while (temp * (double)Math.Pow(10, precision) != Math.Round(temp * (double)Math.Pow(10, precision))) precision++;
            if (variableFloat.decimals <= precision)
            {
                int pow = (int)Math.Pow(10, variableFloat.decimals);
                variableFloat.value = (Math.Floor(temp * pow) / pow).ToString();
                return variableFloat;

            }
            variableFloat.value = temp.ToString("0." + new string('0', variableFloat.decimals));
            return variableFloat;

        }

    }
}