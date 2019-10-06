using System;
using System.Runtime.CompilerServices;

namespace QueryFiltering.Exceptions
{
    internal class ParseRuleException : Exception
    {
        public ParseRuleException(string visitorName, string summary = null, [CallerMemberName] string methodName = null) 
            : base($"Не удалось применить правило '{methodName}' в '{visitorName}'.{(summary != null ? " Примечание: " + summary : "")}")
        {
            
        }
    }
}
