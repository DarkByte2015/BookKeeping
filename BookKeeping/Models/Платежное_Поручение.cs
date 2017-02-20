using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BookKeeping.Models
{
    // Сам не люблю делать названия кириллицей, но уж больно много переводить бы пришлось (транслит не использую в принципе), 
    // да и так проще вытащить названия парсером (еще был вариант с атрибутами).
    public class Платежное_Поручение
    {
        public int Id { get; set; }
        public int Номер { get; set; }
        public DateTime Дата { get; set; } = DateTime.Now;
        public decimal Сумма { get; set; }
        public DateTime ДатаПоступило { get; set; } = DateTime.Now;
        public DateTime ДатаСписано { get; set; } = DateTime.Now;
        public string ПлательщикСчет { get; set; }
        public string Плательщик { get; set; }
        public string Плательщик1 { get; set; }
        public string Плательщик2 { get; set; }
        public string Плательщик3 { get; set; }
        public string Плательщик4 { get; set; }
        public string ПлательщикРасчСчет { get; set; }
        public string ПлательщикБанк1 { get; set; }
        public string ПлательщикБанк2 { get; set; }
        public long ПлательщикБИК { get; set; }
        public long ПлательщикИНН { get; set; }
        public long ПлательщикКПП { get; set; }
        public string ПолучательСчет { get; set; }
        public long ПолучательБИК { get; set; }
        public long ПолучательИНН { get; set; }
        public long ПолучательКПП { get; set; }
        public string Получатель { get; set; }
        public string Получатель1 { get; set; }
        public string Получатель2 { get; set; }
        public string Получатель3 { get; set; }
        public string Получатель4 { get; set; }
        public string ПолучательРасчСчет { get; set; }
        public string ПолучательБанк1 { get; set; }
        public string ПолучательБанк2 { get; set; }
        public string ПолучательКорсчет { get; set; }
        public string НазначениеПлатежа { get; set; }
        public string ВидПлатежа { get; set; }
        public int ВидОплаты { get; set; }
        public int Очередность { get; set; }
    }
}