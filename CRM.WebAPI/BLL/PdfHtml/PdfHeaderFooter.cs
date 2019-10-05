//using System;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

//namespace CRM.WebAPI.BLL.PdfHtml
//{
//    public class PrintHeaderFooter : PdfPageEventHelper
//    {
//        private PdfContentByte pdfContent;
//        private PdfTemplate pageNumberTemplate;
//        private BaseFont baseFont;
//        private BaseFont boldFont;
//        private DateTime printTime;
//        public string FooterTemplate { get; set; }
//        public string Title { get; set; }

//        public override void OnOpenDocument(PdfWriter writer, Document document)
//        {
//            printTime = DateTime.Now;
//            baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
//            boldFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
//            pdfContent = writer.DirectContent;
//            pageNumberTemplate = pdfContent.CreateTemplate(document.PageSize.Width , 50);
//        }

//        public override void OnStartPage(PdfWriter writer, Document document)
//        {
//            base.OnStartPage(writer, document);

//            var pageSize = document.PageSize;

//            if (!string.IsNullOrWhiteSpace(Title))
//            {
//                pdfContent.BeginText();
//                pdfContent.SetFontAndSize(baseFont, 11);
//                //pdfContent.SetRGBColorFill(0, 0, 0);
//                pdfContent.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
//                pdfContent.ShowText(Title);
//                pdfContent.EndText();
//            }
//        }

//        public override void OnEndPage(PdfWriter writer, Document document)
//        {
//            base.OnEndPage(writer, document);

//            int pageN = writer.PageNumber;
//            var text = string.Format(FooterTemplate , pageN);
//            float len = boldFont.GetWidthPoint(text, 8);

//            var pageSize = document.PageSize;
//            pdfContent = writer.DirectContent;
//          //  pdfContent.SetRGBColorFill(100, 100, 100);

//            pdfContent.BeginText();
//            pdfContent.SetFontAndSize(boldFont, 8);
//          //  pdfContent.SetTextMatrix((pageSize.Width -len) / 2, pageSize.GetBottom(30));
//            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, 40, pageSize.GetBottom(15), 0);
//          //  pdfContent.ShowText(text);
//            pdfContent.EndText();

//            pdfContent.AddTemplate(pageNumberTemplate, (pageSize.Width / 2) + len, pageSize.GetBottom(15));

//            pdfContent.BeginText();
//            pdfContent.SetFontAndSize(baseFont, 8);
//            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, printTime.ToString(), pageSize.GetRight(40), pageSize.GetBottom(15), 0);
//            pdfContent.EndText();
//        }

//        public override void OnCloseDocument(PdfWriter writer, Document document)
//        {
//            base.OnCloseDocument(writer, document);

//            pageNumberTemplate.BeginText();
//            pageNumberTemplate.SetFontAndSize(baseFont, 8);
//            pageNumberTemplate.SetTextMatrix(0, 0);
//           // pageNumberTemplate.ShowText(string.Empty + (writer.PageNumber - 1));
//            pageNumberTemplate.EndText();
//        }
//    }
//}
