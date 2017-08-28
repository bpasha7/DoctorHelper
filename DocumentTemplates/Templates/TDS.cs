using Entities;
using Entities.TDS;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentTemplates.Templates
{
    /// <summary>
    /// Транскраниальное дуплексное сканирование
    /// </summary>
    public class TDS : DocumentTemplate, IDocumentTemplate, IDisposable
    {
        private Microsoft.Office.Interop.Word.Application _wordDocument;
        private Microsoft.Office.Interop.Word.Document _document;
        private object _missing;
        private Client _client;
        public TDS(DataTDS Data, Client NewClient, BackgroundWorker bw)
        {
            try
            {
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
                        "Дуплексное сканирование с ЦДК брахиоцефальных сосудов\n" +
                        "Транскраниальное дуплексное сканирование";
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
                infotable.Cell(1, 2).Range.Text = $"Аппарат Новый аппарат";
                infotable.Cell(2, 1).Range.Text = $"Ф.И.О. {NewClient.Name}";
                infotable.Cell(2, 2).Range.Text = $"Условия локации: Условия";
                infotable.Cell(3, 1).Range.Text = $"№ истории болезни {NewClient.HistoryNumber}\tВозраст {NewClient.Age}";
                infotable.Cell(3, 2).Range.Text = $"Врач: Безрук А. П.";

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
                bw.ReportProgress(40);
                #region 1stTable
                int Table1NumRows = 6;
                int Table1NumCols = 9;
                Table Table1 = _document.Tables.Add(mainParagraph.Range, Table1NumRows, Table1NumCols, ref _missing, ref _missing);

                Table1.Borders.Enable = 1;
                Table1.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

                for (int i = 0; i < Data.Table1Columns.Count; i++)
                {
                    Table1.Cell(1, i + 1).Range.Text = Data.Table1Columns[i];
                }

                for (int i = 0; i < Data.Table1.Count; i++)
                {
                    Table1.Cell(i + 2, 1).Range.Text = Data.Table1[i].Name;
                    var criterias = Data.Table1[i].GetVluesList();
                    for (int j = 0; j < Table1NumCols - 1; j++)
                    {
                        Table1.Cell(i + 2, j + 2).Range.Text = criterias[j];
                    }
                }
                foreach (Row row in Table1.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Range.Font.Size = 11;
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            cell.Width = 95f;
                        }
                        else
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            cell.Width = 52f;
                        }
                    }
                }
                #endregion
                mainParagraph.Range.InsertParagraphAfter();
                bw.ReportProgress(60);
                #region 2nd Table
                int Table2NumRows = 4;
                int Table2NumCols = 5;
                Table Table2 = _document.Tables.Add(mainParagraph.Range, Table2NumRows, Table2NumCols, ref _missing, ref _missing);

                Table2.Borders.Enable = 1;
                Table2.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

                for (int i = 0; i < Table2NumRows; i++)
                {
                    Table2.Cell(i + 1, 1).Range.Text = Data.Table2[i].Name;
                    var criterias = Data.Table2[i].GetVluesList();
                    for (int j = 0; j < Table2NumCols - 1; j++)
                    {
                        Table2.Cell(i + 1, j + 2).Range.Text = criterias[j];
                    }
                }
                foreach (Row row in Table2.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Range.Font.Size = 11;
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        cell.Width = 70f;
                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        }
                        else
                        {
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                }
                #endregion
                mainParagraph.Range.InsertParagraphAfter();
                bw.ReportProgress(70);
                //Table header
                Microsoft.Office.Interop.Word.Paragraph titleTable = _document.Content.Paragraphs.Add(ref _missing);
                titleTable.Range.Font.Size = 12;
                titleTable.Range.Font.Name = "Times New Roman";
                titleTable.Range.Font.Bold = 1;
                // titleTable.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter; ;
                titleTable.Range.Text = "\t\t\t\t\tТранскраниальное сканирование";

                titleTable.Range.InsertParagraphAfter();
                titleTable.Range.InsertParagraphAfter();
                #region 3rdTable
                int Table3NumRows = 4;
                int Table3NumCols = 12;
                Table Table3 = _document.Tables.Add(titleTable.Range, Table3NumRows, Table3NumCols, ref _missing, ref _missing);

                Table3.Range.Bold = 0;
                Table3.Range.Font.Size = 11;
                Table3.Borders.Enable = 1;
                Table3.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

                foreach (Row row in Table3.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Range.Font.Size = 12;
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        if (cell.ColumnIndex == 1)
                        {
                            cell.Width = 72f;
                            //cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;// WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        else
                        {
                            cell.Width = 40f;
                        }
                    }
                }

                #region Table3 CustomHeader
                var cellHeader1 = Table3.Cell(1, 1);
                cellHeader1.Merge(Table3.Cell(2, 1));
                cellHeader1.Range.Text = Data.Table3Columns[0];
                cellHeader1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                //cellHeader1.Width = 72f;
                cellHeader1.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //#if DEBUG
                //                _wordDocument.Visible = true ;
                //#endif
                var cellHeader6 = Table3.Cell(1, 10);
                //cellHeader6.Width = 40f;

                cellHeader6.Merge(Table3.Cell(2, 10));
                cellHeader6.Range.Text = Data.Table3Columns[5];
                //Table3.Cell(2, i).Width = 40f;
                //Table3.Cell(2, i).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                cellHeader6.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                //cellHeader6.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                for (int i = Table3NumCols; i > 2; i -= 2)
                {
                    if (i == 10)
                    {
                        i++;
                        continue;
                    }
                    Table3.Cell(2, i).Range.Text = "S";
                    //Table3.Cell(2, i).Width = 40f;
                    Table3.Cell(2, i).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    Table3.Cell(2, i - 1).Range.Text = "d";
                    //Table3.Cell(2, i - 1).Width = 40f;
                    Table3.Cell(2, i - 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                }



                MergeNext(Table3, 1, 2, Data.Table3Columns[1]);

                MergeNext(Table3, 1, 3, Data.Table3Columns[2]);

                MergeNext(Table3, 1, 4, Data.Table3Columns[3]);

                MergeNext(Table3, 1, 5, Data.Table3Columns[4]);

                MergeNext(Table3, 1, 7, Data.Table3Columns[6]);

                #endregion

                for (int i = 0; i < Data.Table3.Count; i++)
                {
                    Table3.Cell(i + 3, 1).Range.Text = Data.Table3[i].Name;
                    Table3.Cell(i + 3, 1).Width = 72f;
                    var criterias = Data.Table3[i].GetVluesList();
                    for (int j = 0; j < Table3NumCols - 1; j++)
                    {
                        Table3.Cell(i + 3, j + 2).Range.Text = criterias[j];
                        Table3.Cell(i + 3, j + 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        //if (j + 2 == 10)
                        //    Table3.Cell(i + 3, j + 2).Width = 45f;
                        //else
                        Table3.Cell(i + 3, j + 2).Width = 40f;
                    }
                }
                #endregion
                bw.ReportProgress(85);
                titleTable.Range.InsertParagraphAfter();
                Microsoft.Office.Interop.Word.Paragraph par1 = _document.Content.Paragraphs.Add(ref _missing);
                par1.Range.Font.Size = 12;
                par1.Range.Font.Name = "Times New Roman";
                if (Data.P1.Length != 0)
                {
                    par1.Range.Text = $"Атеросклеротическая бляшка (АБС) {Data.P1}";
                    par1.Range.InsertParagraphAfter();
                    par1.Range.InsertParagraphAfter();
                }
                if (Data.P2.Length != 0)
                {
                    par1.Range.Text = $"Дополнительные данные {Data.P2}";
                    par1.Range.InsertParagraphAfter();
                    par1.Range.InsertParagraphAfter();
                }
                if (Data.P3.Length != 0)
                {
                    par1.Range.Text = $"Закючение {Data.P3}";
                    par1.Range.InsertParagraphAfter();
                }
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

        public void Dispose()
        {
            _wordDocument?.Quit(ref _missing, ref _missing, ref _missing);
            _wordDocument = null;
        }

        public bool Print()
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

        public bool SavePreview()
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
            catch(Exception ex)
            {
                return false;
            }
        }

        public string Save()
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
