using System;
using System.Data;
using System.IO;
using OfficeOpenXml;


namespace XMLLibrary
{
    public static class SpreadSheet
    {

        public enum Header
        {
            First, Last, Level, Active  ,Renewal, Month, Address, City, State, Zip, EMail, Phone, Url
        }


        public static DataTable ImportToDataTable(string filePath, string sheetName)
        {
            DataTable dt = new DataTable();
            FileInfo fi = new FileInfo(filePath);

            // Check if the file exists
            if (!fi.Exists)
                throw new Exception("File " + filePath + " Does Not Exists");

            DataSet ds = new DataSet();
            using (ExcelPackage xlPackage = new ExcelPackage(fi))
            {

                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[sheetName];


                // Fetch the WorkSheet size
                ExcelCellAddress startCell = worksheet.Dimension.Start;
                ExcelCellAddress endCell = worksheet.Dimension.End;

                // create all the needed DataColumn
                for (int col = startCell.Column; col <= endCell.Column; col++)
                    dt.Columns.Add(col.ToString());

                // place all the data into DataTable
                for (int row = startCell.Row; row <= endCell.Row; row++)
                {
                    DataRow dr = dt.NewRow();
                    int x = 0;
                    for (int col = startCell.Column; col <= endCell.Column; col++)
                    {
                        dr[x++] = worksheet.Cells[row, col].Value;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }



    }
}
