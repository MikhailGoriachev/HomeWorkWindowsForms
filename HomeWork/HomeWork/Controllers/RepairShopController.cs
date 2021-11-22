using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork.Models;      // модели
using System.IO;            // потоки данных

namespace HomeWork.Controllers
{
    // Класс Контроллер ремонтной мастерской
    public class RepairShopController
    {
        // ремонтная мастерская
        private RepairShop _repair;

        // название файла для сохранения
        private string _saveFile;

        #region Констуркторы

        // конструктор по умолчанию
        public RepairShopController() : this(new RepairShop { Name = "Новая мастерская", Address = "Адресс новой мастерской" }) { }

        // конструктор инициализирующий
        public RepairShopController(RepairShop repairShop)
        {
            // установка значений
            _repair = repairShop;
            _saveFile = ".\\save.json";
        }

        #endregion

        #region Свойства

        // доступ к полю _repairShop
        public RepairShop Repair
        {
            get => _repair; 
            set => _repair = value; 
        }

        // доступ к полю _saveFile
        public string SaveFile
        {
            get => _saveFile; 
            set => _saveFile = value; 
        }

        // получение коллекции телевизоров
        public List<Television> Televisions { get => _repair.Televisions; set => _repair.Televisions = value; }

        // размер коллекции 
        public int Count => _repair.Count;

        #endregion

        #region Методы

        // формирование данных коллекции
        public void Initialization(int n = 15) => _repair.Initialization(n);

        // добавление телевизора
        public void Add(Television television) => _repair.Add(television);

        // вставка телевизора
        public void Insert(int index, Television television) => _repair.Insert(index, television);

        // удаление телевизора
        public void Remove(Television television) => _repair.Remove(television);

        // удление телевизора
        public void RemoveAt(int index) => _repair.RemoveAt(index);

        // очистка коллекции
        public void Clear() => _repair.Clear();

        // упорядочивание по производителю и типу           
        public void OrderByModel() => _repair.OrderBy((t1, t2) => t1.Model.CompareTo(t2.Model));

        // упорядочивание по убыванию диагонали экрана
        public void OrderByDiagonal() => _repair.OrderBy((t1, t2) => t2.Diagonal.CompareTo(t1.Diagonal));

        // упорядочивание по мастеру, выполняющему ремонт
        public void OrderByMaster() => _repair.OrderBy((t1, t2) => t1.Master.CompareTo(t2.Master));

        // упорядочивание по владельцу телевизора
        public void OrderByOwner() => _repair.OrderBy((t1, t2) => t1.Owner.CompareTo(t2.Owner));

        // упорядочивание по возрастанию стоимости ремонта
        public void OrderByPriceAscend() => _repair.OrderBy((t1, t2) => t1.Price.CompareTo(t2.Price));

        // упорядочивание по убыванию стоимости ремонта
        public void OrderByPriceDescend() => _repair.OrderBy((t1, t2) => t2.Price.CompareTo(t1.Price));

        // выборка телевизоров с минимальной стоимостью ремонта     
        public List<Television> SelectByMinPrice()
        {
            // минимальная стоимость ремонта
            int min = _repair.MinPrice;

            // выборка элементов с минимальной стоимостью ремонта
            return _repair.SelectBy(item => item.Price == min);
        }

        // выборка телевизоров ремонтируемых выбранным мастером
        public List<Television> SelectByMaster(string master) =>
            _repair.SelectBy(item => item.Master == master);

        // выборка телевизоров с заданной диагональю экрана 
        public List<Television> SelectByDiagonal(double diagonal) =>
            _repair.SelectBy(item => item.Diagonal == diagonal);

        // выборка телевизоров заданного владельца
        public List<Television> SelectByOwner(string owner) =>
            _repair.SelectBy(item => item.Owner == owner);

        // JSON сериализация
        public void JsonSerialization(string fileName) {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) _repair.JsonSerialization(fs);
        }

        // JSON десериализация 
        public void JsonDeserialization(string fileName) {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read)) _repair = RepairShop.JsonDeserialization(fs);
        }

        // сохранение в стандартный файл для сохранений
        public void Save() {
            using (var fs = new FileStream(_saveFile, FileMode.Create, FileAccess.Write)) _repair.JsonSerialization(fs);
        }

        // загрузка из стандартного файла для сохранений
        public void Load() {

            // информация о файле
            var file = new FileInfo(_saveFile);

            // если файл не существует / пуст
            if (!file.Exists || file.Length == 0)
                throw new Exception("RepairShopController: Ошибка загрузки файла");
            
            // десериализация
            using (var fs = new FileStream(_saveFile, FileMode.OpenOrCreate, FileAccess.Read)) _repair = RepairShop.JsonDeserialization(fs);
        }

        #endregion
    }
}
