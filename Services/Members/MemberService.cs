using System;
using SyncStyle.ViewModel;
using SyncStyle.Model;
using SyncStyle.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SyncStyle.EnumType;

namespace SyncStyle.Services.Members
{
    public class MemberService : IMemberService
    {
        private readonly StyleSyncContext _styleSyncContext;
        public MemberService(StyleSyncContext styleSyncContext)
        {
            _styleSyncContext = styleSyncContext;
        }
        public async Task<MemberResponseViewModel> Add(MemberViewModel viewModel)
        {
            var query = await _styleSyncContext.Members.Where(i => i.UserName == viewModel.UserName).FirstOrDefaultAsync();

            if(query != null)
            {
                throw new Exception("Aynı isimde bir User Name vardır.Başka bir User Name ile deneyiniz.");
            }
            
            var member = new Member()
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Password = viewModel.Password,
                DateOfBirth = viewModel.DateOfBirth,
                Gender = (GenderStatus)viewModel.Gender,
                IsActive = true,
            };

			await _styleSyncContext.Members.AddAsync(member);
            await _styleSyncContext.SaveChangesAsync();

            var response = new MemberResponseViewModel()
            {
                MemberId = member.MemberId,
                Name = member.Name,
                LastName = member.LastName,
                UserName = member.UserName,
                DateOfBirth = member.DateOfBirth,
                Gender = member.Gender,
                IsActive = member.IsActive
            };

            return response;
        }

        public async Task Delete(int memberId)
        {
            var result = _styleSyncContext.Members.Where(i => i.MemberId == memberId && i.IsActive).FirstOrDefault();

            if (result == null)
            {
                throw new Exception ("Silinecek User bilgisi bulunamadı.");
            }

			result.IsActive = false;

			_styleSyncContext.Members.Attach(result);
            await _styleSyncContext.SaveChangesAsync();
        }
    }
}