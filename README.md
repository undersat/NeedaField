# NeedaField

NeedaField is a utility that allows you to create on the fly new fields in the entities of your dotnet project even when it is in production, with no need to modify the source, database or the forms.


## HOW DOES IT WORK


NeedaField recycles an entity string field and stores multiple fields over it dynamically in Json format. These new fields are defined in your web or app.config in a parameter of your appSettings. In practice, NeedaField works as a new data type.

```cs
        public string etc { get; set; } // your extra field


        [NotMapped]
        public NeedaField NAFetc // new property
        {
            get { return new NeedaField(etc); }
            set { etc = value.ToString(); }
        }
```
You should guarantee enough space in database field 

```
	etc NVARCHAR(MAX)
```

## WHAT IS NeddaField FOR

In how many of your projects have you found that, once exhaustively analysed, developed, published and already in operation, new requirements arise that force you to create new fields urgently?

With NeedaField you can add new fields to the entities of your project on the fly. You don't need to touch the code of your program, nor modify the structure of your databases, nor even modify the forms, nor publish a new version.

Just define the new data in your appSettings section of to web.config or app.config

It is developed in .net for .net projects.

NeedaField uses the package **Newtonsoft.Json** and the library **ConfigurationManager**


## WHAT DO TOU NEED

- the NeedaField nuget package
- a dotnet project
- an extra varchar(MAX) field in each of your tables (example: etc VARCHAR(MAX))
- the newtonsoft.json package, if you don't already have it.
- the ConfigurationManager library
- Razor forms for Create Edit and get Details of your entity.


## IMPLEMENTATION STEPS

1. Install NeedaField from nuget
```
 > install-package NeedaField
```

2. Extend your extra field in the class that maps your table in this way :

```cs
        public string etc { get; set; } // your extra field

        [NotMapped]
        public NeedaField NAFetc // new property
        {
            get { return new NeedaField(etc); }
            set { etc = value.ToString(); }
        }
```

3. Replace in your **Create, Edit, Details** forms the extra field by the NeedaField field

```
	 - @Html.EditorFor(x => x.etc)      
	 + @Html.EditorFor(x => x.NAFetc) 
	 
	 - @Html.DisplayFor(x => x.etc)      
	 + @Html.DisplayFor(x => x.NAFetc) 
```
4. define a new parameter in your appSettings prefixed NAF

```
    <add key="NAFetc_Teams" value="{}"/>
```

5. ... and use it to append new fields as needed. Thats all! 

    <add key="NAFetc_Teams" value="{'Stadium' : {}}"/>
	
If it is not a text field (is a number, date, checkbox, etc), you will need to define its type according to the html syntax for input fields
```
	<add key="NAFetc_Teams" value="{'Stadium' : {}, 'Founded in' : {'type' : 'date'}, 'Cups' : {'type' : 'number'}}"/>
```
Magically, the field will appear in your forms. You will be able to edit it and its value will be saved in the database just like the other fields.

You can optionally customize the new Razor templates for NeedaField.cshtml in the Views/Shared EditorTemplates and DisplayTemplates folders. Note that if you reinstall the package you will lose your changes.

## DOCUMENTACION

[Documentation here](https://undersat.com/blog/NeedaField)

## LIVE DEMO
[Interactive demo here](https://soccer-NeedaField-sample-by-undersat.azurewebsites.net)
							
## LICENCIA 

The MIT License (MIT)

Copyright (c) 2021 UNDERSAT IT S.L.

All rights reserved.

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





