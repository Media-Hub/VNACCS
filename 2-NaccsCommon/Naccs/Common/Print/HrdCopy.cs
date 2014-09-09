namespace Naccs.Common.Print
{
    using Naccs.Common;
    using Naccs.Common.Properties;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class HrdCopy
    {
        private Bitmap BmpImage;
        private System.Drawing.Printing.PrinterSettings mPrinterSettings;

        public Bitmap Capture(Rectangle rect)
        {
            Bitmap bitmap2;
            try
            {
                Bitmap image = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
                }
                bitmap2 = image;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
            return bitmap2;
        }

        public Bitmap Capture(object sender)
        {
            Bitmap bitmap2;
            try
            {
                Form form = (Form) sender;
                Bitmap bitmap = new Bitmap(form.Width, form.Height);
                form.DrawToBitmap(bitmap, new Rectangle(0, 0, form.Width, form.Height));
                bitmap2 = bitmap;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
            return bitmap2;
        }

        public void Print(Bitmap bitmapimg, bool previewFlg)
        {
            this.Print(bitmapimg, null, previewFlg);
        }

        public void Print(Bitmap bitmapimg, object sender, bool previewFlg)
        {
            try
            {
                using (PrintDocument document = new PrintDocument())
                {
                    if (!document.PrinterSettings.IsValid)
                    {
                        throw new NaccsException(MessageKind.Error, 0x25f, Resources.ResourceManager.GetString("COM15"));
                    }
                    this.BmpImage = bitmapimg;
                    document.PrintController = new StandardPrintController();
                    document.PrintPage += new PrintPageEventHandler(this.prtDoc_PrintPage);
                    document.BeginPrint += new PrintEventHandler(this.prtDoc_BeginPrint);
                    if (previewFlg)
                    {
                        using (PrintPreviewDialog dialog = new PrintPreviewDialog())
                        {
                            dialog.Document = document;
                            dialog.Width = 800;
                            dialog.Height = 600;
                            dialog.PrintPreviewControl.Zoom = 0.75;
                            dialog.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                            dialog.ShowInTaskbar = true;
                            dialog.Shown += new EventHandler(this.pvActivateEvent);
                            dialog.Paint += new PaintEventHandler(this.prtPrevDlg_Paint);
                            dialog.MouseWheel += new MouseEventHandler(this.prtPrevDlg_MouseWheel);
                            dialog.ShowDialog((Form) sender);
                            return;
                        }
                    }
                    document.Print();
                }
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
        }

        private void prtDoc_BeginPrint(object sender, PrintEventArgs ev)
        {
            if (ev.PrintAction != PrintAction.PrintToPreview)
            {
                using (PrintDialog dialog = new PrintDialog())
                {
                    if (IntPtr.Size == 8)
                    {
                        dialog.UseEXDialog = true;
                    }
                    PrintDocument document = (PrintDocument) sender;
                    dialog.PrinterSettings = this.mPrinterSettings;
                    foreach (PaperSize size in dialog.PrinterSettings.PaperSizes)
                    {
                        if (size.Kind == PaperKind.A4)
                        {
                            dialog.PrinterSettings.DefaultPageSettings.PaperSize = size;
                            break;
                        }
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        document.PrinterSettings = dialog.PrinterSettings;
                    }
                    else
                    {
                        ev.Cancel = true;
                    }
                }
            }
        }

        private void prtDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image bmpImage = this.BmpImage;
            SizeF ef = new SizeF((float) bmpImage.Width, (float) bmpImage.Height);
            float num = Math.Min((float) (((float) e.MarginBounds.Width) / ef.Width), (float) (((float) e.MarginBounds.Height) / ef.Height));
            ef.Width *= num;
            ef.Height *= num;
            e.Graphics.DrawImage(bmpImage, (float) e.MarginBounds.Left, (float) e.MarginBounds.Top, ef.Width, ef.Height);
        }

        private void prtPrevDlg_MouseWheel(object sender, MouseEventArgs e)
        {
            PrintPreviewControl printPreviewControl = ((PrintPreviewDialog) sender).PrintPreviewControl;
            if (e.Delta < 0)
            {
                SendMessage(printPreviewControl.Handle, 0x115, 3, 0);
            }
            else
            {
                SendMessage(printPreviewControl.Handle, 0x115, 2, 0);
            }
        }

        private void prtPrevDlg_Paint(object sender, PaintEventArgs e)
        {
            ((Form) sender).Activate();
        }

        private void pvActivateEvent(object sender, EventArgs e)
        {
            ((Form) sender).ShowIcon = false;
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public System.Drawing.Printing.PrinterSettings PrinterSettings
        {
            set
            {
                this.mPrinterSettings = value;
            }
        }
    }
}

