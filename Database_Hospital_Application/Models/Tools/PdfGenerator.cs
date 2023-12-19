using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using IronPdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Tools
{
    public class PdfGenerator
    {
        public static async void GenerateAndDownloadPdfForPatient(Patient patient)
        {
            string pdfName = $"Report_{patient.BirthNumber}_{patient.LastName} {patient.FirstName}.pdf";
            StringBuilder contentBuilder = new StringBuilder();

            contentBuilder.Append($@"
        <html lang=""cs"">
        <head>
            <meta charset=""UTF-8"">
            <title>Report</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 20px;
                }}
                h1 {{
                    text-align: center;
                    color: #333;
                }}
                h2 {{
                    font-size: 15px;
                    font-weight: bold;
                }}
                p {{
                    font-size: 10px;
                }}
                .image-text-container {{display: flex;
                    align-items: center;
                }}

                .side-image {{width: 30px;
                    height: 30px;
                    margin-right: 10px;
                }}

                .side-text {{margin: 0;
                }}

            </style>
        </head>
        <body>
            <div class=""image-text-container"">
                <img src=""https://eduroam.upce.cz/favicon/ms-icon-310x310.png"" alt=""Obrázek"" class=""side-image"">
                <p class=""side-text"" style=""font-size: 20px; color: red; font-weight: bold;"">Nemocnice UPCE</p>
            </div>

            <div style=""position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: -1; opacity: 0.13;"">
             <img src=""https://eduroam.upce.cz/favicon/ms-icon-310x310.png"" alt=""Logo"" style=""max-width: 100%; height: auto;"" />
            </div>
    ");
            contentBuilder.AppendLine($"<p style=\"text-align: right;\">Datum vygenerování: {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}</p>");
            contentBuilder.AppendLine($"<h1>Report o pacientovi {patient.FirstName} {patient.LastName}</h1>");

            // Přidání osobních údajů pacienta
            contentBuilder.AppendLine("<h2>Osobní údaje</h2>");
            contentBuilder.AppendLine($"<p><strong>Jméno a příjmení:</strong> {patient.FirstName} {patient.LastName}</p>");
            contentBuilder.AppendLine($"<p><strong>Rodné číslo:</strong> {patient.BirthNumber}</p>");
            contentBuilder.AppendLine($"<p><strong>Pohlaví:</strong> {SexEnumParser.GetStringFromEnumCzech(patient.Sex).ToLower()}</p>");
            contentBuilder.AppendLine($"<p><strong>Adresa:</strong> {patient.Address}</p>");
            contentBuilder.AppendLine($"<p><strong>Zdravotní pojišťovna:</strong> {patient.HealthInsurance}</p>");
            contentBuilder.AppendLine($"<p><strong>Kontaktní údaje:</strong> {patient.Contact}</p>");
            contentBuilder.AppendLine($"<p><strong>Kuřák:</strong> {(patient.HealthInsurance.IsSmoker ? "Ano" : "Ne")}</p>");
            contentBuilder.AppendLine($"<p><strong>Alergik:</strong> {(patient.HealthInsurance.IsAllergic ? "Ano" : "Ne")}</p>");

            // Přidání osobní anamnézy
            contentBuilder.AppendLine("<h2>Osobní anamnéza</h2>");
            ObservableCollection<PersonalMedicalHistory> medicalHistories = await LoadMedicalHistoryAsync(patient);
            foreach (var history in medicalHistories)
            {
                contentBuilder.AppendLine($"<p>- {history.Description}</p>");
            }

            // Přidání provedených zákroků
            contentBuilder.AppendLine("<h2>Provedené zákroky</h2>");
            ObservableCollection<PerformedProcedure> performedProcedures = await LoadPerformedProcedures(patient);
            foreach (var procedure in performedProcedures)
            {
                contentBuilder.AppendLine($"<p>- {procedure.ToString()}</p>");
            }

            // Přidání nemocí a léků
            contentBuilder.AppendLine("<h2>Nemoci a léky</h2>");
            ObservableCollection<DataActualIllness> actualIllnesses = await LoadActualIllnesses(patient);
            foreach (var illness in actualIllnesses)
            {
                contentBuilder.AppendLine($"<p>- {illness.ToString()}</p>");
            }

            // Přidání informací o hospitalizaci
            contentBuilder.AppendLine("<h2>Hospitalizace</h2>");
            ObservableCollection<Hospitalization> hospitalizations = await LoadHospitalizations(patient);
            foreach (var hospitalization in hospitalizations)
            {
                contentBuilder.AppendLine($"<p>- {hospitalization.ToString()}</p>");
            }

            // Závěr HTML obsahu
            contentBuilder.Append(@"
        </body>
        </html>
    ");

            string content = contentBuilder.ToString();

            //PdfDocument pdf = new ChromePdfRenderer().RenderHtmlAsPdf(content);

            PdfDocument pdf = null;
            try
            {
                // Zde probíhá vykreslování PDF
                pdf = await Task.Run(() => new ChromePdfRenderer().RenderHtmlAsPdf(content));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při generování PDF: " + ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            // Získání cesty k adresáři "Downloads"
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

            // Uložení vygenerovaného PDF do složky "Downloads"
            string pdfFilePath = Path.Combine(downloadsPath, pdfName);
            if(pdf!= null)
            pdf.SaveAs(pdfFilePath);

            // Otevření vygenerovaného PDF v externím PDF prohlížeči

            Process.Start(new ProcessStartInfo
            {
                FileName = pdfFilePath,
                UseShellExecute = true // Použití výchozího programu pro otevření souboru
            });
        }

        private static async Task<ObservableCollection<PersonalMedicalHistory>> LoadMedicalHistoryAsync(Patient CurrentPatient)
        {
            PersonalMedicalHistoriesRepo repo = new PersonalMedicalHistoriesRepo();
            return await repo.GetPersonalMedicalHistoryByPatientIdAsync(CurrentPatient.Id);
        }

        private static async Task<ObservableCollection<PerformedProcedure>> LoadPerformedProcedures(Patient CurrentPatient)
        {
            PerformedProceduresRepo repo = new PerformedProceduresRepo();
            return await repo.GetAllPerformedProceduresAsync(CurrentPatient.Id);
        }

        private static async Task<ObservableCollection<DataActualIllness>> LoadActualIllnesses(Patient CurrentPatient)
        {
            PatientRepo repo = new PatientRepo();
            return await repo.GetActualIllnessByPatientIdAsync(CurrentPatient.Id);
        }

        private static async Task<ObservableCollection<Hospitalization>> LoadHospitalizations(Patient CurrentPatient)
        {
            HospitalizationRepo repo = new HospitalizationRepo();
            return await repo.GetAllHospitalizationsAsync(CurrentPatient.Id);
        }
    }
}
