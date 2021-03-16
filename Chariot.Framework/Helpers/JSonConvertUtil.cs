using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.Helpers
{
    public class JSonConvertUtil
    {
        #region To JSON

        public static string Convert(object inputValue)
        {
            try
            {
                return Serialize(inputValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static string Serialize(object inputObject)
        {
            string returnValue = null;

            if (null != inputObject)
            {
                returnValue = JsonConvert.SerializeObject(inputObject, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            }

            return returnValue;
        }

        #endregion

        #region To Object

        public static T Deserialize<T>(string input) where T : class
        {
            try
            {
                return DeserializeObject<T>(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static T DeserializeObject<T>(string input) where T : class
        {
            T returnItem = null;

            if (!string.IsNullOrEmpty(input))
            {
                returnItem = JsonConvert.DeserializeObject<T>(input);
            }

            return returnItem;
        }

        #endregion
    }
}
