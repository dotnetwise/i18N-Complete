using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Web.Mvc;

/// <summary>i18N localized version of System.ComponentModel.DisplayNameAttribute class.
/// <para>Specifies the display name for a property, event, or public void method which takes no arguments.</para>
/// </summary>
/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 17:33:06 GMT"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Enum | AttributeTargets.Field)]
public class DisplayNameAttribute
	: System.ComponentModel.DisplayNameAttribute
{

	#region Constructors

	/// <summary>Creates a new instance of LocalizableAttribute</summary>
	/// <param name="name"></param>
	/// <param name="args"></param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 17:33:06 GMT"/>
	public DisplayNameAttribute(string name, params object[] args)
		: base(name)
	{
		this.Arguments = args;
	}


	#endregion Constructors


	#region Override Properties

	/// <summary>Gets the DisplayName</summary>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 17:33:06 GMT"/>
	public override string DisplayName
	{
		get
		{
			return Arguments == null || Arguments.Length == 0
			? I18NComplete.GetText(this.DisplayNameValue)
			: string.Format(I18NComplete.GetText(this.DisplayNameValue), Arguments);
		}
	}


	#endregion Override Properties


	#region Properties

	/// <summary>Gets or sets the Arguments</summary>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 17:33:06 GMT"/>
	public object[] Arguments { get; set; }


	#endregion Properties


	private bool? _autoGenerateField;
	private bool? _autoGenerateFilter;
	private string _description;
	private string _groupName;
	private string _name;
	private int? _order;
	private string _prompt;
	//private Type _resourceType;
	private string _shortName;

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:19 GMT"/>
	public bool? GetAutoGenerateField()
	{
		return this._autoGenerateField;
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public bool? GetAutoGenerateFilter()
	{
		return this._autoGenerateFilter;
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public dynamic GetDescription()
	{
		return I18NComplete.GetText(this._description);
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public dynamic GetGroupName()
	{
		return I18NComplete.GetText(_groupName);
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public dynamic GetName()
	{
		return I18NComplete.GetText(_name);
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public int? GetOrder()
	{
		return this._order;
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public dynamic GetPrompt()
	{
		return I18NComplete.GetText(this._prompt);
	}

	/// <summary>
	/// </summary>
	/// <returns></returns>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:20 GMT"/>
	public dynamic GetShortName()
	{
		return I18NComplete.GetText(this._shortName) ?? this.GetName();
	}

	/// <summary>Gets or sets the AutoGenerateField</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:18 GMT"/>
	public bool AutoGenerateField
	{
		get
		{
			if (!this._autoGenerateField.HasValue)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} property has not been set.  Use the {1} method to get the value.", new object[] { "AutoGenerateField", "GetAutoGenerateField" }));
			}
			return this._autoGenerateField.Value;
		}
		set
		{
			this._autoGenerateField = new bool?(value);
		}
	}

	/// <summary>Gets or sets the AutoGenerateFilter</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:18 GMT"/>
	public bool AutoGenerateFilter
	{
		get
		{
			if (!this._autoGenerateFilter.HasValue)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} property has not been set.  Use the {1} method to get the value.", new object[] { "AutoGenerateFilter", "GetAutoGenerateFilter" }));
			}
			return this._autoGenerateFilter.Value;
		}
		set
		{
			this._autoGenerateFilter = new bool?(value);
		}
	}

	/// <summary>Gets or sets the Description</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:18 GMT"/>
	public string Description
	{
		get
		{
			return this._description;
		}
		set
		{
			if (this._description != value)
			{
				this._description = value;
			}
		}
	}

	/// <summary>Gets or sets the GroupName</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:18 GMT"/>
	public string GroupName
	{
		get
		{
			return this._groupName;
		}
		set
		{
			if (this._groupName != value)
			{
				this._groupName = value;
			}
		}
	}

	/// <summary>Gets or sets the Name</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:18 GMT"/>
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (this._name != value)
			{
				this._name = value;
			}
		}
	}

	/// <summary>Gets or sets the Order</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:19 GMT"/>
	public int Order
	{
		get
		{
			if (!this._order.HasValue)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} property has not been set.  Use the {1} method to get the value.", new object[] { "Order", "GetOrder" }));
			}
			return this._order.Value;
		}
		set
		{
			this._order = new int?(value);
		}
	}

	/// <summary>Gets or sets the Prompt</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:19 GMT"/>
	public string Prompt
	{
		get
		{
			return this._prompt;
		}
		set
		{
			if (this._prompt != value)
			{
				this._prompt = value;
			}
		}
	}

	//public Type ResourceType
	//{
	//    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
	//    get
	//    {
	//        return this._resourceType;
	//    }
	//    set
	//    {
	//        if (this._resourceType != value)
	//        {
	//            this._resourceType = value;
	//            //this._shortName.ResourceType = value;
	//            //this._name.ResourceType = value;
	//            //this._description.ResourceType = value;
	//            //this._prompt.ResourceType = value;
	//            //this._groupName.ResourceType = value;
	//        }
	//    }
	//}

	/// <summary>Gets or sets the ShortName</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:19 GMT"/>
	public string ShortName
	{
		get
		{
			return this._shortName;
		}
		set
		{
			if (this._shortName != value)
			{
				this._shortName = value;
			}
		}
	}


}
/// <summary>i18N localized version of System.ComponentModel.DataAnnotations.RangeAttribute class.
/// <para>Specifies the numeric range constraints for the value of a data field.</para></summary>
/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:15 GMT"/>
public class RangeAttribute
	: System.ComponentModel.DataAnnotations.RangeAttribute
{

	#region Constructors

	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute class by using the specified minimum and maximum values whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
	/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:15 GMT"/>
	public RangeAttribute(string errorMessage, double minimum, double maximum)
		: base(minimum, maximum)
	{
		this.ErrorMessage = errorMessage;
	}

	/// <summary>
	/// Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute class by using the specified minimum and maximum values whose error message is localized. 
	/// <para>ErrorMessage is set to: </para>
	/// 	<para>The field {0} must be between {1} and {2}.</para>
	/// </summary>
	/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
	/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:11:37 GMT"/>
	public RangeAttribute(double minimum, double maximum)
		: this("The field {0} must be between {1} and {2}.", minimum, maximum)
	{

	}

	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute class by using the specified minimum and maximum values whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
	/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:15 GMT"/>
	public RangeAttribute(string errorMessage, int minimum, int maximum)
		: base(minimum, maximum)
	{
		this.ErrorMessage = errorMessage;
	}
	/// <summary>
	/// Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute class by using the specified minimum and maximum values whose error message is localized. 
	/// <para>ErrorMessage is set to: </para>
	/// 	<para>The field {0} must be between {1} and {2}.</para>
	/// </summary>
	/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
	/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:23:27 GMT"/>
	public RangeAttribute(int minimum, int maximum)
		: this("The field {0} must be between {1} and {2}.", minimum, maximum)
	{

	}

	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute class by using the specified minimum and maximum values whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="type">Specifies the type of the object to test.</param>
	/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
	/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:15 GMT"/>
	public RangeAttribute(string errorMessage, Type type, string minimum, string maximum)
		: base(type, minimum, maximum)
	{
		this.ErrorMessage = errorMessage;
	}


	/// <summary>
	/// Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute class by using the specified minimum and maximum values whose error message is localized. 
	/// <para>ErrorMessage is set to: </para>
	/// 	<para>The field {0} must be between {1} and {2}.</para>
	/// </summary>
	/// <param name="type">Specifies the type of the object to test.</param>
	/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
	/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:23:28 GMT"/>
	public RangeAttribute(Type type, string minimum, string maximum)
		: this("The field {0} must be between {1} and {2}.", type, minimum, maximum)
	{

	}

	#endregion Constructors


	#region Override Methods

	/// <summary>Formats the error message that is displayed when range validation fails.</summary>
	/// <param name="name">The name of the field that caused the validation failure.</param>
	/// <returns>The localized formatted error message</returns>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:15 GMT"/>
	public override string FormatErrorMessage(string name)
	{
		return CultureInfo.CurrentCulture._(this.ErrorMessageString, name, this.Minimum, this.Maximum);
	}


	#endregion Override Methods

}
/// <summary>i18N localized version of System.ComponentModel.DataAnnotations.RequiredAttribute class.
/// <para>Specifies that a data field value is required.</para></summary>
/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:15 GMT"/>
public class RequiredAttribute
	: System.ComponentModel.DataAnnotations.RequiredAttribute
{

	#region Constructors

	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.RequiredAttribute class whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
	public RequiredAttribute(string errorMessage)
		: base()
	{
		this.ErrorMessage = errorMessage;
	}

	/// <summary>
	/// 	<para>Initializes a new instance of the System.ComponentModel.DataAnnotations.RequiredAttribute class whose error message is localized.</para>
	/// 	<para>ErrorMessage is set to: </para>
	/// 	<para>The {0} field is required.</para>
	/// </summary>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:04:48 GMT"/>
	public RequiredAttribute()
		: this("The {0} field is required.")
	{

	}

	#endregion Constructors


	#region Override Methods

	/// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
	/// <param name="name">The name to include in the formatted message.</param>
	/// <returns>The localized formatted error message</returns>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
	public override string FormatErrorMessage(string name)
	{
		return CultureInfo.CurrentCulture._(this.ErrorMessageString, name);
	}


	#endregion Override Methods

}
/// <summary>i18N localized version of System.ComponentModel.DataAnnotations.RegularExpressionAttribute class.
/// <para>Specifies that a data field value in ASP.NET Dynamic Data must match the specified regular expression.</para></summary>
/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
public class RegularExpressionAttribute
	: System.ComponentModel.DataAnnotations.RegularExpressionAttribute
{

	#region Constructors

	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.RegularExpressionAttribute class whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="pattern">The regular expression that is used to validate the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
	public RegularExpressionAttribute(string errorMessage, string pattern)
		: base(pattern)
	{
		this.ErrorMessage = errorMessage;
	}
	/// <summary>
	/// Initializes a new instance of the System.ComponentModel.DataAnnotations.RegularExpressionAttribute class whose error message is localized. 
	/// <para>ErrorMessage is set to: </para>
	/// 	<para>The field {0} must match the regular expression '{1}'.</para>
	/// </summary>
	/// <param name="pattern">The regular expression that is used to validate the data field value.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:04:07 GMT"/>
	public RegularExpressionAttribute(string pattern)
		: this("The field {0} must match the regular expression '{1}'.", pattern)
	{

	}

	#endregion Constructors


	#region Override Methods

	/// <summary>Formats the error message to display if the regular expression validation fails.</summary>
	/// <param name="name">The name of the field that caused the validation failure.</param>
	/// <returns>The localized formatted error message</returns>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
	public override string FormatErrorMessage(string name)
	{
		return string.Format(CultureInfo.CurrentCulture, I18NComplete.GetText(base.ErrorMessageString), new object[] { name, this.Pattern });
	}


	#endregion Override Methods

}
/// <summary>i18N localized version of System.ComponentModel.DataAnnotations.StringLengthAttribute class.
/// <para>Specifies the minimum and maximum length of characters that are allowed in a data field.</para>
/// </summary>
/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class StringLengthAttribute
	: System.ComponentModel.DataAnnotations.StringLengthAttribute
{

	#region Constructors

	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.StringLengthAttribute class by using a specified maximum length  whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="maximumLength">The maximum length of a string.</param>
	/// <param name="minLenght">The minimum length of a string.</param>
	/// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 04:37:52 GMT"/>
	public StringLengthAttribute(string errorMessage, int maximumLength, int minLenght)
		: base(maximumLength)
	{
		this.MinimumLength = minLenght;
		ErrorMessage = errorMessage;
	}
	/// <summary>Initializes a new instance of the System.ComponentModel.DataAnnotations.StringLengthAttribute class by using a specified maximum length  whose error message is localized.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="maximumLength">The maximum length of a string.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
	public StringLengthAttribute(string errorMessage, int maximumLength)
		: base(maximumLength)
	{
		ErrorMessage = errorMessage;
	}
	/// <summary>
	/// Initializes a new instance of the System.ComponentModel.DataAnnotations.StringLengthAttribute class by using a specified maximum length  whose error message is localized. 
	/// <para>ErrorMessage is set to: </para>
	/// <para>The {0} must be at least {2} characters long.</para>
	/// </summary>
	/// <param name="maximumLength">The maximum length of a string.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:01:59 GMT"/>
	public StringLengthAttribute(int maximumLength)
		: this("The {0} must be at least {2} characters long.", maximumLength)
	{

	}

	/// <summary>
	/// Initializes a new instance of the System.ComponentModel.DataAnnotations.StringLengthAttribute class by using a specified maximum length  whose error message is localized. 
	/// <para>ErrorMessage is set to: </para>
	/// <para>The {0} must be at least {2} characters long but no longer than {1} characters.</para>
	/// </summary>
	/// <param name="maximumLength">The maximum length of a string.</param>
	/// <param name="minLength">The minimum length of the string.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:01:59 GMT"/>
	public StringLengthAttribute(int maximumLength, int minLength)
		: this("The {0} must be at least {2} characters long but no longer than {1} characters.", maximumLength)
	{
		this.MinimumLength = minLength;
	}

	#endregion Constructors


	#region Override Methods

	/// <summary>Applies formatting to a specified error message.</summary>
	/// <param name="name">The name of the field that caused the validation failure.</param>
	/// <returns>The localized formatted error message</returns>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 18:14:16 GMT"/>
	public override string FormatErrorMessage(string name)
	{
		string format = ((this.MinimumLength != 0) && this.ErrorMessage == null) ? I18NComplete.GetText("The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.") : base.ErrorMessageString;

		return CultureInfo.CurrentCulture._(format, name, this.MaximumLength, this.MinimumLength);
	}


	#endregion Override Methods

}

