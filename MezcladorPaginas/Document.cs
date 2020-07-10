using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;


//Revisar PDFSharp
namespace MezcladorPaginas
{
    class ConfigDocument
    {
        public string DocumentFolder
        {
            get;
            set;
        }
        public string Extension
        {
            get;
            set;
        }
        public string FolderOdd
        {
            get;
            set;
        }
        public string SortOdd
        {
            get;
            set;
        }
        public string FolderEven
        {
            get;
            set;
        }
        public string SortEven
        {
            get;
            set;
        }
        public Boolean UnifyFile
        {
            get;
            set;
        }
    }
    
    class Document
    {
        private string _documentFolder;
        private string _folderOdd;
        private string _sortOdd;
        private string _folderEven;
        private string _sortEven;
        private string _extension;
        private string[] paginasImpares;
        private string[] paginasPares;
        private Boolean _unifyFile = false;

        public Document(ConfigDocument configDocument)
        {
            this._documentFolder = configDocument.DocumentFolder;
            this._extension = configDocument.Extension;
            this._folderOdd = configDocument.FolderOdd;
            this._folderEven = configDocument.FolderEven;
            this._unifyFile = configDocument.UnifyFile;
            this._sortEven = configDocument.SortEven;
            this._sortOdd= configDocument.SortOdd;
        }

        public void initializePages()
        {
            _initializePages(this._folderOdd);
            _initializePages(this._folderEven);
        }

        private void _initializePages(string pageType)
        {
            string ruta = Path.Combine(this._documentFolder, pageType);

            List<string> pages = new List<string>();
            var listFiles = Directory.EnumerateFiles(ruta, String.Concat("*", this._extension));
            foreach (string currentFile in listFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(currentFile);
                pages.Add(fileName);
            }
            if (pageType == this._folderOdd)
            {
                this.paginasImpares= pages.ToArray();
            }
            else
            {
                this.paginasPares = pages.ToArray();
            }
        }

        public void SortPages(string pageType, string sortType)
        {
            if (pageType == this._folderOdd)
            {
                if (sortType.ToUpper() == "ASC")
                {
                    Array.Sort(paginasImpares);
                }
                else
                {
                    Array.Reverse(paginasImpares);
                }
            }
            else
            {
                if (sortType.ToUpper() == "ASC")
                {
                    Array.Sort(paginasPares);
                }
                else
                {
                    Array.Reverse(paginasPares);
                }
            }
        }

        public void NewDocument()
        {
            if (this._unifyFile)
            {
                CreateDocument();
            }
            else
            {
                CopyPages();
            }
        }
        private void CreateDocument()
        {
            int fileNumber = 0;
            string destFileName = "";
            string sourceFileName = "";
            string new_extension = "";

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            //configuration for best compression
            outputDocument.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression;
            outputDocument.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic;
            outputDocument.Options.EnableCcittCompressionForBilevelImages = true;
            outputDocument.Options.NoCompression = false;
            outputDocument.Options.CompressContentStreams = true;

            destFileName = Path.Combine(_documentFolder,
                                            String.Concat("NewDocument",
                                                          _extension));
            if (File.Exists(destFileName))
            {
                File.Delete(destFileName);
            }
            
            // Iterate files
            for (int i = 0; i < this.paginasImpares.Length; i++)
            {
                fileNumber++;
                new_extension = String.Concat(fileNumber.ToString("D2"), _extension);
                sourceFileName = String.Concat(Path.Combine(Path.Combine(_documentFolder, this._folderOdd), this.paginasImpares[i]), this._extension);
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(sourceFileName, PdfDocumentOpenMode.Import);
                Console.WriteLine("Processing document {0} of a total of {1}.", fileNumber, this.paginasImpares.Length + this.paginasPares.Length);
                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
                if (paginasPares.Length > i)
                {
                    fileNumber++;
                    sourceFileName = String.Concat(Path.Combine(Path.Combine(_documentFolder,
                                                                            this._folderEven),
                                                                this.paginasPares[i]),
                                                  this._extension);
                    inputDocument = PdfReader.Open(sourceFileName, PdfDocumentOpenMode.Import);
                    // Iterate pages
                    count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                    Console.WriteLine("Processing document {0} of a total of {1}.", fileNumber, this.paginasImpares.Length + this.paginasPares.Length);
                }
            }

            // Save the document...                
            outputDocument.Save(destFileName);
        }
        private void CopyPages()
        {
            int fileNumber = 0;
            string destFileName = "";
            string sourceFileName = "";
            string new_extension = "";
            for (int i = 0; i < this.paginasImpares.Length; i++)
            {
                fileNumber++;
                new_extension = String.Concat(fileNumber.ToString("D2"), this._extension);
                sourceFileName = String.Concat(Path.Combine(Path.Combine(this._documentFolder, this._folderOdd), this.paginasImpares[i]), this._extension);
                destFileName = Path.Combine(this._documentFolder , 
                                            String.Concat("Page_",
                                                          new_extension));

                Console.WriteLine("Processing document {0} of a total of {1}.", fileNumber, this.paginasImpares.Length + this.paginasPares.Length);
                File.Copy(sourceFileName, destFileName, true);

                if (this.paginasPares.Length > i)
                {
                    fileNumber++;
                    new_extension = String.Concat(fileNumber.ToString("D2"), this._extension);
                    destFileName = Path.Combine(this._documentFolder,
                                                String.Concat("Page_",
                                                               new_extension));
                    sourceFileName = String.Concat(Path.Combine(Path.Combine(this._documentFolder,
                                                                            this._folderEven),
                                                                this.paginasPares[i]),
                                                  this._extension);
                    Console.WriteLine("Processing document {0} of a total of {1}.", fileNumber, this.paginasImpares.Length + this.paginasPares.Length);
                    File.Copy(sourceFileName, destFileName, true);
                }
            }
            
        }
    }
}
