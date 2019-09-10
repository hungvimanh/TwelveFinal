﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IGraduationInformationRepository
    {
        Task<bool> Create(GraduationInformation graduation);
        Task<GraduationInformation> Get(Guid Id);
        Task<bool> Update(GraduationInformation graduation);
        Task<bool> Delete(Guid Id);
    }
    public class GraduationInformationRepository : IGraduationInformationRepository
    {
        private readonly TFContext tFContext;
        public GraduationInformationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(GraduationInformation graduation)
        {
            GraduationInformationDAO GraduationDAO = new GraduationInformationDAO
            {
                Id = graduation.Id,
                Mark = graduation.Mark,
                ExceptLanguages = graduation.ExceptLanguages,
            };

            tFContext.GraduationInformation.Add(GraduationDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.GraduationInformationId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.GraduationInformation.Where(g => g.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<GraduationInformation> Get(Guid Id)
        {
            GraduationInformation graduation = await tFContext.GraduationInformation.Where(g => g.Id.Equals(Id)).Select(g => new GraduationInformation
            {
                Id = g.Id,
                Mark = g.Mark,
                ExceptLanguages = g.ExceptLanguages,
            }).FirstOrDefaultAsync();

            return graduation;
        }

        public async Task<bool> Update(GraduationInformation graduation)
        {
            await tFContext.GraduationInformation.Where(g => g.Id.Equals(graduation.Id)).UpdateFromQueryAsync(g => new GraduationInformationDAO
            {
                Id = graduation.Id,
                Mark = graduation.Mark,
                ExceptLanguages = graduation.ExceptLanguages,
            });

            return true;
        }
    }
}