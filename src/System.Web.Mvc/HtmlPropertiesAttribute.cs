using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc
{
	/// <summary>The HtmlPropertiesAttribute class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:07 GMT"/>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class HtmlPropertiesAttribute
		: Attribute, IDictionary<string, object>
	{

		#region Fields

		private RouteValueDictionary _HtmlAttributes;
		private bool _IsReadOnly;

		#endregion Fields


		#region Constructors

		/// <summary>Creates a new instance of HtmlPropertiesAttribute</summary>
		/// <param name="htmlAttributes"></param>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:07 GMT"/>
		public HtmlPropertiesAttribute(params object[] htmlAttributes)
		{
			if (htmlAttributes == null || htmlAttributes.Length == 0)
				this._HtmlAttributes = new RouteValueDictionary();
			if (htmlAttributes.Length % 2 == 1)
				throw new ArgumentException("htmlAttributes", "Parameters needs to be a even number in format key1, value1, key2, value2 etc. ");
			this._HtmlAttributes = new RouteValueDictionary();
			for (int i = 0; i < htmlAttributes.Length - 1; i += 2) 
				_HtmlAttributes[htmlAttributes[i] as string] = htmlAttributes[i + 1];
			_IsReadOnly = true;
		}


		#endregion Constructors


		#region Util Properties

		/// <summary>Gets the IDictionary&lt;string, object&gt;.Keys</summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:07 GMT"/>
		ICollection<string> IDictionary<string, object>.Keys
		{
			get { return _HtmlAttributes.Keys; }
		}

		/// <summary>Gets the IDictionary&lt;string, object&gt;.Values</summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		ICollection<object> IDictionary<string, object>.Values
		{
			get { return _HtmlAttributes.Values; }
		}

		/// <summary>Gets or sets the IDictionary&lt;string, object&gt;.this</summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		object IDictionary<string, object>.this[string key]
		{
			get
			{
				return _HtmlAttributes[key];
			}
			set
			{
				_HtmlAttributes[key] = value;
			}
		}

		/// <summary>Gets the ICollection&lt;KeyValuePair&lt;string, object&gt;&gt;.Count</summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		int ICollection<KeyValuePair<string, object>>.Count
		{
			get { return _HtmlAttributes.Count; }
		}

		/// <summary>Gets the ICollection&lt;KeyValuePair&lt;string, object&gt;&gt;.IsReadOnly</summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get { return _IsReadOnly; }
		}


		#endregion Util Properties


		#region Properties

		/// <summary>Gets or sets the HtmlAttributes</summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		public RouteValueDictionary HtmlAttributes { get { return _HtmlAttributes; } set { _HtmlAttributes = value; } }


		#endregion Properties


		#region Util Methods

		/// <summary>
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		void IDictionary<string, object>.Add(string key, object value)
		{
			_HtmlAttributes.Add(key, value);
		}

		/// <summary>
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		bool IDictionary<string, object>.ContainsKey(string key)
		{
			return _HtmlAttributes.ContainsKey(key);
		}

		/// <summary>
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		bool IDictionary<string, object>.Remove(string key)
		{
			return _HtmlAttributes.Remove(key);
		}

		/// <summary>
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return _HtmlAttributes.TryGetValue(key, out value);
		}

		/// <summary>
		/// </summary>
		/// <param name="item"></param>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			_HtmlAttributes.Add(item.Key, item.Value);
		}

		/// <summary>
		/// </summary>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			_HtmlAttributes.Clear();
		}

		/// <summary>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return _HtmlAttributes.Remove(item.Key);
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return _HtmlAttributes.GetEnumerator();
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:09 GMT"/>
		Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
		{
			return _HtmlAttributes.GetEnumerator();
		}


		#endregion Util Methods


		#region Business Methods

		/// <summary>
		/// </summary>
		/// <param name="arguments"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 19:43:08 GMT"/>
		public static RouteValueDictionary Merge(params object[] arguments)
		{
			if (arguments != null)
			{
				//quick return of HtmlPropertiesAttribute alone?
				if (arguments.Length == 1)
				{
					var htmlAttributes = arguments[0] as HtmlPropertiesAttribute;
					if (htmlAttributes != null)
						return htmlAttributes._HtmlAttributes;
				}
				var result = new RouteValueDictionary().SetValues(true, arguments);
				return result;
			}
			return null;
		}


		#endregion Business Methods


	}
}
