# Mixing-Pages - English
Starting from a scanned document with odd and even pages, it sorts them into a new document.

# Mixing-Pages - Español
A partir de un documento escaneado con páginas pares e impares, las ordena en un nuevo documento.

Cuando no se dispone de un scaner a doble página, puede resultar una tarea pesada conseguir un pdf con todas las páginas bien clasificadas. Tras escanear la primera cara, páginas impares, las páginas pares quedan ordenadas de forma inversa respecto las impares. 

El objetivo de este programa es recuperar la lista de documentos escaneados (formato pdf de una página por documento), y obtener un documento final donde se hayan mezclado las páginas pares e impares del documento, obteniendo el resultado equivalente de un escaneado a doble página.

## Características
- Fácilmente configurable desde un fichero json editable por el usuario. para ello se usa la librería [Newtonsoft.Json.12.0.3](https://www.newtonsoft.com/json)
- Permite procesar cualquier formato de fichero (pdf, jpg,...)
- Se puede seleccionar la copia de los ficheros originales clasificados y mezclados.
- Genera un nuevo pdf, si se ha elegido dicha opción, con los pdf escaneados ordenados y mezclados correctamente. Para ello usa la librería [PDFsharp.1.50.5147](http://www.pdfsharp.net/)

## Requisitos
Se ha desarrollado con Windows 10 y Visual Studio 2019. Se ha probado únicamente con dicho sistema operativo. El requisito básico sería disponer de Windows 10, aunque seguramente funcione en versiones anteriores.
.net Framework 4.7.2

