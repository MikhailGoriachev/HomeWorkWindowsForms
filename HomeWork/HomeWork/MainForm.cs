using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HomeWork.Views;       // формы
using HomeWork.Models;      // модели
using HomeWork.Controllers; // контроллеры
using System.IO;

/* Задача 1. Разработайте приложение Windows Forms с использованием изученных
 * элементов интерфейса (строковое меню, контекстное меню, панель инструментов,
 * лист-бокс, комбо-бокс, строка состояния, строки ввода, чек-боксы и радиокнопки).
 * 
 * Разработайте класс Television (производитель и тип телевизора, диагональ экрана, 
 * строка с описанием дефекта, фамилия и инициалами мастера, фамилия и инициалы 
 * владельца, стоимость ремонта).
 * 
 * Разработайте класс RepairShop (коллекция Television, название ремонтной мастерской, 
 * адрес ремонтной мастерской).
 * 
 * Приложение должно иметь следующий функционал (при помощи пунктов меню, контекстного
 * меню, кнопок панели инструментов):
 *     •	Начальное формирование данных ремонтной мастерской (коллекция телевизоров 
 *          от 12 до 15 штук)
 *     •	Сохранение данных ремонтной мастерской в выбранном файле – сериализация в
 *          формате JSON. Файл для сохранения выбирать стандартным диалогом 
 *     •	Загрузка данных ремонтной мастерской из выбранного файла в – десериализация
 *          формате JSON. Файл для загрузки выбирать стандартным диалогом
 *     •	Изменение шрифта для отображения коллекции телевизоров в ListBox главного 
 *          окна. Выбор шрифта выполняйте при помощи стандартного диалога
 *     •	Изменение фона ListBox отображения коллекции телевизоров в главной форме 
 *          при помощи стандартного диалога 
 *     •	Добавление телевизора в коллекцию, сериализация данных
 *     •	Удаление телевизора из коллекции – выдача телевизора владельцу после ремонта, 
 *          сериализация данных
 *     •	Редактирование выбранного телевизора в отдельной форме, сериализация данных
 *     •	Редактирование данных ремонтной мастерской, сериализация данных
 *     •	Упорядочивание коллекции телевизоров
 *         o	По производителю и типу
 *         o	По убыванию диагонали экрана
 *         o	По мастеру, выполняющему ремонт
 *         o	По владельцу телевизора
 *         o	По стоимости ремонта
 *     •	Выборка и вывод в отдельной форме коллекции телевизоров с минимальной стоимостью
 *          ремонта
 *     •	Выборка и вывод в отдельной форме коллекции телевизоров, ремонтируемых выбранным 
 *          мастером
 *     •	Выборка и вывод в отдельной форме коллекции телевизоров, с заданной диагональю 
 *          экрана 
 *     •	Выборка и вывод в отдельной форме коллекции телевизоров, заданного владельца
 *     
 * В меню и контекстных меню (ListBox, MainForm) включите команды выхода, вывода окна со 
 * сведениями о программе и ее разработчике
 * 
*/

namespace HomeWork
{
    // Главная форма
    public partial class MainForm : Form
    {
        // констроллер ремонтной мастерской
        private RepairShopController _controller;

        // конструктор по умолчанию
        public MainForm()
        {
            InitializeComponent();

            // инициализация полей
            _controller = new RepairShopController();

            // установка лямбда выражений для упорядочивания коллекции в тег
            MsiOrderByDiagonal.Tag = (Action)_controller.OrderByDiagonal;
            MsiOrderByMaster.Tag = (Action)_controller.OrderByMaster;
            MsiOrderByModel.Tag = (Action)_controller.OrderByModel;
            MsiOrderByOwner.Tag = (Action)_controller.OrderByOwner;
            MsiOrderByPriceAscend.Tag = (Action)_controller.OrderByPriceAscend;
            MsiOrderByPriceDescend.Tag = (Action)_controller.OrderByPriceDescend;
            CmiOrderByDiagonal.Tag = (Action)_controller.OrderByDiagonal;
            CmiOrderByMaster.Tag = (Action)_controller.OrderByMaster;
            CmiOrderByModel.Tag = (Action)_controller.OrderByModel;
            CmiOrderByOwner.Tag = (Action)_controller.OrderByOwner;
            CmiOrderByPriceAscend.Tag = (Action)_controller.OrderByPriceAscend;
            CmiOrderByPriceDescend.Tag = (Action)_controller.OrderByPriceDescend;
            TsiOrderByDiagonal.Tag = (Action)_controller.OrderByDiagonal;
            TsiOrderByMaster.Tag = (Action)_controller.OrderByMaster;
            TsiOrderByModel.Tag = (Action)_controller.OrderByModel;
            TsiOrderByOwner.Tag = (Action)_controller.OrderByOwner;
            TsiOrderByPriceAscend.Tag = (Action)_controller.OrderByPriceAscend;
            TsiOrderByPriceDescend.Tag = (Action)_controller.OrderByPriceDescend;

            // установка значения состояния для запуска формы выборки коллекции в тег
            MsiSelectionByDiagonal.Tag = SelectionByForm.States.DiagonalMode;
            MsiSelectionByMaster.Tag = SelectionByForm.States.MasterMode;
            MsiSelectionByMinPrice.Tag = SelectionByForm.States.MinPriceMode;
            MsiSelectionByOwner.Tag = SelectionByForm.States.OwnerMode;
            CmiSelectionByDiagonal.Tag = SelectionByForm.States.DiagonalMode;
            CmiSelectionByMaster.Tag = SelectionByForm.States.MasterMode;
            CmiSelectionByMinPrice.Tag = SelectionByForm.States.MinPriceMode;
            CmiSelectionByOwner.Tag = SelectionByForm.States.OwnerMode;
            TsiSelectionDiagonal.Tag = SelectionByForm.States.DiagonalMode;
            TsiSelectionMaster.Tag = SelectionByForm.States.MasterMode;
            TsiSelectionMinPrice.Tag = SelectionByForm.States.MinPriceMode;
            TsiSelectionOwner.Tag = SelectionByForm.States.OwnerMode;
        }

