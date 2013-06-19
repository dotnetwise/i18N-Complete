var appConfig = appConfig || {};
!function (appConfig) {
	var i = 0;
	$.each(appConfig.messages, function (key, value) {
		value.t = value.t || key;
		value.pt = value.pt || key;
		value.f = typeof key == "string" && key.indexOf('{') >= 0;
		appConfig.messages[i] = value;
	});
	return $.extend(window, {
		_: _,
		_s: _s
	});
	function _(message, args) {
		/// <summary>Gets the translated version of the message formmated with any given args</summary>
		/// <param name="message" type="String">The message key to be translated</param>
		/// <param name="args" type="Object" parameterArray="true" optional="true">Specify arguments so that the message will be formmated i.e. {0}, {1} - similar to string.format("Hello {0}", user)</param>
		var translation = message in appConfig.messages ? appConfig.messages[message] : message;
		var hasFormat = !translation || translation && translation.f !== false;
		translation = translation && translation.t;
		translation = translation || message;
		message = translation;
		return hasFormat ? String.format.apply(this, arguments) : message;
	}
	function _s(singular, plural, count) {
		/// <summary>Gets the translated version of the message for both singular and plural formmated with any given args</summary>
		/// <param name="singular" type="String">The singular message to be translated</param>
		/// <param name="plural" type="String">The plural message to be translated</param>
		/// <param name="args" type="Object" parameterArray="true" optional="true">Specify arguments so that the message will be formmated i.e. {0}, {1} - similar to string.format("Hello {0}", user)</param>

		var translation = message in appConfig.messages ? appConfig.messages[message] : message;
		var hasFormat = !translation || translation && translation.f !== false;
		translation = count != 1 ? translation.pt : translation.t;
		translation = translation || (count != 1 ? plural : singular);
		plural = translation;
		if (hasFormat) {
			Array.prototype.unshift.call(arguments);
			return String.format.apply(this, arguments);
		}
		return translation;
	}
}(appConfig);
