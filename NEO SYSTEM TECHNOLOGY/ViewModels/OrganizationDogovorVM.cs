﻿using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class OrganizationDogovorVM
    {
        public int DogovorId { get; set; }
        [Display(Name = "Договор")]
        public string OrderHeader { get; set; }

        [Display(Name = "Сумма Договора")]
        public decimal DogovorSum { get; set; }

        [Display(Name = "Дата Договора")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата Окончания Договора")]
        public DateTime EndDate { get; set; }
        public Currency Currency { get; set; }

        [Display(Name = "Валюта")]
        public bool IsVatIncluded { get; set; }


        public Receipt Receipt { get; set; }
        public Organization Organization { get; set; }
        public Dogovor Dogovor { get; set; }
        public string OrganizationName { get; set; }
        public int OrganizationId { get; set; }

        public bool IsOneTimeDogovor { get; set; }


    }
}
