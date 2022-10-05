using iTextSharp.text;
using iTextSharp.text.pdf;
using LayerAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Admin;
using ViewModel.Security;

namespace LayerBusiness.Home
{
    public interface IPdfService
    {
        Task<Response> GenerarPdf(int invoiceId);
    }
    public class PdfService : IPdfService
    {
        #region property
        public InvoiceContext Database { get; }
        #endregion
        public PdfService(InvoiceContext database)
        {
            Database = database;

        }
        public async Task<Response> GenerarPdf(int invoiceId)
        {

            var invoice = await Database.Invoices.Include
                        (x => x.Company)
                        .Include(x => x.Items)
                        .Where(x => x.InvoiceId == invoiceId)
                        .FirstOrDefaultAsync();

            if (invoice == null)
                return new Response { Success = false, Message = "Factura no encontrada" };



            string imagen = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "imagen.png");


            string pdfFinal = Path.Combine(Directory.GetCurrentDirectory(), "Resources", $"factura{invoice.InvoiceId}.pdf");
            
            if(File.Exists(pdfFinal))
                File.Delete(pdfFinal);
            
            int x = 830;
            int y = 0;
            int z = 690;


            var claveAcceso = GenerarClaveAcceso(DateTime.Now.ToString("ddMMyyyy"), "01", "2", invoice.InvoiceNumber, invoice.InvoiceNumber, "1", "0950695965001");

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            dynamic writer;

