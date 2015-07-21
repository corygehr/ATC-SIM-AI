using System;
using System.Collections.ObjectModel;

namespace ATC_SIM_AI
{
    /// <summary>
    /// Provides advanced parsing for raw arrays taken from Javascript
    /// </summary>
    class JSArray
    {
        private ReadOnlyCollection<object> _raw;

        /// <summary>
        /// Constructor for the JSArray class
        /// </summary>
        /// <param name="array"></param>
        public JSArray(object array)
        {
            // Parse raw JS Array into ROC of 'object' type (very abstract for all cases)
            this._raw = (ReadOnlyCollection<object>)array;
        }

        /// <summary>
        /// Parses the ReadOnlyCollection into an int array
        /// </summary>
        /// <returns>Typed int array</returns>
        public int[] ParseToInt()
        {
            int[] clean = new int[this._raw.Count];
            
            for(int i=0; i<this._raw.Count; i++)
            {
                clean[i] = Convert.ToInt32(this._raw[i]);
            }

            return clean;
        }

        /// <summary>
        /// Parses the ReadOnlyCollection into a string array
        /// </summary>
        /// <returns>Typed string array</returns>
        public string[] ParseToString()
        {
            string[] clean = new string[this._raw.Count];

            for (int i = 0; i < this._raw.Count; i++)
            {
                clean[i] = Convert.ToString(this._raw[i]);
            }

            return clean;
        }
    }
}
