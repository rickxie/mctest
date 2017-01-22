using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MCTest
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    /// <summary>
    /// pdf文件合并处理类
    /// </summary>
    public class PDFMergeManager
    {
        private PdfWriter pw;
        private PdfReader reader;
        private Document document;
        private PdfContentByte cb;
        private PdfImportedPage newPage;

        private bool ShowQuestion(string str)
        {
            return true;
        }
        /// <summary>
        /// 通过输出文件来构建合并管理，合并到新增文件中,合并完成后调用FinishedMerge方法
        /// </summary>
        /// <param name="sOutFiles"></param>
        public PDFMergeManager(string sOutFiles)
        {
            document = new Document();
            if (File.Exists(sOutFiles) && !ShowQuestion("对应的文件已经存在，文件目录:/r/n " + sOutFiles + "/r/n是否覆盖该文件？"))
            {
                document = null;
                throw new IOException("用户取消操作");
            }
            pw = PdfWriter.GetInstance(document, new FileStream(sOutFiles, FileMode.Create));
            document.Open();
            cb = pw.DirectContent;
        }
        /// <summary>
        /// 通过文件流来合并文件，合并到当前的可写流中，合并完成后调用FinishedMerge方法
        /// </summary>
        /// <param name="sm"></param>
        public PDFMergeManager(Stream sm)
        {
            document = new Document();
            pw = PdfWriter.GetInstance(document, sm);
            document.Open();
            cb = pw.DirectContent;
        }
        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="sFiles">需要合并的文件路径名称</param>
        /// <returns></returns>
        public bool MergeFile(string sFiles)
        {
            reader = new PdfReader(sFiles);
            {
                Execute();
            } 
            return true;
        }
        /// <summary>
        /// 通过字节数据合并文件
        /// </summary>
        /// <param name="pdfIn">PDF字节数据</param>
        /// <returns></returns>
        public bool MergeFile(byte[] pdfIn)
        {
            reader = new PdfReader(pdfIn);
            {
                Execute();
            } 
            return true;
        }
        /// <summary>
        /// 通过PDF文件流合并文件
        /// </summary>
        /// <param name="pdfStream">PDF文件流</param>
        /// <returns></returns>
        public bool MergeFile(Stream pdfStream)
        {
            reader = new PdfReader(pdfStream);
            {
                Execute();
            } 
            return true;
        }

        /// <summary>
        /// 通过网络地址来合并文件
        /// </summary>
        /// <param name="pdfUrl">需要合并的PDF的网络路径</param>
        /// <returns></returns>
        public bool MergeFile(Uri pdfUrl)
        {
            reader = new PdfReader(pdfUrl);
            {
               Execute();
            } 
            return true;
        }
        /// <summary>
        /// 完成合并
        /// </summary>
        public void FinishedMerge()
        {
            try
            {
                //if (reader != null)
                //{
                //    reader.Close();
                //}
                //if (pw != null)
                //{
                //    pw.Flush();
                //    pw.Close();
                //}
                if (document.IsOpen())
                {
                    document.Close();
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void Execute()
        {
            int iPageNum = reader.NumberOfPages;
            for (int j = 1; j <= iPageNum; j++)
            {
                document.NewPage();
                newPage = pw.GetImportedPage(reader, j);
                cb.AddTemplate(newPage, 0, 0);
            }
        }
    }
}
