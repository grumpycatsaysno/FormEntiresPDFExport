using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Data.Utilities.Exporters;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Services;

namespace PdfExporter
{
    /// <summary>
    /// Custom class for exporting form responses to pdf files
    /// </summary>
    public class PdfExporter : DataItemExporterBase
    {
        private string pdfName;
        private bool overridePdfName;
        private bool overrideItemsType;
        private IEnumerable<IDataItem> items;

        public override string ExportFormatMimeType
        {
            get
            {
                return "application/pdf";
            }
        }

        public override string FileExtension
        {
            get
            {
                return ".pdf";
            }
        }

        private void SetPdfName(Type itemsType)
        {
            if (this.overridePdfName)
            {
                this.pdfName = itemsType.Name;
            }
        }

        public PdfExporter()
        {
            this.items = new List<IDataItem>();
        }

        public override void ExportToStream(System.IO.Stream streamToExportTo, IEnumerable<Telerik.Sitefinity.Model.IDataItem> itemsToExport, System.Text.Encoding encoding)
        {
            if (streamToExportTo == null)
            {
                throw new ArgumentNullException("streamToExportTo");
            }
            if (itemsToExport == null)
            {
                throw new ArgumentNullException("itemsToExport");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            // Sets the document to A4 size and rotates it so that the orientation of the page is Landscape.
            using (Document pdfDocument = new Document(PageSize.A4.Rotate(), 0, 0, 15, 5))
            {
                //// Gets the instance of the document created and writes it to the output stream of the Response object.
                PdfWriter.GetInstance(pdfDocument, streamToExportTo);

                pdfDocument.Open();

                pdfDocument.AddTitle("Form entries");

                PropertyDescriptorCollection itemProperties = null;
                IEnumerator<IDataItem> enumerator = itemsToExport.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException("Can't export empty collection");
                }
                IDataItem current = enumerator.Current;
                itemProperties = this.GetItemProperties(current);

                int count = 1;
                this.AppendDataItemContent(pdfDocument, current, itemProperties, count);
                while (enumerator.MoveNext())
                {
                    count++;
                    pdfDocument.NewPage();
                    this.AppendDataItemContent(pdfDocument, enumerator.Current, itemProperties, count);
                }

                pdfDocument.Close();
            }
        }

        private void AppendDataItemContent(Document pdfDocument, IDataItem dataItem, PropertyDescriptorCollection dataItemPropertyDescriptors, int count)
        {
            Paragraph para = new Paragraph("Form entry " + count, new Font(Font.FontFamily.HELVETICA, 18));

            para.Alignment = Element.ALIGN_CENTER;

            pdfDocument.Add(para);
            pdfDocument.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(2);

            foreach (PropertyDescriptor dataItemPropertyDescriptor in dataItemPropertyDescriptors)
            {
                object value = dataItemPropertyDescriptor.GetValue(dataItem);
                if (value != null)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataItemPropertyDescriptor.Name, new Font(Font.FontFamily.HELVETICA, 13, Font.BOLD)));

                    table.AddCell(cell);

                    table.AddCell(this.GetValueAsString(value));
                }               
            }

            // Adds the table to the document.
            pdfDocument.Add(table);
        }

        protected virtual string GetValueAsString(object value)
        {
            string str = null;
            if (!(value is string))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(value);
                str = (!converter.CanConvertTo(typeof(string)) ? value.ToString() : (string)converter.ConvertTo(value, typeof(string)));
            }
            else
            {
                str = (string)value;
            }
            return str;
        }

        private string GetItemsTypeFullName(Type itemsType)
        {
            if (this.overrideItemsType)
            {
                return itemsType.FullName;
            }
            return SystemManager.CurrentHttpContext.Items["ExportingItemsTypeName"] as string;
        }

        private void CheckContextVariables()
        {
            this.overrideItemsType = string.IsNullOrEmpty(SystemManager.CurrentHttpContext.Items["ExportingItemsTypeName"] as string);
            this.overridePdfName = string.IsNullOrEmpty(SystemManager.CurrentHttpContext.Items["PdfName"] as string);
        }
    }
}