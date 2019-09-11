﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class GraduationInformation : DataEntity
    {
        public Guid Id { get; set; }
        public string ExceptLanguages { get; set; }
        public int? Mark { get; set; }
        public int? ReserveMaths { get; set; }
        public int? ReservePhysics { get; set; }
        public int? ReserveChemistry { get; set; }
        public int? ReserveLiterature { get; set; }
        public int? ReserveHistory { get; set; }
        public int? ReserveGeography { get; set; }
        public int? ReserveBiology { get; set; }
        public int? ReserveCivicEducation { get; set; }
        public int? ReserveLanguages { get; set; }
    }

    public class GraduationInformationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter ExceptLanguages { get; set; }
        public IntFilter Mark { get; set; }
        public GuidFilter ReserveId { get; set; }
        public List<Guid> ExceptIds { get; set; }
        public List<Guid> Ids { get; set; }
        public GraduationOrder OrderBy { get; set; }
        public GraduationInformationFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum GraduationOrder
    {
        CX
    }
}
