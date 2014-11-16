using System;
using System.Collections;
using System.Collections.Generic;

namespace Dictator
{
    public static class Dictator
    {
        public static Dictionary<string, object> New()
    	{
    		return new Dictionary<string, object>();
    	}
    }
    
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
    	
    	// TODO: datetime
    	
    	// TODO: enum
    	
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
    	
    	public static Dictionary<string, object> List<T>(this Dictionary<string, object> dictionary, string fieldPath, List<T> fieldValue)
    	{
    	    SetFieldValue(dictionary, fieldPath, fieldValue);
    		
    		return dictionary;
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

