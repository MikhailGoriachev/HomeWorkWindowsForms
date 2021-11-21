using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;       // JSON сериализация

// класс Television:
// производитель и тип телевизора
// диагональ экрана
// строка с описанием дефекта
// фамилия и инициалами мастера
// фамилия и инициалы владельца
// стоимость ремонта

namespace HomeWork.Models
{
    [DataContract]
    // Класс Телевизор
    public class Television
    {
        [DataMember]
        // производитель и тип телевизора             
        private string _model;

        [DataMember]
        // диагональ экрана
        private int _diagonal;

        [DataMember]
        // строка с описанием дефекта
        private string _defect;

        [DataMember]
        // фамилия и инициалами мастера
        private string _master;

        [DataMember]
        // фамилия и инициалы владельца
        private string _owner;

        [DataMember]
        // стоимость ремонта
        private int _price;

        #region Свойства

        // доступ к полю _model
        public string Model { 
            get => _model;
            set => _model = !String.IsNullOrWhiteSpace(value)
                ? value
                : throw new Exception("Television: Поле Model не может быть пустым!");
        }

        // доступ к полю _diagonal
        public int Diagonal
        {
            get => _diagonal;
            set => _diagonal = value > 0
                ? value
                : throw new Exception("Television: Поле Diagonal должно быть больше нуля!");
        }

        // доступ к полю _defect
        public string Defect
        {
            get => _defect;
            set => _defect = !String.IsNullOrWhiteSpace(value)
                ? value
                : throw new Exception("Television: Поле Defect не может быть пустым!");
        }

        // доступ к полю _master
        public string Master
        {
            get => _master;
            set => _master = !String.IsNullOrWhiteSpace(value)
                ? value
                : throw new Exception("Television: Поле Master не может быть пустым!");
        }

        // доступ к полю _owner
        public string Owner
        {
            get => _owner;
            set => _owner = !String.IsNullOrWhiteSpace(value)
                ? value
                : throw new Exception("Television: Поле Owner не может быть пустым!");
        }

        // доступ к полю _price
        public int Price
        {
            get => _price;
            set => _price = value > 0
                ? value
                : throw new Exception("Television: Поле Price должно быть больше нуля!");
        }

        [IgnoreDataMember]
        // вывод в таблицу
        public string ShowElem =>
            $" {_model,-20} │ {_diagonal,5} │ {_defect,-40} │" +
            $" {_master,-20} │ {_owner,-20} │ {_price,7}";

        #endregion
    }
}
