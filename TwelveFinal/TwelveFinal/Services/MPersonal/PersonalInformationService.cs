﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MPersonal
{
    public interface IPersonalInformationService : IServiceScoped
    {
        Task<PersonalInformation> Check(PersonalInformation personalInformation);
        Task<PersonalInformation> Get(Guid Id);
        Task<PersonalInformation> Update(PersonalInformation personalInformation);
    }
    public class PersonalInformationService : IPersonalInformationService
    {
        private readonly IUOW UOW;
        private readonly IPersonalInformationValidator PersonalInformationValidator;

        public PersonalInformationService(
            IUOW UOW,
            IPersonalInformationValidator PersonalInformationValidator
            )
        {
            this.UOW = UOW;
            this.PersonalInformationValidator = PersonalInformationValidator;
        }

        public async Task<PersonalInformation> Check(PersonalInformation personalInformation)
        {
            if (!await PersonalInformationValidator.Check(personalInformation))
                return personalInformation;
            return personalInformation;
        }

        public async Task<PersonalInformation> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            PersonalInformation personalInformation = await UOW.PersonalInformationRepository.Get(Id);
            return personalInformation;
        }

        public async Task<PersonalInformation> Update(PersonalInformation personalInformation)
        {
            if (!await PersonalInformationValidator.Check(personalInformation))
                return personalInformation;

            try
            {
                await UOW.Begin();
                await UOW.PersonalInformationRepository.Update(personalInformation);
                await UOW.Commit();
                return await Get(personalInformation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