/// <summary>i18N localized version of System.Web.Mvc.CompareAttribute class.
/// <para>Provides an attribute that compares two properties of a model.</para></summary>
/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:23:28 GMT"/>
[AttributeUsage(AttributeTargets.Property)]
public class CompareAttribute 
	: System.Web.Mvc.CompareAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="T:System.Web.Mvc.CompareAttribute"/>class. 
	/// <para>ErrorMessage is set to: </para>
	/// 	<para>'{0}' and '{1}' do not match.</para>
	/// </summary>
	/// <param name="otherProperty">The property to compare with the current property.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:23:28 GMT"/>
	public CompareAttribute(string otherProperty)
		: this("'{0}' and '{1}' do not match.", otherProperty)
	{

	}
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.CompareAttribute"/> class.</summary>
	/// <param name="errorMessage">An error message to associate with a validation control if validation fails.</param>
	/// <param name="otherProperty">The property to compare with the current property.</param>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:23:29 GMT"/>
	public CompareAttribute(string errorMessage, string otherProperty)
		: base(otherProperty)
	{
		this.ErrorMessage = errorMessage;
	}

	/// <summary>Applies formatting to a specified error message.</summary>
	/// <param name="name">The name of the field that caused the validation failure.</param>
	/// <returns>The localized formatted error message</returns>
	/// <created author="laurentiu.macovei" date="Mon, 26 Dec 2011 19:23:29 GMT"/>
	public override string FormatErrorMessage(string name)
	{
		return CultureInfo.CurrentCulture._(ErrorMessage, name, this.OtherProperty);
	}
}


/// <summary>Adds a localized version of the PlaceHolder attribute</summary>
/// <created author="laurentiu.macovei" date="Sat, 07 Jan 2012 03:15:06 GMT"/>
public class PlaceHolderAttribute 
	: Attribute, IMetadataAware
{
	private readonly string Placeholder;
	/// <summary>Creates a new instance of PlaceHolderAttribute</summary>
	/// <param name="placeholder"></param>
	/// <created author="laurentiu.macovei" date="Sat, 07 Jan 2012 03:15:06 GMT"/>
	public PlaceHolderAttribute(string placeholder)
	{
		Placeholder = placeholder;
	}

	/// <summary>Provides metadata to the model metadata creation process.</summary>
	/// <param name="metadata">The model metadata.</param>
	/// <created author="laurentiu.macovei" date="Sat, 07 Jan 2012 03:15:06 GMT"/>
	public void OnMetadataCreated(ModelMetadata metadata)
	{
		if (Placeholder != null)
			metadata.AdditionalValues["placeholder"] = CultureInfo.CurrentCulture._(Placeholder);
	}
}