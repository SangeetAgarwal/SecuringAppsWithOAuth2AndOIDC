using AutoMapper;
using Notes.API.Datalayer.DbSet;

namespace Notes.API.Servicelayer.Automapper.Profiles
{
    public class DatalayerProfile : Profile
    {
        public DatalayerProfile()
        {
            CreateMap<Note, Common.ApiModels.NoteApiModel>();
            CreateMap<Common.ApiModels.NoteApiModel, Note>()
                .ForMember(dest=>dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.DeleteFlag, opt => opt.Ignore());
        }
    }
}