        // загрузка формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            // форма для выбора/создания мастерской
            SelectRepairShopForm selectRepairShopForm = new SelectRepairShopForm(_controller);

            // получение результата формы
            DialogResult result = selectRepairShopForm.ShowDialog();

            // если был выбран пункт "Выход"
            if (result == DialogResult.Abort) 
                Close();        

            // установка контроллера
            _controller = selectRepairShopForm.Controller;

            // установка связи лист-бокса и коллекции данных
            UpdateCommunication();

            // загрузка данных о ремонтной мастерской
            TxbNameRepairShop.Text = _controller.Repair.Name;
            TxbAddress.Text = _controller.Repair.Address;

            // установка последнего действия в статус
            SlbHistory.Text = "Форма загружена";

            // установка пути к файлу сохранения
            SlbFileSave.Text = $" Файл сохранения: {new FileInfo(_controller.SaveFile).Name}";
        }

        // выход из приложения
        private void Exit_Command(object sender, EventArgs e) => Application.Exit();

        // информация о приложении
        private void ProgramInfo_Comand(object sender, EventArgs e) => new FormInfo().ShowDialog();

        // формирование коллекции
        private void InitializationCoolection_Command(object sender, EventArgs e)
        {
            // очистка коллекции телевизоров
            _controller.Clear();

            // инициализация коллекции
            _controller.Initialization();

            // обновление связи
            UpdateCommunication();

            // сохранение в файл
            _controller.Save();

            // установка последнего действия в статус
            SlbHistory.Text = "Формирование коллекции";
        }

        // обновление связи лист-бокса и коллекции данных
        private void UpdateCommunication()
        {
            // установка источника данных
            LbxTelevisions.DataSource = null;
            LbxTelevisions.DataSource = _controller.Televisions;

            // установка метода для вывода 
            LbxTelevisions.DisplayMember = "ShowElem";

            // обновление количества элементов в статуса
            SlbInfoCountElement.Text = $"Количество элементов: {_controller.Count}";
        }

        // очистка коллекции
        private void Clear_Command(object sender, EventArgs e)
        {
            // очистка 
            _controller.Clear();

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Очистка коллекции телевизоров";

            // сохранение в файл
            _controller.Save();
        }

        // добавление коллекции 
        private void AddRange_Command(object sender, EventArgs e)
        {
            // очистка 
            _controller.Initialization(100);

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Добавление коллекции телевизоров";

            // сохранение в файл
            _controller.Save();
        }

        // удаление элемента
        private void Remove_Command(object sender, EventArgs e)
        {
            // удаление элемента (т.к. удаление не по индексу, защита от некорретного индекса не требуется)
            _controller.Remove((Television)LbxTelevisions.SelectedItem);

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Удаление выбранного телевизора";

            // сохранение в файл
            _controller.Save();
        }

        // добавление телевизора
        private void AddTelevision_Command(object sender, EventArgs e)
        {
            // форма создания
            TelevisionForm televisionForm = new TelevisionForm();

            // если создание отменено
            if (televisionForm.ShowDialog() != DialogResult.OK) 
                return;

            // добавление созданного телевизора в коллекцию
            _controller.Insert(0, televisionForm.television);

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Добавление телевизора";

            // сохранение в файл
            _controller.Save();
        }

