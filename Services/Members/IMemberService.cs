using System;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.Members
{
    public interface IMemberService
    {
        Task<MemberResponseViewModel> Add(MemberViewModel viewModel);
        Task Delete(int memberId);
    }
}