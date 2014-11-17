using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Dictator
{
    public static class DictionaryExtensions
    {
        #region Field getters
        
        public static bool Bool(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is bool))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain bool type.", fieldPath));
            }
            
            return (bool)fieldValue;
        }
        
        public static byte Byte(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is byte))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain byte type.", fieldPath));
            }
            
            return (byte)fieldValue;
        }
        
        public static short Short(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is short))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain short type.", fieldPath));
            }
            
            return (short)fieldValue;
        }
        
        public static int Int(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is int))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain int type.", fieldPath));
            }
            
            return (int)fieldValue;
        }
        
        public static long Long(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is long))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain long type.", fieldPath));
            }
            
            return (long)fieldValue;
        }
        
        public static float Float(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is float))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain float type.", fieldPath));
            }
            
            return (float)fieldValue;
        }
        
        public static double Double(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is double))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain double type.", fieldPath));
            }
            
            return (double)fieldValue;
        }
        
        public static decimal Decimal(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is decimal))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain decimal type.", fieldPath));
            }
            
            return (decimal)fieldValue;
        }
        
        public static DateTime DateTime(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            DateTime dateTime;
            
            if (fieldValue is DateTime)
            {
                dateTime = (DateTime)fieldValue;
            }
            else if (fieldValue is string)
            {
                dateTime = System.DateTime.Parse((string)fieldValue, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
            }
            else if (fieldValue is long)
            {
                dateTime = Dictator.Settings.UnixEpoch.AddSeconds((long)fieldValue);
            }
            else
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain value which can be converted to DateTime type.", fieldPath));
            }
            
            return dateTime;
        }
        
        public static string String(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is string))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain string type.", fieldPath));
            }
            
            return (string)fieldValue;
        }
        
        public static object Object(this Dictionary<string, object> dictionary, string fieldPath)
        {
            return GetFieldValue(dictionary, fieldPath);
        }
        
        public static T Object<T>(this Dictionary<string, object> dictionary, string fieldPath)
        {
            return (T)GetFieldValue(dictionary, fieldPath);
        }
        
        public static Dictionary<string, object> Document(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue is Dictionary<string, object>))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain Dictionary<string, object> type.", fieldPath));
            }
            
            return (Dictionary<string, object>)fieldValue;
        }
        
        public static T Enum<T>(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var type = typeof(T);
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            T fieldEnum;
            
            if (fieldValue is Enum)
            {
                fieldEnum = (T)fieldValue;
            }
            else if (fieldValue is int)
            {
                fieldEnum = (T)System.Enum.ToObject(type, (int)fieldValue);
            }
            else if (fieldValue is string)
            {
                fieldEnum = (T)System.Enum.Parse(type, (string)fieldValue, true);
            }
            else
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain Enum type.", fieldPath));
            }
            
            return fieldEnum;
        }
        
        public static List<T> List<T>(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var fieldValue = GetFieldValue(dictionary, fieldPath);
            
            if (!(fieldValue.GetType().IsGenericType && (fieldValue is IEnumerable)))
            {
                throw new InvalidFieldTypeException(string.Format("Field path '{0}' value does not contain list type.", fieldPath));
            }
            
            return (List<T>)fieldValue;
        }
        
        #endregion
        
        #region Field setters
        
        public static Dictionary<string, object> Bool(this Dictionary<string, object> dictionary, string fieldPath, bool fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Byte(this Dictionary<string, object> dictionary, string fieldPath, byte fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Short(this Dictionary<string, object> dictionary, string fieldPath, short fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Int(this Dictionary<string, object> dictionary, string fieldPath, int fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Long(this Dictionary<string, object> dictionary, string fieldPath, long fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Float(this Dictionary<string, object> dictionary, string fieldPath, float fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Double(this Dictionary<string, object> dictionary, string fieldPath, double fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Decimal(this Dictionary<string, object> dictionary, string fieldPath, decimal fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }

        #region DateTime
        
        public static Dictionary<string, object> DateTime(this Dictionary<string, object> dictionary, string fieldPath, DateTime fieldValue)
        {
            return DateTime(dictionary, fieldPath, fieldValue, Dictator.Settings.DateTimeFormat);
        }
        
        public static Dictionary<string, object> DateTime(this Dictionary<string, object> dictionary, string fieldPath, DateTime fieldValue, string dateTimeStringFormat)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue.ToUniversalTime().ToString(dateTimeStringFormat, DateTimeFormatInfo.InvariantInfo));
            
            return dictionary;
        }
        
        public static Dictionary<string, object> DateTime(this Dictionary<string, object> dictionary, string fieldPath, DateTime fieldValue, DateTimeFormat dateTimeFormat)
        {
            switch (dateTimeFormat)
            {
                case DateTimeFormat.String:
                    SetFieldValue(dictionary, fieldPath, fieldValue.ToUniversalTime().ToString(Dictator.Settings.DateTimeStringFormat, DateTimeFormatInfo.InvariantInfo));
                    break;
                case DateTimeFormat.UnixTimeStamp:
                    TimeSpan span = (fieldValue.ToUniversalTime() - Dictator.Settings.UnixEpoch);
                    SetFieldValue(dictionary, fieldPath, (long)span.TotalSeconds);
                    break;
                default:
                    SetFieldValue(dictionary, fieldPath, fieldValue);
                    break;
            }
            
            return dictionary;
        }
        
        #endregion
        
        public static Dictionary<string, object> String(this Dictionary<string, object> dictionary, string fieldPath, string fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Object(this Dictionary<string, object> dictionary, string fieldPath, object fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Object<T>(this Dictionary<string, object> dictionary, string fieldPath, T fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Document(this Dictionary<string, object> dictionary, string fieldPath, Dictionary<string, object> fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> Enum<T>(this Dictionary<string, object> dictionary, string fieldPath, T fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        public static Dictionary<string, object> List<T>(this Dictionary<string, object> dictionary, string fieldPath, List<T> fieldValue)
        {
            SetFieldValue(dictionary, fieldPath, fieldValue);
            
            return dictionary;
        }
        
        #endregion
        
        #region Field checkers
        
        public static bool Has(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);

                isValid = true;
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsNull(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue == null)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsNotNull(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue != null)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsBool(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is bool)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsByte(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is byte)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsShort(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is short)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsInt(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is int)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsLong(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is long)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsFloat(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is float)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsDouble(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is double)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsDecimal(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is decimal)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsDateTime(this Dictionary<string, object> dictionary, string fieldPath)
        {
            return IsDateTime(dictionary, fieldPath, Dictator.Settings.DateTimeFormat);
        }
        
        public static bool IsDateTime(this Dictionary<string, object> dictionary, string fieldPath, DateTimeFormat dateTimeFormat)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                switch (dateTimeFormat)
                {
                    case DateTimeFormat.String:
                        if (fieldValue is string)
                        {
                            var dateTime = System.DateTime.Parse((string)fieldValue, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
                            
                            isValid = true;
                        }
                        break;
                    case DateTimeFormat.UnixTimeStamp:
                        if (fieldValue is long)
                        {
                            var dateTime = Dictator.Settings.UnixEpoch.AddSeconds((long)fieldValue);
                            
                            isValid = true;
                        }
                        break;
                    default:
                        if (fieldValue is DateTime)
                        {
                            isValid = true;
                        }
                        break;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsString(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is string)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsObject(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue != null)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsDocument(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue is Dictionary<string, object>)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsEnum(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
            
                if (fieldValue.GetType().IsEnum)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsEnum<T>(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                var type = typeof(T);
                T fieldEnum;
            
                if (fieldValue is Enum)
                {
                    fieldEnum = (T)fieldValue;
                    isValid = true;
                }
                else if (fieldValue is int)
                {
                    fieldEnum = (T)System.Enum.ToObject(type, (int)fieldValue);
                    isValid = true;
                }
                else if (fieldValue is string)
                {
                    fieldEnum = (T)System.Enum.Parse(type, (string)fieldValue, true);
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsList(this Dictionary<string, object> dictionary, string fieldPath)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
            
                if (fieldValue.GetType().IsGenericType && (fieldValue is IEnumerable))
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsType(this Dictionary<string, object> dictionary, string fieldPath, Type type)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue != null)
                {
                    var fieldType = fieldValue.GetType();
                    
                    if (fieldType == type)
                    {
                        isValid = true;
                    }
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        public static bool IsEqual(this Dictionary<string, object> dictionary, string fieldPath, object compareValue)
        {
            var isValid = false;
            
            try
            {
                var fieldValue = GetFieldValue(dictionary, fieldPath);
                
                if (fieldValue != null)
                {
                    if (fieldValue.Equals(compareValue))
                    {
                        isValid = true;
                    }
                }
                else if (fieldValue == null && compareValue == null)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            
            return isValid;
        }
        
        #endregion

        static object GetFieldValue(Dictionary<string, object> dictionary, string fieldPath)
        {
            object fieldValue = null;
            var fieldNames = new [] { fieldPath };
            var parentDictionary = dictionary;                
            
            // split field path to separate field name elements if necessary
            if (fieldPath.Contains("."))
            {
                fieldNames = fieldPath.Split('.');
            }
            
            for (int i = 0; i < fieldNames.Length; i++)
            {
                var fieldName = fieldNames[i];

                // throw exception if the field is not present in dictionary
                if (!parentDictionary.ContainsKey(fieldName))
                {
                    throw new NonExistingFieldException(string.Format("Field path '{0}' does not contain field '{1}'.", fieldPath, fieldName));
                }

                // current field name is final - retrieve field value and break loop
                if (i == (fieldNames.Length - 1))
                {        			
                    fieldValue = parentDictionary[fieldName];
                    
                    break;
                }
                
                // descendant field is dictionary - set is as current parent dictionary
                if (parentDictionary[fieldName] is Dictionary<string, object>)
                {
                    parentDictionary = (Dictionary<string, object>)parentDictionary[fieldName];
                }
                // can not continue with processing - throw exception
                else
                {
                    throw new InvalidFieldException(string.Format("Field path '{0}' contains field '{1}' which is not dictionary.", fieldPath, fieldName));
                }
            }
            
            return fieldValue;
        }
        
        static void SetFieldValue(Dictionary<string, object> dictionary, string fieldPath, object fieldValue)
        {
            var fieldNames = new [] { fieldPath };
            var parentDictionary = dictionary;
            
            // split field path to separate field name elements if necessary
            if (fieldPath.Contains("."))
            {
                fieldNames = fieldPath.Split('.');
            }
            
            for (int i = 0; i < fieldNames.Length; i++)
            {
                var fieldName = fieldNames[i];
                
                // current field name is final - set field value and break loop
                if (i == (fieldNames.Length - 1))
                {
                    parentDictionary[fieldName] = fieldValue;
                    
                    break;
                }
                
                // descendant field is dictionary - set is as current parent dictionary
                if (parentDictionary.ContainsKey(fieldName) && (parentDictionary[fieldName] is Dictionary<string, object>))
                {
                    parentDictionary = (Dictionary<string, object>)parentDictionary[fieldName];
                }
                // descendant field does not exist or isn't dictioanry - field needs to be set as dictionary
                else
                {
                    var newDictionary = Dictator.New();
                    parentDictionary[fieldName] = newDictionary;
                    parentDictionary = newDictionary;
                }
            }
        }
    }
}
