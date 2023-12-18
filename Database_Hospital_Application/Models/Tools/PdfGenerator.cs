using System;
using System.IO;
using Database_Hospital_Application.Models.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace Database_Hospital_Application.Models.Tools
{
    public class PdfGenerator
    {
        public static void GeneratePdfForPatient(Patient patient)
        {
            string pdfName = $"Report_{patient.BirthNumber}_{patient.LastName} {patient.FirstName}.pdf";
            string content = "";
            // Cesta, kde bude uložen vygenerovaný PDF soubor
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), pdfName);

            // Vytvoření nového dokumentu
            Document document = new Document();

            // Inicializace PdfWriter pro zápis do souboru
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));

            // Otevření dokumentu pro editaci
            document.Open();

            document = EditDocument(document, patient); 

            // Přidání obsahu do PDF
            document.Add(new Paragraph(content));

            // Uzavření dokumentu
            document.Close();

            // Stažení PDF souboru
            DownloadPdf(pdfPath);
        }

        private static Document EditDocument(Document document, Patient patient)
        {
            Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 15f, Font.BOLD);
            Font title2Font = FontFactory.GetFont(FontFactory.HELVETICA, 13f, Font.BOLD);

            string generatedDate = "Datum vygenerování: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            string titleText = "Úplný výpis o pacientovi";
            string personalDataText = "Osobní údaje";
            string nameLabelText = "Jméno a příjmení: ";
            string birthNumberLabelText = "Rodné číslo: ";


            Paragraph dateParagraph = new Paragraph(generatedDate, textFont);
            dateParagraph.Alignment = Element.ALIGN_RIGHT;
            document.Add(dateParagraph);

            Paragraph titleParagraph = new Paragraph(titleText, titleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(titleParagraph);

            Paragraph titlePersonalDataParagraph = new Paragraph(personalDataText, title2Font);
            titlePersonalDataParagraph.Alignment = Element.ALIGN_LEFT;
            document.Add(titlePersonalDataParagraph);

            // Jméno a příjmení
            Chunk boldNameLabel = new Chunk(nameLabelText, FontFactory.GetFont(FontFactory.HELVETICA, 10f, Font.BOLD));
            Chunk patientName = new Chunk($"{patient.FirstName} {patient.LastName}", textFont);
            Paragraph nameParagraph = new Paragraph();
            nameParagraph.Add(boldNameLabel);
            nameParagraph.Add(patientName);
            document.Add(nameParagraph);

            // Rodné číslo
            Chunk boldBirthNumberLabel = new Chunk(birthNumberLabelText, FontFactory.GetFont(FontFactory.HELVETICA, 10f, Font.BOLD));
            Chunk patientBirthNumber = new Chunk(patient.BirthNumber, textFont);
            Paragraph birthNumberParagraph = new Paragraph();
            birthNumberParagraph.Add(boldBirthNumberLabel);
            birthNumberParagraph.Add(patientBirthNumber);
            document.Add(birthNumberParagraph);


            return document;
        }

        private static void DownloadPdf(string filePath)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = Path.GetFileName(filePath);

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                File.Copy(filePath, saveFileDialog.FileName, true);
            }
        }
    }
}
