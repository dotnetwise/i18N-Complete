i18N-Complete
=============

ASP.NET MVC &amp; Javascript localization done right

* Singular
* Plural


### Singular 
`_`, `__` or `___`

#### Quick translation in `C#`

```cs
//var msg = "Some message"; 

//becomes
var msg = _("Some message");
```

#### Quick translation in `cshtml`

```cshtml
<div>Some message</div>

<div>@_("Some message")</div>
```

#### `_`: HTML encoded format expression and arguments
Both the expression and the arguments will be HTML encodeed

```cshtml
@{ var names = "John&Doe"; }
<div>@_("Welcome {0}. Your age is < 100", name)</div>

//becomes
<div>Welcome John&amp;Doe. Your age is &lt; 100</div>
```

#### `__`: Expression can contain HTML, while arguments are HTML encoded
Make sure that you provide a valid HTML when you translate the expression

```cshtml
@{ var names = "John&Doe"; }
<div>@__("Welcome <b>{0}</b>. Your age is &lt; 100", name)</div>

//becomes
<div>Welcome <b>John&amp;Doe<b>. Your age is &lt; 100</div>
```

#### `___`: Both expression and arguments can contain HTML
This could be potentially dangerous if the arguments are not trusted i.e. come from user input such as query string, database etc. as it could lead to XSS attacks.

```cshtml
@{ var names = "John <i>Doe</i>"; }
<div>@__("Welcome <b>{0}</b>. Your age is &lt; 100", name)</div>

//becomes
<div>Welcome <b>John <i>Doe</i><b>. Your age is &lt; 100</div>
```

###Plural 
`_s`, `__s`, `___s`

#### Quick translation in `cshtml`
```cs
//var msg = count == 1 ? "One item in the basket" : string.Format("{0} items in the basket", count); 

//becomes
var msg = _s("One item in the basked", "{0} items in the basket", count);
```

#### `_s`: Plural simple usage in .cshtml
```cshtml
@if (count == 1) {
  <div>One item in the basked</div>
}
else {
  <div>@count items in the basked</div>
}

//becomes
<div>@_s("One item in the basked", "{0} items in the basket", count)</div>
```

[More Documentation to be written]
In the meanwhile you could also navigate to your webiste to `/Localization/Help` for more documentation

###Licensing information

The MIT License (MIT)

Copyright (c) 2014 DotNetWise

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