            Font _standardFont = new Font(Font.HELVETICA, 8, Font.NORMAL, BaseColor.Black);
            FileStream output = new FileStream(pdfFinal,
                FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            writer = PdfWriter.GetInstance(document, output);


            document.Open();
            var titleFont = FontFactory.GetFont("Arial", "18", Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", "14", Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", "12", Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", "10", Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", "12");

            Font fHeaderFont = FontFactory.GetFont("Arial", 8, BaseColor.White);
            var titulo1 = new Phrase("Factura", fHeaderFont);

            PdfContentByte rectangulo =
                default(PdfContentByte);

            rectangulo = writer.DirectContent;
            rectangulo.SetLineWidth(Convert.ToSingle(1.2));
            rectangulo.SetColorStroke(BaseColor.Black);
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            //logo
            Image oImagen;
            float coordenadaX = 15;
            float coordenadaY = 720;
            float percentage = 0.0f;
            oImagen = Image.GetInstance(imagen);
            oImagen.Alignment = Element.ALIGN_LEFT;


            oImagen.SetAbsolutePosition(coordenadaX, coordenadaY);
            percentage = 150 / oImagen.Width;
            oImagen.ScalePercent(percentage * 100);
            document.Add(oImagen);

            cb.SetFontAndSize(BaseFont.CreateFont(), 9);
            cb.SetTextMatrix(20, z + 15);
            cb.ShowText(invoice.Company.BusinessName);
            cb.SetTextMatrix(20, z + 5);
            //cb.ShowText("" + emp1["aemnombre"].ToString());
            cb.ShowText(invoice.Company.BusinessName);
            cb.SetFontAndSize(BaseFont.CreateFont(), 8);


            z = z - 20;
            cb.SetTextMatrix(20, z);
            cb.ShowText("Dir. Sucursal: ");
            cb.SetTextMatrix(20, z - 10);
            cb.ShowText(invoice.Company.Address);

            z = z - 20;
            cb.SetTextMatrix(20, z);
            cb.ShowText("Dir. Sucursal:");
            cb.SetTextMatrix(20, z - 10);
            cb.ShowText(invoice.Company.Address);

            int a = z;

            a = a - 50;
            cb.SetTextMatrix(20, a);
            cb.ShowText("OBLIGADO A LLEVAR CONTABILIDAD: SI");
            cb.RoundRectangle(15.0F, 590, 260.0F, 130.0F, 10.0F);
            cb.Stroke();

            cb.SetFontAndSize(BaseFont.CreateFont(), 9);
            x = x - 50;
            cb.SetTextMatrix(300, x);
            cb.ShowText("R.U.C: " + invoice.Company.Ruc);
            x = x - 20;
            cb.SetFontAndSize(BaseFont.CreateFont(), 12);
            cb.SetTextMatrix(300, x);
            cb.ShowText("FACTURA");
            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.SetFontAndSize(BaseFont.CreateFont(), 9);
            string factura = invoice.InvoiceNumber;
            cb.ShowText("No. " + invoice.InvoiceNumber);
            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.ShowText("NÚMERO DE AUTORIZACIÓN");
            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.ShowText(claveAcceso);
            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.ShowText("FECHA Y HORA DE AUTORIZACIÓN: " + DateTime.Now);


            string production = "2";
            string tipoEmision = "1";

            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.ShowText("AMBIENTE: " + production);

            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.ShowText("EMISION: " + tipoEmision);

            x = x - 20;
            cb.SetTextMatrix(300, x);
            cb.ShowText("CLAVE DE ACCESO: ");
            Barcode128 code39 = new Barcode128();
            code39.Code = claveAcceso;
            code39.StartStopText = false;
            code39.BarHeight = 20;
            Image imagen39 = code39.CreateImageWithBarcode(cb, null, null);
            imagen39.SetAbsolutePosition(300, x - 35);
            cb.AddImage(imagen39);
            cb.RoundRectangle(290.0F, 580.0F, 280.0F, 230.0F, 10.0F);

            //DATOS DEL CONTRIBUYENTE
            cb.SetFontAndSize(BaseFont.CreateFont(), 7);
            x = x - 60;
            cb.SetTextMatrix(20, x);
            cb.ShowText("Cliente: " + $"{invoice.ClientName}");
            cb.RoundRectangle(15.0F, x - 35, 555.0F, 50.0F, 10.0F);
            cb.Stroke();
            y = x;

            x = x - 10;
            cb.SetTextMatrix(20, x);
            cb.ShowText("Email: ");

            x = x - 15;
            cb.SetTextMatrix(20, x);
            cb.ShowText("Direccion: " + invoice.ClienteAddress);

            cb.SetTextMatrix(350, y);
            cb.ShowText("RUC/CI: " + invoice.ClientId);

            y = y - 15;
            cb.SetTextMatrix(350, y);
            cb.ShowText("Telefono: " + invoice.ClientePhone);

            //detalle de la factura
            cb.SetFontAndSize(BaseFont.CreateFont(), 8);
            x = x - 30;

            cb.SetTextMatrix(20, x);
            cb.ShowText("Cod. Principal");

            cb.SetTextMatrix(90, x);
            cb.ShowText("Cod. Auxiliar");

            cb.SetTextMatrix(160, x);
            cb.ShowText("Cant");

            cb.SetTextMatrix(250, x);
            cb.ShowText("Descripción");

            cb.SetTextMatrix(420, x);
            cb.ShowText("Precio Unit.");

            cb.SetTextMatrix(480, x);
            cb.ShowText("Tax");

            cb.SetTextMatrix(540, x);
            cb.ShowText("Total");

            //bucle con detalle de la factura;
            rectangulo.Rectangle(15.0F, x - 5, 555.0F, 15.0F);
            rectangulo.Stroke();

            cb.SetFontAndSize(BaseFont.CreateFont(), 7);
            decimal total = 0;
            decimal subTotal = 0M;
            decimal subTotalTax = 0M;
            decimal subTotalSinIva = 0M;
            foreach (var item in invoice.Items)
            {
                x = x - 15;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, item.InvoiceDetailId.ToString(), 20, x, 0);
                // total = Convert.ToDouble(detalles.FirstOrDefault().Precio);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Convert.ToDecimal(item.Amount).
                    ToString().Replace(",", "."), 170, x, 0);
                cb.SetTextMatrix(250, x);
                cb.ShowText(item.Description);
                //total = Convert.ToDouble(i);
                //precio unitario
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Convert.ToDecimal(item.UnitPrice).
                    ToString().Replace(",", "."), 450, x, 0);

              
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Convert.ToDecimal(item.Tax).
                    ToString().Replace(",", "."), 490, x, 0);
                total = Convert.ToDecimal((item.Amount * item.UnitPrice) - item.Tax);

                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Convert.ToDecimal(total).
                    ToString().Replace(",", "."), 560, x, 0);

                rectangulo.Rectangle(15.0F, x - 5, 555.0F, 15.0F);
                rectangulo.Stroke();
                if(item.Tax > 0)
                {
                    subTotal += item.Amount * item.UnitPrice;
                    decimal procenta =Math.Round(Convert.ToDecimal(item.Tax) / 100,3);
                    subTotalTax = subTotalTax +( subTotal * procenta);

                }
               else
                {
                    subTotalSinIva += item.Amount * item.UnitPrice;
                }


            }

            x -= 100;
            rectangulo.Rectangle(360.0F, x - 5, 210.0F, 15.0F);
            rectangulo.Stroke();
            cb.SetTextMatrix(365, x);
            cb.ShowText("SUBTOTAL 12.00%");
            total = Convert.ToDecimal(23);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, subTotal.ToString().Replace(",", "."), 560, x, 0);

            x = x - 15;
            cb.SetTextMatrix(365, x);
            y = x;
            cb.ShowText("SUBTOTAL 0.00%");
            rectangulo.Rectangle(360.0F, x - 5, 210.0F, 15.0F);
            rectangulo.Stroke();
            total = Convert.ToDecimal(0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, subTotalSinIva.ToString().Replace(",", "."), 560, x, 0);

            //basecero + baseiva
            x = x - 15;
            cb.SetTextMatrix(365, x);
            cb.ShowText("SUBTOTAL SIN IMPUESTOS");
            rectangulo.Rectangle(360.0F, x - 5, 210.0F, 15.0F);
            rectangulo.Stroke();
            total = subTotal + subTotalSinIva;

            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, total.ToString().Replace(",", "."), 560, x, 0);
            x = x - 15;
            cb.SetTextMatrix(365, x);
            cb.ShowText("TAX");
            rectangulo.Rectangle(360.0F, x - 5, 210.0F, 15.0F);
            rectangulo.Stroke();
            total = Convert.ToDecimal(0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, subTotalTax.ToString().Replace(",", "."), 560, x, 0);

            x = x - 15;
            cb.SetTextMatrix(365, x);
            cb.ShowText("VALOR TOTAL");
            rectangulo.Rectangle(360.0F, x - 5, 210.0F, 15.0F);
            rectangulo.Stroke();
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, (subTotal+subTotalSinIva+subTotalTax).ToString().Replace(",", "."), 560, x, 0);

            cb.EndText();
            document.Close();
            output.Close();

            string base64 = Security.ConvertBase64Ruta(pdfFinal);   

            return new Response { Success = true,Message= base64 };

        }


        public static string GenerarClaveAcceso(string fecha, string tipocomprobante, string ambiente, string seridocu, string secudocu, string tipoemision, string rucempresa)
        {
            string clave = "";

            clave = Strings.Trim(fecha) + Strings.Trim(tipocomprobante) + Strings.Trim(rucempresa) + Strings.Trim(ambiente) + Strings.Trim(seridocu) + Strings.Trim(secudocu) + "12345678" + "1";
            clave = clave + "090125451";
            return clave;
        }
    }
}
