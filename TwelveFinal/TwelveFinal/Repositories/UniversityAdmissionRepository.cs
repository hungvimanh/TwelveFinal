﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IUniversityAdmissionRepository
    {
        Task<bool> Create(UniversityAdmission universityAdmission);
        Task<UniversityAdmission> Get(Guid Id);
        Task<bool> Update(UniversityAdmission universityAdmission);
        Task<bool> Delete(Guid Id);
    }
    public class UniversityAdmissionRepository : IUniversityAdmissionRepository
    {
        private readonly TFContext tFContext;
        public UniversityAdmissionRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(UniversityAdmission universityAdmission)
        {
            UniversityAdmissionDAO UniversityAdmissionDAO = new UniversityAdmissionDAO
            {
                Id = universityAdmission.Id,
                Area = universityAdmission.Area,
                Connected = universityAdmission.Connected,
                GraduateYear = universityAdmission.GraduateYear,
                PriorityType = universityAdmission.PriorityType
            };

            tFContext.UniversityAdmission.Add(UniversityAdmissionDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.UniversityAdmissionId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.University_Majors.Where(a => a.UniversityAdmissionId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.UniversityAdmission.Where(u => u.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<UniversityAdmission> Get(Guid Id)
        {
            UniversityAdmission universityAdmission = await tFContext.UniversityAdmission.Where(g => g.Id.Equals(Id)).Select(g => new UniversityAdmission
            {
                Id = g.Id,
                Area = g.Area,
                Connected = g.Connected,
                GraduateYear = g.GraduateYear,
                PriorityType = g.PriorityType,
                University_Majorses = g.University_Majors.Select(a => new University_Majors
                {
                    Id = a.Id,
                    MajorsId = a.MajorsId,
                    MajorsCode = a.Majors.Code,
                    MajorsName = a.Majors.Name,
                    UniversityId = a.UniversityId,
                    UniversityCode = a.University.Code,
                    UniversityName = a.University.Name,
                    SubjectGroupType = a.SubjectGroupType,
                    Benchmark = a.Benchmark,
                    UniversityAdmissionId = a.UniversityAdmissionId,
                }).ToList()
            }).FirstOrDefaultAsync();

            return universityAdmission;
        }

        public async Task<bool> Update(UniversityAdmission universityAdmission)
        {
            await tFContext.UniversityAdmission.Where(g => g.Id.Equals(universityAdmission.Id)).UpdateFromQueryAsync(g => new UniversityAdmissionDAO
            {
                Id = universityAdmission.Id,
                Area = universityAdmission.Area,
                Connected = universityAdmission.Connected,
                GraduateYear = universityAdmission.GraduateYear,
                PriorityType = universityAdmission.PriorityType,
                University_Majors = universityAdmission.University_Majorses.Select(a => new University_MajorsDAO
                {
                    Id = a.Id,
                    MajorsId = a.MajorsId,
                    UniversityId = a.UniversityId,
                    SubjectGroupType = a.SubjectGroupType,
                    Benchmark = a.Benchmark,
                    UniversityAdmissionId = a.UniversityAdmissionId
                }).ToList()
            });

            return true;
        }
    }
}