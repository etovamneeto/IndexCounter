using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RiskCalculatorLib;
using Excel = Microsoft.Office.Interop.Excel;

namespace IndexCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            //Задание стартовых параметров формы
            this.Size = new Size(300, 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            //Задание стартовых параметров модели
            this.Text = "Index Calculator";
            minAgeBox.Text = "20";
            maxAgeBox.Text = "70";
            manRB.Checked = true;
            externalRB.Checked = true;
            larRB.Checked = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Задание путей для молуля калькулятора
            String libPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DataRus2012";
            RiskCalculatorLib.RiskCalculator.FillData(ref libPath);
        }

        private void getLarButton_Click(object sender, EventArgs e)
        {
            //Переключение выбора пола
            byte sex = 0;
            String sexName = "";
            String sheetName = "";//Задание имени страницы
            if (manRB.Checked)
            {
                sex = RiskCalculator.SEX_MALE;
                sexName = "Мужчины";
            }
            else if (womanRB.Checked)
            {
                sex = RiskCalculator.SEX_FEMALE;
                sexName = "Женщины";
            }

            //Задание весовых коэффициентов для тканей (в нашем случае учитывается только влияние на лёгкие)
            double wLung = 0.12;

            //Задание возрастных границ и заполнение дозовых историй
            int ages = Convert.ToInt32(maxAgeBox.Text) - Convert.ToInt32(minAgeBox.Text);
            List<RiskCalculator.DoseHistoryRecord[]> listOfDoseHistories = new List<RiskCalculator.DoseHistoryRecord[]>();
            for (int i = 0; i <= ages; i++)
            {
                listOfDoseHistories.Add(new RiskCalculator.DoseHistoryRecord[1]);
            }

            foreach (RiskCalculator.DoseHistoryRecord[] record in listOfDoseHistories)
            {
                for (int i = 0; i < record.Length; i++)
                    record[i] = new RiskCalculator.DoseHistoryRecord();
            }

            for (int i = 0; i <= ages; i++)
            {
                listOfDoseHistories[i][0].AgeAtExposure = Convert.ToInt16(Convert.ToInt32(minAgeBox.Text) + i);
                listOfDoseHistories[i][0].AllSolidDoseInmGy = 1000;
                listOfDoseHistories[i][0].LeukaemiaDoseInmGy = 1000;
                listOfDoseHistories[i][0].LungDoseInmGy = 0 / wLung;
            }

            //RiskCalculator.DoseHistoryRecord[] recordTest = listOfDoseHistories[0];
            //RiskCalculatorLib.RiskCalculator calculator = new RiskCalculatorLib.RiskCalculator(sex, listOfDoseHistories[0][0].AgeAtExposure, ref recordTest, true);
            //bool var = false;
            ////calculator.createEARSamples(1, ref var);
            //calculator.createEARSamples(0, ref var);// - правильный вариант
            //RiskCalculator.ValueBounds<RiskCalculator.LAR> value = calculator.getDetriment();
            //testTextBox.Text = value.Value.AllCancers.ToString();

            //Создание словаря, где ключ - возраст, а значение - LAR
            Dictionary<short, double> resultList = new Dictionary<short, double>();
            bool isIncidence = false;
            for (int i = 0; i <= ages; i++)
            {
                RiskCalculator.DoseHistoryRecord[] record = listOfDoseHistories[i];
                if (externalRB.Checked)
                {
                    RiskCalculatorLib.RiskCalculator calculator = new RiskCalculatorLib.RiskCalculator(sex, listOfDoseHistories[i][0].AgeAtExposure, ref record, true);
                    if (larRB.Checked)
                    {
                        resultList.Add(listOfDoseHistories[i][0].AgeAtExposure, calculator.getLAR(false, isIncidence).AllCancers);
                    }
                    if (detRB.Checked)
                    {
                        calculator.createEARSamples(0, ref isIncidence);
                        resultList.Add(listOfDoseHistories[i][0].AgeAtExposure, calculator.getDetriment().Value.AllCancers);
                    }
                    sheetName = sexName + " Внешнее";
                }
                if (internalRB.Checked)
                {
                    RiskCalculatorLib.RiskCalculator calculator = new RiskCalculatorLib.RiskCalculator(sex, listOfDoseHistories[i][0].AgeAtExposure, ref record, true);
                    if (larRB.Checked)
                    {
                        resultList.Add(listOfDoseHistories[i][0].AgeAtExposure, calculator.getLAR(false, isIncidence).Lung);
                    }
                    if (detRB.Checked)
                    {
                        calculator.createEARSamples(0, ref isIncidence);
                        resultList.Add(listOfDoseHistories[i][0].AgeAtExposure, calculator.getDetriment().Value.Lung);
                    }
                    sheetName = sexName + " Внутреннее";
                }
            }

            //if (larRB.Checked)
            //{
            //    testTextBox.Text = "LAR " + resultList.Count;
            //}
            //else if (detRB.Checked)
            //{
            //    testTextBox.Text = "Det " + resultList.Count;
            //}

            //Вывод в Excel-файл
            List<short> keyList = new List<short>(resultList.Keys);//Список ключей из словаря
            //Инициализация Excel-файла
            Excel.Application excelApp = new Excel.Application();
            //excelApp.Visible = true;
            excelApp.DisplayAlerts = true;
            excelApp.StandardFont = "Times-New-Roman";
            excelApp.StandardFontSize = 12;

            //Создание рабочей книги с 4 страницами, в которые будет выводиться информация
            excelApp.Workbooks.Add(Type.Missing);
            Excel.Workbook excelWorkbook = excelApp.Workbooks[1];
            excelApp.SheetsInNewWorkbook = 4;
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(1);
            excelWorksheet.Name = sheetName;

            //Описываем ячейку А1 на странице
            Excel.Range excelCells = excelWorksheet.get_Range("A1");
            excelCells.VerticalAlignment = Excel.Constants.xlCenter;
            excelCells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelCells.Borders.Weight = Excel.XlBorderWeight.xlThick;
            excelCells.Value2 = "AgeAtExp";

            //Описываем ячейку В1 на странице
            excelCells = excelWorksheet.get_Range("B1");
            excelCells.VerticalAlignment = Excel.Constants.xlCenter;
            excelCells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelCells.Borders.Weight = Excel.XlBorderWeight.xlThick;
            if (larRB.Checked)
                excelCells.Value2 = "LAR";
            else if (detRB.Checked)
                excelCells.Value2 = "Det";

            //Вывод в столбцы
            for (int i = 2; i <= resultList.Count + 1; i++)
            {
                excelCells = (Excel.Range)excelWorksheet.Cells[i, "A"];
                excelCells.Value2 = keyList[i - 2];
                excelCells.Borders.ColorIndex = 1;
                excelCells = (Excel.Range)excelWorksheet.Cells[i, "B"];
                excelCells.Value2 = resultList[keyList[i - 2]];
                excelCells.Borders.ColorIndex = 1;
            }

            char[] timeNameBuffer = DateTime.Now.ToString().ToCharArray();
            for (int i = 0; i < timeNameBuffer.Length; i++)
            {
                if (timeNameBuffer[i] == ':')
                    timeNameBuffer[i] = '-';
            }

            String saveAs = "";
            if (larRB.Checked)
            {
                if (externalRB.Checked)
                    saveAs = sexName + " внешнее (LAR)";
                if (internalRB.Checked)
                    saveAs = sexName + " внутреннее (LAR)";
            }
            if (detRB.Checked)
            {
                if (externalRB.Checked)
                    saveAs = sexName + " внешнее (Det)";
                if (internalRB.Checked)
                    saveAs = sexName + " внутреннее (Det)";
            }

            excelWorkbook.SaveAs(@Path.GetDirectoryName(Application.ExecutablePath) + "\\" + saveAs + "(" + new string(timeNameBuffer) + ").xlsx",  //object Filename
                    Excel.XlFileFormat.xlOpenXMLWorkbook,                       //object FileFormat
                    Type.Missing,                       //object Password 
                    Type.Missing,                       //object WriteResPassword  
                    Type.Missing,                       //object ReadOnlyRecommended
                    Type.Missing,                       //object CreateBackup
                    Excel.XlSaveAsAccessMode.xlNoChange,//XlSaveAsAccessMode AccessMode
                    Type.Missing,                       //object ConflictResolution
                    Type.Missing,                       //object AddToMru 
                    Type.Missing,                       //object TextCodepage
                    Type.Missing,                       //object TextVisualLayout
                    Type.Missing);                      //object Local
            excelApp.Quit();
        }

        private void minAgeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void maxAgeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void manRB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void womanRB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void externalRB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void sexGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void radGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void yearRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void larRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void detRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
