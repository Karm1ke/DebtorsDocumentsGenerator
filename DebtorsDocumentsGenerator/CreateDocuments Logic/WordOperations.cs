using DBWorkLB;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using AdvancedFunctions;
using System.Drawing;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Excel.Log;
using Logging;
using System.CodeDom;

namespace DebtorsDocumentsGenerator
{
    internal class WordOperations
    {
        public static bool CreateDocumentsByTemplate(
            List<Debtor> debtors, Dictionary<string, string> templateFiles, Action<int, int> updateLbl)
        {
            try
            {
                var headerBody = "";
                var mainBody = "";
                Image subImg = null;
                string imageFilePath = "";

                if (templateFiles.ContainsKey("header"))
                {
                    if (templateFiles["header"].StartsWith("ftp:/"))
                    {
                        headerBody = FTP.DownloadFileToString(
                            templateFiles["header"],
                            new Action(delegate
                            {
                                FTPConnectionSettingsForm fcsf = new FTPConnectionSettingsForm();
                                fcsf.ShowDialog();
                            }));
                    }
                    else
                    {
                        if (File.Exists(templateFiles["header"]))
                        {
                            using (var fs = new FileStream(templateFiles["header"], FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (var reader = new StreamReader(fs))
                                {
                                    headerBody = reader.ReadToEnd();
                                }
                            }
                        }
                        else
                        {
                            InformOperations.setDisplayMessage($"Локально файл {headerBody} не обнаружен", icon: ToolTipIcon.Warning);
                        }
                    }
                }

                if (templateFiles["main"].StartsWith("ftp:/"))
                {
                    mainBody = FTP.DownloadFileToString(
                        templateFiles["main"],
                        new Action(delegate {
                            FTPConnectionSettingsForm fcsf = new FTPConnectionSettingsForm();
                            fcsf.ShowDialog();
                        })
                    );
                }
                else
                {
                    if (File.Exists(templateFiles["main"]))
                    {
                        if (templateFiles["doctype"] == "txt")
                        {
                            using (var fs = new FileStream(templateFiles["main"], FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (var reader = new StreamReader(fs))
                                {
                                    mainBody = reader.ReadToEnd();
                                }
                            }
                        }
                        else if (templateFiles["doctype"] == "docx")
                        {
                            mainBody = templateFiles["main"];
                        }
                    }
                    else
                    {
                        InformOperations.setDisplayMessage($"Локально файл {templateFiles["main"]} не обнаружен", icon: ToolTipIcon.Warning);
                    }
                }
                if (templateFiles.ContainsKey("sub"))
                {
                    if (templateFiles["sub"].StartsWith("ftp:/"))
                    {
                        subImg = FTP.DownloadImage(templateFiles["sub"]);
                    }
                    else
                    {
                        imageFilePath = templateFiles["sub"];
                    }
                }

                var sett = SaveFormedFilesSettingsManager.Load();
                var savePath = sett?.FilePath;
                if (string.IsNullOrEmpty(savePath))
                {
                    savePath = $"{Environment.CurrentDirectory}\\Results\\";
                }
                ConcurrentBag<string> filePaths = new ConcurrentBag<string>();
                int completedFiles = 0;
                Action<int> iteration = new Action<int>(i =>
                {
                    updateLbl(completedFiles, debtors.Count);
                    string finalFilePath = CreateDebtorDocument(
                        headerBody,
                        mainBody,
                        imageFilePath,
                        debtors[i],
                        savePath,
                        templateFiles["doctype"]);
                    if (!string.IsNullOrEmpty(finalFilePath))
                    {
                        filePaths.Add(finalFilePath);
                    }
                    completedFiles++;
                });
                //for (int i = 0; i < debtors.Count; i++)
                //{
                //}
                Parallel.For(0, debtors.Count, new ParallelOptions { MaxDegreeOfParallelism = 6 }, (i, state) =>
                {
                    iteration(i);
                });
                
                while(true)
                {
                    if (debtors.Count == filePaths.Count)
                    {
                        break;
                    }
                }
                if (filePaths.Count > 0)
                {
                    if (sett != null && sett.Archiving)
                    {
                        var fileNames = filePaths
                            .Select(f => new ArchivingFunctions.FileName()
                            {
                                full_filename = new FileInfo(f).FullName,
                                short_filename = new FileInfo(f).Name
                            })
                            .ToList();

                        var archFilePath = ArchivingFunctions.Operations.Pack2(fileNames, savePath);
                        var fInfo = new FileInfo(archFilePath);
                        foreach (var path in filePaths)
                        {
                            File.Delete(path);
                        }
                        Process.Start(fInfo.Directory.FullName);
                    }
                    else
                    {
                        if (filePaths.Count > 0)
                        {
                            var directory = new FileInfo(filePaths.First()).Directory.Parent.FullName;
                            Process.Start(directory);
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage(
                    $"Ошибка при формировании документов по данным должников: {ex.Message}");
                return false;
            }
        }


        private static string CreateDebtorDocument(
            string headerTemplateBody, 
            string mainTemplate, 
            string imageFilePath,
            Debtor debtor,
            string SaveDirectoryPath,
            string docType)
        {
            #region формирование шаблона
            if (!string.IsNullOrEmpty(SaveDirectoryPath))
            {
                Word.Application app = null;
                Word.Application app_just_created = null;
                string appVersion = "";

                string fileName = $"{Util.GetOnlyLetter(debtor.FIO.Replace(" ", "-")).Trim()}_{Util.DateTimeToString(DateTime.Now)}";
                string roomNumber = "";
                if (debtor.RoomNumber.Length <= 3)
                {
                    roomNumber = debtor.RoomNumber;
                }
                else
                {
                    roomNumber = Util.GetDigit(debtor.RoomNumber, usePunctuationMarks: true);
                }
                string directoryPath = $"{SaveDirectoryPath}\\{debtor.ResidencePlace} {debtor.Street} {debtor.HouseNumber}-{roomNumber}";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string curFilePath = $"{directoryPath}\\{fileName}.docx";

                try
                {
                    //запускаем ворд
                    app = new Word.Application();
                    appVersion = app.Version;

                    Object missing = Missing.Value;

                    if (docType == "txt")
                    {
                        //дoбавляем в новый документ содержимое шаблона
                        var document = app.Documents.Add(ref missing, ref missing, ref missing);

                        Paragraph p1 = document.Paragraphs.Add(ref missing);
                        p1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        p1.Range.Font.Name = "Times New Roman";
                        p1.Range.Font.Size = 12;
                        p1.Range.Text = headerTemplateBody;
                        p1.Range.InsertParagraphAfter();


                        Paragraph p2 = document.Paragraphs.Add(ref missing);
                        p2.FirstLineIndent = 5;
                        p2.Range.Font.Name = "Times New Roman";
                        p2.Range.Font.Size = 12;
                        p2.Range.Text = mainTemplate;
                        p2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        p2.Range.InsertParagraphAfter();

                        if (!string.IsNullOrEmpty(imageFilePath))
                        {
                            object o_CollapseEnd = WdCollapseDirection.wdCollapseEnd;
                            p2.Range.Collapse(o_CollapseEnd);
                            p2.Range.InlineShapes.AddPicture(imageFilePath, missing, missing, p2.Range);
                        }
                    }
                    else if (docType == "docx")
                    {
                        if (File.Exists(mainTemplate))
                        {
                            File.Copy(mainTemplate, curFilePath);
                            app.Documents.Open(curFilePath, ref missing, ref missing, ref missing);
                        }
                    }


                    Object wrap = WdFindWrap.wdFindContinue;
                    Object replace = WdReplace.wdReplaceAll;

                    string[] fnd = {
                        ParamValues.fio,
                        ParamValues.region,
                        ParamValues.city,
                        ParamValues.street,
                        ParamValues.sector,
                        ParamValues.house,
                        ParamValues.room,
                        ParamValues.accountNumber,
                        ParamValues.amount,
                        ParamValues.duty,
                        ParamValues.penny,
                        ParamValues.totalAmount,
                        ParamValues.amountPeriodStart,
                        ParamValues.amountPeriodEnd,
                        ParamValues.pennyPeriodStart,
                        ParamValues.pennyPeriodEnd
                };

                    var courtcase = debtor.Courtcases.FirstOrDefault();
                    string[] rpl = {
                        $"{debtor.Lastname} {debtor.Name} {debtor.Secondname}",
                        "",
                        debtor.ResidencePlace,
                        debtor.Street,
                        "",
                        debtor.HouseNumber.ToString(),
                        debtor.RoomNumber.ToString(),
                        debtor.AccountNumber,
                        courtcase?.DebtSum.ToString(),
                        courtcase?.RecoveredGovernmentDuty.ToString(),
                        courtcase?.RecoveredAmountPenny.ToString(),
                        courtcase?.TotalDebtSum.ToString(),
                        courtcase?.DebtPeriodStartDate != null ?
                            courtcase?.DebtPeriodStartDate.Value.ToShortDateString() : "",
                        courtcase?.DebtPeriodEndDate != null ?
                            courtcase?.DebtPeriodEndDate.Value.ToShortDateString() : "",
                        courtcase?.PennyPeriodStartDate != null ?
                            courtcase?.PennyPeriodStartDate.Value.ToShortDateString() : "",
                        courtcase?.PennyPeriodEndDate != null ?
                            courtcase?.PennyPeriodEndDate.Value.ToShortDateString() : "",
                };

                    //заменяем по меткам главные слова
                    for (int i = 0; i < fnd.Length; i++)
                    {
                        Find find = app.Selection.Find;
                        Object missin = Type.Missing;

                        object[] Parameters;
                        Parameters = new object[15];
                        Parameters[0] = fnd[i];
                        Parameters[1] = missin;
                        Parameters[2] = missin;
                        Parameters[3] = missin;
                        Parameters[4] = missin;
                        Parameters[5] = missin;
                        Parameters[6] = missin;
                        Parameters[7] = wrap;
                        Parameters[8] = missin;
                        Parameters[9] = rpl[i];
                        Parameters[10] = replace;
                        Parameters[11] = missin;
                        Parameters[12] = missin;
                        Parameters[13] = missin;
                        Parameters[14] = missin;
                        find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, Parameters);
                    }

                    object FileFormat = WdSaveFormat.wdFormatDocument;
                    object LockComments = false;
                    object AddToRecentFiles = true;
                    object ReadOnlyRecommended = false;
                    object EmbedTrueTypeFonts = false;
                    object SaveNativePictureFormat = true;
                    object SaveFormsData = true;
                    object SaveAsAOCELetter = false;
                    object Encoding = new UTF8Encoding();
                    object InsertLineBreaks = false;
                    object AllowSubstitutions = false;
                    object LineEnding = WdLineEndingType.wdCRLF;
                    object AddBiDiMarks = false;

                    var curDoc = app.Documents[1];

                    switch (app.Version)
                    {
                        case "7.0":
                        case "8.0":
                        case "9.0":
                        case "10.0":
                            curDoc.SaveAs2000(curFilePath);
                            break;
                        case "11.0":
                        case "12.0":
                            curDoc.SaveAs(
                                curFilePath, ref missing, ref LockComments,
                                ref missing, ref AddToRecentFiles, ref missing,
                                ref ReadOnlyRecommended, ref EmbedTrueTypeFonts,
                                ref SaveNativePictureFormat, ref missing,
                                ref SaveAsAOCELetter, ref missing, ref InsertLineBreaks,
                                ref AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
                            break;
                        case "14.0":
                        case "15.0":
                        case "16.0":
                        default:
                            curDoc.SaveAs(
                                curFilePath, ref missing, ref LockComments,
                                ref missing, ref AddToRecentFiles, ref missing,
                                ref ReadOnlyRecommended, ref EmbedTrueTypeFonts,
                                ref SaveNativePictureFormat, ref missing,
                                ref SaveAsAOCELetter, ref missing, ref InsertLineBreaks,
                                ref AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
                            break;
                    }
                    app.Quit(true);
                    return curFilePath;
                }
                catch (Exception ex)
                {
                    GC.Collect();
                    try { Marshal.FinalReleaseComObject(app); }
                    catch { }
                    try { Marshal.FinalReleaseComObject(app_just_created); }
                    catch { }
                    InformOperations.setDisplayMessage("Формирование шаблона: app.Version " + appVersion + "; \r\n" + ex.ToString());
                    Logger.Write("Формирование шаблона: app.Version " + appVersion + "; \r\n" + ex.ToString());
                    Logger.Write($"Путь к файлу: {curFilePath}");
                    return "";
                }
                #endregion
            }
            return "";
        }
    }
}
