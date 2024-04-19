﻿using Application.Response;
using Domain.Entities;

namespace Application.Response.MenuResponses
{
    public class MenuResponse
    {
        public Guid id { get; set; }
        public DateTime fecha_consumo { get; set; }
        public DateTime fecha_carga { get; set; }
        public DateTime fecha_cierre { get; set; }

        public List<MenuOption> platillos { get; set; }
    }
}
