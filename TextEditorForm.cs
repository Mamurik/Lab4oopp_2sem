using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4oopp
{
    public class TextEditorForm : Form
    {
        private TextBox textBox;
        private Button openButton;
        private Button saveButton;
        private Button newButton;

        public TextEditorForm()
        {
            InitializeComponents();
            WireUpEvents();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();
            this.Size = new Size(800, 600);

            // Создание TableLayoutPanel
            var tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill; // Заполняет все доступное пространство формы
            tableLayout.ColumnCount = 2; // Два столбца
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F)); // Первый столбец занимает 100% ширины
            tableLayout.ColumnStyles.Add(new ColumnStyle()); // Второй столбец автоматически подстраивается под содержимое
            tableLayout.RowCount = 2; // Две строки
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 90F)); // Первая строка занимает 100% высоты
            tableLayout.RowStyles.Add(new RowStyle()); // Вторая строка автоматически подстраивается под содержимое
            this.Controls.Add(tableLayout);

            // Создание TextBox
            this.textBox = new TextBox();
            this.textBox.Multiline = true; // Позволяет вводить и отображать несколько строк текста
            this.textBox.Dock = DockStyle.Fill; // Заполняет все доступное пространство внутри TableLayoutPanel
            tableLayout.SetColumnSpan(textBox, 2); // TextBox располагается в обоих столбцах
            tableLayout.SetRow(textBox, 0); // TextBox располагается в первой строке
            tableLayout.Controls.Add(textBox);

            // Создание кнопки "Открыть"
            this.openButton = new Button();
            this.openButton.Text = "Открыть";
            this.openButton.BackColor = Color.Pink;
            tableLayout.SetColumn(openButton, 0); // Кнопка "Открыть" располагается в первом столбце
            tableLayout.SetRow(openButton, 1); // Кнопка "Открыть" располагается во второй строке
            tableLayout.Controls.Add(openButton);

            // Создание кнопки "Сохранить"
            this.saveButton = new Button();
            this.saveButton.Text = "Сохранить";
            this.saveButton.BackColor = Color.Pink;
            tableLayout.SetColumn(saveButton, 1); // Кнопка "Сохранить" располагается во втором столбце
            tableLayout.SetRow(saveButton, 1); // Кнопка "Сохранить" располагается во второй строке
            tableLayout.Controls.Add(saveButton);

            // Создание кнопки "Новый файл"
            this.newButton = new Button();
            this.newButton.BackColor = Color.Pink;
            this.newButton.Text = "Новый файл";
            tableLayout.SetColumn(newButton, 1); // Кнопка "Новый файл" располагается во втором столбце
            tableLayout.SetRow(newButton, 1); // Кнопка "Новый файл" располагается во второй строке
            tableLayout.Controls.Add(newButton);

            this.ResumeLayout(false);
        }
        private void WireUpEvents()
        {
            // Подключение обработчиков событий для кнопок

            // Обработчик события Click для кнопки "Открыть"
            this.openButton.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
                }
            };

            // Обработчик события Click для кнопки "Сохранить"
            this.saveButton.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                }
            };

            // Обработчик события Click для кнопки "Новый файл" ДЕЛЕГАТ
            this.newButton.Click += NewButton_Click;
        }

        // Обработчик события Click для кнопки "Новый файл"
        private void NewButton_Click(object sender, EventArgs e)
        {
            textBox.Text = string.Empty;
        }

        public class Program
        {
            [STAThread]
            public static void Main()
            {
                // Включение визуальных стилей для приложения
                Application.EnableVisualStyles();
                // Установка совместимого с рендерингом текста по умолчанию для приложения
                Application.SetCompatibleTextRenderingDefault(false);

                // Создание формы текстового редактора
                TextEditorForm form = new TextEditorForm();
                // Запуск приложения с формой
                Application.Run(form);
            }
        }
    }
}