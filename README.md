# NeedaField

NeedaField es una utilidad de emergencia que permite crear nuevos campos en la entidades de tu proyecto dotnet de forma inmediata cuando éste ya está en explotación, sin necesidad de modificar el fuente, la base de datos o los formularios. 


## COMO FUNCIONA


NeedaField reutiliza un campo string de tu entidad y almacena sobre él múltiples campos dinámicamente en formato Json. Estos nuevos campos se definen en tu web o app.config en un parámetro de tu appSettings. En la prácica, NeedaField funciona como un nuevo tipo de dato.

```cs
        public string etc { get; set; } // your extra field


        [NotMapped]
        public NeedaField dinaetc // new property
        {
            get { return new NeedaField(etc); }
            set { etc = value.ToString(); }
        }
```


## PARA QUE SIRVE

En cuantos de tus proyectos te has encontrado con que, una vez analizado exhaustivamente, desarrollado, publicado y ya en explotación, salen nuevos requisitos que te obligan a crear nuevos campos urgentemente ?

Con NeedaField puedes añadir nuevos campos a las entidades de tu proyecto en caliente. No necesitas tocar el código de tu programa, ni modificar la estructura de tus base de datos, ni siquiera modificar los formularios, ni publicar una nueva versión. 

Basta con definir el nuevo dato en la sección tu appSettings de to web.config o app.config

Está desarrollado en .net para proyectos .net. 

NeedaField necesita el paquete **Newtonsoft.Json** y la libreria **ConfigurationManager**


## QUE NECESITAS

- un proyecto dotnet
- un campo extra varchar(MAX) en cada una de tus tablas (ejemplo: etc VARCHAR(MAX))
- el paquete newtonsoft.json, si aún no lo tienes.
- la libreria ConfigurationManager
- formularios Razor para Create Edit y Details


## PASOS PARA IMPLEMENTAR NeedaField

1. Instalar NeedaField de nuget
```
 > install-package NeedaField
```

2. Extender tu campo extra en la clase que mapea tu tabla de esta forma

```cs
        public string etc { get; set; } // your extra field
        
        
        [NotMapped]
        public NeedaField dinaetc // new property
        {
            get { return new NeedaField(etc); }
            set { etc = value.ToString(); }
        }
```

3. Incorporar las nuevas plantillas Razor para NeedaField.cshtml en las carpetas de Views/Shared EditorTemplates y DisplayTemplates

4. Sustituir en tus formularios **Create, Edit, Details** el campo extra por el campo NeedaField
```
	 - @Html.EditorFor(x => x.etc)      
	 + @Html.EditorFor(x => x.dinaetc) 
	 
	 - @Html.DisplayFor(x => x.etc)      
	 + @Html.DisplayFor(x => x.dinaetc) 
```
5. definir un parámetros en tu appSettings y añadirle nuevos campos
```
    <add key="dinaetc_Teams" value="{}"/>
```

Y eso es todo. Te piden un nuevo campo ? Lo añades en tu app.config o web.config

    <add key="dinaetc_Teams" value="{'Stadium' : {}}"/>
	
Si no es de tipo texto, necesitarás definir su tipo según la sintaxis html para campos input
```
	<add key="dinaetc_Teams" value="{'Stadium' : {}, 'Founded in' : {'type' : 'date'}, 'Cups' : {'type' : 'number'}}"/>
```


## DOCUMENTACION

[Documentación aquí](https://undersat.com/blog/NeedaField)

## LIVE DEMO
[Demo interactiva aquí](https://soccer-NeedaField-sample-by-undersat.azurewebsites.net)
							
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





