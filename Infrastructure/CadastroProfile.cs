﻿using AutoMapper;
using DataTransferObject.Cadastro;
using Domain.Entities;

namespace Infrastructure
{
    public class CadastroProfile:Profile
    {
        public CadastroProfile()
        {
            CreateMap<Carros, CarrosDTO>();
            CreateMap<CarrosDTO, Carros>();
        }
    }
}
