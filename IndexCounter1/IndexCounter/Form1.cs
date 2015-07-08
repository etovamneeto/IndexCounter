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
                listOfDoseHistories[i][0].LungDoseInmGy = 1000/wLung;
            }

            //Создание словаря, где ключ - возраст, а значение - LAR
            Dictionary<short, double> ageLar = new Dictionary<short, double>();
            for (int i = 0; i <= ages; i++)
            {
                RiskCalculator.DoseHistoryRecord[] record = listOfDoseHistories[i];
                if (externalRB.Checked)
                {
                    RiskCalculatorLib.RiskCalculator calculator = new RiskCalculatorLib.RiskCalculator(sex, listOfDoseHistories[i][0].AgeAtExposure, ref record, true);
                    ageLar.Add(listOfDoseHistories[i][0].AgeAtExposure, calculator.getLAR(false, true).AllCancers);
                    sheetName = sexName + " Внешнее";
                }
                else if (internalRB.Checked)
                {
                    RiskCalculatorLib.RiskCalculator calculator = new RiskCalculatorLib.RiskCalculator(sex, listOfDoseHistories[i][0].AgeAtExposure, ref record, true);
                    ageLar.Add(listOfDoseHistories[i][0].AgeAtExposure, calculator.getLAR(false, true).Lung);
                    sheetName = sexName + " Внутреннее";
                }
            }
            List<short> keyList = new List<short>(ageLar.Keys);//Список ключей из словаря

            //Вывод в Excel-файл
            //Инициализация Excel-файла
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
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
            excelCells.Value2 = "LAR";

            //Вывод в столбцы
            for (int i = 2; i <= ageLar.Count + 1; i++)
            {
                excelCells = (Excel.Range)excelWorksheet.Cells[i, "A"];
                excelCells.Value2 = keyList[i - 2];
                excelCells.Borders.ColorIndex = 1;
                excelCells = (Excel.Range)excelWorksheet.Cells[i, "B"];
                excelCells.Value2 = ageLar[keyList[i - 2]];
                excelCells.Borders.ColorIndex = 1;
            }
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
    }
}
