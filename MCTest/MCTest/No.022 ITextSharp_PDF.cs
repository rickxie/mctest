using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MCTest
{
    public class ITextPdf: TestBase, IOutput
    {
       public void Main()
       {
           for (int i = 0; i < 200; i++)
           {
                var streamSN = new FileStream(@"D:\a\SN.pdf", FileMode.Open);
                var streamNJ = new FileStream(@"D:\a\NJ.pdf", FileMode.Open);
                var stream = new MemoryStream();
                PDFMergeManager mrg = new PDFMergeManager($"D:\\a\\{i}.pdf");
                mrg.MergeFile(streamSN);
                mrg.MergeFile(streamNJ);
                mrg.FinishedMerge();
            }

            //var streamSN = new FileStream(@"D:\a\SN.pdf", FileMode.Open);
//            mergePDFFiles(new[] { @"D:\a\SN.pdf", @"D:\a\NJ.pdf" }, @"D:\a\SN-NJ.pdf");
//            mergePDFFiles(new[] { @"D:\a\SN.pdf", @"D:\a\HQ.pdf" }, @"D:\a\SN-HQ.pdf");
//            mergePDFFiles(new[] { @"D:\a\SN.pdf", @"D:\a\TC.pdf" }, @"D:\a\SN-TC.pdf");
//            mergePDFFiles(new[] { @"D:\a\SN.pdf", @"D:\a\FD.pdf" }, @"D:\a\SN-FD.pdf");
        }

        private void mergePDFFiles(string[] fileList, string outMergeFile)
        {
            PdfReader reader;
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;
            for (int i = 0; i < fileList.Length; i++)
            {
                reader = new PdfReader(fileList[i]);
                int iPageNum = reader.NumberOfPages;
                for (int j = 1; j <= iPageNum; j++)
                {
                    document.NewPage();
                    newPage = writer.GetImportedPage(reader, j);
                    cb.AddTemplate(newPage, 0, 0);
                }
            }
            document.Close();
        }

    }
}
