using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum MoveTypes
    {
        [Description("Exit")]
        Exit,
        [Description("Invalid Move")]
        InvalidMove,
        [Description("Correct")]
        Correct
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string explanation = Enum.GetName(type, value);
            if (explanation != null)
            {
                FieldInfo field = type.GetField(explanation);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }
    }
}
