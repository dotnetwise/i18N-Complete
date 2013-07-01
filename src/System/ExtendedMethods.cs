using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Extended methods for getting <see cref="DisplayNameAttribute"/> and <see cref="PlaceHolderAttribute"/>
    /// </summary>
    public static class ExtendedMethodsDisplayName
    {
        /// <summary>Returns the given <see cref="DisplayNameAttribute.DisplayName"/> applied to a member of this type, or null if no <see cref="DisplayNameAttribute"/> was found</summary>
        /// <param name="type">The type to get the member from </param>
        /// <param name="bindingFlags">Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</param>
        /// <param name="memberName">The name of the member to look for the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes's virtual/override member for the attribute if not present on the member itself</param>
        public static string GetDisplayName(this Type type, string memberName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = true)
        {
            var attr = GetCustomAttribute<DisplayNameAttribute>(type, memberName, bindingFlags);
            return attr == null ? null : attr.DisplayName;
        }
        /// <summary>Returns the given <see cref="DisplayNameAttribute"/> applied to a member of this type, or null if no <see cref="DisplayNameAttribute"/> was found</summary>
        /// <param name="type">The type to get the member from </param>
        /// <param name="bindingFlags">Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</param>
        /// <param name="memberName">The name of the member to look for the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes's virtual/override member for the attribute if not present on the member itself</param>
        /// <param name="returnAttribute">Only used for getting method overload so that you'll get the DisplayNameAttribute instead of its DisplayName property</param>
        public static DisplayNameAttribute GetDisplayName(this Type type, string memberName, bool returnAttribute, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = true)
        {
            return GetCustomAttribute<DisplayNameAttribute>(type, memberName, bindingFlags);
        }

        /// <summary>Returns the given <typeparamref name="T"/> applied to a member of this type</summary>
        /// <param name="type">The type to get the member from</param>
        /// <param name="bindingFlags">Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</param>
        /// <param name="memberName">The name of the member to look for the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes's virtual/override member for the attribute if not present on the member itself</param>
        public static T GetCustomAttribute<T>(this Type type, string memberName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = true)
            where T : Attribute
        {
            var attribute = (T)Attribute.GetCustomAttribute(type.GetMember(memberName, bindingFlags)[0], typeof(T));
            return attribute;
        }
        /// <summary>Returns the given <see cref="DisplayNameAttribute.DisplayName"/> applied to a member of this type, or null if no <see cref="DisplayNameAttribute"/> was found</summary>
        /// <param name="type">The type to get the member from</param>
        /// <param name="bindingFlags">Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</param>
        /// <param name="memberName">The name of the member to look for the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes's virtual/override member for the attribute if not present on the member itself</param>
        public static string GetPlaceholder(this Type type, string memberName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = true)
        {
            var attribute = GetCustomAttribute<PlaceHolderAttribute>(type, memberName, bindingFlags, inherit);
            return attribute == null ? null : attribute.DisplayName;
        }
        /// <summary>Returns the given <see name="PlaceHolderAttribute.DisplayName"/> applied to a member of this type, or null if no <see cref="PlaceHolderAttribute"/> was found</summary>
        /// <param name="type">The type to get the member from</param>
        /// <param name="bindingFlags">Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</param>
        /// <param name="memberName">The name of the member to look for the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes's virtual/override member for the attribute if not present on the member itself</param>
        /// <param name="returnAttribute">Only used for getting method overload so that you'll get the DisplayNameAttribute instead of its DisplayName property</param>
        public static PlaceHolderAttribute GetPlaceholder(this Type type, string memberName, bool returnAttribute, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = true)
        {
            return GetCustomAttribute<PlaceHolderAttribute>(type, memberName, bindingFlags, inherit);
        }
        /// <summary>Returns the given <see cref="DisplayNameAttribute.DisplayName"/> applied to this type</summary>
        /// <param name="type">The type to get the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes for the attribute if not present on the type itself</param>
        public static string GetDisplayName(this Type type, bool inherit = true)
        {
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>(inherit);
            return attribute == null ? null : attribute.DisplayName;
        }
        /// <summary>Returns the given <see name="PlaceHolderAttribute.DisplayName"/> applied to this type</summary>
        /// <param name="type">The type to get the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes for the attribute if not present on the type itself</param>
        public static string GetPlaceholder(this Type type, bool inherit = true)
        {
            var attribute = type.GetCustomAttribute<PlaceHolderAttribute>(inherit);
            return attribute == null ? null : attribute.DisplayName;
        }
        /// <summary>Returns the given <see cref="DisplayNameAttribute"/> applied to this type</summary>
        /// <param name="type">The type to get the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes for the attribute if not present on the type itself</param>
        /// <param name="returnAttribute">Only used for getting method overload so that you'll get the DisplayNameAttribute instead of its DisplayName property</param>
        public static DisplayNameAttribute GetDisplayName(this Type type, bool returnAttribute, bool inherit = true)
        {
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>(inherit);
            return attribute;
        }
        /// <summary>Returns the given <see cref="PlaceHolderAttribute"/> applied to this type</summary>
        /// <param name="type">The type to get the attribute from</param>
        /// <param name="inherit">Specify whether to look on the base classes for the attribute if not present on the type itself</param>
        /// <param name="returnAttribute">Only used for getting method overload so that you'll get the DisplayNameAttribute instead of its DisplayName property</param>
        public static PlaceHolderAttribute GetPlaceholder(this Type type, bool returnAttribute, bool inherit = true)
        {
            var attribute = type.GetCustomAttribute<PlaceHolderAttribute>(inherit);
            return attribute;
        }
    }
}
