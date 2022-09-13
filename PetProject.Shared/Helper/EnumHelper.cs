using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PetProject.Shared.Helper
{

    public static class EnumHelper
    {
        public static IEnumerable<string> GetNames<TEnum>() where TEnum : Enum
        {
            return Enum.GetNames(typeof(TEnum));
        }

        public static string? GetName<TEnum>(int value) where TEnum : Enum
        {
            Type typeModel = typeof(TEnum);
            var result = Enum.GetName(typeModel, value);
            return result;
        }
        public static string? GetName<TEnum>(long value) where TEnum : Enum
        {
            Type typeModel = typeof(TEnum);
            var result = Enum.GetName(typeModel, value);
            return result;
        }

        public static string? GetName<TEnum>(object value) where TEnum : Enum
        {
            Type typeModel = typeof(TEnum);
            var result = Enum.GetName(typeModel, value);
            return result;
        }

        public static IEnumerable<string?> GetDisplayNames<TEnum>() where TEnum : Enum
        {
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                yield return GetDisplayName((Enum)value);
            }
        }

        public static string? GetDisplayName(Enum value)
        {
            if (value == null)
            {
                return null;
            }
            var type = value.GetType();
            if (type == null)
            {
                return null;
            }
            var members = type.GetMember(value.ToString());
            if (members == null || members.Count() == 0)
            {
                return null;
            }
            var displayAttribute = members.First().GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute == null)
            {
                return null;
            }
            string displayName = value.ToString();
            //if (displayAttribute.ResourceType != null)
            //{
            //    displayName = ResourceHelper.GetDisplay(displayAttribute.ResourceType, displayAttribute.Name);
            //}
            //if (string.IsNullOrEmpty(displayName))
            //{
            //    displayName = ResourceHelper.GetDisplayEnum(displayAttribute.Name);
            //}

            return displayName;
        }

        public static T ParseEnum<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static bool TryParseEnum<TEnum>(string value, out TEnum enumValue) where TEnum : struct, Enum
        {
            try
            {
                enumValue = ParseEnum<TEnum>(value);
                return true;
            }
            catch (Exception)
            {
                var enums = Enum.GetValues<TEnum>();
                enumValue = enums[0];
                return false;
            }
        }

        public static string GetDisplayName<TEnum>(int value) where TEnum : Enum
        {
            var castEnum = ParseEnum<TEnum>(value.ToString());
            return GetDisplayName(castEnum);
        }

        public static int GetValue<TEnum>(string name) where TEnum : Enum
        {
            var result = Enum.Parse(typeof(TEnum), name);
            return Convert.ToInt32(result);
        }

        public static byte GetByteValue<TEnum>(string name) where TEnum : Enum
        {
            var result = Enum.Parse(typeof(TEnum), name);
            return Convert.ToByte(result);
        }

        public static bool GetValueTryParse<TEnum>(string name, out int resultOut) where TEnum : Enum
        {
            resultOut = 0;
            try
            {
                resultOut = GetValue<TEnum>(name);
                return true;
            }
            catch (Exception) { return false; }
        }

        public static bool GetByteValueTryParse<TEnum>(string name, out byte resultOut) where TEnum : Enum
        {
            resultOut = 0;
            try
            {
                resultOut = GetByteValue<TEnum>(name);
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}
