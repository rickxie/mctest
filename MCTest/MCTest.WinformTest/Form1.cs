using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace MCTest.WinformTest
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = openFileDialog1.ShowDialog();
            if (file == DialogResult.OK )
            {
                var destWidth = 872;
                var destHeight = 384;
                var fileName = openFileDialog1.FileName;
                System.Drawing.Image imgSource = Image.FromFile(fileName); ;
                System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
                int sW = destWidth, sH = destHeight;

                Bitmap outBmp = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.Black);

                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
                g.Dispose();

                // 以下代码为保存图片时，设置压缩质量
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;

                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;

                try
                {
                    //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                    ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo jpegICI = null;
                    for (int x = 0; x < arrayICI.Length; x++)
                    {
                        if (arrayICI[x].FormatDescription.Equals("JPEG"))
                        {
                            jpegICI = arrayICI[x];//设置JPEG编码
                            break;
                        }
                    }

                    if (jpegICI != null)
                    {
                        outBmp.Save(@"C:\\abc.jpg", jpegICI, encoderParams);
                    }
                    else
                    {
                        outBmp.Save(@"C:\\abc.jpg", thisFormat);
                    }

                }
                catch
                {
                }
                finally
                {
                    imgSource.Dispose();
                    outBmp.Dispose();
                }
            }

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.Visible = true; //托盘图标隐藏
            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();//隐藏本窗体
                this.notifyIcon1.Visible = true;//展示出notifyicon控件
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true; //弹出MainForm
                this.WindowState = FormWindowState.Normal; //还原窗体 
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) //关闭事件
        {
            DialogResult result;
            result = MessageBox.Show("确定退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {

                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
