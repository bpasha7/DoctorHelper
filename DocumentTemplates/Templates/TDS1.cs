using Entities;
using Entities.TDS;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace DocumentTemplates.Templates
{
    public class TDS1 : DocumentTemplate
    {
        private Microsoft.Office.Interop.Word.Application _wordDocument;
        private Microsoft.Office.Interop.Word.Document _document;
        private object _missing;
        private Client _client;

        public TDS1(DataTDS1 Data, Client NewClient, BackgroundWorker bw, string owner, string location, string device)
        {

            try
            {
                Owner = owner;
                Location = location;
                Device = device;
                _client = NewClient;
                #region start Word
                //Create an instance for word app
                _wordDocument = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //2013
                //_wordDocument.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                _wordDocument.Visible = false;

                _missing = System.Reflection.Missing.Value;

                //Create a new document
                _document = _wordDocument.Documents.Add(ref _missing, ref _missing, ref _missing, ref _missing);
                //Поля
                _document.PageSetup.RightMargin = 35;
                _document.PageSetup.LeftMargin = 35;
                _document.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                _wordDocument.ActiveWindow.Selection.ParagraphFormat.LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle;
                _wordDocument.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0.0F;
                bw.ReportProgress(30);
                #endregion
                #region Header
                //Add header into the document
                foreach (Microsoft.Office.Interop.Word.Section section in _document.Sections)
                {
                    //Get the header range and add the header details.
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                    headerRange.Font.Name = "Times New Roman";
                    //headerRange.Font.ColorIndexBi = 
                    headerRange.Font.Size = 14;
                    headerRange.Text = "КГБУЗ «КМКБ №20 им. И. С. Берзона»\n" +
                        "Дуплексное сканирование с ЦДК аорты и Аолчечных артерий";
                }

                _document.Content.SetRange(1, 1);
                #endregion
                Microsoft.Office.Interop.Word.Paragraph mainParagraph = _document.Content.Paragraphs.Add(ref _missing);
                mainParagraph.Range.Text = "";
                mainParagraph.Range.Font.Size = 12;
                mainParagraph.Range.Font.Name = "Times New Roman";
                mainParagraph.Range.InsertParagraphAfter();
                #region InfoTable
                Table infotable = _document.Tables.Add(mainParagraph.Range, 3, 2, ref _missing, ref _missing);
                infotable.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

                infotable.Cell(1, 1).Range.Text = $"Дата {DateTime.Now.ToShortDateString()}";
                infotable.Cell(1, 2).Range.Text = $"Аппарат: {Device}";
                infotable.Cell(2, 1).Range.Text = $"Ф.И.О. {NewClient.Name}";
                infotable.Cell(2, 2).Range.Text = $"Условия локации: {Location}";
                infotable.Cell(3, 1).Range.Text = NewClient.HistoryNumber.Trim().Length != 0 ? $"№ истории болезни {NewClient.HistoryNumber}\tВозраст {NewClient.Age}" : $"Возраст {NewClient.Age}";
                infotable.Cell(3, 2).Range.Text = $"Врач: {Owner}";

                foreach (Row row in infotable.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Range.Font.Size = 12;
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;// WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        if (cell.ColumnIndex == 2)
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;// WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                }
                #endregion
                mainParagraph.Range.InsertParagraphAfter();
                bw.ReportProgress(20);

                Microsoft.Office.Interop.Word.Paragraph par1 = _document.Content.Paragraphs.Add(ref _missing);
                par1.Range.Font.Size = 12;
                par1.Range.Font.Name = "Times New Roman";
                par1.Range.Bold = 1;
                par1.Range.Text = $" Аорта";
                par1.Range.InsertParagraphAfter();
                par1.Range.InsertParagraphAfter();
                par1.Range.Bold = 0;

                par1.Range.Text = $" {Data.Paragraphs[0]}";
                par1.Range.InsertParagraphAfter();
                par1.Range.InsertParagraphAfter();

                par1.Range.Text = $" {Data.Paragraphs[1]}";
                par1.Range.InsertParagraphAfter();
                par1.Range.InsertParagraphAfter();

                par1.Range.Text = $" {Data.Paragraphs[2]}";
                par1.Range.InsertParagraphAfter();
                par1.Range.InsertParagraphAfter();

                par1.Range.Text = $" {Data.Paragraphs[3]}";
                par1.Range.InsertParagraphAfter();
                par1.Range.InsertParagraphAfter();

                if (Data.Paragraphs[4] != "")
                {
                    par1.Range.Text = $" Дополнительные данные: {Data.Paragraphs[4]}";
                    par1.Range.InsertParagraphAfter();
                    par1.Range.InsertParagraphAfter();
                }

                bw.ReportProgress(40);

                #region 1stTable
                //Table header
                Microsoft.Office.Interop.Word.Paragraph titleTable = _document.Content.Paragraphs.Add(ref _missing);
                titleTable.Range.Font.Size = 12;
                titleTable.Range.Font.Name = "Times New Roman";
                titleTable.Range.Font.Bold = 1;
                // titleTable.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter; ;
                titleTable.Range.Text = "\t\tКоличественные характеристики кровотока в почечных аретриях";

                titleTable.Range.InsertParagraphAfter();
                titleTable.Range.InsertParagraphAfter();
                int Table1NumRows = 7;
                int Table1NumCols = 5;
                Table Table1 = _document.Tables.Add(mainParagraph.Range, Table1NumRows, Table1NumCols, ref _missing, ref _missing);

                Table1.Borders.Enable = 1;
                Table1.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;



                for (int i = 0; i < Data.TableColumns.Count; i++)
                {
                    Table1.Cell(1, i + 2).Range.Text = Data.TableColumns[i];
                }

                for (int i = 0; i < Data.Table.Count; i++)
                {
                    Table1.Cell(i + 2, 2).Range.Text = Data.Table[i].Name;
                    var criterias = Data.Table[i].GetVluesList();
                    for (int j = 0; j < Table1NumCols - 2; j++)
                    {
                        Table1.Cell(i + 2, j + 3).Range.Text = criterias[j];
                    }
                }
                foreach (Row row in Table1.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Range.Font.Size = 11;
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        cell.Height = 30;// WdRowHeightRule.wdRowHeightAtLeast;

                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            cell.Width = 120f;
                        }
                        else
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            cell.Width = 95f;
                        }
                    }
                }
                var cellLeft1 = Table1.Cell(2, 1);
                cellLeft1.Merge(Table1.Cell(3, 1));
                cellLeft1.Merge(Table1.Cell(4, 1));
                cellLeft1.Range.Text = "Правая ПА";
                cellLeft1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                var cellLeft2 = Table1.Cell(5, 1);
                cellLeft2.Merge(Table1.Cell(6, 1));
                cellLeft2.Merge(Table1.Cell(7, 1));
                cellLeft2.Range.Text = "Правая ПА";
                cellLeft2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                #endregion
                mainParagraph.Range.InsertParagraphAfter();
                bw.ReportProgress(60);

                if (Data.Paragraphs[5] != "")
                {
                    par1.Range.Text = $"{Data.Paragraphs[5]}";
                    par1.Range.InsertParagraphAfter();
                }


                if (Data.Paragraphs[6] != "")
                {
                    par1.Range.InsertParagraphAfter();
                    par1.Range.Text = $"Заключение: {Data.Paragraphs[6]}";
                    par1.Range.InsertParagraphAfter();
                }

                bw.ReportProgress(85);
                titleTable.Range.InsertParagraphAfter();

                bw.ReportProgress(90);

            }
            catch (Exception ex)
            {
                _wordDocument?.Quit(ref _missing, ref _missing, ref _missing);
                _wordDocument = null;
                // Compose a string that consists of three lines.
                string lines = ex.ToString();

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter($@"{Directory.GetCurrentDirectory()}\err.txt");
                file.WriteLine(lines);

                file.Close();
            }
        }

        private static void MergeNext(Table table, int curRow, int curCol, string caption)
        {
            var cellHeader = table.Cell(curRow, curCol);
            cellHeader.Merge(table.Cell(curRow, curCol + 1));
            cellHeader.Range.Text = caption;
            cellHeader.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            //cellHeader.Width = 80f;
        }

        public override void Dispose()
        {
            _wordDocument?.Quit(ref _missing, ref _missing, ref _missing);
            _wordDocument = null;
        }

        public override bool Print()
        {
#if DEBUG
            return true;
#endif
            try
            {
                _document.PrintOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            // throw new NotImplementedException();
        }

        public override bool SavePreview()
        {
            try
            {
                object fileType = (object)WdSaveFormat.wdFormatPDF;

                object filename1 = $@"{Directory.GetCurrentDirectory()}\Temp\temp.pdf";

                _document.SaveAs(ref filename1, ref fileType,
                  ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing,
                  ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override string Save()
        {
            try
            {
                //Save the document
                var dt = DateTime.Now.ToString(@"HH-mm-ss");
                var path = $@"\Data\{DateTime.Now.ToShortDateString()}\{_client.Name} ({dt}).docx";
                object filename = $@"{Directory.GetCurrentDirectory()}\{path}";
                //object filename = $@"{Directory.GetCurrentDirectory()}\Temp\temp.docx";
                _document.SaveAs(ref filename);
                return path;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                string lines = ex.ToString();

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter($@"{Directory.GetCurrentDirectory()}\err.txt");
                file.WriteLine(lines);

                file.Close();
                return null;
            }
            finally
            {
                _wordDocument?.Quit(ref _missing, ref _missing, ref _missing);
                _wordDocument = null;
            }
        }
    }
}
