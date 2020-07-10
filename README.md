# Mixing-Pages - English

Starting from a scanned document with odd and even pages, it sorts them into a new document.

## License - The MIT License ([MIT](https://en.wikipedia.org/wiki/MIT_License))

Copyright (c) 2020, apps4every

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

# Mixing-Pages - Español

A partir de un documento escaneado con páginas pares e impares, las ordena en un nuevo documento.

Cuando no se dispone de un scaner a doble página, puede resultar una tarea pesada conseguir un pdf con todas las páginas bien clasificadas. Tras escanear la primera cara, páginas impares, las páginas pares quedan ordenadas de forma inversa respecto las impares. 

El objetivo de este programa es recuperar la lista de documentos escaneados (formato pdf de una página por documento), y obtener un documento final donde se hayan mezclado las páginas pares e impares del documento, obteniendo el resultado equivalente de un escaneado a doble página.

Para escanear, un buen programa es [VueScan](https://www.hamrick.com/), también puedes usar otras [alternativas](https://alternativasde.com/tools/vuescan).

El procedimiento a seguir sería el siguiente:
- Configurar el programa de scanner que dispongas para que cada página escaneada se genere en un pdf independiente y configurar la ruta donde se grabara el resultado escaneado (por ejemplo C:\temp\document).
- Poner el documento en el alimentador de páginas, para empezar a escanear las páginas impares.
- Tras finalizar el escenado de las páginas impares, moverlas a una carpeta donde se identifique como páginas impares (por ejemplo C:\temp\document\impar).
- Poner el documento en el alimentador de páginas, para empezar a escanear las páginas pares.
- Tras finalizar el escenado de las páginas pares, moverlas a una carpeta donde se identifique como páginas pares (por ejemplo C:\temp\document\par).
- Ejecutar el programa C:\Program Files (x86)\Apps4Every\MixerPages\MezcladorPaginas.exe, ver instrucción de ejecución.
- El resultado lo obtendrás en la carpeta donde indicaste el escaneado del documento (por ejemplo C:\temp\document)

## Características
- Fácilmente configurable desde un fichero json editable por el usuario. para ello se usa la librería [Newtonsoft.Json.12.0.3](https://www.newtonsoft.com/json)
- Permite procesar cualquier formato de fichero (pdf, jpg,...)
- Se puede seleccionar la copia de los ficheros originales clasificados y mezclados.
- Genera un nuevo pdf, si se ha elegido dicha opción, con los pdf escaneados ordenados y mezclados correctamente. Para ello usa la librería [PDFsharp.1.50.5147](http://www.pdfsharp.net/)

## Requisitos
Inicialmente necesita:
- Windows 10, aunque seguramente funcione en versiones anteriores.
- .net Framework 4.7.2

## Instalación
Desde Visual Studio:
- Abre el fichero MezcladorPaginas.sln y ejecútalo.

Desde Windows:
- Abre el fichero setup.exe y ejecútalo.

## Licencia - The MIT License ([MIT](https://es.wikipedia.org/wiki/Licencia_MIT))

Copyright (c) 2020, apps4every

Por la presente, se otorga permiso, sin cargo, a cualquier persona que obtenga una copia de este software y los archivos de documentación asociados
(el "Software"), para negociar en el Software sin restricciones, incluidos, entre otros, los derechos de uso, copia, modificación, fusión,
publicar, distribuir, sublicenciar y / o vender copias del Software, y permitir a las personas a quienes se les proporciona el Software
entonces, sujeto a las siguientes condiciones:

El aviso de copyright anterior y este aviso de permiso se incluirán en todas las copias o partes sustanciales del Software.

EL SOFTWARE SE PROPORCIONA "TAL CUAL", SIN GARANTÍA DE NINGÚN TIPO, EXPRESA O IMPLÍCITA, INCLUYENDO PERO SIN LIMITARSE A LAS GARANTÍAS DE
COMERCIABILIDAD, APTITUD PARA UN PROPÓSITO PARTICULAR Y NO INFRACCIÓN. EN NINGÚN CASO SERÁN RESPONSABLES LOS AUTORES O LOS TITULARES DE LOS DERECHOS DE AUTOR
POR CUALQUIER RECLAMACIÓN, DAÑO U OTRA RESPONSABILIDAD, YA SEA EN UNA ACCIÓN DE CONTRATO, O DE OTRA MANERA, DERIVADA DE, FUERA DE O EN CONEXIÓN
CON EL SOFTWARE O EL USO U OTRAS OFERTAS EN EL SOFTWARE.

## Inicio Rápido

Las siguientes instrucciones presuponen que estás usando la versión instalada, no la ejecución desde Visual Studio.

#### Configuración

El programa se encuentra en C:\Program Files (x86)\Apps4Every\MixerPages.

Lo primero es editar (deberás haber abierto tu editor de textos, por ejemplo el bloc de notas, con permisos de administrador) el contenido del fichero MixerPages.json. 

Si los ficheros a procesar en tu equipo están en las siguientes carpetas:

- c:\temp\document        <- Carpeta donde se ubican los ficheros a procesar.
- c:\temp\document\impar  <- Carpeta donde se ubican los ficheros con las páginas pares a procesar.
- c:\temp\document\par    <- Carpeta donde se ubican los ficheros con las páginas impares a procesar.

El fichero MixerPages.json deberá tener el siguiente contenido:
```json
{
  "documentFolder": "c:\\temp\\document",
  "extension": ".pdf",
  "folderOdd": "impar",
  "sortOdd": "ASC",
  "folderEven": "par",
  "sortEven": "ASC",
  "unifyFile": "true"
}
```
Explicación del contenido del fichero de configuración:
```
  "documentFolder": Carpeta donde están los documentos a procesar.
  "extension":      Formato/extensión de los ficheros a procesar, puede ser cualquier formato, pero si no es .pdf el parámetro unifyFile deberá ser false
  "folderOdd":      Carpeta donde estarán las páginas impares del documento.
  "sortOdd":        Ordenación que deberá aplicarse a la páginas impares, puede usarse ASC o DESC. Normalmente las páginas impares se escanean ascendentemente por lo que debería elegirse ASC.
  "folderEven":     Carpeta donde estarán las páginas pares del documento.
  "sortEven":       Ordenación que deberá aplicarse a la páginas pares, puede usarse ASC o DESC. Normalmente las páginas pares se escanean descendentemente por lo que debería elegirse DESC.
  "unifyFile":      Indica si se desea generar un fichero en formato pdf con todas las páginas (true) o copiar los ficheros (false) de forma que ya se enceuntren mezclados y en orden. Los ficheros se grabarán en la carpeta documentFolder. Si el formato original es pdf, se podrá configurar true o false según el resultado deseado. Si el formato es cualquier otro (jpg por ejemplo) deberá elegirse false.
```
#### Ejecución

Una vez configurado correctamente el programa, se ejecutará con el siguiente programa: C:\Program Files (x86)\Apps4Every\MixerPages\MezcladorPaginas.exe

Si hubiera algún error, el programa finalizará indicando qué errores se han encontrado.

Si el programa finaliza correctamente (no se muestra mensaje de error), en la carpeta `documentFolder` aparecerá o bien un fichero denominado 'NewDocument.pdf' con los documentos procesados, o bien cada uno de los ficheros pdf pero en el orden original del documento escaneado.  Ello dependerá del valor (true o false) asignado a `unifyFile`
