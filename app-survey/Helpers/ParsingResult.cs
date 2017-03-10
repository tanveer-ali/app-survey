using System.Collections.Generic;

namespace LocalizationService.Helpers
{
    /// <summary>
    /// Parsing Result container
    /// </summary>
    public class ParsingResult
    {
        /// <summary>
        /// locale ids contained in file in order 
        /// </summary>
        public short[] Locales { get; set; }

        /// <summary>
        /// field id with locale values in order 
        /// </summary>
        public Dictionary<int, List<string>> FieldsWithLocaleValues { get; set; }
    }
}