        // редактирование выбранного элемента
        private void Edit_Command(object sender, EventArgs e)
        {
            // если не выбран элемент
            if (LbxTelevisions.SelectedItem == null) 
                return;

            // форма редактирования 
            new TelevisionForm((Television)LbxTelevisions.SelectedItem).ShowDialog();

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Редактирование выбранного телевизора";

            // сохранение в файл
            _controller.Save();
        }

        // редактирование данных о мастерской
        private void EditRepairShop_Command(object sender, EventArgs e)
        {
            // форма для редактирования данных о мастерсой
            var repairShopForm = new RepairShopForm(_controller.Repair, _controller.SaveFile);

            // запуск формы
            if (repairShopForm.ShowDialog() != DialogResult.OK)
                return;

            // загрузка данных о ремонтной мастерской
            TxbNameRepairShop.Text = _controller.Repair.Name;
            TxbAddress.Text = _controller.Repair.Address;

            // если файл сохранения изменился
            if (_controller.SaveFile != repairShopForm.SaveFile)
                _controller.JsonSerialization(repairShopForm.SaveFile);

            // установка файла для сохранения
            _controller.SaveFile = repairShopForm.SaveFile;

            // установка последнего действия в статус
            SlbHistory.Text = "Редактирование данных о мастерской";

            // сохранение в файл
            _controller.Save();
        }

        // упорядочивание по компаратору
        private void OrderBy_Command(object sender, EventArgs e)
        {
            // упорядочивание
            ((Action)((ToolStripMenuItem)sender).Tag).Invoke();

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Упорядочивание";
        }

        // выборка
        private void Selection_Command(object sender, EventArgs e)
        {
            // если нет данных для выбора
            if (_controller.Count == 0) {
                MessageBox.Show("Нет данных для выбора!", "Действие невозможно!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // упорядочивание
            new SelectionByForm(_controller, (SelectionByForm.States)((ToolStripMenuItem)sender).Tag).ShowDialog();

            // установка последнего действия в статус
            SlbHistory.Text = "Выборка";
        }

        // загрузка из файла
        private void LoadFile_Command(object sender, EventArgs e)
        {
            // получение результата формы
            if (OfdLoad.ShowDialog() != DialogResult.OK) return;

            // выбор файла для сохранения
            _controller.SaveFile = OfdLoad.FileName;

            // десериализация из файла
            _controller.Load();

            // обновление связи
            UpdateCommunication();

            // загрузка данных о ремонтной мастерской
            TxbNameRepairShop.Text = _controller.Repair.Name;
            TxbAddress.Text = _controller.Repair.Address;

            // установка последнего действия в статус
            SlbHistory.Text = "Загрузка из файла";

            // установка пути к файлу сохранения
            SlbFileSave.Text = $" Файл сохранения: {new FileInfo(_controller.SaveFile).Name}";
        }

        // сохранение в файл
        private void SaveFile_Command(object sender, EventArgs e)
        {
            // получение результата формы
            if (SfdSave.ShowDialog() != DialogResult.OK) return;

            // выбор файла для сохранения
            _controller.SaveFile = SfdSave.FileName;

            // сериализация в файл
            _controller.Save();

            // обновление связи
            UpdateCommunication();

            // установка последнего действия в статус
            SlbHistory.Text = "Сохранение в файл";

            // установка пути к файлу сохранения
            SlbFileSave.Text = $" Файл сохранения: {new FileInfo(_controller.SaveFile).Name}";
        }

        // создание мастерской
        private void CreateRepairShop_Command(object sender, EventArgs e)
        {
            // форма для создания данных о мастерсой
            var repairForm = new RepairShopForm();

            // форма для создания данных о мастерсой
            if (repairForm.ShowDialog() != DialogResult.OK)
                return;

            // установка ссылки на новую мастерскую
            _controller.Repair = repairForm.Repair;

            // установка файла для сохранения
            _controller.SaveFile = repairForm.SaveFile;

            // загрузка данных о ремонтной мастерской
            TxbNameRepairShop.Text = _controller.Repair.Name;
            TxbAddress.Text = _controller.Repair.Address;

            // установка последнего действия в статус
            SlbHistory.Text = "Создание мастерской";

            // обновление связи
            UpdateCommunication();

            // сохранение в файл
            _controller.Save();
        }

        // изменение шрифта лист-бокса
        private void ChangeFont_Command(object sender, EventArgs e)
        {
            // запуск формы выбора шрифта
            if (FdgChangeFontList.ShowDialog() != DialogResult.OK)
                return;

            // применение шрифта
            LbxTelevisions.Font = FdgChangeFontList.Font;
        }

        // изменение фона лист-бокса
        private void MsiBackColor_Click(object sender, EventArgs e)
        {
            // запуск формы выбора цвета
            if (CdgChangeColorList.ShowDialog() != DialogResult.OK)
                return;

            // применение цвета
            LbxTelevisions.BackColor = CdgChangeColorList.Color;
        }
    }
}
