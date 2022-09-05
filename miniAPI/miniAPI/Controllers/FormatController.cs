using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft;
using Newtonsoft.Json;
using System.Globalization;
//using Microsoft.AspNetCore.Cors;
using System.Web.Http.Cors;

namespace miniAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class IntClass
    {
        public string value { get; set; }

        public int decimals { get; set; }
    }

    public class FloatClass
    {
        public double value { get; set; }

        public int desiredDecimals { get; set; }
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

        [EnableCors(origins:"*", headers:"*", methods:"*")]
        [HttpPost("/FormatInteger")]

        public String formatIntRequestHandler(string request)
        {
            Console.WriteLine(request);
            return "Done";

            try
            {
                IntClass variableInt = JsonConvert.DeserializeObject<IntClass>(request);
                return JsonConvert.SerializeObject(formatInt(variableInt));
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public IntClass formatInt(IntClass input)
        {
            double temp = float.Parse(input.value, CultureInfo.InvariantCulture.NumberFormat);
            if ((int)temp == 0 || input.decimals == 0)
            {
                input.value = "0";
                return input;
            }

            int numberDigits = countDigits((int)temp);

            if (numberDigits <= input.decimals)
            {
                input.value=((int)temp).ToString();
                return input;
            }

            input.value = new String('#', input.decimals);
            return input;
        }

        [HttpPost("/FormatFloat")]

        public String formatFloat(FloatClass variableFloat)
        {
            if (variableFloat.desiredDecimals == 0)
            {
                return ((int)variableFloat.value).ToString();
            }

            var precision = 0;
            while (variableFloat.value * (double)Math.Pow(10, precision) != Math.Round(variableFloat.value * (double)Math.Pow(10, precision))) precision++;
            if (variableFloat.desiredDecimals <= precision)
            {
                int pow = (int)Math.Pow(10, variableFloat.desiredDecimals);
                return (Math.Floor(variableFloat.value * pow) / pow).ToString();
            }
            return variableFloat.value.ToString("0." + new string('0', variableFloat.desiredDecimals));

        }

        //Json -> Object
        //DESERIALIZATION
        /*public ValueParameterInt(string response)
        {
            var result = JsonConverter.DeserializeObject<ValueParameterInt>(response);
            return result;
        }

        //result.decimals = 2;


        //Object -> Json
        //SERIALIZATION
        public boolean(ValueParameterInt value)
        {
            HttpClient.response(value.toJson());
        }*/


    }
}