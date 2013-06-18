using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace System.Web.Mvc
{

	/// <summary>The EnhancedDataAnnotationsModelMetadataProvider class</summary>
	/// <created author="laurentiu.macovei" date="Sun, 27 Mar 2011 00:07:08 GMT"/>
	public class EnhancedDataAnnotationsModelMetadataProvider
		: DataAnnotationsModelMetadataProvider
	{
		/// <summary>Gets the metadata for the specified property.</summary>
		/// <param name="attributes"></param>
		/// <param name="containerType"></param>
		/// <param name="modelAccessor"></param>
		/// <param name="modelType"></param>
		/// <param name="propertyName"></param>
		/// <created author="laurentiu.macovei" date="Sun, 27 Mar 2011 00:07:08 GMT"/>
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			var displayAttribute = attributes.OfType<global::DisplayNameAttribute>().FirstOrDefault();
			var attrs = attributes;
			var defaultValueAttribute = attributes.OfType<DefaultValueAttribute>().FirstOrDefault();
			//var eitherAttribute = attributes.OfType<EitherAttribute>().FirstOrDefault();
			var htmlEncodeAttribute = attributes.OfType<HtmlEncodeAttribute>().FirstOrDefault();
			//var pageAttribute = attributes.OfType<PageAttribute>().FirstOrDefault();
			var htmlPropertiesAttribute = attributes.OfType<HtmlPropertiesAttribute>().FirstOrDefault();


			if (displayAttribute != null)
			{
				if (!attrs.OfType<System.ComponentModel.DataAnnotations.DisplayAttribute>().Any())
					attrs = attrs.Concat(new System.ComponentModel.DataAnnotations.DisplayAttribute
						{
							Description = GetString(displayAttribute.GetDescription()),
							AutoGenerateField = displayAttribute.GetAutoGenerateField() ?? true,
							AutoGenerateFilter = displayAttribute.GetAutoGenerateFilter() ?? true,
							GroupName = GetString(displayAttribute.GetGroupName()),
							Name = GetString(displayAttribute.GetName()),
							Order = displayAttribute.GetOrder() ?? 0,
							Prompt = GetString(displayAttribute.GetPrompt()),
							ShortName = GetString(displayAttribute.GetShortName()),
						}.ToEnumerable());
			}
			var modelData = base.CreateMetadata(attrs, containerType, modelAccessor, modelType, propertyName);
			if (htmlPropertiesAttribute != null)
				modelData.AdditionalValues.Add("HtmlAttributes", htmlPropertiesAttribute);

			if (htmlEncodeAttribute != null)
				modelData.AdditionalValues["HtmlEncodeAttribute"] = htmlEncodeAttribute;
			if (displayAttribute != null)
				modelData.AdditionalValues["DisplayAttribute"] = displayAttribute;
			if (defaultValueAttribute != null)
				modelData.AdditionalValues["DefaultValue"] = defaultValueAttribute.Value;
			//if (pageAttribute != null)
			//    modelData.AdditionalValues["PageAttribute"] = pageAttribute;

			//if (eitherAttribute != null)
			//{
			//    Dictionary<string, ModelMetadata> other;
			//    object otherValue;
			//    if (!modelData.AdditionalValues.TryGetValue("EitherAttribute", out otherValue) || (other = otherValue as Dictionary<string, ModelMetadata>) == null)
			//        modelData.AdditionalValues["EitherAttribute"] = other = new Dictionary<string, ModelMetadata>();
			//    var otherProperty = containerType.GetProperty(eitherAttribute.OtherProperty, Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public | Reflection.BindingFlags.NonPublic);
			//    var eitherAttributes = otherProperty.GetCustomAttributes(true).OfType<Attribute>().ToArray();
			//    var otherPropertyModel = this.CreateMetadata(eitherAttributes, containerType, () => null, modelType, eitherAttribute.OtherProperty);
			//    other[eitherAttribute.OtherProperty] = otherPropertyModel;
			//    if (eitherAttribute.OtherPropertyName == null)
			//        eitherAttribute.OtherPropertyName = otherPropertyModel.GetDisplayName();
			//}
			return modelData;
		}

		/// <summary>
		/// </summary>
		/// <param name="stringOrMvcString"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Sun, 27 Mar 2011 00:07:08 GMT"/>
		private string GetString(dynamic stringOrMvcString)
		{
			return stringOrMvcString == null ? null : stringOrMvcString.ToString();
		}
	}
}
